using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DataStructures.ListSpace
{
    /// <summary>
    /// Use insertion sort for every element added
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class SortedList<T> : IEnumerable<T>, ICollection<T>
        where T : IComparable<T>
    {
        private List<T> list;

        public int Capacity
        {
            get { return list.Capacity; }
        }

        bool ICollection<T>.Remove(T item)
        {
            var success = ((ICollection<T>)list).Remove(item);
            return success;
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly { get; private set; }
        public bool IsSynchronized { get { return false; } }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Count >= 0);
            Contract.Invariant(Capacity >= 0);
        }

        public SortedList() 
        {
            list = new List<T>();
        }

        public SortedList(int capacity)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity > 0);

            list = new List<T>(capacity);
        }

        public SortedList(IEnumerable<T> list)
        {
            Contract.Requires<ArgumentNullException>(list != null);

            this.list = new List<T>(list);
            this.list.Sort();
        }

        public void Add(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            var count = 0;
            //search for the previous element only when there are other elements in the list
            if(list.Count>0){
                for (var i = 0; i < list.Count; i++, count++)
                {
                    if (element.CompareTo(list[i]) <= 0)
                    {
                        count = i;
                        break;
                    }
                }
                if(count>0)
                    Contract.Assert(list[count-1].CompareTo(element) <= 0);
            }
            list.Insert(count, element);
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
            Contract.Requires<ArgumentNullException>(array != null, "array is null");
            Contract.Requires<ArgumentOutOfRangeException>(arrayIndex >= 0, "arrayIndex less than 0");

            for (var i = arrayIndex; i < list.Count; i++)
            {
                array[i] = list[i];
            }
        }

        public void Remove(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            list.Remove(element);
        }

        public void CopyTo(Array array, int index)
        {
            Contract.Requires<ArgumentNullException>(array != null, "array is null");
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0, "arrayIndex less than 0");
            Contract.Requires<ArgumentException>(array.Length < Count, "array not big enough");

            int i = index;
            foreach (T element in list)
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
}
