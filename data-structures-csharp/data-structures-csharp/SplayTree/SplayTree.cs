using System;


namespace DataStructures.SplayTreeSpace
{
    [Serializable]
    public class SplayTree<T>
        where T : IComparable<T>, IEquatable<T>
    {
        public Node<T> Root { get; set; }

        public void Add()
        {

        }

        public T Find(T data)
        {

        }
    }
}
