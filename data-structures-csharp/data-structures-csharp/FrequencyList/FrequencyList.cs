using System;
using System.Diagnostics.Contracts;


namespace DataStructures.FrequencyListSpace
{
    /// <summary>
    /// Linked List which places frequently accessed items near the head
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public partial class FrequencyList<T>
        where T : IComparable<T>
    {
        private int count;
        private readonly Node<T> dummy = new Node<T>();

        private readonly Node<T> header;
        public int Count { get { return count; } }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(count >= 0);
        }

        public FrequencyList()
        {
            count = 0;
            dummy.AccessCount = Int32.MaxValue;
            header = dummy;
        }

        private Node<T> GetLastNode()
        {
            Contract.Ensures(Contract.Result<Node<T>>() != null);

            Node<T> current = header;
            while (current.Next != null)
            {
                current = current.Next;
            }
            return current;
        }


        private void Adjust(Node<T> node)
        {
            Contract.Requires<ArgumentNullException>(node != null);

            var current = node;
            while (current.Previous.AccessCount <= node.AccessCount)
            {
                current = current.Previous;
            }

            //Unlink from previous location
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;

            //Insert into new location
            node.Previous = current.Previous;
            node.Next = current;
            current.Previous = node;
            node.Previous.Next = node;
        }

        /// <summary>
        /// Return if that element exists, otherwise returns null
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public T Get(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            var current = header;
            while (current != null)
            {
                if (current.Data.Equals(element))
                {
                    current.AccessCount++;
                    Adjust(current);
                    return current.Data;
                }
                current = current.Next;
            }

            return default(T);
        }

        /// <summary>
        /// Get the node containing element, otherwise returns null
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public Node<T> GetNode(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            var current = header;
            while (current != null)
            {
                if (current.Data.Equals(element))
                {
                    return current;
                }
                current = current.Next;
            }

            return null;
        }

        /// <summary>
        /// Remove that element from the list
        /// </summary>
        /// <param name="element"></param>
        public void Remove(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            Contract.Ensures(count >= 0);

            var node = GetNode(element);
            if (node == null)
            {
                //Element doesn't exist
                return;
            }
            //unlink that node from the list
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            count--;
        }


        public void Add(T data)
        {
            Contract.Requires<ArgumentNullException>(data != null);
            Contract.Ensures(count == Contract.OldValue<int>(count) + 1);

            Node<T> node = new Node<T>(data);
            var lastNode = GetLastNode();
            lastNode.Next = node;
            node.Previous = lastNode;
            count++;
        }

    }
}
