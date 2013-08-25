using System;


namespace DataStructures.CompressedTrieSpace
{
    public partial class CompressedTrie
    {
        /// <summary>
        /// Null object pattern for Node
        /// </summary>
        [Serializable]
        private sealed class NullNode : Node
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
}