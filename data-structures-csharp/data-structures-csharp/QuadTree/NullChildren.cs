using System;

namespace DataStructures.QuadTreeSpace
{
    public partial class QuadTree<T>
    {    
        [Serializable]
        private sealed class NullChildren<T> : Children<T>
        {
            public NullChildren(Node<T> parent)
                : base(parent, null, null, null, null)
            {

            }

            public override Node<T> GetContainingChild(Point point)
            {
                return null;
            }
        }
    }
}
