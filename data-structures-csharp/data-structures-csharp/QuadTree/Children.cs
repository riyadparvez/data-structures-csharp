using System;
using System.Collections.Generic;

namespace DataStructures.QuadTreeSpace
{
    [Serializable]
    public class Children<T>
        where T : IComparable<T>, IEquatable<T>
    {
        internal Node<T> Parent { get; set; }
        internal Node<T> TopLeft { get; set; }
        internal Node<T> TopRight { get; set; }
        internal Node<T> BottomLeft { get; set; }
        internal Node<T> BottomRight { get; set; }

        public Children(Rectangle region, Node<T> parent)
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

            TopLeft = new Node<T>(topLeftRectangle, default(T), parent);
            TopRight = new Node<T>(topRightRectangle, default(T), parent);
            BottomLeft = new Node<T>(bottomLeftRectangle, default(T), parent);
            BottomRight = new Node<T>(bottomRightRectangle, default(T), parent);
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

        public List<Node<T>> ToList()
        {
            return new List<Node<T>> 
                        {
                            TopLeft,
                            TopRight,
                            BottomLeft,
                            BottomRight,
                        };
        }
    }
}
