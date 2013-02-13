using System;
using System.Collections;


namespace DataStructures.BloomFilterSpace
{
    [Serializable]
    public class BloomFilter
    {
        private readonly BitArray bits;
        private readonly int bitsPerElement;
        private readonly int numberOfHashFunctions;

        public int Count { get; private set; }

        public BloomFilter(int bitsPerElement, int numberOfElements)
        {
            this.bitsPerElement = bitsPerElement;
            bits = new BitArray(bitsPerElement * numberOfElements);
            Count = 0;
        }

        public void Clear()
        {
            bits.SetAll(false);
            Count = 0;
        }
    }
}
