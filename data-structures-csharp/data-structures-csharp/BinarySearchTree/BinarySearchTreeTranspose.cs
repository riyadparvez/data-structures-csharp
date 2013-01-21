using System;
using System.Diagnostics;


namespace DataStructures.BinarySearchTreeSpace
{
    /// <summary>
    /// A bianry search tree which moves accessed element a level closer to root
    /// by using appropriate rotation
    /// </summary>
    [Serializable]
    public class BinarySearchTreeTranspose<T> : BinarySearchTree<T>
        where T : IComparable<T>, IEquatable<T>
    {
        private Node<T> Transpose(Node<T> node)
        {
            Node<T> parent = node.Parent;
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
                Debug.Assert(false);
                return null;
            }
        }

        private void Reorient(Node<T> node)
        {
            Node<T> parent = node.Parent;
            if (parent == Root)
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
                    Debug.Assert(false);
                }
            }
        }

        private Node<T> RotateLeft(Node<T> root)
        {
            Debug.Assert(root != null);

            Node<T> temp = root.Right;
            root.Right.Left = root;
            root.Right = temp.Left;
            return temp;
        }

        private Node<T> RotateRight(Node<T> root)
        {
            Debug.Assert(root != null);

            Node<T> temp = root.Left;
            root.Left.Right = root;
            root.Left = temp.Right;
            return temp;
        }

        /// <summary>
        /// Find element in BST, returns null if not found
        /// </summary>
        /// <param name="element">Element to be found</param>
        /// <returns></returns>
        public virtual T Find(T element)
        {
            Debug.Assert(element != null, "BST can't have null values");
            Node<T> node = FindNode(element);
            Reorient(node);
            return (node != null) ? node.Data : default(T);
        }
    }
}
