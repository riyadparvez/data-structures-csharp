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
    public class HeapStructuredBinaryTree<TKey, TValue>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private Node<TKey, TValue> root;

        public int Count { get; private set; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Count >= 0);
        }

        public void Add(TKey key, TValue value)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentNullException>(value != null);

            var current = root;

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
