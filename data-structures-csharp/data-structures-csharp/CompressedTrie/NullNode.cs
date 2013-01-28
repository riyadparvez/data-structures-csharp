using System;


namespace DataStructures.CompressedTrieSpace
{
    [Serializable]
    public sealed class NullNode : Node
    {
        public override string StringFragment
        {
            get
            {
                return string.Empty;
            }
        }

        public NullNode(string wordFromRoot)
            : base(wordFromRoot)
        {
        }
    }
}
