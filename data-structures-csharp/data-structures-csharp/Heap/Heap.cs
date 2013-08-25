using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;


namespace DataStructures.HeapSpace
{
    /// <summary>
    /// Heap
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public partial class Heap<T>
        where T : IComparable<T>
    {
        private Node<T> root;

        public T Peek
        {
            get 
            {
                if(root == null)
                {
                    return default(T);
                }
                return root.Value; 
            }
        }
        public int Count { get; private set; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Count >= 0);
        }

        /// <summary>
        /// Adds an element to the heap
        /// </summary>
        /// <param name="element"></param>
        public void Add(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null, "element");
            Contract.Ensures(Count == Contract.OldValue<int>(Count) + 1);

            if (root == null)
            {
                root = new Node<T>(element, null);
                Count++;
                return;
            }

            Queue<Node<T>> queue = new Queue<Node<T>>();
            queue.Enqueue(root);
            while (queue.Any())
            {
                var node = queue.Dequeue();
                Contract.Assert(node != null);
                if (node.Left == null)
                {
                    node.Left = new Node<T>(element, node);
                    break;
                }
                else
                {
                    Contract.Assert(node.Value.CompareTo(node.Left.Value) < 0);
                    queue.Enqueue(node.Left);
                }
                if (node.Right == null)
                {
                    node.Left = new Node<T>(element, node);
                    break;
                }
                else
                {
                    Contract.Assert(node.Value.CompareTo(node.Right.Value) < 0);
                    queue.Enqueue(node.Right);
                }
            }
            //Restore heap property
            root = Heapify(root);
            Count++;
        }

        private Node<T> Heapify(Node<T> root)
        {
            Contract.Requires<ArgumentNullException>(root != null, "root");
            Contract.Ensures(Contract.Result<Node<T>>() != null);

            root.Right = Heapify(root.Right);
            root.Left = Heapify(root.Left);
            int compareRight = root.Value.CompareTo(root.Right.Value);
            if (compareRight > 0)
            {
                //root is bigger than right
                SwapData(root, root.Right);
            }
            int compareLeft = root.Value.CompareTo(root.Left.Value);
            if (compareLeft > 0)
            {
                //root is bigger than left
                SwapData(root, root.Left);
            }
            return root;
        }

        private void SwapData(Node<T> node1, Node<T> node2)
        {
            Contract.Requires<ArgumentNullException>(node1 != null, "node1");
            Contract.Requires<ArgumentNullException>(node2 != null, "node2");

            T tempValue = node1.Value;
            node1.Value = node2.Value;
            node2.Value = tempValue;
        }

        public T GetMin()
        {
            if (root == null)
            {
                return default(T);
            }

            return root.Value;
        }

        private Node<T> LastNode()
        {
            if (root == null)
            {
                return null;
            }

            Queue<Node<T>> queue = new Queue<Node<T>>();
            queue.Enqueue(root);
            Node<T> last = root;

            while (queue.Any())
            {
                var current = queue.Dequeue();
                Contract.Assert(current != null);
                last = current;
                if (current.Left != null)
                {
                    queue.Enqueue(current.Left);
                }
                if (current.Right != null)
                {
                    queue.Enqueue(current.Right);
                }
            }
            return last;
        }

        public T RemoveMin()
        {
            if (root == null)
            {
                return default(T);
            }

            Node<T> lastNode = LastNode();

            if (lastNode.Parent.Right == lastNode)
            {
                lastNode.Parent.Right = null;
            }
            if (lastNode.Parent.Left == lastNode)
            {
                lastNode.Parent.Left = null;
            }
            SwapData(root, lastNode);
            root = Heapify(root);
            Count--;
            return lastNode.Value;
        }
    }
}
