using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;


namespace DataStructures.TrieSpace
{
    [Serializable]
    public class Node : IEnumerable<Node>
    {
        private HashSet<Node> children;
        private readonly char ch;
        private readonly Comparer<Node> comparer = new NodeComparare();
        private readonly string wordFromRoot;

        public virtual char Character
        {
            get { return ch; }
        }
        public virtual string WordFromRoot
        {
            get { return wordFromRoot; }
        }
        public virtual ReadOnlyCollection<Node> Children
        {
            get { return new ReadOnlyCollection<Node>(children.ToList()); }
        }
        public Comparer<Node> Comparer
        {
            get { return comparer; }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(!string.IsNullOrEmpty(wordFromRoot));
        }

        public Node()
        {
        }


        public Node(char ch, string wordFromRoot)
        {
            Contract.Requires(wordFromRoot != null);

            children = new HashSet<Node>();
            this.ch = ch;
            this.wordFromRoot = wordFromRoot + ch;
        }

        /// <summary>
        /// Check if particular node presents in children
        /// </summary>
        /// <param name="ch">character to search in children</param>
        /// <returns>Null if that children is not present otherwise node having param character</returns>
        public Node HasChild(char ch)
        {
            return children.SingleOrDefault(n => n.ch.Equals(ch));
        }


        /// <summary>
        /// Add current node to children if it doesn't exist; otherwise return that child node
        /// </summary>
        /// <param name="ch">Character to be added to children list</param>
        /// <returns>Newly added child if it doesn't exist</returns>
        public Node AddChild(char ch)
        {
            Contract.Ensures(Contract.Result<Node>() != null);

            Node n = HasChild(ch);
            if (n == null)
            {
                Node newNode = new Node(ch, wordFromRoot);
                children.Add(newNode);
                return newNode;
            }
            return n;
        }


        public void AddNullChild()
        {
            children.Add(new NullNode());
        }

        public bool HasNullChild()
        {
            return children.Contains(new NullNode());
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + ch.GetHashCode();
                hash = hash * 23 + wordFromRoot.GetHashCode();
                return hash;
            }
        }


        public override bool Equals(object obj)
        {
            Node otherNode = obj as Node;
            if (otherNode == null || this == null)
            {
                return false;
            }
            return wordFromRoot.Equals(otherNode.wordFromRoot) &&
                   ch.Equals(otherNode.ch);
        }


        public override string ToString()
        {
            return wordFromRoot;
        }


        private sealed class NodeComparare : Comparer<Node>
        {
            public override int Compare(Node x, Node y)
            {
                Contract.Requires(x != null);
                Contract.Requires(y != null);

                return x.Character.CompareTo(y.Character);
            }
        }

        public List<Node> GetChildrenList()
        {
            return new List<Node>(children);
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
