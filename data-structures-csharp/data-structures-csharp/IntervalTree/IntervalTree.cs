using System;
using System.Collections.Generic;


namespace DataStructures.IntervalTreeSpace
{
    /// <summary>
    /// Interval tree
    /// </summary>
    [Serializable]
    public class IntervalTree
    {
        public Node Root { get; set; }

        public IntervalTree()
        {
        }

        /// <summary>
        /// Add an interval to the tree
        /// </summary>
        /// <param name="interval"></param>
        public void Add(Interval interval)
        {
            if (Root == null)
            {
                Root = new Node(interval.Median);
                Root.AddInterval(interval);
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
                        break;
                    }
                    current = current.Left;
                }
                current.AddInterval(interval);
                break;
            }
        }

        private List<Interval> Find(Node treeNode, double x)
        {
            if (treeNode == null)
            {
                return new List<Interval>();
            }

            if (x < treeNode.X)
            {
                var intervals = treeNode.GetIntervals(x);
                intervals.AddRange(Find(treeNode.Left, x));
                return intervals;
            }
            else if (x > treeNode.X)
            {
                var intervals = treeNode.GetIntervals(x);
                intervals.AddRange(Find(treeNode.Right, x));
                return intervals;
            }
            else
            {
                //all the intervals
                var intervals = new List<Interval>(treeNode.Intervals);
                return intervals;
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
        }

        public IEnumerable<Interval> Find(double x)
        {
            Node current = Root;
            return Find(current, x);
        }
    }
}
