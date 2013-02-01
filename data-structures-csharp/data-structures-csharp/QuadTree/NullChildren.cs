using System;

namespace DataStructures.QuadTreeSpace
{
    [Serializable]
    public sealed class NullChildren<T> : Children<T>
    {
        public NullChildren()
            : base(null, null, null, null)
        {

        }
    }
}
