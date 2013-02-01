using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DataStructures.ListSpace
{
    [Serializable]
    public class SortedList<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private List<T> list;

        public int Capacity
        {
            get { return list.Capacity; }
        }
        public int Count
        {
            get { return list.Count; }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Count >= 0);
            Contract.Invariant(Capacity > 0);
        }

        public SortedList() { }

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

            int count = 0;
            for (int i = 0; i < list.Count; i++, count++)
            {
                if (element.CompareTo(list[i]) < 0)
                {
                    break;
                }
            }
            Contract.Assert(list[count - 1].CompareTo(element) < 0);
            list.Insert(count, element);
        }

        public void Remove(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            list.Remove(element);
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
