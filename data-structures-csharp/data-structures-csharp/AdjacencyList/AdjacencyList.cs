using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

//TODO: unweighted adjacency list and weighted adjacency list
namespace DataStructures.AdjacencyList
{
    [Serializable]
    public class AdjacencyList<T>
    {
        private readonly Dictionary<T, HashSet<T>> dict;

        public IList<T> Vertices 
        {
            get { return dict.Keys.ToList(); }
        }

        public int Count 
        {
            get { return dict.Count; }
        }

        public AdjacencyList() 
        {
            dict = new Dictionary<T, HashSet<T>>();
        }

        public AdjacencyList(int capacity)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity > 0);
            dict = new Dictionary<T, HashSet<T>>(capacity);
        }

        public void AddVertex(T vertex)
        {
            Contract.Requires<ArgumentNullException>(vertex != null);
            if(dict.ContainsKey(vertex))
            {
                return;
            }
            dict[vertex] = new HashSet<T>();
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
            return dict.ContainsKey(vertex)? dict[vertex].ToList(): new List<T>();
        } 
    }
}
