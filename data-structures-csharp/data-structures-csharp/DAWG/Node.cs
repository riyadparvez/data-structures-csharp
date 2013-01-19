using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace DataStructures.DAWGSpace
{
    [Serializable]
    public class Node
    {
        public List<Edge> edges = new List<Edge>();
        public Guid Id { get; private set; }

        private Node(Guid id)
        {
        }

        public void AddEdge(Edge e)
        {
            Debug.Assert((e.StartNode.Equals(this)) ||
                          (e.EndNode.Equals(this)));
            edges.Add(e);
        }

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

        public static class Builder
        {
            public static Node CreateInstance()
            {
                return new Node(Guid.NewGuid());
            }
        }
    }
}
