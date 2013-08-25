using System;
using System.Diagnostics.Contracts;


namespace DataStructures.BinarySearchTreeSpace
{
    /// <summary>
    /// A binary search tree which moves accessed element a level closer to root
    /// by using appropriate rotation
    /// </summary>
    [Serializable]
    public class BinarySearchTreeTranspose<TKey, TValue> : BinarySearchTree<TKey, TValue>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private Node<TKey, TValue> Transpose(Node<TKey, TValue> node)
        {
            Node<TKey, TValue> parent = node.Parent;
            if (parent.Left == node)
            {
                //Left child
                //Rotate right
                return RotateRight(node);
            }
            else if (parent.Right == node)
            {
                return RotateLeft(node);
            }
            else
            {
                Contract.Assert(false);
                return null;
            }
        }

        private void Reorient(Node<TKey, TValue> node)
        {
            if(node == null)
            {
                return;
            }
            Node<TKey, TValue> parent = node.Parent;
            if (parent == root)
            {
                root = Transpose(parent);
            }
            else
            {
                if (parent.Parent.Right == parent)
                {
                    parent.Parent.Right = Transpose(node);
                }
                else if (parent.Parent.Left == parent)
                {
                    parent.Parent.Left = Transpose(node);
                }
                else
                {
                    Contract.Assert(false);
                }
            }
        }

        private Node<TKey, TValue> RotateLeft(Node<TKey, TValue> root)
        {
            if(root == null)
            {
                return null;
            }

            Node<TKey, TValue> temp = root.Right;
            root.Right.Left = root;
            root.Right = temp.Left;
            return temp;
        }

        private Node<TKey, TValue> RotateRight(Node<TKey, TValue> root)
        {
            if(root == null)
            {
                return null;
            }

            Node<TKey, TValue> temp = root.Left;
            root.Left.Right = root;
            root.Left = temp.Right;
            return temp;
        }

        /// <summary>
        /// Find element in BST, returns null if not found
        /// </summary>
        /// <param name="element">Element to be found</param>
        /// <returns></returns>
        public override TValue Find(TKey element)
        {
            Contract.Requires<ArgumentNullException>(element != null, "BST can't have null values");

            Node<TKey, TValue> node = FindNode(element);
            Reorient(node);
            return (node != null) ? node.Value : default(TValue);
        }
    }
}
