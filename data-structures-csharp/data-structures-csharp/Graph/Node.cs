using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace CSharp.DataStructures.GraphSpace
{
    /// <summary>
    /// GraphNode of Graph
    /// </summary>
    /// <typeparam name="TValue">Data type</typeparam>
    [Serializable]
    public class GraphNode<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private TKey key;
        private TValue val;
        private NodeList<TKey, TValue> neighbors = null;
        private List<int> costs;

        public virtual TKey Key
        {
            get { return key; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                key = value;
            }

        }
        public virtual TValue Value
        {
            get { return val; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                val = value;
            }
        }

        public NodeList<TKey, TValue> Neighbors
        {
            get
            {
                return neighbors;
            }
            set
            {
                neighbors = value;
            }
        }

        public List<int> Costs
        {
            get
            {
                if (costs == null)
                    costs = new List<int>();

                return costs;
            }
        }

        public virtual int Height { get; set; }
        public virtual GraphNode<TKey, TValue> Parent { get; set; }
        public virtual GraphNode<TKey, TValue> Left { get; set; }
        public virtual GraphNode<TKey, TValue> Right { get; set; }

        protected GraphNode()
        {
            //Do Nothing
        }

        public GraphNode(TKey key, TValue val, GraphNode<TKey, TValue> parent)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentNullException>(val != null);
            Contract.Requires(parent != null);

            this.key = key;
            this.val = val;
            Parent = parent;
            Left = null;
            Right = null;

        }
        public GraphNode(TKey key, TValue val, NodeList<TKey, TValue> neighbors)
        {
            this.key = key;
            this.val = val;
            this.neighbors = neighbors;
        }

        public virtual bool Equals(GraphNode<TKey, TValue> otherNode)
        {
            if (otherNode == null || otherNode.Parent == null)        //Only NullNode have Parent to be null
            {
                return false;
            }
            return val.Equals(otherNode.Value);
        }

        public override bool Equals(object obj)
        {
            GraphNode<TKey, TValue> otherNode = obj as GraphNode<TKey, TValue>;
            if (otherNode == null || otherNode.Parent == null)        //Only NullNode have Parent to be null
            {
                return false;
            }
            return val.Equals(otherNode.Value);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + key.GetHashCode();
                return hash;
            }
        }
    }
}
