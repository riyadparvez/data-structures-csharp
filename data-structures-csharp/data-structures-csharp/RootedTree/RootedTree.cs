using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;


namespace DataStructures.RootedTreeSpace
{
    [Serializable]
    public partial class RootedTree<T>
        where T : IComparable<T>, IEquatable<T>
    {
        private void PushLeft(Stack<Node<T>> stack, Node<T> x)
        {
            Contract.Requires<ArgumentNullException>(stack != null);

            while (x != null)
            { 
                stack.Push(x); 
                x = x.Left; 
            }
        }

        /// <summary>
        /// Merge two trees
        /// </summary>
        /// <returns>Returned merged tree node</returns>
        public Node<T> Merge(Node<T> root1, Node<T> root2)
        {
            Contract.Requires<ArgumentNullException>(root1 != null);
            Contract.Requires<ArgumentNullException>(root2 != null);

            Stack<Node<T>> stack = new Stack<Node<T>>();
            PushLeft(stack, root2);
            while (stack.Any())
            {
                Node<T> x = stack.Pop();
                x.Root = root1.Root;
                PushLeft(stack, x.Right);
            }
            return root1.Root;
        }
    }
}
