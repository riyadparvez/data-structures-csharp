using System;
using System.Diagnostics.Contracts;


namespace DataStructures.BPlusTreeSpace
{
    [Serializable]
    public partial class BPlusTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private INode<TKey, TValue> root;
        /// <summary>
        /// the maximum number of key value pairs in the leaf node, M must be > 0
        /// </summary>
        public readonly int NumberOfValuesInLeafNode;
        /// <summary>
        /// the maximum number of keys in inner node, the number of pointer is N+1, N must be > 2
        /// </summary>
        public readonly int NumberOfKeysInIntermediateNode;

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(NumberOfValuesInLeafNode > 0);
            Contract.Invariant(NumberOfKeysInIntermediateNode > 2);
        }

        public BPlusTree(int m, int n)
        {
            Contract.Requires<ArgumentOutOfRangeException>(m > 0);
            Contract.Requires<ArgumentOutOfRangeException>(n > 2);

            NumberOfValuesInLeafNode = m;
            NumberOfKeysInIntermediateNode = n;
        }

        private bool Find(TKey key, INode<TKey, TValue> node)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentNullException>(node != null);

            if (node is LeafNode<TKey, TValue>)
            {
            }
            return false;
        }

        [Pure]
        public bool Find(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            return Find(key, root);
        }

    }
}
