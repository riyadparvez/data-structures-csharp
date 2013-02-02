using System;


namespace DataStructures.TrieSpace
{
    [Serializable]
    public sealed class NullNode : Node
    {
        public NullNode(string wordFromRoot)
            : base((char)0, wordFromRoot)
        {

        }
    }
}
