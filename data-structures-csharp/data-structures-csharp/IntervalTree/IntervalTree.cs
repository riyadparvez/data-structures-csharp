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

        public IEnumerable<Interval> Find()
        {
            Node current = Root;

            while()
            {
                
            }
        }
    }
}
