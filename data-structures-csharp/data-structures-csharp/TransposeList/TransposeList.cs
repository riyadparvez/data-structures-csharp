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
    public partial class TransposeList<T> : IEnumerable<T>, ICollection<T>
    {
        private int count;
        private Node<T> dummy = new Node<T>();

        private Node<T> head;
        public int Count 
        { 
            get { return count; } 
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(dummy != null);
            Contract.Invariant(count >= 0);
        }

        public TransposeList()
        {
            count = 0;
            head = dummy;
        }

        [Pure]
        private Node<T> GetLastNode()
        {
            Contract.Ensures(Contract.Result<Node<T>>() != null);

            Node<T> current = head;
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

        [Pure]
        public T Get(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            var current = head;
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

        [Pure]
        private Node<T> GetNode(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            var current = head;
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
            Contract.Ensures(count == Contract.OldValue<int>(count) + 1);

            Node<T> node = new Node<T>(data);
            var lastNode = GetLastNode();
            lastNode.Next = node;
            node.Previous = lastNode;
            count++;
        }

        [Pure]
        public IEnumerator<T> GetEnumerator()
        {
            var current = head.Next;
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


        public void Clear()
        {
            head.Next = null;
            count = 0;
        }

        public bool Contains(T item)
        {
            var current = head.Next;

            while (current != null)
            {
                if(current.Data.Equals(item))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Contract.Requires<ArgumentNullException>(array != null);

            throw new NotImplementedException();
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }
    }
}
