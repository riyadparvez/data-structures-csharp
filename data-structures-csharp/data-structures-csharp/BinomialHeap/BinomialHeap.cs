using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace DataStructures.BinomialHeapSpace
{
    [Serializable]
    public partial class BinomialHeap<T>
        where T : IComparable<T>
    {
        private Node<T> root;

        public static BinomialHeap<T> CreateBinomialHeap()
        {
            return new BinomialHeap<T>();
        }

        public static BinomialHeap<T> CreateBinomialHeap(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            Node<T> root = new Node<T>(element, null);
            return new BinomialHeap<T>(root);
        }

        private BinomialHeap()
        {
        }

        private BinomialHeap(Node<T> root)
        {
            Contract.Requires<ArgumentNullException>(root != null);
            this.root = root;
        }

        private void Link(Node<T> root, Node<T> branch)
        {
            Contract.Requires<ArgumentNullException>(root != null);
            Contract.Requires<ArgumentNullException>(branch != null);

            branch.Parent = root;
            branch.RightSibling = root.LeftChild;
            root.LeftChild = branch;
            root.Degree++;
        }

        private Node<T> Merge(BinomialHeap<T> otherHeap)
        {
            Contract.Requires<ArgumentNullException>(otherHeap != null);

            //for traversing first heap
            Node<T> current1;
            //for traversing second heap
            Node<T> current2;

            //Make root the smaller root
            if (root.Value.CompareTo(otherHeap.root.Value) <= 0)
            {
                current1 = otherHeap.root;
                current2 = root.RightSibling;
            }
            else
            {
                current2 = root;
                root = otherHeap.root;
                current1 = otherHeap.root.RightSibling;
            }
            //pointer for currently building merged heap
            Node<T> current = root;
            while (current1 != null && current2 != null)
            {
                //merge phase like merge sort, insert smaller element first
                if (current1.Value.CompareTo(current2.Value) < 0)
                {
                    current.RightSibling = current1;
                    current1 = current1.RightSibling;
                    current = current.RightSibling;
                }
                else
                {
                    current.RightSibling = current2;
                    current2 = current2.RightSibling;
                    current = current.RightSibling;
                }
            }
            Node<T> tail;
            //check which heap is not fully visited
            if (current1 == null)
            {
                tail = current2;
            }
            else
            {
                tail = current1;
            }
            //insert remaining elements of other heap
            while (tail != null)
            {
                current.RightSibling = tail;
                current = current.RightSibling;
                tail = tail.RightSibling;
            }
            return root;
        }

        public BinomialHeap<T> Unify(BinomialHeap<T> heap)
        {
            Contract.Requires<ArgumentNullException>(heap != null);

            Node<T> root = Merge(heap);
            if (root == null)
            {
                return null;
            }
            //restore the property no two sub tree has same order
            Node<T> prev = null;
            Node<T> current = root;
            Node<T> next = current.RightSibling;
            while (next != null)
            {
                bool needMerge = true;
                if (current.Degree != next.Degree)
                {
                    //orders are not same, don't need merging
                    needMerge = false;
                }
                if (next.RightSibling != null && next.RightSibling.Degree == next.Degree)
                {
                    //orders are not same, don't need merging
                    needMerge = false;
                }
                if (needMerge)
                {
                    if (current.Value.CompareTo(next.Value) <= 0)
                    {
                        //current node is smaller than next node
                        //link the next to next node and skip next node
                        //make next node child of current node
                        current.RightSibling = next.RightSibling;
                        Link(current, next);
                    }
                    else if (prev != null)
                    {
                        //current node is greater than next node
                        //link to next node skipping current node and
                        //make it child of next node
                        prev.RightSibling = next;
                        Link(next, current);
                    }
                    else
                    {
                        //previous is null means this is root node
                        //make current node child of next node
                        //and next node is the new root
                        root = next;
                        Link(next, current);
                    }
                }
                else
                {
                    prev = current;
                    current = next;
                }
                next = current.RightSibling;
            }
            return new BinomialHeap<T>(root);
        }

        public void Insert(T element)
        {
            //first make 0 order binomial heap
            //then merge it with current heap
            BinomialHeap<T> heap = CreateBinomialHeap(element);
            heap.root.Degree = 1;
            Unify(heap);
        }

        public T GetMin()
        {
            Node<T> minNode = root;
            Node<T> current = minNode.RightSibling;
            //for heap root is the minimum, so we are traversing over the
            //roots of heap
            while (current != null)
            {
                if (current.Value.CompareTo(minNode.Value) < 0)
                {
                    minNode = current;
                }
            }
            return minNode.Value;
        }

        public static T ExtractMin(BinomialHeap<T> heap)
        {
            Contract.Requires<ArgumentNullException>(heap != null, "heap");
            T min = heap.GetMin();
            //List of children to root node, because it's getting removed
            List<Node<T>> rootList = new List<Node<T>>();
            Node<T> current = heap.root.RightSibling;
            while (current != null)
            {
                //Assign parent field of children to null
                current.Parent = null;
                rootList.Add(current);
            }
            BinomialHeap<T> newHeap = CreateBinomialHeap();
            newHeap.root = rootList[0];
            heap = heap.Unify(newHeap);
            return min;
        }
    }
}
