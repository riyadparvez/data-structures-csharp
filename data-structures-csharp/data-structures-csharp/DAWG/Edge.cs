using System;
using System.Diagnostics.Contracts;


namespace DataStructures.DAWGSpace
{
    public partial class DirectedAcyclicWordGraph
    {
        /// <summary>
        /// Every edge corresponds to different edge between two nodes of graph
        /// </summary>
        [Serializable]
        private class Edge
        {
            private readonly char ch;

            public char Char { get { return ch; } }
            public Node StartNode { get; private set; }
            public Node EndNode { get; private set; }

            public Edge(char character, Node startNode, Node endNode)
            {
                Contract.Requires(startNode != null);
                Contract.Requires(endNode != null);

                ch = character;
                StartNode = startNode;
                EndNode = endNode;
            }

            public override bool Equals(object obj)
            {
                Edge otherEdge = obj as Edge;
                if (otherEdge == null)
                {
                    return false;
                }
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
}
