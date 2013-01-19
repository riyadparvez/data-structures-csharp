using System;

namespace DataStructures.DAWGSpace
{
    /// <summary>
    /// Every edge corrosponds to different edge between two nodes of graph
    /// </summary>
    [Serializable]
    public struct Edge
    {
        private readonly char ch;

        public char Char { get { return ch; } }
        public Node StartNode { get; private set; }
        public Node EndNode { get; private set; }

        public Edge(char ch, Node startNode, Node endNode)
        {
            this.ch = ch;
            StartNode = startNode;
            EndNode = endNode;
        }

        public override bool Equals(object obj)
        {
            Edge otherEdge = (Edge)obj;
            return ch.Equals(otherEdge.ch) &&
                    StartNode.Equals(otherEdge.StartNode) &&
                    EndNode.Equals(otherEdge.EndNode);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = 23 * ch.GetHashCode() + hash;
                hash = 23 * StartNode.GetHashCode() + hash;
                hash = 23 * EndNode.GetHashCode() + hash;
                return hash;
            }
        }
    }
}
