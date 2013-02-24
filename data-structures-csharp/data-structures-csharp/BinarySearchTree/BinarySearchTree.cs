﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;


namespace DataStructures.BinarySearchTreeSpace
{
    /// <summary>
    /// Binary search tree providing O(n) worst case search time
    /// </summary>
    /// <typeparam name="T">Type must inherit IComparable</typeparam>
    [Serializable]
    public class BinarySearchTree<TKey, TValue> : IEnumerable<TValue>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        protected int count;
        protected Node<TKey, TValue> root;

        public Node<TKey, TValue> Root
        {
            get { return root; }
        }
        public int Count
        {
            get { return count; }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(count >= 0);
        }

        /// <summary>
        /// Find node from red black tree
        /// </summary>
        /// <param name="key">element to be searched</param>
        /// <returns>Returns that element, otherwise default of that type</returns>
        public Node<TKey, TValue> FindNode(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            Node<TKey, TValue> current = root;

            while (current != null)
            {
                int i = current.Key.CompareTo(key);
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
                    Contract.Assert(current != null);
                    return current;
                }
            }
            return null;
        }

        /// <summary>
        /// Find element in BST, returns null if not found
        /// </summary>
        /// <param name="key">Element to be found</param>
        /// <returns></returns>
        public virtual TValue Find(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null, "BST can't have null values");

            Node<TKey, TValue> node = FindNode(key);
            return (node != null) ? node.Value : default(TValue);
        }


        /// <summary>
        /// Adds newly added element
        /// </summary>
        /// <param name="key">true if added, false if already added</param>
        /// <returns>Newly added elements</returns>
        public virtual bool Add(TKey key, TValue val)
        {
            Contract.Requires<ArgumentNullException>(key != null, "BST can't have null values");
            Contract.Requires<ArgumentNullException>(val != null);
            Contract.Ensures(count == (Contract.OldValue(count) + 1));

            if (Root == null)
            {
                root = new Node<TKey, TValue>(key, val, null);
                count++;
                return true;
            }

            Node<TKey, TValue> current = root;

            while (true)
            {
                int i = current.Key.CompareTo(key);
                if (i < 0)
                {
                    if (current.Right == null)
                    {
                        current.Right = new Node<TKey, TValue>(key, val, current);
                        count++;
                        return true;
                    }
                    current = current.Right;
                }
                else if (i > 0)
                {
                    if (current.Left == null)
                    {
                        current.Left = new Node<TKey, TValue>(key, val, current);
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
        /// Returns in order predecessor, returns null if it has no predecessor
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Node<TKey, TValue> Predecessor(Node<TKey, TValue> node)
        {
            Contract.Requires<ArgumentNullException>(node != null, "Null values doesn't have predecesspr");

            if (node.Left == null)
            {
                return null;
            }
            if (node.Left.Right == null)
            {
                return node.Left;
            }
            node = node.Left;
            while (node.Right != null)
            {
                node = node.Right;
            }

            return node;
        }


        /// <summary>
        /// Returns in order successor, returns if it's the last element
        /// </summary>
        /// <param name="node"></param>
        /// <returns>returns null if in order successor doesn't exist</returns>
        public Node<TKey, TValue> Successor(Node<TKey, TValue> node)
        {
            Contract.Requires<ArgumentNullException>(node != null, "Null values doesn't have successor");

            if (node.Right == null)
            {
                return null;
            }
            if (node.Right.Left == null)
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
        /// <param name="key"></param>
        /// <returns>true if removed</returns>
        public virtual bool Remove(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null, "BST doesn't contain null values");

            Node<TKey, TValue> node = FindNode(key);
            if (node == null)                            //node isn't there
            {
                return false;
            }
            if (node.Left == null && node.Right == null)     //node doesn't have any children
            {
                if (node.Parent.Right == node)
                {
                    node.Parent.Right = null;
                }
                else
                {
                    node.Parent.Left = null;
                }
                return true;
            }
            if (node.Left == null)                           //node has only right child
            {
                if (node.Parent.Right == node)
                {
                    node.Parent.Right = node.Right;
                }
                else
                {
                    node.Parent.Left = node.Right;
                }
                return true;
            }
            if (node.Right == null)                         //node has only right child
            {
                if (node.Parent.Right == node)
                {
                    node.Parent.Right = node.Left;
                }
                else
                {
                    node.Parent.Left = node.Left;
                }
                return true;
            }

            Node<TKey, TValue> current = node.Right;
            //node has both children
            if (node.Parent.Right == node)
            {
                node.Parent.Right = current;
            }
            else
            {
                node.Parent.Left = current;
            }
            if (current.Left == null)
            {
                current.Left = node.Left;
            }
            Predecessor(current).Left = node.Left;
            return true;
        }

        private Node<TKey, TValue> FindSplitNode(TKey start, TKey end)
        {
            Contract.Requires<ArgumentNullException>(start != null);
            Contract.Requires<ArgumentNullException>(end != null);
            Contract.Requires<ArgumentException>(start.CompareTo(end) < 0);

            var current = Root;
            while (current != null)
            {
                int startCompare = start.CompareTo(current.Key);
                int endCompare = end.CompareTo(current.Key);

                if (startCompare != endCompare)
                {
                    return current;
                }
                else if (endCompare <= 0)
                {
                    current = current.Left;
                }
                else if (endCompare > 0)
                {
                    current = current.Right;
                }
            }
            return null;
        }

        private bool IsLeafNode(Node<TKey, TValue> node)
        {
            Contract.Requires<ArgumentNullException>(node != null);
            return (node.Left == null) && (node.Right == null);
        }


        private bool IsInRange(TKey start, TKey end, Node<TKey, TValue> node)
        {
            Contract.Requires<ArgumentNullException>(start != null);
            Contract.Requires<ArgumentNullException>(end != null);
            Contract.Requires<ArgumentException>(start.CompareTo(end) < 0);
            Contract.Requires<ArgumentNullException>(node != null);
            return (start.CompareTo(node.Key) >= 0) &&
                   (end.CompareTo(node.Key) <= 0);
        }

        private List<Node<TKey, TValue>> GetAllNodes(Node<TKey, TValue> root)
        {
            Contract.Requires<ArgumentNullException>(root != null);
            Contract.Ensures(Contract.Result<List<Node<TKey, TValue>>>() != null);

            List<Node<TKey, TValue>> list = new List<Node<TKey, TValue>>();
            Queue<Node<TKey, TValue>> queue = new Queue<Node<TKey, TValue>>();
            queue.Enqueue(root);
            while (queue.Any())
            {
                var node = queue.Dequeue();
                if (node == null)
                {
                    break;
                }
                list.Add(node);
                queue.Enqueue(node.Left);
                queue.Enqueue(node.Right);
            }
            return list;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>All the values exclusive range</returns>
        public List<Node<TKey, TValue>> GetAllNodes(TKey start, TKey end)
        {
            Contract.Requires<ArgumentNullException>(start != null);
            Contract.Requires<ArgumentNullException>(end != null);
            Contract.Requires<ArgumentException>(start.CompareTo(end) < 0);
            Contract.Ensures(Contract.Result<List<Node<TKey, TValue>>>() != null);

            Node<TKey, TValue> node = FindSplitNode(start, end);
            var nodes = new List<Node<TKey, TValue>>();
            if (IsLeafNode(node))
            {
                if (IsInRange(start, end, node))
                {
                    nodes.Add(node);
                }
            }
            else
            {
                //Enumerate left subtree of split node
                var current = node.Left;
                while (!IsLeafNode(current))
                {
                    if (start.CompareTo(current.Key) < 0)
                    {
                        nodes.AddRange(GetAllNodes(current.Right));
                        current = current.Left;
                    }
                    else
                    {
                        current = current.Right;
                    }
                }
                //Enumerate right subtree of split node
                current = node.Right;
                while (!IsLeafNode(current))
                {
                    if (end.CompareTo(current.Key) > 0)
                    {
                        nodes.AddRange(GetAllNodes(current.Left));
                        current = current.Right;
                    }
                    else
                    {
                        current = current.Left;
                    }
                }
            }
            return nodes;
        }

        private Stack<Node<TKey, TValue>> PushLeft(Stack<Node<TKey, TValue>> stack, Node<TKey, TValue> x)
        {
            Contract.Requires<ArgumentNullException>(stack != null);
            while (x != null)
            { stack.Push(x); x = x.Left; }
            return stack;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            Stack<Node<TKey, TValue>> stack = new Stack<Node<TKey, TValue>>();
            stack = PushLeft(stack, root);
            while (stack.Any())
            {
                Node<TKey, TValue> x = stack.Pop();
                yield return x.Value;
                PushLeft(stack, x.Right);
            }
            yield break;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
