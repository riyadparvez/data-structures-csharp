using System;
using System.Diagnostics.Contracts;


namespace DataStructures.BPlusTreeSpace
{
    public partial class BPlusTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        [Serializable]
        public abstract class Node<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            protected readonly int numberOfKeys;
            protected TKey[] keys;

            public Node(int numberOfKeys)
            {
                Contract.Requires<ArgumentNullException>(numberOfKeys > 2);

                this.numberOfKeys = numberOfKeys;
                keys = new TKey[numberOfKeys];
            }

            public abstract int GetLocation(TKey key);
            // returns null if no split, otherwise returns split info
            public abstract Split Insert(TKey key, TValue value);
        }
    }
}
