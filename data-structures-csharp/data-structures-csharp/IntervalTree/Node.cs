using System;
using System.Collections.Generic;
using System.Linq;


namespace DataStructures.IntervalTreeSpace
{
    /// <summary>
    /// Node for interval tree
    /// </summary>
    [Serializable]
    public class Node : IEquatable<Node>
    {
        private List<Interval> rightSortedIntervals;
        private List<Interval> leftSortedIntervals;

        public int X { get; set; }
        public IEnumerable<Interval> Intervals
        {
            get { return leftSortedIntervals.AsEnumerable(); }
        }
        public Node Left { get; set; }
        public Node Right { get; set; }


        public Node(int x)
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

        public bool Equals(Node other)
        {

            throw new NotImplementedException();
        }
    }
}
