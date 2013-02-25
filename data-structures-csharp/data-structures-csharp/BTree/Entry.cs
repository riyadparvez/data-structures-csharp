using System;

namespace DataStructures.BTreeSpace
{
    [Serializable]
    public partial class BTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        /// <summary>
        /// Every entry in children contains either a key value pair or link to next child
        /// internal nodes: only use key and next
        /// external nodes: only use key and value
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        [Serializable]
        private sealed class Entry<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node<TKey, TValue> ChildNode { get; set; }

            public Entry(TKey key, TValue value, Node<TKey, TValue> next)
            {
                this.Key = key;
                this.Value = value;
                this.ChildNode = next;
            }
        }
    }
}
