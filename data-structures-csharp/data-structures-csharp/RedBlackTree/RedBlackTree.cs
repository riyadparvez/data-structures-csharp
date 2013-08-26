using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace DataStructures.RedBlackTreeSpace
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public partial class RedBlackTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private readonly NullNode<TKey, TValue> nullNode = new NullNode<TKey, TValue>();

        private Node<TKey, TValue> current;
        private Node<TKey, TValue> parent;
        private Node<TKey, TValue> grandParent;
        private Node<TKey, TValue> greatParent;
        private Node<TKey, TValue> header;

        public int Count { get; private set; }

        public RedBlackTree(T data)
        {
            Contract.Requires<ArgumentNullException>(data != null);

            current = new Node<TKey, TValue>(default(T), nullNode, nullNode);
            parent = new Node<TKey, TValue>(default(T), nullNode, nullNode);
            grandParent = new Node<TKey, TValue>(default(T), nullNode, nullNode);
            greatParent = new Node<TKey, TValue>(default(T), nullNode, nullNode);
            nullNode.Left = nullNode;
            nullNode.Right = nullNode;
            header = new Node<TKey, TValue>(data, nullNode, nullNode);
        }

        /// <summary>
        /// Find an element, otherwise return default
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Pure]
        public TValue Find(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            Node<TKey, TValue> current = header.Right;
            while (true)
            {
                if (key.CompareTo(current.Key) < 0)
                {
                    current = current.Left;
                }
                else if (key.CompareTo(current.Key) > 0)
                {
                    current = current.Right;
                }
                else if (!(current == nullNode))
                {
                    return current.Value;
                }
                else
                {
                    return default(TValue);
                }
            }
        }

        /// <summary>
        /// Insert key to Red Black Tree
        /// </summary>
        /// <param name="key"></param>
        public void Insert(TKey key, TValue value)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            grandParent = header;
            parent = grandParent;
            current = parent;
            nullNode.Data = key;

            while (current.Key.CompareTo(key) != 0)
            {
                Node<TKey, TValue> greatParent = grandParent;
                grandParent = parent;
                parent = current;
                if (key.CompareTo(current.Key) < 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
                if (current.Left.Color == NodeType.Red &&
                    current.Right.Color == NodeType.Red)
                {
                    //A node can't have two red children
                    //Then HandleReorient method is called whenever a node has two red children
                    HandleReorient(key);
                }
            }
            if (!(current == nullNode))
            {
                //This node is already inserted in RB Tree
                return;
            }
            //Allocate new node
            current = new Node<TKey, TValue>(key, value, nullNode, nullNode);
            if (key.CompareTo(parent.Key) < 0)
            {
                parent.Left = current;
            }
            else
            {
                parent.Right = current;
            }
            //New child is added so
            //Then HandleReorient method is called whenever a node has two red children
            HandleReorient(key);
        }

        /// <summary>
        /// Clear current tree
        /// </summary>
        public void Clear()
        {
            header.Right = nullNode;
            Count = 0;
        }

        /// <summary>
        /// Check if the tree is empty
        /// </summary>
        /// <returns></returns>
        [Pure]
        public bool IsEmpty()
        {
            return (header.Right == nullNode);
        }

        /// <summary>
        /// Returns max element of the tree
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<TKey, TValue> FindMax()
        {
            if (this.IsEmpty())
            {
                return new KeyValuePair<TKey,TValue>();
            }
            //Rightmost child is the max child
            Node<TKey, TValue> itrNode = header.Right;
            while (itrNode.Right != nullNode)
            {
                itrNode = itrNode.Right;
            }
            return new KeyValuePair<TKey,TValue>(itrNode.Key, itrNode.Value);
        }

        /// <summary>
        /// Returns min element of the tree
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<TKey, TValue> FindMin()
        {
            if (this.IsEmpty())
            {
                return new KeyValuePair<TKey,TValue>();
            }
            //Leftmost child is the min child
            Node<TKey, TValue> itrNode = header.Right;

            while (itrNode.Left != nullNode)
            {
                itrNode = itrNode.Left;
            }
            return new KeyValuePair<TKey,TValue>(itrNode.Key, itrNode.Value);
        }

        public void Print()
        {
            if (this.IsEmpty())
            {
                Console.WriteLine("Empty");
            }
            else
            {
                Print(header.Right);
            }
        }

        private void Print(Node<TKey, TValue> n)
        {
            Contract.Requires<ArgumentNullException>(n != null);
            if (n != nullNode)
            {
                Print(n.Left);
                Console.WriteLine(n.Key);
                Print(n.Right);
            }
        }

        private void HandleReorient(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            current.Color = NodeType.Red;
            current.Left.Color = NodeType.Black;
            current.Right.Color = NodeType.Black;
            if (parent.Color == NodeType.Red)
            {
                grandParent.Color = NodeType.Red;
                if ((key.CompareTo(grandParent.Key) < 0) !=
                    (key.CompareTo(parent.Key) == 0))
                {
                    //Balance grandParent subtree by item
                    current = Rotate(key, grandParent);
                    current.Color = NodeType.Black;
                }
                header.Right.Color = NodeType.Black;
            }
        }

        private Node<TKey, TValue> Rotate(TKey key, Node<TKey, TValue> parent)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            if(parent == null)
            {
                return null;
            }

            //Left subtree is unbalanced
            if (key.CompareTo(parent.Key) < 0)
            {
                if (key.CompareTo(parent.Left.Key) < 0)
                {
                    //Left subtree of Right Left subtree is unbalanced
                    parent.Left = RotateRight(parent.Left);
                }
                else
                {
                    //Right subtree of Left subtree is unbalanced
                    parent.Left = RotateLeft(parent.Left);
                }
                return parent.Left;
            }
            //Right subtree is unbalanced
            else
            {
                if (key.CompareTo(parent.Right.Key) < 0)
                {
                    //Left subtree of Right subtree is unbalanced
                    parent.Right = RotateRight(parent.Right);
                }
                else
                {
                    //Right subtree of Left subtree is unbalanced
                    parent.Right = RotateLeft(parent.Right);
                }
                return parent.Right;
            }
        }

        public Node<TKey, TValue> RotateRight(Node<TKey, TValue> root)
        {
            if(root == null)
            {
                return null;
            }
            Node<TKey, TValue> k1 = root.Left;
            root.Left = k1.Right;
            k1.Right = root;
            return k1;
        }

        public Node<TKey, TValue> RotateLeft(Node<TKey, TValue> root)
        {
            if (root == null)
            {
                return null;
            }
            Node<TKey, TValue> k2 = root.Right;
            root.Right = k2.Left;
            k2.Left = root;
            return k2;
        }

    }
}