using System;
using System.Collections.Generic;


namespace CSharp.DataStructures.TrieSpace
{
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