using System;

namespace DataStructures.BTreeSpace
{
    [Serializable]
    public partial class BTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        [Serializable]
        private sealed class Node<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            internal int numberOfChildren;
            internal Entry<TKey, TValue>[] Children { get; set; }

            public Node(int maximumNumberOfChildren)
            {
                this.numberOfChildren = maximumNumberOfChildren;
                Children = new Entry<TKey, TValue>[maximumNumberOfChildren];
            }
        }
    }
}
