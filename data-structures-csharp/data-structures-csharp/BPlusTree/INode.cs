using System;


namespace DataStructures.BPlusTreeSpace
{
    public partial class BPlusTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public interface INode<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            int GetLocation(TKey key);
            // returns null if no split, otherwise returns split info
            //Split Insert(TKey key, TValue value);
        }
    }
}
