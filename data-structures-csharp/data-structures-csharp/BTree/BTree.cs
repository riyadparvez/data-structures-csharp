using System;
using System.Diagnostics.Contracts;

namespace DataStructures.BTreeSpace
{
    [Serializable]
    public partial class BTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private readonly int maximumChildrenPerNode;

        private Node<TKey, TValue> root;

        public int MaximumChildrenPerNode
        {
            get { return maximumChildrenPerNode; }
        }
        public int Height { get; private set; }
        public int Count { get; private set; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(root != null);
            Contract.Invariant(Count >= 0);
            Contract.Invariant(Height > 0);
        }

        public BTree()
        {
            root = new Node<TKey, TValue>(0);
        }

        private TValue Search(Node<TKey, TValue> node, TKey key, int ht)
        {
            Contract.Requires<ArgumentNullException>(node != null);
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentOutOfRangeException>(ht >= 0);

            Entry<TKey, TValue>[] children = node.Children;

            // leaf node i.e. external node
            if (ht == 0)
            {
                //search all the keys in leaf nodes
                for (int j = 0; j < node.maximumNumberOfChildren; j++)
                {
                    if (Equals(key, children[j].Key))
                    {
                        return children[j].Value;
                    }
                }
            }
            // internal node
            else
            {
                //search all the children
                for (int j = 0; j < node.maximumNumberOfChildren; j++)
                {
                    if (j + 1 == node.maximumNumberOfChildren || Less(key, children[j + 1].Key))
                    {
                        return Search(children[j].ChildNode, key, ht - 1);
                    }
                }
            }
            //key isn't found
            return default(TValue);
        }

        private bool Less(IComparable<TKey> k1, TKey k2)
        {
            return k1.CompareTo(k2) < 0;
        }

        private bool Equals(IComparable<TKey> k1, TKey k2)
        {
            return k1.CompareTo(k2) == 0;
        }
    }
}
