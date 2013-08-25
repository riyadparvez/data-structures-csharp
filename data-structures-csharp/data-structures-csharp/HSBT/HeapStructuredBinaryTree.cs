using System;
using System.Diagnostics.Contracts;


namespace DataStructures.HsbtSpace
{
    /// <summary>
    /// Root &lt left &lt right
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public partial class HeapStructuredBinaryTree<TKey, TValue>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private Node<TKey, TValue> root;

        public int Count { get; private set; }


        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Count >= 0);
        }

        /// <summary>
        /// Add keyed value to the tree, updates value if key already exists
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentNullException>(value != null);

            if (root == null)
            {
                root = new Node<TKey, TValue>(key, value, null);
                Count++;
                return;
            }

            var current = root;
            while (true)
            {
                Contract.Assert(current.Key.CompareTo(current.Left.Key) < 0);
                Contract.Assert(current.Key.CompareTo(current.Right.Key) < 0);
                Contract.Assert(current.Left.Key.CompareTo(current.Right.Key) < 0);

                int compareCurrent = current.Key.CompareTo(key);
                if (compareCurrent == 0)
                {
                    current.Value = value;
                    return;
                }
                if (compareCurrent < 0)
                {
                    var node = new Node<TKey, TValue>(key, value, current.Parent);
                    current.Parent.Left = node;
                    node.Left = current;
                    Count++;
                    return;
                }
                if (current.Left == null)
                {
                    current.Left = new Node<TKey, TValue>(key, value, current);
                    Count++;
                    return;
                }
                if (current.Right == null)
                {
                    current.Right = new Node<TKey, TValue>(key, value, current);
                    Count++;
                    return;
                }
                int compareLeft = current.Left.Key.CompareTo(key);
                int compareRight = current.Right.Key.CompareTo(key);
                if (compareLeft < 0)
                {
                    //key is less then left element
                    var node = new Node<TKey, TValue>(key, value, current);
                    node.Left = current.Left;
                    current.Left = node;
                    node.Left = current;
                    Count++;
                    return;
                }
                if (compareLeft < 0 && compareRight > 0)
                {
                    //key is greater than left and smaller than right
                    current = current.Left;
                    continue;
                }
                if (compareRight < 0)
                {
                    //key is greater than right
                    current = current.Right;
                    continue;
                }
            }
        }

        public TValue Find(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            var current = root;
            while (true)
            {
                Contract.Assert(current.Key.CompareTo(current.Left.Key) < 0);
                Contract.Assert(current.Key.CompareTo(current.Right.Key) < 0);
                Contract.Assert(current.Left.Key.CompareTo(current.Right.Key) < 0);

                int compare = key.CompareTo(current.Key);
                if (compare == 0)
                {
                    //Value found
                    return current.Value;
                }
                int compareRight = key.CompareTo(current.Right.Key);
                if (compareRight > 0)
                {
                    //Current key is in right sub tree
                    current = current.Right;
                    continue;
                }
                else if (compareRight == 0)
                {
                    return current.Right.Value;
                }
                int compareLeft = key.CompareTo(current.Left.Key);
                if (compareLeft < 0)
                {
                    return default(TValue);
                }
                else if (compareLeft > 0)
                {
                    //Current key is in left sub tree
                    current = current.Left;
                    continue;
                }
                else
                {
                    return current.Left.Value;
                }
            }

            return default(TValue);
        }
    }
}
