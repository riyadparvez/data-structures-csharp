using System;
using System.Collections.Generic;

namespace DataStructures.QueueSpace
{
    [Serializable]
    public class Deque<T>
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
            internalList = new List<T>(capacity);
        }
    }
}
