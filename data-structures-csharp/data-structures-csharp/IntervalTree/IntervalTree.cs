using System;
using System.Collections.Generic;


namespace DataStructures.IntervalTreeSpace
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class IntervalTree
    {
        private IEnumerable<Interval> intervals;


        public IntervalTree(IEnumerable<Interval> intervals)
        {
            this.intervals = intervals;
        }
    }
}
