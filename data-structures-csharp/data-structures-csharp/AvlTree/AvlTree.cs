using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures.BinarySearchTreeSpace;


namespace DataStructures.AvlTreeSpace
{
    public class AvlTree<T> : BinarySearchTree<T> where T : IComparable<T>
    {
        private void RotateLeft() 
        { 
        
        }

        private void RotateRight() 
        {
        
        }

        public bool IsBalanced(Node<T> node)
        {
            if (node == null)
            {
                return true;
            }
            int h = node.Height;
            int hl = node.Left.Height;
            int hr = node.Right.Height;
            if (!(h == (hl > hr ? hl + 1 : hr + 1)))
            {
                return false;
            }
            if (hl > hr + 1 || hr > hl + 1)
            {
                return false;
            }
            return IsBalanced(node.Left) && IsBalanced(node.Right);
        }
    }
}
