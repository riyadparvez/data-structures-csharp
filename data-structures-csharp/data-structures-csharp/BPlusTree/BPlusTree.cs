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

        public void insert(TKey key, TValue value)
        {
            Split result = root.Insert(key, value);
            if (result != null)
            {
                // The old root was splitted in two parts.
                // We have to create a new root pointing to them
                IntermediateNode<TKey, TValue> _root = new IntermediateNode<TKey, TValue>();
                _root.num = 1;
                _root.keys[0] = result.key;
                _root.children[0] = result.left;
                _root.children[1] = result.right;
                root = _root;
            }
        }
    }
}
