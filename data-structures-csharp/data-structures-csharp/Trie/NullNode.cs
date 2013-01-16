using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataStructures.TrieSpace
{
    [Serializable]
    public sealed class NullNode : Node
    {
        public override char Character
        {
            get
            {
                return (char)0;
            }
        }

        public override string WordFromRoot
        {
            get
            {
                return string.Empty;
            }
        }

        public NullNode() 
        {
        
        }
    }
}
