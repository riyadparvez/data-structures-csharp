using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.SkipListSpace
{
    [Serializable]
    public class SkipNode<TKey, TValue> 
        where TKey : IComparable<TKey>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public IList<SkipNode<TKey, TValue>> Links;

        public SkipNode(int level) 
        {
            Links = new List<SkipNode<TKey, TValue>>(level);
        }

        public SkipNode(int level, TKey key, TValue value) 
        {
            Key = key;
            Value = value;
            Links = new List<SkipNode<TKey, TValue>>(level);
        }
    }
}
