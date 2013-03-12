using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures.Rope
{
    [Serializable]
    public partial class Rope
    {
        [Serializable]
        private class Node
        {
            public int Weight { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }
    }
}
