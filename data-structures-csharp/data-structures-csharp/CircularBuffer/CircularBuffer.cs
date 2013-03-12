using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DataStructures.CircularBufferSpace
{
    [Serializable]
    public class CircularBuffer<T> : IEnumerable<T>
    {
        private List<T> internalList;

        public int Count { get; private set; }
        public int Capacity
        {
            get { return internalList.Capacity; }
        }
        public bool IsFull 
        {
            get { return (Count == Capacity); }
        }
        public bool IsEmpty
        {
            get { return (Count == 0); }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Capacity > 0);
            Contract.Invariant(Count > 0);
            Contract.Invariant(Count <= Capacity);
        }

        public CircularBuffer(int capacity)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity > 0);
            internalList = new List<T>(capacity);
        }


    }
}
