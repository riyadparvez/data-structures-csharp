using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.BinarySearchTreeSpace
{
    /// <summary>
    /// Node of BST, left<root<right
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public class Node<T> where T : IComparable<T>
    {
        public readonly T data; 

        public T Data
        {
            get { return data; }
        }
        internal Node<T> Parent { get; set; }
        internal Node<T> Left { get; set; }
        internal Node<T> Right { get; set; }

        public Node(T data, Node<T> parent) 
        { 
            this.data = data;
            Parent = parent;
            Left = null;
            Right = null;
        }
    }
}
