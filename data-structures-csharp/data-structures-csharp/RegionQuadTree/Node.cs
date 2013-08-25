using System;
using System.Diagnostics.Contracts;


namespace DataStructures.RegionQuadTreeSpace
{
    public partial class RegionQuadTree<T>
    {
        /// <summary>
        /// Node of Heap
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        [Serializable]
        private class Node<T>
            where T : IComparable<T>, IEquatable<T>
        {
            private T val;
            private readonly Rectangle region;

            public T Value
            {
                get { return val; }
                internal set
                {
                    Contract.Requires<ArgumentNullException>(value != null);
                    val = value;
                }
            }
            public Node<T> Parent { get; set; }
            public Rectangle Region
            {
                get { return region; }
            }
            internal Children<T> Children { get; set; }

            public Node(Rectangle rectangle, T val, Node<T> parent)
            {
                this.val = val;
                Parent = parent;
                region = rectangle;
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
                Children = new Children<T>(region, this);

            }

            /// <summary>
            /// Sets data
            /// </summary>
            /// <param name="p"></param>
            /// <param name="data"></param>
            /// <returns>The node containing new data</returns>
            public Node<T> SetData(Point point, T data)
            {
                if (val.Equals(default(T)) || val.Equals(data))
                {
                    val = data;
                    return this;
                }
                else
                {
                    SplitRegionIntoChildNodes();
                    Node<T> newChild = null;
                    foreach (var child in Children)
                    {
                        if (child.IsInRegion(point))
                        {
                            //
                            newChild = child;
                            child.SetData(point, data);
                        }
                        else
                        {
                            child.SetData(point, val);
                        }
                    }
                    return newChild;
                }
            }

            public bool Equals(Node<T> otherNode)
            {
                if (otherNode == null)
                {
                    return false;
                }
                return val.Equals(otherNode.Value);
            }

            public override bool Equals(object obj)
            {
                Node<T> otherNode = obj as Node<T>;
                if (otherNode == null)
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
                    hash = hash * 23 + val.GetHashCode();
                    return hash;
                }
            }
        }
    }
}