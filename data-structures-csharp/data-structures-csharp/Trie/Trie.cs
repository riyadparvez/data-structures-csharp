using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace DataStructures.TrieSpace
{
    [Serializable]
    public partial class Trie : IEnumerable<string>
    {
        private Node root;

        public int Count { get; private set; }
        
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(root != null);
            Contract.Invariant(Count >= 0);
        }

        public Trie()
        {
            root = new Node(string.Empty, null);
        }


        /// <summary>
        /// Check if an word exists
        /// </summary>
        /// <param name="word">word to search for</param>
        /// <returns>True if that word exists</returns>
        [Pure]
        public bool Exists(string word)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(word),
                            "Trie doesn't include empty string or null values");

            Node current = root;
            foreach (char ch in word)
            {
                Node childNode = current.HasChild(ch);
                if (childNode == null)
                {
                    //Element doesn't exist
                    return false;
                }
                current = childNode;
            }
            return current.HasNullChild();
        }


        /// <summary>
        /// Adds word to the end of specified node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="word"></param>
        private void Add(Node node, string word)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(word),
                            "Trie doesn't include empty string or null values");
            Contract.Requires<ArgumentNullException>(node != null);
            Contract.Ensures(Count == Contract.OldValue<int>(Count) + 1);

            foreach (char ch in word)
            {
                Node childNode = node.AddChild(ch);
                node = childNode;
            }
            node.AddNullChild();
            Count++;
        }

        /// <summary>
        /// Adds a word if it doesn't exist
        /// </summary>
        /// <param name="word"></param>
        public void Add(string word)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(word),
                            "Trie doesn't include empty string or null values");

            if (Exists(word))
            {
                return;
            }

            Node current = root;
            for (int i = 0; i < word.Length; i++)
            {
                Node childNode = current.HasChild(word[i]);
                if (childNode == null)
                {
                    Add(current, word.Substring(i));
                    return;
                }
                current = childNode;
            }
        }

        [Pure]
        private bool HasOneChild(Node node)
        {
            Contract.Requires<ArgumentNullException>(node != null);
            return (node.Children.Count == 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="word"></param>
        /// <returns>False if word isn't already in trie</returns>
        public bool Remove(string word)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(word),
                            "Trie doesn't include empty string or null values");

            if (!Exists(word))
            {
                return false;
            }
            var current = root;
            foreach (var ch in word)
            {
                Node childNode = current.HasChild(ch);
                current = childNode;
            }
            int i = word.Length - 1;
            current.RemoveNullChild();
            while (HasOneChild(current) && i >= 0)
            {
                current.RemoveChild(word[i]);
                current = current.Parent;
                i--;
            }
            Count--;
            return true;
        }

        [Pure]
        private IList<string> AllStrings(Node node)
        {
            Contract.Requires<ArgumentNullException>(node != null);
            Contract.Ensures(Contract.Result<IList<string>>() != null);

            if (node is NullNode)
            {
                return new List<string> { node.WordFromRoot };
            }
            var words = new List<string>();
            foreach (var child in node.Children)
            {
                words.AddRange(AllStrings(child));
            }
            return words;
        }

        [Pure]
        public IList<string> GetStringsContainingPrefix(string prefix)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(prefix));
            Contract.Ensures(Contract.Result<IList<string>>() != null);

            var current = root;
            foreach (char ch in prefix)
            {
                Node childNode = current.HasChild(ch);
                if (childNode == null)
                {
                    //No words with current prefix
                    return new List<string>();
                }
                current = childNode;
            }

            return AllStrings(current);
        }

        private IEnumerator<string> Enumerate(Node node)
        {
            Contract.Requires<ArgumentNullException>(node != null);

            if (node is NullNode)
            {
                yield return node.WordFromRoot;
                yield break;
            }
            var words = new List<string>();
            foreach (var child in node.Children)
            {
                var enumerator = Enumerate(child);
                do
                {
                    yield return enumerator.Current;
                } while (enumerator.MoveNext());
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return Enumerate(root);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
