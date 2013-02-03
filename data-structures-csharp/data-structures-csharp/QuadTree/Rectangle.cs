using System;
using System.Diagnostics.Contracts;


namespace DataStructures.QuadTreeSpace
{
    [Serializable]
    public struct Rectangle
    {
        private Point topLeftPoint;
        private double width;
        private double height;

        public Point TopLeftPoint
        {
            get { return topLeftPoint; }
        }
        public double Width
        {
            get { return width; }
        }
        public double Height
        {
            get { return height; }
        }

        public Rectangle(Point topLeftPoint, double width, double height)
        {
            Contract.Requires<ArgumentNullException>(width > 0);
            Contract.Requires<ArgumentNullException>(height > 0);

            this.topLeftPoint = topLeftPoint;
            this.width = width;
            this.height = height;
        }

        public bool Intersects(Rectangle rectangle)
        {
            return IsInRectangle(rectangle.TopLeftPoint) ||
                   rectangle.IsInRectangle(this.TopLeftPoint);
        }

        public bool Contains(Rectangle rectangle)
        {
            return IsInRectangle(rectangle.TopLeftPoint) &&
                   IsInRectangle(new Point(rectangle.TopLeftPoint.X + Width, rectangle.TopLeftPoint.Y + Height));
        }

        public bool IsInRectangle(Point point)
        {
            return (point.X >= TopLeftPoint.X) &&
                   (point.X <= (TopLeftPoint.X + Width)) &&
                   (point.Y >= TopLeftPoint.Y) &&
                   (point.Y >= (TopLeftPoint.Y + Height));
        }
    }
}
