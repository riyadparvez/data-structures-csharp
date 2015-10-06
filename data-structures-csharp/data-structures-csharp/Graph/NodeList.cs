using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataStructures.GraphSpace
{
    /// <summary>
    /// NodeList
    /// </summary>
    [Serializable]
    public class NodeList<TKey, TValue> : Collection<GraphNode<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        public NodeList() : base() { }

        public NodeList(int initialSize = 0)
        {
            // Add the specified number of items
            for (int i = 0; i < initialSize; i++)
                base.Items.Add(null);
        }

        public GraphNode<TKey, TValue> FindByValue(TValue value)
        {
            // search the list for the value
            foreach (var node in base.Items)
                if (node.Value.Equals(value))
                    return node;

            // if we reached here, we didn't find a matching node
            return null;
        }

        public GraphNode<TKey, TValue> FindByKey(TKey key)
        {
            // search the list for the value
            foreach (var node in base.Items)
                if (node.Key.Equals(key))
                    return node;

            // if we reached here, we didn't find a matching node
            return null;
        }

    }
}
