using System;
using System.Diagnostics.Contracts;

namespace DataStructures.AvlTreeSpace
{
    /// <summary>
    /// Node of AVL tree, left<root<right
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    [Serializable]
    public class Node<T>
        where T : IComparable<T>
    {
        public readonly T data;

        public T Data
        {
            get { return data; }
        }
        public int Height { get; internal set; }
        internal Node<T> Parent { get; set; }
        internal Node<T> Left { get; set; }
        internal Node<T> Right { get; set; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Height >= 0);
            Contract.Invariant(data != null);
        }

        public Node(T data, Node<T> parent, int height)
        {
            Contract.Requires<ArgumentNullException>(data != null);
            Contract.Requires<ArgumentNullException>(parent != null);
            Contract.Requires(height >= 0);

            this.data = data;
            Parent = parent;
            Left = null;
            Right = null;
            Height = height;
        }
    }
}
