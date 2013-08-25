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
    public partial class SplayTree<T>
        where T : IComparable<T>, IEquatable<T>
    {
        public int Count { get; private set; }
        private Node<T> root;

        public void Add(T data)
        {
            Contract.Requires<ArgumentNullException>(data != null);

        }

        public void Remove(T data)
        {
            Contract.Requires<ArgumentNullException>(data != null);
        }

        [Pure]
        public T Find(T data)
        {
            Contract.Requires<ArgumentNullException>(data != null);
            return default(T);
        }
    }
}
