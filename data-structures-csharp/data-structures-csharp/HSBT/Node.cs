using System;
using System.Diagnostics.Contracts;


namespace DataStructures.HsbtSpace
{
    /// <summary>
    /// Node of Heap
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    [Serializable]
    public class Node<TKey, TValue>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private TKey key;
        private TValue val;

        public TKey Key
        {
            get { return key; }
            internal set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                key = value;
            }

        }
        public TValue Value
        {
            get { return val; }
            internal set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                val = value;
            }
        }
        public int Height { get; internal set; }
        internal Node<TKey, TValue> Parent { get; set; }
        internal Node<TKey, TValue> Left { get; set; }
        internal Node<TKey, TValue> Right { get; set; }

        public Node(TKey key, TValue val, Node<TKey, TValue> parent)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentNullException>(val != null);

            this.val = val;
            Parent = parent;
            Left = null;
            Right = null;
            if (Parent != null)
            {
                Height = Parent.Height + 1;
            }
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
