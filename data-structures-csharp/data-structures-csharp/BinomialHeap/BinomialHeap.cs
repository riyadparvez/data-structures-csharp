using System;
using System.Diagnostics.Contracts;


namespace DataStructures.BinomialHeapSpace
{
    [Serializable]
    public class BinomialHeap<T>
        where T : IComparable<T>
    {
        private Node<T> root;

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
            
            Node<T> current1;
            Node<T> current2;

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
            Node<T> current = root;
            while(current1 != null && current2 != null)
            {
                if (current1.Value.CompareTo(current2.Value) < 0)
                {
                    current.RightSibling = current1;
                    current1 = current1.RightSibling;
                    current = current.RightSibling
                }
                else
                {
                    current.RightSibling = current2;
                    current2 = current2.RightSibling;
                    current = current.RightSibling
                }
            }
            Node<T> tail;
            if (current1 == null)
            {
                tail = current2;
            }
            else
            {
                tail = current1;
            }
            while(tail != null)
            {
                current.RightSibling = tail;
                current = current.RightSibling;
                tail = tail.RightSibling;
            }
            return root;
        }

        public Node<T> Unify(BinomialHeap<T> heap)
        {
            Contract.Requires<ArgumentNullException>(heap != null);

            Node<T> root = Merge(heap);
            if (root == null)
            {
                return null;
            }
            Node<T> prev = null;
            Node<T> current = root;
            Node<T> next = current.RightSibling;
            while (next != null)
            {
                bool needMerge = true;
                if (current.Degree != next.Degree)
                {
                    needMerge = false;
                }
                if (next.RightSibling != null && next.RightSibling.Degree == next.Degree)
                {
                    needMerge = false;
                }
                if (needMerge)
                {
                    if (current.Value.CompareTo(next.Value) <= 0)
                    {
                        current.RightSibling = next.RightSibling;
                        Link(current, next);
                    }
                    else if (prev != null)
                    {
                        prev.RightSibling = next;
                        Link(next, current);
                    }
                    else
                    {
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
            return root;
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
    }
}
