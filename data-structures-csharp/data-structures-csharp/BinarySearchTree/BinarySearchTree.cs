using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataStructures.BinarySearchTreeSpace
{
    /// <summary>
    /// Binary search tree providing O(n) worst case search time
    /// </summary>
    /// <typeparam name="T">Type must inherit IComparable</typeparam>
    [Serializable]
    public class BinarySearchTree<T> : IEnumerable<T> 
        where T : IComparable<T>
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

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element">element to be searched</param>
        /// <returns>Returns that element, otherwise default</returns>
        public Node<T> FindNode(T element) 
        {
            Node<T> current = root;

            while (current != null)
            {
                int i = current.Data.CompareTo(element);
                if (i < 0)
                {
                    current = current.Right;
                }
                else if (i > 0)
                {
                    current = current.Left;
                }
                else
                {
                    return current;
                }
            }
            return null;
        }

        /// <summary>
        /// Find element in BST, returns null if not found
        /// </summary>
        /// <param name="element">Element to be found</param>
        /// <returns></returns>
        public T Find(T element) 
        {
            Debug.Assert(element != null, "BST can't have null values");
            Node<T> node = FindNode(element);
            return (node != null) ? node.Data : default(T); 
        }


        /// <summary>
        /// Adds newly added element
        /// </summary>
        /// <param name="element">true if added, false if already added</param>
        /// <returns>Newly added elements</returns>
        public virtual bool Add(T element)
        {
            Debug.Assert(element != null, "BST can't have null values");
            if (Root == null)
            {
                root = new Node<T>(element, null);
                count++;
                return true;
            }

            Node<T> current = root;
            
            while(true)
            {
                int i= current.Data.CompareTo(element);
                if(i < 0)
                {
                    if (current.Right == null)
                    {
                        current.Right = new Node<T>(element, current);
                        count++;
                        return true;
                    }
                    current = current.Right;
                }
                else if(i > 0)
                {
                    if (current.Left == null)
                    {
                        current.Left = new Node<T>(element, current);
                        count++;
                        return true;
                    }
                    current = current.Left;
                }
                else
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// Returns in order predecessor, returns null if it has no predecessor
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Node<T> Predecessor(Node<T> node) 
        {
            Debug.Assert(node != null, "Null values donesn't have predecesspr");

            if(node.Left == null)
            {
                return null;
            }
            if(node.Left.Right == null)
            {
                return node.Left;
            }
            node = node.Left;
            while(node.Right != null)
            {
                node = node.Right;
            }
            return node;
        }


        /// <summary>
        /// Returns in order successor, returns if it's the lst element
        /// </summary>
        /// <param name="node"></param>
        /// <returns>returns null if in order successor doesn't exist</returns>
        public Node<T> Successor(Node<T> node) 
        {
            Debug.Assert(node != null, "Null values donesn't have successor");

            if (node.Right == null)
            {
                return null;
            }
            if(node.Right.Left == null)
            {
                return node.Right;
            }
            node = node.Right;
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }


        /// <summary>
        /// Remove specified element
        /// </summary>
        /// <param name="element"></param>
        /// <returns>true if removed</returns>
        public virtual bool Remove(T element) 
        {
            Debug.Assert(element != null, "BST doesn't contain null values");
            
            Node<T> node = FindNode(element);
            if(node == null)                            //node isn't there
            {
                return false;
            }
            if(node.Left == null && node.Right == null)     //node doesn't have any children
            {
                if (node.Parent.Right == node)
                {
                    node.Parent.Right = null;
                }
                else 
                {
                    node.Parent.Left = null;                
                }
                return true;
            }
            if(node.Left == null)                           //node has only right child
            {
                if (node.Parent.Right == node)
                {
                    node.Parent.Right = node.Right;
                }
                else
                {
                    node.Parent.Left = node.Right;
                }
                return true;
            }
            if (node.Right == null)                         //node has only right child
            {
                if (node.Parent.Right == node)
                {
                    node.Parent.Right = node.Left;
                }
                else
                {
                    node.Parent.Left = node.Left;
                }
                return true;
            }

            Node<T> current = node.Right;
            //node has both children
            if (node.Parent.Right == node)
            {
                node.Parent.Right = current;
            }
            else
            {
                node.Parent.Left = current;
            }
            if(current.Left == null)
            {
                current.Left = node.Left;
            }
            Predecessor(current).Left = node.Left;
            return true;
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
