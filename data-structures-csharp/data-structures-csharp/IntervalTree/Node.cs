using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace DataStructures.IntervalTreeSpace
{
    public partial class IntervalTree
    {
        /// <summary>
        /// Node for interval tree
        /// </summary>
        [Serializable]
        private class Node : IEquatable<Node>, IComparable<Interval>, IComparable<Node>
        {
            private List<Interval> rightSortedIntervals;
            private List<Interval> leftSortedIntervals;

            public double X { get; set; }
            public ReadOnlyCollection<Interval> Intervals
            {
                get { return new ReadOnlyCollection<Interval>(leftSortedIntervals); }
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
                rightSortedIntervals.Sort(new EndComparer());
                
                leftSortedIntervals.Add(interval);
                leftSortedIntervals.Sort(new StartComparer());
            }

            public List<Interval> GetIntervals(double x)
            {
                List<Interval> intervals = new List<Interval>();
                if (x < X)
                {
                    foreach (var interval in leftSortedIntervals)
                    {
                        if (interval.Start > x)
                        {
                            break;
                        }
                        intervals.Add(interval);
                    }
                }
                else if (x > X)
                {
                    foreach (var interval in rightSortedIntervals)
                    {
                        if (interval.End < x)
                        {
                            break;
                        }
                        intervals.Add(interval);
                    }
                }
                else
                {
                    intervals.Concat(leftSortedIntervals);
                }
                return intervals;
            }

            public bool IsInInterval(Interval interval)
            {
                return X >= interval.Start && X <= interval.End;
            }

            public void Remove(Interval interval)
            {
                rightSortedIntervals.Remove(interval);
                leftSortedIntervals.Remove(interval);
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

            public int CompareTo(Node other)
            {
                if (other.X > X)
                {
                    return -1;
                }
                else if (other.X < X)
                {
                    return 1;
                }
                return 0;
            }
        }
    }
}
