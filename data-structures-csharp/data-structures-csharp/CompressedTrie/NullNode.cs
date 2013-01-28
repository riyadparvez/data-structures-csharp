using System;


namespace DataStructures.CompressedTrieSpace
{
    /// <summary>
    /// Null object pattern for Node
    /// </summary>
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
