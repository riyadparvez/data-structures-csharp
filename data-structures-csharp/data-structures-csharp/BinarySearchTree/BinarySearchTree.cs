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
    }
}
