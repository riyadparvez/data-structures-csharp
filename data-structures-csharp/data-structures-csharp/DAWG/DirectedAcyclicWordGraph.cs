using System;
using System.Diagnostics;

//TODO: Add word
namespace DataStructures.DAWGSpace
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class DirectedAcyclicWordGraph
    {
        public Node Root { get; private set; }

        public void AddWord()
        {

        }

        /// <summary>
        /// Check if this word exists in this tree
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool Find(string word)
        {
            Debug.Assert(!string.IsNullOrEmpty(word));

            Node currentNode = Root;
            foreach (var ch in word)
            {
                Edge e = currentNode.FindEdge(ch);
                if (e.Equals(default(Edge)))
                {
                    return false;
                }
                Debug.Assert(e.StartNode.Equals(currentNode));
                currentNode = e.EndNode;
            }
            return true;
        }
    }
}
