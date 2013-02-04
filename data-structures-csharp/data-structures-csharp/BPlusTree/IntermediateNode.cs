using System;

namespace DataStructures.BPlusTreeSpace
{
    public partial class BPlusTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        [Serializable]
        private class IntermediateNode<TKey, TValue> : Node<TKey, TValue>
            where TKey : IComparable<TKey>
        {
            readonly Node<TKey, TValue>[] children;

            public IntermediateNode(int numberOfChildren)
                : base(numberOfChildren)
            {
                children = new Node<TKey, TValue>[numberOfChildren + 1];
            }

            public Split Insert(TKey key, TValue value)
            {
                //Early split if node is full.
                //This is not the canonical algorithm for B+ trees,
                //but it is simpler and it does break the definition
                //which might result in immature split, which might not be desired in database
                //because additional split lead to tree's height increase by 1, thus the number of disk read
                //so first search to the leaf, and split from bottom up is the correct approach.
                /*
                if (this.numberOfKeys == N)
                { // Split
                    int mid = (N + 1) / 2;
                    int sNum = this.num - mid;
                    INode sibling = new INode();
                    sibling.num = sNum;
                    System.arraycopy(this.keys, mid, sibling.keys, 0, sNum);
                    System.arraycopy(this.children, mid, sibling.children, 0, sNum + 1);

                    this.num = mid - 1;//this is important, so the middle one elevate to next depth(height), inner node's key don't repeat itself

                    // Set up the return variable
                    Split result = new Split(this.keys[mid - 1],
                                             this,
                                             sibling);

                    // Now insert in the appropriate sibling
                    if (key.compareTo(result.key) < 0)
                    {
                        this.insertNonfull(key, value);
                    }
                    else
                    {
                        sibling.insertNonfull(key, value);
                    }
                    return result;

                }
                else
                {// No split
                    this.insertNonfull(key, value);
                    return null;
                }*/
            }
        }

    }
}