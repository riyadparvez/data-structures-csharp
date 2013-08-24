using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;


namespace DataStructures.IntervalTreeSpace
{
    /// <summary>
    /// Interval tree
    /// </summary>
    [Serializable]
    public partial class IntervalTree
    {
        private int count;

        public int Count
        {
            get { return count; }
        }
        private Node root;

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(count >= 0);
        }

        public IntervalTree()
        {
        }

        /// <summary>
        /// Add an interval to the tree
        /// </summary>
        /// <param name="interval"></param>
        public void Add(Interval interval)
        {
            Contract.Ensures(count == Contract.OldValue(count) + 1);

            if (root == null)
            {
                root = new Node(interval.Median);
                root.AddInterval(interval);
                count++;
                return;
            }

            Node current = root;
            while (true)
            {
                int temp = current.CompareTo(interval);
                if (temp < 0)
                {
                    if (current.Right == null)
                    {
                        current.Right = new Node(interval.Median);
                        current.Right.AddInterval(interval);
                        count++;
                        break;
                    }
                    current = current.Right;
                }
                else if (temp > 0)
                {
                    if (current.Left == null)
                    {
                        current.Left = new Node(interval.Median);
                        current.Left.AddInterval(interval);
                        count++;
                        break;
                    }
                    current = current.Left;
                }
                else
                {
                    current.AddInterval(interval);
                    count++;
                    break;
                }
            }
        }

        private IList<Interval> Find(Node treeNode, double x)
        {
            Contract.Ensures(Contract.Result<ReadOnlyCollection<Interval>>() != null);

            if (treeNode == null)
            {
                return new List<Interval>();
            }
            List<Interval> intervals;
            if (x < treeNode.X)
            {
                intervals = treeNode.GetIntervals(x);
                intervals.AddRange(Find(treeNode.Left, x));
            }
            else if (x > treeNode.X)
            {
                intervals = treeNode.GetIntervals(x);
                intervals.AddRange(Find(treeNode.Right, x));
            }
            else
            {
                //all the intervals
                intervals = new List<Interval>(treeNode.Intervals);
            }
            return intervals;
        }

        private Node FindNode(Interval interval)
        {
            Node current = root;

            while (current != null)
            {
                if (current.X > interval.Start)
                {
                    current = current.Left;
                }
                else if (current.X < interval.End)
                {
                    current = current.Right;
                }
                else
                {
                    return current;
                }
            }

            return null;
        }

        /// <summary>
        /// Remove an interval from tree if it exists, otherwise ignore
        /// </summary>
        /// <param name="interval">Interval to be removed</param>
        public void Remove(Interval interval)
        {
            Node node = FindNode(interval);
            if (node == null)
            {
                return;
            }
            node.Remove(interval);
            count--;
        }

        public IEnumerable<Interval> Find(double x)
        {
            Node current = root;
            return Find(current, x);
        }
    }
}
