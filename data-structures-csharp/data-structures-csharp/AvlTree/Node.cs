using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.AvlTreeSpace
{
    /// <summary>
    /// Node of AVL tree, left<root<right
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public class Node<T> where T : IComparable<T>
    {
        public readonly T data;

        public T Data
        {
            get { return data; }
        }
        public int Height { get; internal set; }
        internal Node<T> Parent { get; set; }
        internal Node<T> Left { get; set; }
        internal Node<T> Right { get; set; }

        public Node(T data, Node<T> parent, int height)
        {
            this.data = data;
            Parent = parent;
            Left = null;
            Right = null;
            Height = height;
        }
    }
}
