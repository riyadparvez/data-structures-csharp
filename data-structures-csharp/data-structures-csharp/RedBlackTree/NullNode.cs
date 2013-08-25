using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataStructures.RedBlackTreeSpace
{
    public partial class RedBlackTree<T>
    {
        [Serializable]
        private class NullNode<T> : Node<T>
            where T : IComparable<T>
        {

        }
    }
}