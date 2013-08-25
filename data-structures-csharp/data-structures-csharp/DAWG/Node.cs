using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;


namespace DataStructures.DAWGSpace
{
    public partial class DirectedAcyclicWordGraph
    {
        [Serializable]
        private class Node
        {
            public List<Edge> edges = new List<Edge>();
            public readonly Guid Id { get; private set; }

            private Node(Guid id)
            {
                Id = id;
            }

            public void AddEdge(Edge e)
            {
                //Edge should have this node as one of the end nodes
                Contract.Requires((e.StartNode.Equals(this)) ||
                              (e.EndNode.Equals(this)));
                edges.Add(e);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="ch"></param>
            /// <returns>Null if there is no such edge</returns>
            public Edge FindEdge(char ch)
            {
                return edges.Where(e => e.Char.Equals(ch)).SingleOrDefault();
            }

            public override bool Equals(object obj)
            {
                Node otherNode = obj as Node;
                if (otherNode == null)
                {
                    return false;
                }
                return Id.Equals(otherNode.Id);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;
                    hash = 23 * Id.GetHashCode() + hash;
                    return hash;
                }
            }

            private static class Builder
            {
                public static Node CreateInstance()
                {
                    return new Node(Guid.NewGuid());
                }
            }
        }
    }
}
