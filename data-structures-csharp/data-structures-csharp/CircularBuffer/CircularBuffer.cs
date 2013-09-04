using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DataStructures.CircularBufferSpace
{
    [Serializable]
    public class CircularBuffer<T> : IEnumerable<T>, ICollection<T>
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
            Contract.Invariant(Count >= 0);
            Contract.Invariant(Count <= Capacity);
        }

        public CircularBuffer() 
        {
            internalList = new List<T>();
        }

        public CircularBuffer(int capacity)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity > 0);

            internalList = new List<T>(capacity);
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            internalList.Clear();
        }

        public bool Contains(T item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            return internalList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(T item)
        {
            return internalList.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
