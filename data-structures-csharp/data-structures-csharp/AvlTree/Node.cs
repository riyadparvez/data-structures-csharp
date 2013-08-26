using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DataStructures.AvlTreeSpace
{
    [Serializable]
    public partial class AvlTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        /// <summary>
        /// Node of Heap
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        [Serializable]
        protected class Node<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            private TKey key;
            private TValue val;

            public TKey Key
            {
                get { return key; }
                set
                {
                    Contract.Requires<ArgumentNullException>(value != null);
                    key = value;
                }

            }
            public TValue Value
            {
                get { return val; }
                set
                {
                    Contract.Requires<ArgumentNullException>(value != null);
                    val = value;
                }
            }
            public int Height { get; set; }
            public Node<TKey, TValue> Parent { get; set; }
            public Node<TKey, TValue> Left { get; set; }
            public Node<TKey, TValue> Right { get; set; }

            public Node(TKey key, TValue val, Node<TKey, TValue> parent, int height)
            {
                Contract.Requires<ArgumentNullException>(key != null);
                Contract.Requires<ArgumentNullException>(val != null);

                this.val = val;
                Parent = parent;
                Height = height;
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
    }
}