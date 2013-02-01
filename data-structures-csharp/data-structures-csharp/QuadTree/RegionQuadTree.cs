using System;


namespace DataStructures.QuadTreeSpace
{
    [Serializable]
    public class RegionQuadTree<T>
        where T : IComparable<T>, IEquatable<T>
    {
        private readonly Rectangle region;
        private readonly Node<T> root;

        public Rectangle Region
        {
            get { return region; }
        }

        public RegionQuadTree(Rectangle region)
        {
            this.region = region;
            root = new Node<T>(region, default(T), null);
        }



    }
}
