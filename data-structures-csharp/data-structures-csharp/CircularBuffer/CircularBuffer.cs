using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DataStructures.CircularBufferSpace
{
    [Serializable]
    public class CircularBuffer<T> : IEnumerable<T>
    {
        private int getIndex, addIndex;
        private T[] buffer;

        public int Count
        { get; private set; }

        public int Capacity
        {
            get;
            private set;
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

        /// <summary>
        /// Instantiate a new circular buffer.
        /// </summary>
        /// <param name="capacity">The maximum size of the buffer before it begins overwriting itself.</param>
        public CircularBuffer(int capacity)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity > 0);
            buffer = new T[capacity];
            Capacity = capacity;
            Count = 0;
            addIndex = 0;
            getIndex = 0;
        }

        /// <summary>
        /// Add an element to the circular buffer.
        /// </summary>
        public void Add(T item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            buffer[addIndex] = item;

            if (Count < Capacity)
                Count++;

            if (addIndex == getIndex)
                getIndex = getIndex < Capacity - 1 ? getIndex + 1 : 0;

            addIndex = addIndex < Capacity - 1 ? addIndex + 1 : 0;
        }

        /// <summary>
        /// Clears the circular buffer of all elements.
        /// </summary>
        public void Clear()
        {
            buffer = new T[Capacity];
            Count = 0;
            addIndex = 0;
            getIndex = 0;
        }

        /// <summary>
        /// Determines whether a sequence contains a specified element by using the default equality comparer.
        /// </summary>
        public bool Contains(T item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            return buffer.Contains(item);
        }

        /// <summary>
        /// Copy the circular buffer to an array starting at the specified index in the destination array.
        /// </summary>
        public void CopyTo(T[] array, int index = 0)
        {
            Contract.Requires<ArgumentNullException>(array != null);
            buffer.CopyTo(array, index);
        }

        /// <summary>
        /// Get the oldest element in the circular buffer.
        /// </summary>
        public T Get()
        {
            Contract.Requires<InvalidOperationException>(Count > 0);
            var temp = getIndex;
            getIndex = getIndex < Capacity - 1 ? getIndex + 1 : 0;
            return buffer[temp];
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < buffer.Length; i++)
                yield return buffer[i];
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
