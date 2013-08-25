using System;
using System.Diagnostics.Contracts;


namespace DataStructures.RegionQuadTreeSpace
{
    [Serializable]
    public partial class RegionQuadTree<T>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetData(Point point, T data)
        {
            Contract.Requires<ArgumentNullException>(data != null);

            var current = root;
            while (true)
            {
                if (!current.IsInRegion(point))
                {
                    return false;
                }
                else
                {
                    Node<T> node = current.GetContainingChild(point);
                    if (node != null)
                    {
                        current = node;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            current.SetData(point, data);
            return true;
        }

    }
}
