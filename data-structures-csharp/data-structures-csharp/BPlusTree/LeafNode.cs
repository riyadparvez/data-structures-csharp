using System;
using System.Diagnostics.Contracts;


namespace DataStructures.BPlusTreeSpace
{
    public partial class BPlusTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        [Serializable]
        private class LeafNode<TKey, TValue> : INode<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            private TValue[] values;

            public LeafNode(int numberOfValues)
            {
                Contract.Requires<ArgumentOutOfRangeException>(numberOfValues > 0);

                values = new TValue[numberOfValues];
            }

            private int GetChildIndex(TKey key)
            {
                Contract.Requires<ArgumentNullException>(key != null);
                Contract.Ensures(Contract.Result<int>() >= 0);
                Contract.Ensures(Contract.Result<int>() <= numberOfKeys);

                // Simple linear search. Faster for small values of N or M, binary search would be faster for larger M / N
                for (int i = 0; i < numberOfKeys; i++)
                {
                    if (keys[i].CompareTo(key) >= 0)
                    {
                        return i;
                    }
                }
                return numberOfKeys;
            }

            public INode<TKey, TValue> GetChild(TKey key)
            {
                Contract.Requires<ArgumentNullException>(key != null);
                return children[GetChildIndex(key)];
            }
        }
    }
}