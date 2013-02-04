using System;

namespace DataStructures.BPlusTreeSpace
{
    public partial class BPlusTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        [Serializable]
        private class LeafNode<TKey, TValue> : Node<TKey, TValue>
            where TKey : IComparable<TKey>
        {

        }
    }
}