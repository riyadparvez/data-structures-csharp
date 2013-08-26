using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;


namespace DataStructures.AvlTreeSpace
{
    /// <summary>
    /// Balanced binary search tree
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public partial class AvlTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        protected int count;
        private Node<TKey, TValue> root;

        public int Count
        {
            get { return count; }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(count >= 0);
        }

        public void Add(TKey key, TValue value)
        {
            Contract.Requires<ArgumentNullException>(key != null, "Can't insert null values");

            root = Add(root, key, value);
            root.Height = 1;
            FixHeight(root);
        }

        /// <summary>
        /// Adds newly added element
        /// </summary>
        /// <param name="root">root of the tree the element to be added</param>
        /// <param name="key">true if added, false if already added</param>
        /// <returns>Newly added elements</returns>
        private Node<TKey, TValue> Add(Node<TKey, TValue> root, TKey key, TValue value, int height = 1)
        {
            Contract.Requires<ArgumentNullException>(key != null, "Can't insert null values");
            Contract.Requires<ArgumentOutOfRangeException>(height >= 0);
            Contract.Ensures(Contract.Result<Node<TKey, TValue>>() != null);

            if (root == null)
            {
                root = new Node<TKey, TValue>(key, value, root, height);
                return root;
            }
            else
            {
                int i = root.Key.CompareTo(key);

                if (i > 0)
                {
                    root.Left = Add(root.Left, key, value, height + 1);
                    root = RebalanceLeft(root);
                }
                else if (i < 0)
                {
                    root.Right = Add(root.Right, key, value, height + 1);
                    root = RebalanceRight(root);
                }
                //Element is already added to the tree
                return root;
            }
        }


        private void FixHeight(Node<TKey, TValue> root)
        {
            if(root == null)
            {
                return;
            }

            var queue = new Queue<Node<TKey, TValue>>();
            queue.Enqueue(root);
            while (queue.Any())
            {
                var node = queue.Dequeue();
                
                if (node.Left != null)
                {
                    node.Left.Height = node.Height + 1;
                    queue.Enqueue(node.Left);
                }
                if (node.Right != null)
                {
                    node.Right.Height = node.Height + 1;
                    queue.Enqueue(node.Right);
                }
            }
        }


        private Node<TKey, TValue> RebalanceLeft(Node<TKey, TValue> root)
        {
            if(root == null)
            {
                return null;
            }

            var left = root.Left;
            var right = root.Right;
            int leftHeight = (root.Left == null) ? 0 : root.Left.Height;
            int rightHeight = (root.Right == null) ? 0 : root.Right.Height;

            if (leftHeight > (rightHeight + 1))
            {
                Contract.Assert((leftHeight == rightHeight + 2), "Tree is unbalanced already");
                //Check which one of the left subtree is greater
                if (left.Left.Height > left.Right.Height)
                {
                    Contract.Assert((left.Left.Height == left.Right.Height + 1), "Tree is unbalanced already");
                    root = RotateRight(root);
                    return root;
                }
                else
                {
                    Contract.Assert((left.Right.Height == left.Left.Height + 1), "Tree is unbalanced already");
                    root.Left = RotateLeft(root);
                    root = RotateRight(root);
                    return root;
                }
            }
            else
            {
                //Tree is already balanced
                Contract.Assert((leftHeight == rightHeight), "Tree is beyond balanced state");
                return root;
            }
        }

        private Node<TKey, TValue> RebalanceRight(Node<TKey, TValue> root)
        {
            if (root == null)
            {
                return null;
            }

            var left = root.Left;
            var right = root.Right;
            int leftHeight = (root.Left == null) ? 0 : root.Left.Height;
            int rightHeight = (root.Right == null) ? 0 : root.Right.Height;

            if (rightHeight > (leftHeight + 1))
            {
                Contract.Assert((rightHeight == leftHeight + 2), "Tree is unbalanced already");

                if (right.Right.Height > right.Left.Height)
                {
                    Contract.Assert((right.Right.Height == right.Left.Height + 1), "Tree is unbalanced already");
                    root = RotateLeft(root);
                    return root;
                }
                else
                {
                    Contract.Assert((right.Left.Height == right.Right.Height + 1), "Tree is unbalanced already");
                    root.Right = RotateRight(root.Right);
                    root = RotateLeft(root);
                    return root;
                }
            }
            else
            {
                //Tree is already balanced
                Contract.Assert((leftHeight == rightHeight), "Tree is beyond balanced state");
                return root;
            }
        }

        private Node<TKey, TValue> RotateLeft(Node<TKey, TValue> root)
        {
            if (root == null)
            {
                return null;
            }

            var temp = root.Right;
            root.Right.Left = root;
            root.Right = temp.Left;
            return temp;
        }

        private Node<TKey, TValue> RotateRight(Node<TKey, TValue> root)
        {
            if (root == null)
            {
                return null;
            }

            var temp = root.Left;
            root.Left.Right = root;
            root.Left = temp.Right;
            return temp;
        }

        [Pure]
        private bool Exists(TKey item, Node<TKey, TValue> root) 
        {
            if(root == null)
            {
                return false;
            }

            int i = root.Key.CompareTo(item);
            if(i < 0)
            {
                return Exists(item, root.Right);
            }
            else if(i > 0)
            {
                return Exists(item, root.Left);
            }
            else
            {
                return true;
            }
        }

        [Pure]
        public bool Exists(TKey key) 
        {
            Contract.Requires<ArgumentNullException>(key != null);

            return Exists(key, root);
        }

        [Pure]
        public bool IsBalanced()
        {
            return IsBalanced(root);
        }

        [Pure]
        private bool IsBalanced(Node<TKey, TValue> node)
        {
            if (node == null)
            {
                return true;
            }

            int h = node.Height;
            int hl = (node.Left == null) ? 0 : node.Left.Height;
            int hr = (node.Right == null) ? 0 : node.Right.Height;
            if (!(h == (hl > hr ? hl + 1 : hr + 1)))
            {
                return false;
            }
            if (hl > hr + 1 || hr > hl + 1)
            {
                return false;
            }
            return IsBalanced(node.Left) && IsBalanced(node.Right);
        }
        //TODO: implement find
        [Pure]
        public TValue Find(TKey key) 
        {
            Contract.Requires<ArgumentNullException>(key != null);
            throw new NotImplementedException();
        }
        //TODO: implement remove
        public void Remove(TKey key) 
        {
            Contract.Requires<ArgumentNullException>(key != null);
            if(Find(key).Equals(default(TValue)))
            {
            
            }
        }

        private void PushLeft(Stack<Node<TKey, TValue>> stack, Node<TKey, TValue> x)
        {
            Contract.Requires<ArgumentNullException>(stack != null);

            while (x != null)
            { 
                stack.Push(x); 
                x = x.Left; 
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var stack = new Stack<Node<TKey, TValue>>();
            PushLeft(stack, root);
            while (stack.Any())
            {
                var x = stack.Pop();
                if(x == null)
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
