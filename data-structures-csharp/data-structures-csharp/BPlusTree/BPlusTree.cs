using System;
using System.Diagnostics.Contracts;


namespace DataStructures.BPlusTreeSpace
{
    [Serializable]
    public class BPlusTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private Node<TKey, TValue> root;
        /// <summary>
        /// the maximum number of keys in the leaf node, M must be > 0      
        /// </summary>
        public readonly int M;
        /// <summary>
        /// the maximum number of keys in inner node, the number of pointer is N+1, N must be > 2
        /// </summary>
        public readonly int N;

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(M > 0);
            Contract.Invariant(N > 2);
        }

        public BPlusTree(int m, int n)
        {
            Contract.Requires<ArgumentOutOfRangeException>(m > 0);
            Contract.Requires<ArgumentOutOfRangeException>(n > 2);

            M = m;
            N = n;
        }
    }
}
