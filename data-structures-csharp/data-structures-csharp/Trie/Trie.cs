using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataStructures.TrieSpace
{
    [Serializable]
    public class Trie
    {
        public Node Root { get; private set; }


        public Trie() 
        {
            Root = new NullNode();
        }


        /// <summary>
        /// Check if an word exists
        /// </summary>
        /// <param name="word">word to search for</param>
        /// <returns>True if that word exists</returns>
        public bool Exists(string word) 
        {
            Node current = Root;
            foreach (char ch in word)
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
            foreach(char ch in word)
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
            Node current = Root;
            for (int i = 0; i < word.Length; i++ )
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
