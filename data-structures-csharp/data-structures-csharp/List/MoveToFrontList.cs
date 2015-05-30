using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DataStructures.MoveToFrontListSpace
{
    /// <summary>
    /// Moves most recently accessed element to the root
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class MoveToFrontList<T> : IEnumerable<T>, ICollection<T>
    {
        private List<T> list;
        private readonly object syncRoot = new object();

        bool ICollection<T>.Remove(T item)
        {
            var removedItem = ((ICollection<T>) list).Remove(item);
            return removedItem;
        }

        public int Count 
        { 
            get { return list.Count; } 
        }

        public bool IsReadOnly { get; private set; }

        public int Capacity
        {
            get { return list.Capacity; }
        }
        public object SyncRoot
        {
            get { return syncRoot; }
        }
        public bool IsSynchronized 
        { 
            get { return false; } 
        }
        
        public MoveToFrontList()
        {
            list = new List<T>();
        }

        public MoveToFrontList(int capacity) 
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity > 0);

            list = new List<T>(capacity);
        }

        /// <summary>
        /// Retrieves specific node and updates that node position
        /// </summary>
        /// <param name="element"></param>
        /// <returns>null if element isn't in the list</returns>
        public T Get(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
        
            var node = list.FirstOrDefault(e => e.Equals(element));
            if (node == null)
            {
                return default(T);
            }
            list.Remove(node);
            list.Insert(0, node);
            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public void Add(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            
            list.Add(element);
        }

        public void Clear()
        {
            list = new List<T>();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Contract.Requires<ArgumentNullException>(array != null, "array");
            Contract.Requires<ArgumentOutOfRangeException>(arrayIndex >= 0, "index");
            Contract.Requires<ArgumentException>(arrayIndex <= Count);

            var i = arrayIndex;
            foreach (var element in list)
            {
                array.SetValue(element, i++);
            }
        }

        /// <summary>
        /// Removes element from list, throws exception
        /// </summary>
        /// <param name="element"></param>
        public void Remove(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            
            list.Remove(element);
        }

        public void CopyTo(Array array, int index)
        {
            Contract.Requires<ArgumentNullException>(array != null, "array");
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0, "index");
            Contract.Requires<ArgumentException>(index <= Count);

            int i = index;
            foreach (var element in list)
            {
                array.SetValue(element, i++);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public static class MoveToFrontListExtension
    {
        public static T[] CopyToArray<T>(this MoveToFrontList<T> self)
        {
            var returnedArray = new T[self.Count];
            var count = 0;
            foreach (var element in self)
            {
                returnedArray[count++] = element;
            }
            return returnedArray;
        }
    }
}
