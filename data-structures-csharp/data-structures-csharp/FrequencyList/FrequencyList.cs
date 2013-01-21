using System;
using System.Diagnostics;


namespace DataStructures.FrequencyListSpace
{
    /// <summary>
    /// Linked List which places frequently accessed items near to the root
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class FrequencyList<T>
        where T : IComparable<T>
    {
        private int count;
        private Node<T> dummy = new Node<T>();

        public Node<T> Header { get; private set; }
        public int Count { get { return count; } }

        public FrequencyList()
        {
            count = 0;
            dummy.AccessCount = Int32.MaxValue;
            Header = dummy;
        }

        private Node<T> GetLastNode()
        {
            Node<T> current = Header;
            while (current.Next != null)
            {
                current = current.Next;
            }
            Debug.Assert(current != null);
            return current;
        }


        private void Adjust(Node<T> node)
        {
            Debug.Assert(node != null);

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
            Debug.Assert(element != null);

            var current = Header;
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
            Debug.Assert(element != null);

            var current = Header;
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
            Debug.Assert(element != null);

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
            Debug.Assert(count >= 0);
        }


        public void Add(T data)
        {
            Debug.Assert(data != null);

            Node<T> node = new Node<T>(data);
            var lastNode = GetLastNode();
            lastNode.Next = node;
            node.Previous = lastNode;
            count++;
        }

    }
}
