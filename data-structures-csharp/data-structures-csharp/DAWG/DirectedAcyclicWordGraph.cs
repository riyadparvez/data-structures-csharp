using System;
using System.Diagnostics.Contracts;

//TODO: Add word
namespace DataStructures.DAWGSpace
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class DirectedAcyclicWordGraph
    {
        private Node root;

        public void Add(string word)
        {
            Contract.Requires(!string.IsNullOrEmpty(word));
        }

        /// <summary>
        /// Check if this word exists in this tree
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool Find(string word)
        {
            Contract.Requires(!string.IsNullOrEmpty(word));

            var currentNode = root;
            foreach (var ch in word)
            {
                var e = currentNode.FindEdge(ch);
                if (e == null)
                {
                    return false;
                }
                Contract.Assert(e.StartNode.Equals(currentNode));
                currentNode = e.EndNode;
            }
            return true;
        }
    }
}
