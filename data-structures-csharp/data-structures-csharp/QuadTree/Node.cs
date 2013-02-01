using System;
using System.Diagnostics.Contracts;


namespace DataStructures.QuadTreeSpace
{
    /// <summary>
    /// Node of Heap
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    [Serializable]
    public class Node<T>
        where T : IComparable<T>, IEquatable<T>
    {
        private T key;

        public TValue Value
        {
            get { return val; }
            internal set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                val = value;
            }
        }
        internal Node<T> Parent { get; set; }
        internal Children<T> Children { get; set; }

        public Node(Point topLeftPoint, Rectangle rectangle, Node<T> parent)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires(parent != null);

            this.val = val;
            Parent = parent;
            Children = new NullChildren<T>();
        }

        public bool Equals(Node<TKey, TValue> otherNode)
        {
            if (otherNode == null)
            {
                return false;
            }
            return val.Equals(otherNode.Value);
        }

        public override bool Equals(object obj)
        {
            Node<TKey, TValue> otherNode = obj as Node<TKey, TValue>;
            if (otherNode == null)
            {
                return false;
            }
            return val.Equals(otherNode.Value);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + key.GetHashCode();
                return hash;
            }
        }
    }
}
