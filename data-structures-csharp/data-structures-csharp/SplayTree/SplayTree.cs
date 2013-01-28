using System;
using System.Diagnostics.Contracts;


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
        public int Count { get; set; }
        public Node<T> Root { get; set; }

        public void Add(T data)
        {
            Contract.Requires(data != null);

        }

        public void Remove(T data)
        {
            Contract.Requires(data != null);
        }

        public T Find(T data)
        {
            Contract.Requires(data != null);
        }
    }
}
