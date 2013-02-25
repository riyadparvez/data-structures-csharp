using System;

namespace DataStructures.BTreeSpace
{
    [Serializable]
    public partial class BTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        [Serializable]
        private sealed class Entry<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node<TKey, TValue> Next { get; set; }

            public Entry(TKey key, TValue value, Node<TKey, TValue> next)
            {
                this.Key = key;
                this.Value = value;
                this.Next = next;
            }
        }
    }
}
