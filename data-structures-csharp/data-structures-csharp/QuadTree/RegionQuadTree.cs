using System;


namespace DataStructures.QuadTreeSpace
{
    [Serializable]
    public class RegionQuadTree
    {
        private readonly int maximumItemsPerNode;
        public int MaximumItemsPerNode
        {
            get { return maximumItemsPerNode; }
        }

        public RegionQuadTree(int maximumItems)
        {
            maximumItemsPerNode = maximumItems;
        }
    }
}
