using System;
using System.Diagnostics.Contracts;

namespace CSharp.DataStructures.RedBlackTreeSpace
{
    public partial class RedBlackTree<TKey, TValue>
    {
        [Serializable]
        public class Node<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public NodeType Color { get; set; }
            public Node<TKey, TValue> Left { get; set; }
            public Node<TKey, TValue> Right { get; set; }

            public Node()
            {
                Color = NodeType.Black;
                Key = default(TKey);
                Left = null;
                Right = null;
            }

            public Node(TKey key)
            {
                Color = NodeType.Black;
                Key = key;
                Left = null;
                Right = null;
            }

            public Node(TKey key, TValue value, Node<TKey, TValue> left, Node<TKey, TValue> right)
            {
                Contract.Requires<ArgumentNullException>(key != null);

                Key = key;
                Value = value;
                Left = left;
                Right = right;
            }

            //public Node(TKey key, TValue value, Node<TKey, TValue> left, Node<TKey, TValue> right)
            //{
            //    Key = key;
            //    Value = value;
            //    Left = left;
            //    Right = right;
            //}
        }
    }
}
