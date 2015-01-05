using System;
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
    public partial class BinarySearchTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        protected int count;
        protected Node<TKey, TValue> root;

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
        protected Node<TKey, TValue> FindNode(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            var current = root;

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
        public virtual bool Add(TKey key, TValue value)
        {
            Contract.Requires<ArgumentNullException>(key != null, "BST can't have null values");
            Contract.Requires<ArgumentNullException>(value != null);
            //If the same key is added once again it will update the Value rather that adding a new Node
            Contract.Ensures(count == (Contract.OldValue(count) + 1) || count == (Contract.OldValue(count)));

            if (root == null)
            {
                root = new Node<TKey, TValue>(key, value, new NullNode<TKey,TValue>());
                count++;
                return true;
            }

            var current = root;

            while (true)
            {
                int i = current.Key.CompareTo(key);
                if (i < 0)
                {
                    if (current.Right == null)
                    {
                        current.Right = new Node<TKey, TValue>(key, value, current);
                        count++;
                        break;
                    }
                    current = current.Right;
                }
                else if (i > 0)
                {
                    if (current.Left == null)
                    {
                        current.Left = new Node<TKey, TValue>(key, value, current);
                        count++;
                        break;
                    }
                    current = current.Left;
                }
                else
                {
                    current.Value = value;
                    break;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns in order predecessor, returns null if it has no predecessor
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Predecessor(Node<TKey, TValue> node)
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

        public KeyValuePair<TKey, TValue> Predecessor(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            var node = FindNode(key);
            var predecessor = Predecessor(node);
            return new KeyValuePair<TKey, TValue>(predecessor.Key, predecessor.Value);
        }

        /// <summary>
        /// Returns in order successor, returns if it's the last element
        /// </summary>
        /// <param name="node"></param>
        /// <returns>returns null if in order successor doesn't exist</returns>
        private Node<TKey, TValue> Successor(Node<TKey, TValue> node)
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

        public KeyValuePair<TKey, TValue> Successor(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            var node = FindNode(key);
            var successor = Successor(node);
            return new KeyValuePair<TKey,TValue>(successor.Key, successor.Value);
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
            if (node == root)
            {
                //TODO: handle root case
                count--;
                return true;
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

            Node<TKey, TValue> result = null;
            var current = root;
            while (true)
            {
                int startCompare = start.CompareTo(current.Key);
                int endCompare = end.CompareTo(current.Key);

                if (startCompare == 0 || endCompare == 0)
                {
                    result = current;
                    break;
                }
                else if (startCompare < 0 && endCompare > 0)
                {
                    result = current;
                    break;
                }
                else if (startCompare > 0 && endCompare > 0)
                {
                    //If no Right node exists then the range specified is greater the highest node in the Tree
                    if (current.Right == null)
                    { break; }
                    current = current.Right;
                }
                else if (startCompare < 0 && endCompare < 0)
                {
                    //If no Left node exists then the range specified is below the least node in the Tree
                    if (current.Left == null)
                    { break; }
                    current = current.Left;
                }
            }
            return result;
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

        /// <summary>
        /// Traverse through all the nodes from root node and write the data in In-Order Traversal. This returns the result in ascending order
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private List<KeyValuePair<TKey, TValue>> GetAllNodes(Node<TKey, TValue> root)
        {
            Contract.Requires<ArgumentNullException>(root != null);
            Contract.Requires<ArgumentNullException>(this.root != null);
            Contract.Ensures(Contract.Result<List<KeyValuePair<TKey, TValue>>>() != null);

            var list = new List<KeyValuePair<TKey, TValue>>();
            if (root.Left != null)
            {
                list.AddRange(GetAllNodes(root.Left));
            }
            list.Add(new KeyValuePair<TKey, TValue>(root.Key, root.Value));
            if (root.Right != null)
            {
                list.AddRange(GetAllNodes(root.Right));
            }

            return list;
        }

        /// <summary>
        /// Traverse through all the nodes from root node within the range and write the data in In-Order Traversal. This returns the result in ascending order
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private List<KeyValuePair<TKey, TValue>> GetAllNodes(Node<TKey, TValue> root, TKey start, TKey end)
        {
            Contract.Requires<ArgumentNullException>(root != null);
            Contract.Requires<ArgumentNullException>(this.root != null);
            Contract.Ensures(Contract.Result<List<KeyValuePair<TKey, TValue>>>() != null);

            var list = new List<KeyValuePair<TKey, TValue>>();
            if (root.Left != null && (root.Left.Key.CompareTo(start) >= 0))
            {
                list.AddRange(GetAllNodes(root.Left,start,end));
            }
            list.Add(new KeyValuePair<TKey, TValue>(root.Key, root.Value));
            if (root.Right != null && (root.Right.Key.CompareTo(end) <= 0))
            {
                list.AddRange(GetAllNodes(root.Right,start,end));
            }

            return list;
        }

        /// <summary>
        /// Get All the Nodes in the Tree in In-Order Traversal. Get all the nodes in ascending order 
        /// </summary>
        /// <returns>All the values in the Tree in Sorted Order</returns>
        public IList<KeyValuePair<TKey, TValue>> GetAllNodes()
        {
            Contract.Ensures(Contract.Result<IList<KeyValuePair<TKey, TValue>>>() != null);

            Node<TKey, TValue> node = root;
            var pairs = new List<KeyValuePair<TKey, TValue>>();

            pairs.AddRange(GetAllNodes(node));
            return pairs;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>All the values inclusive range</returns>
        public IList<KeyValuePair<TKey, TValue>> GetAllNodes(TKey start, TKey end)
        {
            Contract.Requires<ArgumentNullException>(start != null);
            Contract.Requires<ArgumentNullException>(end != null);
            Contract.Requires<ArgumentException>(start.CompareTo(end) <= 0);
            Contract.Ensures(Contract.Result<IList<KeyValuePair<TKey, TValue>>>() != null);

            Node<TKey, TValue> node = FindSplitNode(start, end);
            var pairs = new List<KeyValuePair<TKey, TValue>>();
            //If null then the range is either higher or lower than the available data.
            if (node != null)
            {
                pairs.AddRange(GetAllNodes(node,start,end));
            }
            return pairs;
        }

        public TValue this[TKey key]
        {
            get
            {
                Contract.Requires<ArgumentNullException>(key != null);

                return Find(key);
            }
            set
            {
                Add(key, value);
            }
        }

        private Stack<Node<TKey, TValue>> PushLeft(Stack<Node<TKey, TValue>> stack, Node<TKey, TValue> x)
        {
            Contract.Requires<ArgumentNullException>(stack != null);
            while (x != null)
            {
                stack.Push(x);
                x = x.Left;
            }
            return stack;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            Stack<Node<TKey, TValue>> stack = new Stack<Node<TKey, TValue>>();
            stack = PushLeft(stack, root);
            while (stack.Any())
            {
                Node<TKey, TValue> x = stack.Pop();
                if (x == null)
                {
                    continue;
                }
                yield return new KeyValuePair<TKey, TValue>(x.Key, x.Value);
                PushLeft(stack, x.Right);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
