using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using DataStructures.Utils;


namespace DataStructures.CompressedTrieSpace
{
    public partial class CompressedTrie
    {
        /// <summary>
        /// Node of compressed trie
        /// </summary>
        [Serializable]
        private class Node
        {
            private readonly HashSet<Node> children = new HashSet<Node>();
            private string stringFragment;
            private readonly Comparer<Node> comparer = new NodeComparare();
            private readonly string wordFromRoot;

            public virtual string StringFragment
            {
                get { return stringFragment; }
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
                Contract.Invariant(children != null);
            }

            public Node(string wordFromRoot)
            {
                Contract.Requires<ArgumentNullException>(wordFromRoot != null);

                this.wordFromRoot = wordFromRoot;
                this.stringFragment = string.Empty;
            }


            public Node(string stringFragment, string wordFromRoot)
            {
                Contract.Requires(!string.IsNullOrEmpty(stringFragment));
                Contract.Requires<ArgumentNullException>(wordFromRoot != null);

                children = new HashSet<Node>();
                this.stringFragment = stringFragment;
                this.wordFromRoot = wordFromRoot + stringFragment;
            }

            /// <summary>
            /// Check if particular node presents in children
            /// </summary>
            /// <param name="ch">character to search in children</param>
            /// <returns>Null if that children is not present otherwise node having param character</returns>
            public Node GetChild(string str)
            {
                Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(str));
                int n = children.Select(child => child.StringFragment.CommonPrefixLength(str)).Max();
                if (n > 0)
                {
                    return children.Where(child => child.StringFragment.CommonPrefixLength(str) == n).Single();
                }
                return null;
            }

            /// <summary>
            /// Add current node to children if it doesn't exist; otherwise return that child node
            /// </summary>
            /// <param name="str">Character to be added to children list</param>
            /// <returns>Newly added child</returns>
            public Node AddChild(string str)
            {
                Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(str));
                Contract.Ensures(Contract.Result<Node>() != null);

                int n = stringFragment.CommonPrefixLength(str);
                if (n == str.Length)
                {
                    //string is already in node's string, just add a null node to denote
                    children.Add(new NullNode(wordFromRoot));
                }
                else
                {
                    //current node will split into twp branches, node1 and node2
                    //node1 will inherit all the children of current node
                    //node2 will be the new node for newly added string 
                    //and contain a null node to indicate end of the word
                    string rootWord = wordFromRoot.Substring(0, wordFromRoot.LastIndexOf(stringFragment));
                    string prefix = stringFragment.CommonPrefix(str);
                    string node1String = stringFragment.Substring(n);
                    string node2String = str.Substring(n);
                    Node node1 = new Node(node1String, rootWord);
                    Node node2 = new Node(node2String, rootWord);

                    node1.children = new HashSet<Node>(children);
                    node1.children.Add(new NullNode(wordFromRoot));
                    node2.children.Add(new NullNode(str));
                    //clear children of current node
                    //add newly created two children
                    this.stringFragment = prefix;
                    this.children.Clear();
                    this.children.Add(node1);
                    this.children.Add(node2);
                }
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="other">Current remainder string fragment</param>
            /// <returns></returns>
            public bool MoveToChildren(string other)
            {
                int count = 0;

                for (int i = 0; i < stringFragment.Length && i < other.Length; i++, count++)
                {
                    if (stringFragment[i] != other[i])
                    {
                        break;
                    }
                }
                return (count == stringFragment.Length);
            }

            public void AddNullNode()
            {
                this.children.Add(new NullNode(wordFromRoot));
            }

            public override int GetHashCode()
            {
                unchecked // Overflow is fine, just wrap
                {
                    int hash = 17;
                    // Suitable nullity checks etc, of course :)
                    hash = hash * 23 + stringFragment.GetHashCode();
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
                       stringFragment.Equals(otherNode.stringFragment);
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

                    return x.StringFragment.CompareTo(y.StringFragment);
                }
            }
        }
    }
}
