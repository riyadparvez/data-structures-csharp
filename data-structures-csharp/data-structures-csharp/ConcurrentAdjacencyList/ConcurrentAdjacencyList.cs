using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace DataStructures.ConcurrentAdjacencyList
{
    //TODO: incomplete, requires inspection
    [Serializable]
    public class ConcurrentAdjacencyList<T>
    {
        private readonly ConcurrentDictionary<T, List<T>> dict = new ConcurrentDictionary<T, List<T>>();

        public IList<T> Vertices 
        {
            get { return dict.Keys.ToList(); }
        }

        public int Count 
        {
            get { return dict.Count; }
        }

        public void AddVertex(T vertex)
        {
            Contract.Requires<ArgumentNullException>(vertex != null);
            dict[vertex] = new List<T>();
        }

        public void AddEdge(T vertex1, T vertex2) 
        {
            Contract.Requires<ArgumentNullException>(vertex1 != null);
            Contract.Requires<ArgumentNullException>(vertex2 != null);
            dict[vertex1].Add(vertex2);
            dict[vertex2].Add(vertex1);
        }

        public bool IsNeighbourOf(T vertex, T neighbour) 
        {
            Contract.Requires<ArgumentNullException>(vertex != null);
            Contract.Requires<ArgumentNullException>(neighbour != null);
            return dict.ContainsKey(vertex) && dict[vertex].Contains(neighbour);
        }

        public IList<T> GetNeighbours(T vertex) 
        {
            Contract.Requires<ArgumentNullException>(vertex != null);
            return dict.ContainsKey(vertex)? dict[vertex]: new List<T>();
        }
    }
}
