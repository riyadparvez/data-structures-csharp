using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DataStructures.SkipListSpace
{
    public partial class SkipList<TKey, TValue> : IEnumerable<TValue>, ICollection<TValue>
            where TKey : IComparable<TKey>
    {
        [Serializable]
        private class SkipNode<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            //Each link contains next level successor in skip list
            public IList<SkipNode<TKey, TValue>> Links { get; private set; }

            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(Links != null);
                Contract.Invariant(Links.Count > 0);
            }

            public SkipNode(int level)
            {
                Contract.Requires<ArgumentOutOfRangeException>(level > 0);
                Links = new List<SkipNode<TKey, TValue>>(level);
            }

            public SkipNode(int level, TKey key, TValue value)
            {
                Contract.Requires<ArgumentNullException>(key != null);
                Contract.Requires<ArgumentOutOfRangeException>(level > 0);

                Key = key;
                Value = value;
                Links = new List<SkipNode<TKey, TValue>>(level);
            }
        }
    }
}