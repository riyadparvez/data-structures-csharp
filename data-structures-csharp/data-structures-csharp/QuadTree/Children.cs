using System;

namespace DataStructures.QuadTreeSpace
{
    [Serializable]
    public class Children<T>
    {
        internal Node<T> TopLeft { get; set; }
        internal Node<T> TopRight { get; set; }
        internal Node<T> BottomLeft { get; set; }
        internal Node<T> BottomRight { get; set; }

        public Children(Node<T> topLeft, Node<T> topRight,
                        Node<T> bottomLeft, Node<T> bottomRight)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }
    }
}
