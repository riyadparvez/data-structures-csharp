using System;

namespace DataStructures.BTreeSpace
{
    [Serializable]
    public partial class BTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        [Serializable]
        private sealed class Node<TKey, TValue>
            where TKey : IComparable<TKey>
        {

        }
    }
}
