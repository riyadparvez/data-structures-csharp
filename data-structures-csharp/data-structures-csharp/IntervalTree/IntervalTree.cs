using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataStructures.IntervalTreeSpace
{
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
