using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataStructures.RedBlackTreeSpace
{
    [Serializable]
    public class RedBlackTree<T>
        where T : IComparable<T>
    {
        private NullNode<T> nullNode = new NullNode<T>();

        private Node<T> current;
        private Node<T> parent;
        private Node<T> grandParent;
        private Node<T> greatParent;
        private Node<T> header;

        public RedBlackTree(T data)
        {
            current = new Node<T>(default(T), nullNode, nullNode);
            parent = new Node<T>(default(T), nullNode, nullNode);
            grandParent = new Node<T>(default(T), nullNode, nullNode);
            greatParent = new Node<T>(default(T), nullNode, nullNode);
            nullNode.Left = nullNode;
            nullNode.Right = nullNode;
            header = new Node<T>(data, nullNode, nullNode);
        }

        /// <summary>
        /// Insert key to Red Black Tree
        /// </summary>
        /// <param name="key"></param>
        public void Insert(T key)
        {
            Debug.Assert(key != null);

            grandParent = header;
            parent = grandParent;
            current = parent;
            nullNode.Data = key;

            while (current.Data.CompareTo(key) != 0)
            {
                Node<T> greatParent = grandParent;
                grandParent = parent;
                parent = current;
                if (key.CompareTo(current.Data) < 0)
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
                    HandleReorient(key);
                }
            }
            if (!(current == nullNode))
            {
                return;
            }
            current = new Node<T>(key, nullNode, nullNode);
            if (key.CompareTo(parent.Data) < 0)
            {
                parent.Left = current;
            }
            else
            {
                parent.Right = current;
            }
            HandleReorient(key);
        }

        /// <summary>
        /// 
        /// </summary>
        public void MakeEmpty()
        {
            header.Right = nullNode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return (header.Right == nullNode);
        }

        /// <summary>
        /// Returns max element of the tree
        /// </summary>
        /// <returns></returns>
        public T FindMax()
        {
            if (this.IsEmpty())
            {
                return default(T);
            }
            Node<T> itrNode = header.Right;
            while (itrNode.Right != nullNode)
            {
                itrNode = itrNode.Right;
            }
            return itrNode.Data;
        }

        /// <summary>
        /// Returns min element of the tree
        /// </summary>
        /// <returns></returns>
        public T FindMin()
        {
            if (this.IsEmpty())
            {
                return default(T);
            }

            Node<T> itrNode = header.Right;

            while (itrNode.Left != nullNode)
            {
                itrNode = itrNode.Left;
            }
            return itrNode.Data;
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

        public void Print(Node<T> n)
        {
            if (n != nullNode)
            {
                Print(n.Left);
                Console.WriteLine(n.Data);
                Print(n.Right);
            }
        }

        public void HandleReorient(T item)
        {
            current.Color = NodeType.Red;
            current.Left.Color = NodeType.Black;
            current.Right.Color = NodeType.Black;
            if (parent.Color == NodeType.Red)
            {
                grandParent.Color = NodeType.Red;
                if ((item.CompareTo(grandParent.Data) < 0) !=
                    (item.CompareTo(parent.Data)))
                {
                    current = Rotate(item, grandParent);
                    current.Color = NodeType.Black ;
                }
                header.Right.Color = NodeType.Black;
            }
        }

        public Node<T> Rotate(T item, Node<T> parent) 
        {
            if (item.CompareTo(parent.Data) < 0)
            {
                if (item.CompareTo(parent.Left.Data) < 0)
                {
                    parent.Left = RotateWithLeftChild(parent.Left);
                }
                else
                {
                    parent.Left = RotateWithRightChild(parent.Left);
                }
                return parent.Left;
            }

            else
            {
                if (item.CompareTo(parent.Right.Data) < 0)
                {
                    parent.Right = RotateWithLeftChild(parent.Right);
                }
                else
                {
                    parent.Right = RotateWithRightChild(parent.Right);
                }
                return parent.Right;
            }
        }

        public Node<T> RotateWithLeftChild(Node<T> k2)
        {
            Node<T> k1 = k2.Left;
            k2.Left = k1.Right;
            k1.Right = k2;
            return k1;
        }

        public Node<T> RotateWithRightChild(Node<T> k1)
        {
            Node<T> k2 = k1.Right;
            k1.Right = k2.Left;
            k2.Left = k1;
            return k2;
        }

    }
}