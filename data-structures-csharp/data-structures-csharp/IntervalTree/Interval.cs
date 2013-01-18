using System;
using System.Collections.Generic;


namespace DataStructures.IntervalTreeSpace
{
    [Serializable]
    public struct Interval
    {
        public int Start { get; set; }
        public int End { get; set; }
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
