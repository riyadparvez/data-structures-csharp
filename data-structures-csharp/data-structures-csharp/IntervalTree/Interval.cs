using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace DataStructures.IntervalTreeSpace
{
    [Serializable]
    public struct Interval : IEquatable<Interval>
    {
        public int Start { get; set; }
        public int End { get; set; }
        public double Median { get { return (Start + End) / 2; } }

        [ContractInvariantMethod]
        private void StructInvariant()
        {
            Contract.Invariant(Start <= End);
        }

        public bool Equals(Interval otherInterval)
        {
            return Start.Equals(otherInterval.Start) &&
                    End.Equals(otherInterval.End);
        }
    }

    public class StartComparison : Comparer<Interval>
    {
        public override int Compare(Interval x, Interval y)
        {
            return x.Start.CompareTo(y.Start);
        }
    }

    public class EndComparison : Comparer<Interval>
    {
        public override int Compare(Interval x, Interval y)
        {
            return x.End.CompareTo(y.End);
        }
    }
}
