using System;
using System.Diagnostics.Contracts;


namespace DataStructures.BinomialHeapSpace
{
    public partial class BinomialHeap<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Node of Heap
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        [Serializable]
        private class Node<T>
            where T : IComparable<T>
        {
            private T val;

            public T Value
            {
                get { return val; }
                internal set
                {
                    Contract.Requires<ArgumentNullException>(value != null);
                    val = value;
                }
            }
            public int Degree { get; internal set; }
            internal Node<T> Parent { get; set; }
            internal Node<T> LeftChild { get; set; }
            internal Node<T> RightSibling { get; set; }

            public Node(T val, Node<T> parent, Node<T> leftChild, Node<T> rightSibling)
            {
                Contract.Requires<ArgumentNullException>(val != null);

                this.val = val;
                Parent = parent;
                LeftChild = null;
                RightSibling = null;
            }

            public Node(T val, Node<T> parent)
                : this(val, parent, null, null)
            {
            }

            public bool Equals(Node<T> otherNode)
            {
                if (otherNode == null)
                {
                    return false;
                }
                return val.Equals(otherNode.Value);
            }

            public override bool Equals(object obj)
            {
                Node<T> otherNode = obj as Node<T>;
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
                    hash = hash * 23 + val.GetHashCode();
                    return hash;
                }
            }
        }
    }
}