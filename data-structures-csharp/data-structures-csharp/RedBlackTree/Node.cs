using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.RedBlackTreeSpace
{
    [Serializable]
    public class Node<T> where T : IComparable<T>
    {
        public T Data { get; set; }
        public NodeType Type { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
    }
}
