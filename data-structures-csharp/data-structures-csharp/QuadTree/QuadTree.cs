using System;
using System.Diagnostics.Contracts;


namespace DataStructures.QuadTreeSpace
{
    [Serializable]
    public class QuadTree<T>
        where T : IComparable<T>, IEquatable<T>
    {
        public readonly int MaximumElementsPerNode;
        private readonly Rectangle region;
        private readonly Node<T> root;

        public Rectangle Region
        {
            get { return region; }
        }


        public QuadTree(int maximumElementsPerNode, Rectangle region)
        {
            Contract.Requires<ArgumentOutOfRangeException>(maximumElementsPerNode > 0);

            MaximumElementsPerNode = maximumElementsPerNode;
            this.region = region;
            root = new Node<T>(region, default(T), null);
        }
    }
}
