using System;
using System.Collections.Generic;

namespace DataStructures.SkipListSpace
{
    [Serializable]
    public partial class SkipList<TKey, TValue> : IEnumerable<TValue>, ICollection<TValue>
            where TKey : IComparable<TKey>
    {
        [Serializable]
        private sealed class NullSkipNode<TKey, TValue> : SkipNode<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            public NullSkipNode(int level)
                : base(level)
            {

            }

            public bool Equals(SkipNode<TKey, TValue> otherNode)
            {
                NullSkipNode<TKey, TValue> otherNullNode = otherNode as NullSkipNode<TKey, TValue>;
                if (otherNullNode == null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}