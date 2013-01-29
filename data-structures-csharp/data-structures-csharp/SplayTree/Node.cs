using System;
using System.Diagnostics.Contracts;

namespace DataStructures.SplayTreeSpace
{
    /// <summary>
    /// Node of BST, left<root<right
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    [Serializable]
    public class Node<T>
        where T : IComparable<T>, IEquatable<T>
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
        }

        public Node(T data, Node<T> parent)
        {
            Contract.Requires(data != null);

            this.data = data;
            Parent = parent;
            Left = null;
            Right = null;
        }

        public bool Equals(Node<T> otherNode)
        {
            if (otherNode == null)
            {
                return false;
            }
            return data.Equals(otherNode.Data);
        }

        public override bool Equals(object obj)
        {
            Node<T> otherNode = obj as Node<T>;
            if (otherNode == null)
            {
                return false;
            }
            return Data.Equals(otherNode.Data);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + data.GetHashCode();
                return hash;
            }
        }
    }
}
