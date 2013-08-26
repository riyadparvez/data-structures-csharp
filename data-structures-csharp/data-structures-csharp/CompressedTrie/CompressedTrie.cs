using System;
using System.Diagnostics.Contracts;


namespace DataStructures.CompressedTrieSpace
{
    /// <summary>
    /// Compressed trie which saves node space by compressing non branching
    /// nodes into one node
    /// </summary>
    [Serializable]
    public partial class CompressedTrie
    {
        private readonly Node root;

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(root != null);
        }

        public CompressedTrie()
        {
            root = new NullNode(string.Empty);
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

            Node current = root;
            int count = 0;
            for (int i = 0; i < word.Length; i++, count++)
            {
                if (current.MoveToChildren(word.Substring(i)))
                {
                    current = current.GetChild(word.Substring(i));
                }
                else
                {
                    break;
                }
            }
            return (count == word.Length);
        }

        /// <summary>
        /// Adds a word if it doesn't exist
        /// </summary>
        /// <param name="word"></param>
        public void Add(string word)
        {
            Contract.Requires(!string.IsNullOrEmpty(word), "Trie doesn't include empty string or null values");

            Node current = root;
            int count = 0;
            for (int i = 0; i < word.Length; i++, count++)
            {
                if (current.MoveToChildren(word.Substring(i)))
                {
                    current = current.GetChild(word.Substring(i));
                }
                else
                {
                    current.AddChild(word.Substring(i));
                }
            }

            if (count == word.Length)
            {
                //New word ends in this node
                current.AddNullNode();
            }
        }
    }
}
