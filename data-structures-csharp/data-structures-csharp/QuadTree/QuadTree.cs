using System;
using System.Diagnostics.Contracts;


namespace DataStructures.QuadTreeSpace
{
    [Serializable]
    public class QuadTree<T>
    {
        public readonly int MaximumElementsPerNode;
        private readonly Rectangle region;
        private readonly Node<T> root;

        public Rectangle Region
        {
            get { return region; }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(MaximumElementsPerNode > 0);
        }

        public QuadTree(int maximumElementsPerNode, Rectangle region)
        {
            Contract.Requires<ArgumentOutOfRangeException>(maximumElementsPerNode > 0);

            MaximumElementsPerNode = maximumElementsPerNode;
            this.region = region;
            root = new Node<T>(region, null, maximumElementsPerNode);
        }

        public bool Add(Point point, T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            var current = root;
            while (current.IsInRegion(point))
            {
                var node = current.GetContainingChild(point);
                if (node == null)
                {
                    node.Add(point, element);
                }
                else
                {
                    current = node;
                }
            }
            return false;
        }
    }
}
