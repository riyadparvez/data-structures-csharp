using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataStructures.IntervalTreeSpace
{
    [Serializable]
    public class Node
    {
        private List<Interval> intervals;

        public int X;
        public IEnumerable<Interval> Intervals
        {
            get { return intervals.AsEnumerable(); }
        }
        public Node Left { get; set; }
        public Node Right { get; set; }


        public Node(int x) 
        {
            this.X = x;
            intervals = new List<Interval>();
        }

        public void AddInterval() 
        {
 
        }
    }
}
