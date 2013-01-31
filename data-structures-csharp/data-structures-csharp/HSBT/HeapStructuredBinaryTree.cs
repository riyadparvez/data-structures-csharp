using System;
using System.Diagnostics.Contracts;

namespace DataStructures.HsbtSpace
{
    [Serializable]
    public class HeapStructuredBinaryTree<T>
        where T : IComparable<T>, IEquatable<T>
    {
        public T Peek { get; set; }
        public int Count { get; private set; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Count >= 0);
        }

        public void Add(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
        }

        public T Find(T element)
        {

        }
    }
}
