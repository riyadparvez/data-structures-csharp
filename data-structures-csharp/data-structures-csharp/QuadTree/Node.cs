using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace DataStructures.QuadTreeSpace
{
    public partial class QuadTree<T>
    {
        /// <summary>
        /// Node of Heap
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        [Serializable]
        private class Node<T>
        {
            private List<T> values = new List<T>();
            private readonly Rectangle region;
            public readonly int MaximumValuesPerNode;

            public IEnumerable<T> Value
            {
                get { return values.AsReadOnly(); }
            }
            public int Count
            {
                get { return values.Count; }
            }
            public Node<T> Parent { get; set; }
            internal Rectangle Region
            {
                get { return region; }
            }
            public Children<T> Children { get; set; }

            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(values != null);
                Contract.Invariant(values.Count <= MaximumValuesPerNode);
            }

            public Node(Rectangle rectangle, Node<T> parent, int maximumValuesPerNode)
            {
                Contract.Requires<ArgumentOutOfRangeException>(maximumValuesPerNode > 0);

                Parent = parent;
                region = rectangle;
                this.MaximumValuesPerNode = maximumValuesPerNode;
                Children = new NullChildren<T>(parent);
            }

            /// <summary>
            /// Returns true if the region of the node contains the point
            /// other false
            /// </summary>
            /// <param name="p"></param>
            /// <returns></returns>
            public bool IsInRegion(Point p)
            {
                return region.IsInRectangle(p);
            }

            /// <summary>
            /// Get the child node that contains the point
            /// </summary>
            /// <param name="point"></param>
            /// <returns></returns>
            public Node<T> GetContainingChild(Point point)
            {
                return Children.GetContainingChild(point);
            }

            /// <summary>
            /// Splits current node into four quadrants
            /// </summary>
            private void SplitRegionIntoChildNodes()
            {
                Children = new Children<T>(region, this, MaximumValuesPerNode);
            }

            public void Add(Point point, T element)
            {
                Contract.Requires<ArgumentNullException>(element != null);

                if (Count == MaximumValuesPerNode)
                {
                    SplitRegionIntoChildNodes();

                }
                else
                {
                    values.Add(element);
                }
            }

            public bool Equals(Node<T> otherNode)
            {
                if (otherNode == null)
                {
                    return false;
                }
                return values.Equals(otherNode.Value);
            }

            public override bool Equals(object obj)
            {
                Node<T> otherNode = obj as Node<T>;
                if (otherNode == null)
                {
                    return false;
                }
                return values.Equals(otherNode.Value);
            }

            public override int GetHashCode()
            {
                unchecked // Overflow is fine, just wrap
                {
                    int hash = 17;
                    // Suitable nullity checks etc, of course :)
                    hash = hash * 23 + values.GetHashCode();
                    return hash;
                }
            }
        }
    }
}
