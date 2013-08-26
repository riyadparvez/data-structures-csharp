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

        private readonly Node<T> head;
        private Node<T> tail;

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
            head = dummy;
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
        public bool Exists(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            var current = head;
            while (current != null)
            {
                if (current.Value.Equals(element))
                {
                    current.AccessCount++;
                    Adjust(current);
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public T Get(int index) 
        {
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(index < count);
            
            return GetNode(index).Value;
        }

        private Node<T> GetNode(int index) 
        {
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(index < count);

            var current = head;
            int i = 0;
            while (i < index)
            {
                current = current.Next;
                i++;
            }
            current.AccessCount++;
            Adjust(current);
            return current;
        }

        /// <summary>
        /// Get the node containing element, otherwise returns null
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public Node<T> GetNode(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            var current = head;
            while (current != null)
            {
                if (current.Value.Equals(element))
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
            var lastNode = tail;
            lastNode.Next = node;
            node.Previous = lastNode;
            tail = node;
            count++;
        }

        public T this[int index]
        {
            get 
            {
                return Get(index);    
            }
            set 
            { 

            }
        }
    }
}
