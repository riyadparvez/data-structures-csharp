using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataStructures.RedBlackTreeSpace
{
    public partial class RedBlackTree<TKey, TValue>
    {
        [Serializable]
        public sealed class NullNode<TKey, TValue> : Node<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            public NullNode()
                : base()
            {
            }
            public NullNode(TKey key) : base(key)
            {
            }

            public NullNode(TKey key, TValue value, Node<TKey, TValue> left, Node<TKey, TValue> right) : base(key, value, left, right)
            {
            }

        }
    }
}