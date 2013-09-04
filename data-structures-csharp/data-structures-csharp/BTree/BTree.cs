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
            Contract.Invariant(Height >= 0);
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
                for (int j = 0; j < node.numberOfChildren; j++)
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
                for (int j = 0; j < node.numberOfChildren; j++)
                {
                    if (j + 1 == node.numberOfChildren || Less(key, children[j + 1].Key))
                    {
                        return Search(children[j].ChildNode, key, ht - 1);
                    }
                }
            }
            //key isn't found
            return default(TValue);
        }

        private Node<TKey, TValue> Insert(Node<TKey, TValue> node, TKey key, TValue value, int ht)
        {
            Contract.Requires<ArgumentNullException>(node != null);
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentOutOfRangeException>(ht >= 0);

            int j;
            Entry<TKey, TValue> newEntry = new Entry<TKey, TValue>(key, value, null);

            // external node
            if (ht == 0)
            {
                //find a place to insert new element
                for (j = 0; j < node.numberOfChildren; j++)
                {
                    if (Less(key, node.Children[j].Key))
                    {
                        break;
                    }
                }
            }
            // internal node
            else
            {
                for (j = 0; j < node.numberOfChildren; j++)
                {
                    //if it's the last node or less than current node
                    if ((j + 1 == node.numberOfChildren) || Less(key, node.Children[j + 1].Key))
                    {
                        Node<TKey, TValue> newNode = Insert(node.Children[j++].ChildNode, key, value, ht - 1);
                        if (newNode == null)
                        {
                            return null;
                        }
                        newEntry.Key = newNode.Children[0].Key;
                        newEntry.ChildNode = newNode;
                        break;
                    }
                }
            }

            for (int i = node.numberOfChildren; i > j; i--)
            {
                //shift right the children to place new children
                node.Children[i] = node.Children[i - 1];
            }
            node.Children[j] = newEntry;
            node.numberOfChildren++;

            if (node.numberOfChildren < MaximumChildrenPerNode)
            {
                //No need to split the node
                return null;
            }
            else
            {
                //other hald node after splitting
                return Split(node);
            }
        }

        // split node in half, return the other half
        private Node<TKey, TValue> Split(Node<TKey, TValue> h)
        {
            Node<TKey, TValue> t = new Node<TKey, TValue>(MaximumChildrenPerNode / 2);
            h.numberOfChildren = MaximumChildrenPerNode / 2;
            for (int j = 0; j < MaximumChildrenPerNode / 2; j++)
            {
                t.Children[j] = h.Children[MaximumChildrenPerNode / 2 + j];
            }
            return t;
        }

        // insert key-value pair
        // add code to check for duplicate keys
        public void Add(TKey key, TValue value)
        {
            Node<TKey, TValue> newNode = Insert(root, key, value, Height);
            Count++;
            if (newNode == null) return;

            // need to split root
            Node<TKey, TValue> t = new Node<TKey, TValue>(2);
            t.Children[0] = new Entry<TKey, TValue>(root.Children[0].Key, default(TValue), root);
            t.Children[1] = new Entry<TKey, TValue>(newNode.Children[0].Key, default(TValue), newNode);
            root = t;
            Height++;
        }

        private bool Less(IComparable<TKey> k1, TKey k2)
        {
            return k1.CompareTo(k2) < 0;
        }

        private bool Equals(IComparable<TKey> k1, TKey k2)
        {
            return k1.CompareTo(k2) == 0;
        }

        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                {
                    return default(TValue);
                }
                return Search(root, key, Height);
            }
            set
            {
                Add(key, value);
            }
        }
    }
}
