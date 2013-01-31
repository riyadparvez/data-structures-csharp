using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DataStructures.QueueSpace
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Deque<T> : IEnumerable<T>
    {
        private List<T> internalList;

        public int Count { get { return internalList.Count; } }
        public int Capacity { get { return internalList.Capacity; } }

        /// <summary>
        /// Creates a queue using default capacity
        /// </summary>
        public Deque()
        {
            internalList = new List<T>();
        }

        /// <summary>
        /// Creates a deque with default capacity
        /// </summary>
        /// <param name="capacity">Default capacity of deque</param>
        public Deque(int capacity)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity > 0);
            internalList = new List<T>(capacity);
        }

        /// <summary>
        /// Adds an item to deque
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            internalList.Add(item);
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
