using System;
using System.Collections.Generic;


namespace DataStructures.TrieSpace
{
    [Serializable]
    public partial class Trie : IEnumerable<string>
    {
        [Serializable]
        private sealed class NullNode : Node
        {
            public NullNode(string wordFromRoot, Node parent)
                : base((char)0, wordFromRoot, parent)
            {

            }
        }
    }
}