using System;
using System.Diagnostics.Contracts;

namespace DataStructures.HsbtSpace
{
    [Serializable]
    public class HeapStructuredBinaryTree<TKey, TValue>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        public TValue Peek { get; set; }
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
        }

        public TValue Find(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);
        }
    }
}
