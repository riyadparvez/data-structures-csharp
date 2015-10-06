using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CSharp.DataStructures.GraphSpace
{
    /// <summary>
    /// Graph
    /// </summary>
    /// <typeparam name="TKey">Type must inherit IComparable</typeparam>
    [Serializable]
    public partial class Graph<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        private NodeList<TKey, TValue> nodeSet;

        public Graph() : this(null) { }
        public Graph(NodeList<TKey, TValue> nodeSet)
        {
            if (nodeSet == null)
                this.nodeSet = new NodeList<TKey, TValue>();
            else
                this.nodeSet = nodeSet;
        }

        /// <summary>
        /// Find node from graph
        /// </summary>
        /// <param name="key">element to be searched</param>
        /// <returns>Returns that element, otherwise default of that type</returns>
        protected GraphNode<TKey, TValue> FindNode(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            foreach (var gnode in nodeSet)
            {
                GraphNode<TKey, TValue> node = gnode.Neighbors.FindByKey(key);
                if (node != null)
                {
                    Contract.Assert(node != null);
                    return node;                    
                }
            }

            return null;
        }

        /// <summary>
        /// Find element in graph, returns false if not found
        /// </summary>
        /// <param name="key">Element to be found</param>
        /// <returns></returns>
        public virtual bool Find(TKey key, out TValue value)
        {
            Contract.Requires<ArgumentNullException>(key != null, "Graph can't have null values");
            value = default(TValue);

            GraphNode<TKey, TValue> node = FindNode(key);
            if (node != null)
            {
                value = node.Value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void Add(GraphNode<TKey, TValue> node)
        {
            // adds a node to the graph
            nodeSet.Add(node);
        }

        public virtual void Add(TKey key, TValue value)
        {
            // adds a node to the graph
            nodeSet.Add(new GraphNode<TKey, TValue>(key, value, NullNode<TKey, TValue>.Instance));
        }

        public void AddDirectedEdge(GraphNode<TKey, TValue> from, GraphNode<TKey, TValue> to, int cost)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);
        }

        public void AddUndirectedEdge(GraphNode<TKey, TValue> from, GraphNode<TKey, TValue> to, int cost)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);

            to.Neighbors.Add(from);
            to.Costs.Add(cost);
        }

        public bool Contains(TValue value)
        {
            return nodeSet.FindByValue(value) != null;
        }

        public bool Remove(TValue value)
        {
            // first remove the node from the nodeset
            GraphNode<TKey, TValue> nodeToRemove = nodeSet.FindByValue(value);
            if (nodeToRemove == null)
                // node wasn't found
                return false;

            // otherwise, the node was found
            nodeSet.Remove(nodeToRemove);

            // enumerate through each node in the nodeSet, removing edges to this node
            foreach (var node in nodeSet)
            {
                int index = node.Neighbors.IndexOf(nodeToRemove);
                if (index != -1)
                {
                    // remove the reference to the node and associated cost
                    node.Neighbors.RemoveAt(index);
                    node.Costs.RemoveAt(index);
                }
            }

            return true;
        }

        public NodeList<TKey, TValue> Nodes
        {
            get
            {
                return nodeSet;
            }
        }

        public int Count
        {
            get { return nodeSet.Count; }
        }

        public TValue this[TKey key]
        {
            get
            {
                Contract.Requires<ArgumentNullException>(key != null);
                TValue value;
                Find(key, out value);
                return value;
            }
            set
            {
                Add(key, value);
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var node in nodeSet)
            {
                yield return new KeyValuePair<TKey, TValue>(node.Key, node.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

}
