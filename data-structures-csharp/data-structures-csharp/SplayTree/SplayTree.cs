using System;


namespace DataStructures.SplayTreeSpace
{
    //TODO: Splay Tree
    /// <summary>
    /// Splay Tree upon access an element it makes that element root
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
