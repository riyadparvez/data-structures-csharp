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
            get { return root.Value; }
        }
        public int Count { get; private set; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Count >= 0);
        }

        /// <summary>
        /// Adds an element to the heap
        /// </summary>
        /// <param name="element"></param>
        public void Add(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null, "element");
            Contract.Ensures(Count == Contract.OldValue<int>(Count) + 1);

            Queue<Node<T>> queue = new Queue<Node<T>>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                Contract.Assert(node != null);
                if (node.Left == null)
                {
                    node.Left = new Node<T>(element, node);
                    break;
                }
                else
                {
                    Contract.Assert(node.Key.CompareTo(node.Left.Key) < 0);
                    queue.Enqueue(node.Left);
                }
                if (node.Right == null)
                {
                    node.Left = new Node<T>(element, node);
                    break;
                }
                else
                {
                    Contract.Assert(node.Key.CompareTo(node.Right.Key) < 0);
                    queue.Enqueue(node.Right);
                }
            }
            //Restore heap property
            root = Heapify(root);
        }

        private Node<T> Heapify(Node<T> root)
        {
            Contract.Requires<ArgumentNullException>(root != null, "root");
            Contract.Ensures(Contract.Result<Node<T>>() != null);

            root.Right = Heapify(root.Right);
            root.Left = Heapify(root.Left);
            int compareRight = root.Key.CompareTo(root.Right.Key);
            if (compareRight > 0)
            {
                //root is bigger than right
                SwapData(root, root.Right);
            }
            int compareLeft = root.Key.CompareTo(root.Left.Key);
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

            T tempValue = node1.Value;
            node1.Value = node2.Value;
            node2.Value = tempValue;
        }

        //public void Remove(T element)
        //{
        //    Contract.Requires<ArgumentNullException>(element != null, "element");
        //    //Restore heap property
        //    root = Heapify(root);
        //}


        public T GetMin()
        {
            if (root == null)
            {
                return default(T);
            }
            var temp = root;

            return temp.Value;
        }

    }
}
