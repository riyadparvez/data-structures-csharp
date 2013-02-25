using System;
using System.Diagnostics.Contracts;

namespace DataStructures.BTreeSpace
{
    [Serializable]
    public partial class BTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private readonly int maximumChildrenPerNode;

        private Node<TKey, TValue> root;

        public int MaximumChildrenPerNode
        {
            get { return maximumChildrenPerNode; }
        }

        public int Count { get; private set; }

        [ContractInvariantMethod]
        private void ObjectInvariant() 
        {
            Contract.Invariant(Count >= 0);
        }

        public 
    }
}
