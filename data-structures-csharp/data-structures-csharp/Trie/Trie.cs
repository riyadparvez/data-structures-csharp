using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace DataStructures.TrieSpace
{
    [Serializable]
    public class Trie : IEnumerable<string>
    {
        public Node Root { get; private set; }


        public Trie()
        {
            Root = new NullNode(string.Empty);
        }


        /// <summary>
        /// Check if an word exists
        /// </summary>
        /// <param name="word">word to search for</param>
        /// <returns>True if that word exists</returns>
        [Pure]
        public bool Exists(string word)
        {
            Contract.Requires(!string.IsNullOrEmpty(word), "Trie doesn't include empty string or null values");

            Node current = Root;
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
            return current.HasNullChild(word);
        }


        /// <summary>
        /// Adds word to the end of speicified node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="word"></param>
        public void Add(Node node, string word)
        {
            Contract.Requires(!string.IsNullOrEmpty(word), "Trie doesn't include empty string or null values");

            foreach (char ch in word)
            {
                Node childNode = node.AddChild(ch);
                node = childNode;
            }
            node.AddNullChild(word);
        }

        /// <summary>
        /// Adds a word if it doesn't exist
        /// </summary>
        /// <param name="word"></param>
        public void Add(string word)
        {
            Contract.Requires(!string.IsNullOrEmpty(word), "Trie doesn't include empty string or null values");

            Node current = Root;
            for (int i = 0; i < word.Length; i++)
            {
                Node childNode = current.HasChild(word[i]);
                if (childNode == null)
                {
                    Add(current, word.Substring(i));
                    break;
                }
                current = childNode;
            }
        }

        public void Remove(string word)
        {
            Contract.Requires(!string.IsNullOrEmpty(word), "Trie doesn't include empty string or null values");

            if (!Exists(word))
            {
                return;
            }
            var current = Root;
            foreach (var ch in word)
            {

            }
        }

        private List<string> AllStrings(Node node)
        {
            Contract.Requires<ArgumentNullException>(node != null);
            Contract.Ensures(Contract.Result<List<string>>() != null);

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

        public List<string> GetStringsContainingPrefix(string prefix)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(prefix));
            Contract.Ensures(Contract.Result<List<string>>() != null);

            var words = new List<string>();
            var current = Root;
            foreach (char ch in prefix)
            {
                Node childNode = current.HasChild(ch);
                if (childNode == null)
                {
                    //No words with current prefix
                    return words;
                }
                current = childNode;
            }

            return AllStrings(current);
        }

        private IEnumerator<string> Enumerate(Node node)
        {
            if (node is NullNode)
            {
                yield return node.WordFromRoot;
            }
            var words = new List<string>();
            foreach (var child in node.Children)
            {
                Enumerate(child);
            }
            yield break;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return Enumerate(Root);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
