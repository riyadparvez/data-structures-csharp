using System;
using System.Diagnostics.Contracts;


namespace CSharp.DataStructures.BinarySearchTreeSpace
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
                return RotateRight(parent);
            }
            else if (parent.Right == node)
            {
                return RotateLeft(parent);
            }
            else
            {
                Contract.Assert(false);
                return null;
            }
        }

        private void Reorient(Node<TKey, TValue> node)
        {
            if (node == null)
            {
                return;
            }
            if (node == root)
            {
                return;
            }
            Node<TKey, TValue> parent = node.Parent;
            if (parent == root)
            {
                root = Transpose(node);
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
            if (root == null)
            {
                return null;
            }
            //temp- original node to be swapped with
            Node<TKey, TValue> temp = root.Right;
            Node<TKey, TValue> tempLeft = temp.Left;
            root.Right = tempLeft;
            if (tempLeft != null)
            {
                tempLeft.Parent = root;
            }

            temp.Left = root;
            temp.Parent = root.Parent;
            root.Parent = temp;
            return temp;
        }

        private Node<TKey, TValue> RotateRight(Node<TKey, TValue> root)
        {
            if (root == null)
            {
                return null;
            }
            //temp- original node to be swapped with
            Node<TKey, TValue> temp = root.Left;
            Node<TKey, TValue> tempRight = temp.Right;

            root.Left = tempRight;
            if (tempRight != null)
            {
                tempRight.Parent = root;
            }

            temp.Right = root;
            temp.Parent = root.Parent;
            root.Parent = temp;
            return temp;
        }

        /// <summary>
        /// Find element in BST, returns false if not found
        /// </summary>
        /// <param name="element">Element to be found</param>
        /// <returns></returns>
        public override bool Find(TKey element, out TValue value)
        {
            Contract.Requires<ArgumentNullException>(element != null, "BST can't have null values");
            Node<TKey, TValue> node = FindNode(element);
            Reorient(node);
            if (node != null)
            {
                value = node.Value;
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }
    }
}
