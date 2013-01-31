using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace DataStructures.HeapSpace
{
    /// <summary>
    /// Heap
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Heap<T> where T : IComparable<T>, IEquatable<T>
    {
        private Node<T> root;

        public T Peek
        {
            get { return root.Data; }
        }
        public int Count { get; private set; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Count >= 0);
        }

        public void Add(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null, "element");
            Contract.Ensures(Count == Contract.OldValue<int>(Count) + 1);

            Queue<Node<T>> queue = new Queue<Node<T>>();
            //Restore heap property
            root = Heapify(root);
        }

        private Node<T> Heapify(Node<T> root)
        {
            Contract.Requires<ArgumentNullException>(root != null, "root");
            Contract.Ensures(Contract.Result<Node<T>>() != null);

            root.Right = Heapify(root.Right);
            root.Left = Heapify(root.Left);
            int compareRight = root.Data.CompareTo(root.Right.Data);
            if (compareRight > 0)
            {
                //root is bigger than right
                SwapData(root, root.Right);
            }
            int compareLeft = root.Data.CompareTo(root.Left.Data);
            if (compareLeft > 0)
            {
                //root is bigger than left
                SwapData(root, root.Left);
            }
            return root;
        }

        private void SwapData(Node<T> node1, Node<T> node2)
        {
            Contract.Requires<ArgumentNullException>(node1 != null, "node1");
            Contract.Requires<ArgumentNullException>(node2 != null, "node2");

            T temp = node1.Data;
            node1.Data = node2.Data;
            node2.Data = temp;
        }

        public void Remove(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null, "element");
            //Restore heap property
            root = Heapify(root);
        }


        public T GetMin()
        {
            throw new NotImplementedException();
        }

    }
}
