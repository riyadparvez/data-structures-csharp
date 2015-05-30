using System;
using System.Diagnostics.Contracts;


namespace DataStructures.BPlusTreeSpace
{
    public partial class BPlusTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        [Serializable]
        private class IntermediateNode<TKey, TValue> : INode<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            private readonly int numberOfChildren;
            private readonly TKey[] keys;
            private readonly INode<TKey, TValue>[] children;


            public IntermediateNode(int numberOfChildren)
            {
                Contract.Requires<ArgumentOutOfRangeException>(numberOfChildren > 2);

                keys = new TKey[numberOfChildren - 1];
                children = new INode<TKey, TValue>[numberOfChildren];
            }

            //private int GetIndex(TKey key)
            //{
            //    // Simple linear search. Faster for small values of N or M
            //    for (int i = 0; i < num; i++)
            //    {
            //        if (keys[i].CompareTo(key) > 0)
            //        {
            //            return i;
            //        }
            //    }
            //    return num;
            //}

            //public INode<TKey, TValue>

            public int GetLocation(TKey key)
            {
                const int errorNum = -1;
                // Simple linear search. Faster for small values of N or M
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i].CompareTo(key) > 0)
                    {
                        return i;
                    }
                }
                return errorNum;
            }
        }
    }
}