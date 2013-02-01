using System;


namespace DataStructures.QuadTreeSpace
{
    [Serializable]
    public struct Rectangle
    {
        public Point TopLeftPoint { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }


        public bool IsInRectangle(Point point)
        {
            return (point.X >= TopLeftPoint.X) &&
                   (point.X <= (TopLeftPoint.X + Width)) &&
                   (point.Y >= TopLeftPoint.Y) &&
                   (point.Y >= (TopLeftPoint.Y + Height));
        }
    }
}
