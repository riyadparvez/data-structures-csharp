using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace CSharp.DataStructures.GraphSpace
{
    public partial class Graph<TKey, TValue>
    {
        [Serializable]
        protected sealed class NullNode<TKey, TValue> : GraphNode<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            private TKey key;
            private TValue val;

            private static readonly NullNode<TKey, TValue> instance = new NullNode<TKey, TValue>();

            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static NullNode()
            {
                //Do Nothing
            }

            private NullNode()
            {
                //Do Nothing
            }

            public static NullNode<TKey, TValue> Instance
            {
                get
                {
                    return instance;
                }
            }

            public override TKey Key
            {
                get { return key; }

            }
            public override TValue Value
            {
                get { return val; }
            }
            public override int Height { get { return 0; } }
            public override GraphNode<TKey, TValue> Parent { get { return null; } }
            public override GraphNode<TKey, TValue> Left { get { return null; } }
            public override GraphNode<TKey, TValue> Right { get { return null; } }

            public override bool Equals(GraphNode<TKey, TValue> otherNode)
            {
                if (otherNode == null)
                {
                    return false;
                }
                if (otherNode.Parent == null)        //Only NullNode have Parent to be null
                {
                    return true;
                }
                return false;
            }

            public override bool Equals(object obj)
            {
                GraphNode<TKey, TValue> otherNode = obj as GraphNode<TKey, TValue>;
                return this.Equals(otherNode);
            }

        }

    }
}
