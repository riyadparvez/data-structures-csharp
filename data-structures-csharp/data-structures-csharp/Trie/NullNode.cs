using System;


namespace DataStructures.TrieSpace
{
    [Serializable]
    public sealed class NullNode : Node
    {
        public NullNode(string wordFromRoot, Node parent)
            : base((char)0, wordFromRoot, parent)
        {

        }
    }
}
