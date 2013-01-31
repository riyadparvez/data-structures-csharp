using System;
using System.Diagnostics.Contracts;

namespace DataStructures.RedBlackTreeSpace
{
    [Serializable]
    public class Node<T> where T : IComparable<T>
    {
        public T Data { get; set; }
        public NodeType Color { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node()
        {
            Color = NodeType.Black;
            Data = default(T);
            Left = null;
            Right = null;
        }

        public Node(T data, Node<T> left, Node<T> right)
        {
            Contract.Requires<ArgumentNullException>(data != null);

            Data = data;
            Left = left;
            Right = right;
        }
    }
}
