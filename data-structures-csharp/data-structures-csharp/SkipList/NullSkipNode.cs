using System;
using System.Collections.Generic;

namespace DataStructures.SkipListSpace
{
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

        public void Add(TValue item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(TValue item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TValue item)
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly { get; private set; }
    }
}