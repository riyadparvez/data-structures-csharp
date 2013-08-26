using System;
using System.Diagnostics.Contracts;

namespace DataStructures.RedBlackTreeSpace
{
    public partial class RedBlackTree<TKey, TValue>
    {
        [Serializable]
        private class Node<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public NodeType Color { get; set; }
            public Node<TKey, TValue> Left { get; set; }
            public Node<TKey, TValue> Right { get; set; }

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
        }
    }
}
