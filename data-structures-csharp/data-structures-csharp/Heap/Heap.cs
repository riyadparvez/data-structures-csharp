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
    public class Heap<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private Node<TKey, TValue> root;

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
        public void Add(TKey key, TValue element)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentNullException>(element != null, "element");
            Contract.Ensures(Count == Contract.OldValue<int>(Count) + 1);

            Queue<Node<TKey, TValue>> queue = new Queue<Node<TKey, TValue>>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                Contract.Assert(node != null);
                if (node.Left == null)
                {
                    node.Left = new Node<TKey, TValue>(key, element, node);
                    break;
                }
                else
                {
                    Contract.Assert(node.Key.CompareTo(node.Left.Key) < 0);
                    queue.Enqueue(node.Left);
                }
                if (node.Right == null)
                {
                    node.Left = new Node<TKey, TValue>(key, element, node);
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

        private Node<TKey, TValue> Heapify(Node<TKey, TValue> root)
        {
            Contract.Requires<ArgumentNullException>(root != null, "root");
            Contract.Ensures(Contract.Result<Node<TKey, TValue>>() != null);

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

        private void SwapData(Node<TKey, TValue> node1, Node<TKey, TValue> node2)
        {
            Contract.Requires<ArgumentNullException>(node1 != null, "node1");
            Contract.Requires<ArgumentNullException>(node2 != null, "node2");

            TValue tempValue = node1.Value;
            node1.Value = node2.Value;
            node2.Value = tempValue;

            TKey tempKey = node1.Key;
            node1.Key = node2.Key;
            node2.Key = tempKey;
        }

        //public void Remove(T element)
        //{
        //    Contract.Requires<ArgumentNullException>(element != null, "element");
        //    //Restore heap property
        //    root = Heapify(root);
        //}


        public TValue GetMin()
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
