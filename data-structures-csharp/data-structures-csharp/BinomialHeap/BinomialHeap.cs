using System;
using System.Diagnostics.Contracts;


namespace DataStructures.BinomialHeapSpace
{
    [Serializable]
    public class BinomialHeap<T>
        where T : IComparable<T>
    {
        private Node<T> root;

        private Node<T> AddSubTree(Node<T> root)
        {
            Contract.Requires<ArgumentNullException>(root != null);
        }

        public Node<T> Merge(BinomialHeap<T> otherHeap)
        {
            Contract.Requires<ArgumentNullException>(otherHeap != null);

            if (root.Value.CompareTo(otherHeap.root.Value) <= 0)
            {
                return AddSubTree(otherHeap.root);
            }
            else
            {
                return otherHeap.AddSubTree(root);
            }
        }
    }
}
