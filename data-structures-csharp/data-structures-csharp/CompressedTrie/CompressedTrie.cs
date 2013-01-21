using System;
using System.Diagnostics;


namespace DataStructures.CompressedTrieSpace
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CompressedTrie
    {
        public Node Root { get; private set; }


        public CompressedTrie()
        {
            Root = new NullNode("");
        }


        /// <summary>
        /// Check if an word exists
        /// </summary>
        /// <param name="word">word to search for</param>
        /// <returns>True if that word exists</returns>
        public bool Exists(string word)
        {
            Debug.Assert(string.IsNullOrEmpty(word), "Trie doesn't include empty string or null values");

            Node current = Root;
            for (int i = 0; i < word.Length; i++)
            {
                Node childNode = current.HasChild(ch);
                if (childNode == null)
                {
                    return false;
                }
                current = childNode;
            }
            return true;
        }


        /// <summary>
        /// Adds word to the end of speicified node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="word"></param>
        public void Add(Node node, string word)
        {
            Debug.Assert(string.IsNullOrEmpty(word), "Trie doesn't include empty string or null values");

            foreach (char ch in word)
            {
                Node childNode = node.AddChild(ch);
                node = childNode;
            }
        }

        /// <summary>
        /// Adds a word if it doesn't exist
        /// </summary>
        /// <param name="word"></param>
        public void Add(string word)
        {
            Debug.Assert(string.IsNullOrEmpty(word), "Trie doesn't include empty string or null values");

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
    }
}
