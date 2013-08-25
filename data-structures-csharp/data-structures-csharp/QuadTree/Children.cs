using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace DataStructures.QuadTreeSpace
{
    public partial class QuadTree<T>
    {
        [Serializable]
        private class Children<T> : IEnumerable<Node<T>>
        {
            public Node<T> Parent { get; set; }
            public Node<T> TopLeft { get; set; }
            public Node<T> TopRight { get; set; }
            public Node<T> BottomLeft { get; set; }
            public Node<T> BottomRight { get; set; }

            public Children(Rectangle region, Node<T> parent, int maximumValuesPerNode)
            {
                var topLeftTopPoint = region.TopLeftPoint;
                var topRightTopPoint = new Point(region.TopLeftPoint.X + region.Width / 2, region.TopLeftPoint.Y);
                var bottomLeftTopPoint = new Point(region.TopLeftPoint.X, region.TopLeftPoint.Y + region.Height / 2);
                var bottomRightTopPoint = new Point(region.TopLeftPoint.X + region.Width / 2, region.TopLeftPoint.Y + region.Height / 2);

                var topLeftRectangle = new Rectangle(topLeftTopPoint, region.Width / 2, region.Height / 2);
                var topRightRectangle = new Rectangle(topRightTopPoint, region.Width / 2, region.Height / 2);
                var bottomLeftRectangle = new Rectangle(bottomLeftTopPoint, region.Width / 2, region.Height / 2);
                var bottomRightRectangle = new Rectangle(bottomRightTopPoint, region.Width / 2, region.Height / 2);

                Parent = parent;

                TopLeft = new Node<T>(topLeftRectangle, parent, maximumValuesPerNode);
                TopRight = new Node<T>(topRightRectangle, parent, maximumValuesPerNode);
                BottomLeft = new Node<T>(bottomLeftRectangle, parent, maximumValuesPerNode);
                BottomRight = new Node<T>(bottomRightRectangle, parent, maximumValuesPerNode);
            }

            public Children(Node<T> parent, Node<T> topLeft, Node<T> topRight,
                            Node<T> bottomLeft, Node<T> bottomRight)
            {
                Parent = parent;
                TopLeft = topLeft;
                TopRight = topRight;
                BottomLeft = bottomLeft;
                BottomRight = bottomRight;
            }

            /// <summary>
            /// Find the child containing point, otherwise null
            /// </summary>
            /// <param name="point"></param>
            /// <returns>Null if no children contains the point</returns>
            public virtual Node<T> GetContainingChild(Point point)
            {
                List<Node<T>> childrenList = ToList();

                foreach (var child in childrenList)
                {
                    if (child.IsInRegion(point))
                    {
                        return child;
                    }
                }
                return null;
            }

            /// <summary>
            /// Returns all the children as list of nodes
            /// </summary>
            /// <returns></returns>
            public List<Node<T>> ToList()
            {
                Contract.Ensures(Contract.Result<List<Node<T>>>() != null);

                return new List<Node<T>>
                            {
                                TopLeft,
                                TopRight,
                                BottomLeft,
                                BottomRight,
                            };
            }

            public IEnumerator<Node<T>> GetEnumerator()
            {
                return this.ToList().GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
