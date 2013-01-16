using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.SkipListSpace
{
    [Serializable]
    public sealed class NullSkipNode<TKey, TValue> : SkipNode<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public NullSkipNode(int level)
            : base (level)
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
