using System;
using System.Diagnostics.Contracts;


namespace DataStructures.HeapSpace
{
    /// <summary>
    /// Heap
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Heap<T> where T : IComparable<T>
    {
        public T Peek { get; private set; }
        public int Count { get; private set; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Count >= 0);
        }

        public void Add(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null, "element");

        }


        public void Remove(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null, "element");

        }


        public T GetMin(T element)
        {
            throw new NotImplementedException();
        }

    }
}
