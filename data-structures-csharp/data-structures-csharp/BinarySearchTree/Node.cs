using System;
using System.Diagnostics.Contracts;


namespace DataStructures.BinarySearchTreeSpace
{
    public partial class BinarySearchTree<TKey, TValue>
    {
        /// <summary>
        /// Node of Binary Search Tree
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        [Serializable]
        protected class Node<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            private TKey key;
            private TValue val;

            public virtual TKey Key
            {
                get { return key; }
                set
                {
                    Contract.Requires<ArgumentNullException>(value != null);
                    key = value;
                }

            }
            public virtual TValue Value
            {
                get { return val; }
                set
                {
                    Contract.Requires<ArgumentNullException>(value != null);
                    val = value;
                }
            }
            public virtual int Height { get; set; }
            public virtual Node<TKey, TValue> Parent { get; set; }
            public virtual Node<TKey, TValue> Left { get; set; }
            public virtual Node<TKey, TValue> Right { get; set; }

            protected Node()
            {
                //Do Nothing
            }

            public Node(TKey key, TValue val, Node<TKey, TValue> parent)
            {
                Contract.Requires<ArgumentNullException>(key != null);
                Contract.Requires<ArgumentNullException>(val != null);
                Contract.Requires(parent != null);
                this.key = key;
                this.val = val;
                Parent = parent;
                Left = null;
                Right = null;
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

        [Serializable]
        protected sealed class NullNode<TKey, TValue> : Node<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            private TKey key;
            private TValue val;

            public override TKey Key
            {
                get { return key; }

            }
            public override TValue Value
            {
                get { return val; }
            }
            public override int Height { get { return 0; } }
            public override Node<TKey, TValue> Parent { get { return null; } }
            public override Node<TKey, TValue> Left { get { return null; } }
            public override Node<TKey, TValue> Right { get { return null; } }

        }

    }
}
