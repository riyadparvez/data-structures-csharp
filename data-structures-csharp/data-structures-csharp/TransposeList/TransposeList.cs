using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace DataStructures.TransposeListSpace
{
    /// <summary>
    /// Linked list which makes most recently accessed element head of the list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class TransposeList<T> : IEnumerable<T>
    {
        private int count;
        private Node<T> dummy = new Node<T>();

        public Node<T> Head { get; private set; }
        public int Count { get { return count; } }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(dummy != null);
            Contract.Invariant(count >= 0);
        }

        public TransposeList()
        {
            count = 0;
            Head = dummy;
        }

        private Node<T> GetLastNode()
        {
            Contract.Ensures(Contract.Result<Node<T>>() != null);

            Node<T> current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            return current;
        }


        private void Adjust(Node<T> node)
        {
            Contract.Requires<ArgumentNullException>(node != null);
            if (node.Previous == dummy)
            {
                //already at the front of the list
                return;
            }
            var temp = node.Previous;
            node.Previous = node.Previous.Previous;
            temp.Next = node.Next;
            node.Next = temp;
            temp.Previous = node;
        }


        public T Get(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            var current = Head;
            while (current != null)
            {
                if (current.Data.Equals(element))
                {
                    Adjust(current);
                    return current.Data;
                }
                current = current.Next;
            }

            return default(T);
        }

        public Node<T> GetNode(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            var current = Head;
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
            Contract.Requires<ArgumentNullException>(element != null);

            var node = GetNode(element);
            if (node == null)
            {
                return;
            }
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            count--;
        }


        public void Add(T data)
        {
            Contract.Requires<ArgumentNullException>(data != null);

            Node<T> node = new Node<T>(data);
            var lastNode = GetLastNode();
            lastNode.Next = node;
            node.Previous = lastNode;
            count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
