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
            private readonly TValue[] _values;
            private readonly int _numberOfValues;

            public LeafNode(int numberOfValues)
            {
                Contract.Requires<ArgumentOutOfRangeException>(numberOfValues > 0);
                _numberOfValues = numberOfValues;
                _values = new TValue[numberOfValues];
            }

            private int GetChildIndex(TKey key)
            {
                Contract.Requires<ArgumentNullException>(key != null);
                Contract.Ensures(Contract.Result<int>() >= 0);
                Contract.Ensures(Contract.Result<int>() <= _numberOfValues);

                // Simple linear search. Faster for small values of N or M, binary search would be faster for larger M / N
                for (int i = 0; i < _numberOfValues; i++)
                {
                    if (_values[i].Equals(key))
                    {
                        return i;
                    }
                }
                return _numberOfValues;
            }

            public TValue GetChild(TKey key)
            {
                Contract.Requires<ArgumentNullException>(key != null);
                return _values[GetChildIndex(key)];
            }

            public int GetLocation(TKey key)
            {
                throw new NotImplementedException();
            }
        }
    }
}