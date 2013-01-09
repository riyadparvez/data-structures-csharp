using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataStructures.BinarySearchTreeSpace
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private int count;    
        private Node<T> root;

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
        public T Find(T element) 
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
                    return current.Data;
                }
            }
            return default(T);
        }


        /// <summary>
        /// Adds newly added element
        /// </summary>
        /// <param name="element">true if added, false if already added</param>
        /// <returns>Newly added elements</returns>
        public bool Add(T element)
        {
            if(Root == null)
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
        /// Returns in order predecessor
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Node<T> Predecessor(Node<T> node) 
        { 
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
        /// Returns in order successor
        /// </summary>
        /// <param name="node"></param>
        /// <returns>returns null if in order successor doesn't exist</returns>
        public Node<T> Successor(Node<T> node) 
        { 
            if(node.Right == null)
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
        public bool Remove(T element) 
        { 
        
        }
    }
}
