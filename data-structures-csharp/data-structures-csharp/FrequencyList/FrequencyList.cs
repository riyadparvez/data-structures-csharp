using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataStructures.FrequencyListSpace
{
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
            while(current.Next != null)
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
            while(current.Previous.AccessCount <= node.AccessCount)
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


        public T Get(T element) 
        {
            Debug.Assert(element != null);

            var current = Header;
            while(current != null)
            {
                if(current.Data.Equals(element))
                {
                    current.AccessCount++;
                    Adjust(current);
                    return current.Data;
                }
                current = current.Next;
            }

            return default(T);
        }

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

        public void Remove(T element) 
        {
            Debug.Assert(element != null);

            var node = GetNode(element);
            if(node == null)
            {
                return;
            }
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
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
