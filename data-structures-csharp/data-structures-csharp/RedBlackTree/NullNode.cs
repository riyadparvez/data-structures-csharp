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
        private sealed class NullNode<TKey, TValue> : Node<TKey, TValue>
            where TKey : IComparable<TKey>
        {

        }
    }
}