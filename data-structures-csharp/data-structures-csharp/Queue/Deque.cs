using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;


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
        public T PeekFirst
        {
            get 
            {
                if (!internalList.Any())
                {
                    return default(T);
                }
                return internalList[0]; 
            }
        }
        public T PeekLast
        {
            get 
            { 
                if(!internalList.Any())
                {
                    return default(T);
                }
                return internalList[internalList.Count - 1]; 
            }
        }

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

        public void AddFirst(T item)
        {
            Contract.Requires<ArgumentNullException>(item != null);

            internalList.Insert(0, item);
        }

        /// <summary>
        /// Adds an item to the last of deque
        /// </summary>
        /// <param name="item"></param>
        public void AddLast(T item)
        {
            Contract.Requires<ArgumentNullException>(item != null);

            internalList.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns null if list is empty</returns>
        public T RemoveFirst()
        {
            if (!internalList.Any())
            {
                return default(T);
            }
            T element = internalList[0];
            internalList.RemoveAt(0);
            return element;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns null if list is empty</returns>
        public T RemoveLast()
        {
            if (!internalList.Any())
            {
                return default(T);
            }
            T element = internalList[Count - 1];
            internalList.RemoveAt(Count - 1);
            return element;
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
