using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace DataStructures.IntervalTreeSpace
{
    [Serializable]
    public struct Interval : IEquatable<Interval>
    {
        private double start;
        private double end;
        public double Start 
        {
            get { return start; }
            set { start = value; }
        }
        public double End 
        {
            get { return end; }
            set { end = value; }
        }
        public double Median 
        { 
            get { return (start + end) / 2; } 
        }

        [ContractInvariantMethod]
        private void StructInvariant()
        {
            Contract.Invariant(start <= end);
        }

        public Interval(double start, double end) 
        {
            this.start = start;
            this.end = end;
        }

        public bool Equals(Interval otherInterval)
        {
            return start.Equals(otherInterval.Start) &&
                    end.Equals(otherInterval.End);
        }
    }

    public class StartComparer : Comparer<Interval>
    {
        public override int Compare(Interval x, Interval y)
        {
            return x.Start.CompareTo(y.Start);
        }
    }

    public class EndComparer : Comparer<Interval>
    {
        public override int Compare(Interval x, Interval y)
        {
            return x.End.CompareTo(y.End);
        }
    }
}
