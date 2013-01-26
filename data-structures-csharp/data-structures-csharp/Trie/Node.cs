using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;


namespace DataStructures.TrieSpace
{
    [Serializable]
    public class Node
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
        public virtual IEnumerable<Node> Children
        {
            get { return children.AsEnumerable(); }
        }
        public Comparer<Node> Comparer
        {
            get { return comparer; }
        }


        public Node()
        {
        }


        public Node(char ch, string wordFromRoot)
        {
            children = new HashSet<Node>();
            this.ch = ch;
            this.wordFromRoot = wordFromRoot + ch;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(!string.IsNullOrEmpty(wordFromRoot));
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
        /// <returns>Newly added child</returns>
        public Node AddChild(char ch)
        {
            Node n = HasChild(ch);
            if (n == null)
            {
                Node newNode = new Node(ch, wordFromRoot);
                children.Add(newNode);
                return newNode;
            }
            return n;
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
                return x.Character.CompareTo(y.Character);
            }
        }
    }
}
