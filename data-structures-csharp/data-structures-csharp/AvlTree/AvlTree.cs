using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures.BinarySearchTreeSpace;


namespace DataStructures.AvlTreeSpace
{
    public class AvlTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        protected int count;
        protected Node<T> root;

        public Node<T> Root
        {
            get { return root; }
        }
        public int Count
        {
            get { return count; }
        }

        public Node<T> Add(T element)
        {
            return Add(root, element);
        }
        
        /// <summary>
        /// Adds newly added element
        /// </summary>
        /// <param name="root">root of the tree the element to be added</param>
        /// <param name="element">true if added, false if already added</param>
        /// <returns>Newly added elements</returns>
        public Node<T> Add(Node<T> root, T element, int height = 0)
        {
            height++;
            if(root == null)
            {
                root = new Node<T>(element, root, height);
                return root;
            }
            else
            {
                int i = root.Data.CompareTo(element);

                if(i < 0)
                {
                    root.Left = Add(root.Left, element);
                    root = RebalanceLeft(root);
                }
                else if (i > 0)
                {
                    root.Right = Add(root.Right, element);
                    root = RebalanceRight(root);
                }
                
                return root;
            }
        }


        private void FixHeight(Node<T> root) 
        {
            List<Node<T>> queue = new List<Node<T>>{root};
            while(queue.Any())
            {
                Node<T> node = queue[0];
                queue.Remove(node);
                node.Left.Height = node.Height + 1;
                node.Right.Height = node.Height + 1;
                queue.Add(node.Left);
                queue.Add(node.Right);
            }
        }


        private Node<T> RebalanceLeft(Node<T> root) 
        {
            Node<T> left = root.Left;
            Node<T> right = root.Right;
            int leftHeight = root.Left.Height;
            int rightHeight = root.Right.Height;

            if (leftHeight > (rightHeight + 1))
            {
                Debug.Assert((leftHeight == rightHeight + 2), "Tree is unbalanced already");
                //Check which one of the left subtree is greater
                if (left.Left.Height > left.Right.Height)
                {
                    Debug.Assert((left.Left.Height == left.Right.Height + 1), "Tree is unbalanced already");
                    root = RotateRight(root);
                    return root;
                }
                else
                {
                    Debug.Assert((left.Right.Height == left.Left.Height + 1), "Tree is unbalanced already");
                    root.Left = RotateLeft(root);
                    root = RotateRight(root);
                    return root;
                }
            }
            else
            {
                //Tree is already balanced
                Debug.Assert((leftHeight == rightHeight), "Tree is beyond balanved state");
                FixHeight(root);
                return root;
            }
        }

        private Node<T> RebalanceRight(Node<T> root)
        {
            Node<T> left = root.Left;
            Node<T> right = root.Right;
            int leftHeight = root.Left.Height;
            int rightHeight = root.Right.Height;

            if (rightHeight > (leftHeight + 1))
            {
                Debug.Assert((rightHeight == leftHeight + 2), "Tree is unbalanced already");

                if (right.Right.Height > right.Left.Height)
                {
                    Debug.Assert((right.Right.Height == right.Left.Height + 1), "Tree is unbalanced already");
                    root = RotateLeft(root);
                    return root;
                }
                else
                {
                    Debug.Assert((right.Left.Height == right.Right.Height + 1), "Tree is unbalanced already");
                    root.Right = RotateRight(root.Right);
                    root = RotateLeft(root);
                    return root;
                }
            }
            else
            {
                //Tree is already balanced
                Debug.Assert((leftHeight == rightHeight), "Tree is beyond balanved state");
                FixHeight(root);
                return root; 
            }
        }

        private Node<T> RotateLeft(Node<T> root) 
        { 
            Node<T> temp = root.Right;
            root.Right.Left = root;
            root.Right = temp.Left;
            return temp;
        }

        private Node<T> RotateRight(Node<T> root) 
        {
            Node<T> temp = root.Left;
            root.Left.Right = root;
            root.Left = temp.Right;
            return temp;
        }

        public bool IsBalanced(Node<T> node)
        {
            if (node == null)
            {
                return true;
            }
            int h = node.Height;
            int hl = node.Left.Height;
            int hr = node.Right.Height;
            if (!(h == (hl > hr ? hl + 1 : hr + 1)))
            {
                return false;
            }
            if (hl > hr + 1 || hr > hl + 1)
            {
                return false;
            }
            return IsBalanced(node.Left) && IsBalanced(node.Right);
        }

        private void PushLeft(Stack<Node<T>> stack, Node<T> x)
        {
            while (x != null)
            { stack.Push(x); x = x.Left; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Stack<Node<T>> stack = new Stack<Node<T>>();
            PushLeft(stack, root);
            while(stack.Any())
            {
                Node<T> x = stack.Pop();
                yield return x.Data;
                PushLeft(stack, x.Right);
            }
            yield break;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
