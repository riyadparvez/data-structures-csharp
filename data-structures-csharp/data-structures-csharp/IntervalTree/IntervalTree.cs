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
    public class IntervalTree
    {
        private int count;

        public int Count
        {
            get { return count; }
        }
        public Node Root { get; set; }

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

            if (Root == null)
            {
                Root = new Node(interval.Median);
                Root.AddInterval(interval);
                count++;
                return;
            }

            Node current = Root;
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

        private ReadOnlyCollection<Interval> Find(Node treeNode, double x)
        {
            Contract.Ensures(Contract.Result<ReadOnlyCollection<Interval>>() != null);

            if (treeNode == null)
            {
                return new ReadOnlyCollection<Interval>(new List<Interval>());
            }

            if (x < treeNode.X)
            {
                var intervals = treeNode.GetIntervals(x);
                intervals.AddRange(Find(treeNode.Left, x));
                return new ReadOnlyCollection<Interval>(intervals);
            }
            else if (x > treeNode.X)
            {
                var intervals = treeNode.GetIntervals(x);
                intervals.AddRange(Find(treeNode.Right, x));
                return new ReadOnlyCollection<Interval>(intervals);
            }
            else
            {
                //all the intervals
                var intervals = new List<Interval>(treeNode.Intervals);
                return new ReadOnlyCollection<Interval>(intervals);
            }
        }

        private Node FindNode(Interval interval)
        {
            Node current = Root;

            while (current != null)
            {
                if (current.X < interval.Start)
                {
                    current = current.Left;
                }
                else if (current.X > interval.End)
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
            Node current = Root;
            return Find(current, x);
        }
    }
}
