using System;
using System.Collections.Generic;
using System.Linq;


namespace DataStructures.IntervalTreeSpace
{
    /// <summary>
    /// Node for interval tree
    /// </summary>
    [Serializable]
    public class Node : IEquatable<Node>, IComparable<Interval>
    {
        private List<Interval> rightSortedIntervals;
        private List<Interval> leftSortedIntervals;

        public double X { get; set; }
        public IEnumerable<Interval> Intervals
        {
            get { return leftSortedIntervals.AsEnumerable(); }
        }
        public Node Left { get; set; }
        public Node Right { get; set; }


        public Node(double x)
        {
            this.X = x;
            rightSortedIntervals = new List<Interval>();
            leftSortedIntervals = new List<Interval>();
        }

        public void AddInterval(Interval interval)
        {
            rightSortedIntervals.Add(interval);
            rightSortedIntervals.Sort(new EndComparison());
            leftSortedIntervals.Add(interval);
            leftSortedIntervals.Sort(new StartComparison());
        }

        public bool IsInInterval(Interval interval)
        {
            return X >= interval.Start && X <= interval.End;
        }

        public bool Equals(Node other)
        {

            throw new NotImplementedException();
        }

        public int CompareTo(Interval other)
        {
            if (other.Start > X)
            {
                return -1;
            }
            else if (other.End < X)
            {
                return 1;
            }
            return 0;
        }
    }
}
