using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;


namespace DataStructures.BloomFilterSpace
{
    [Serializable]
    public abstract class BloomFilter<T>
    {
        private readonly BitArray bits;
        private readonly int bitsPerElement;
        private readonly int numberOfHashFunctions;
        private readonly string[] algorithmNames =
        {
            "SHA",
            "MD5", 
            "SHA1",
            "SHA256",
            "SHA384",
        };

        public int BitsPerElement
        {
            get { return bitsPerElement; }
        }
        
        public int Count { get; private set; }
        
        public int NumberOfHashes
        {
            get { return algorithmNames.Length; }
        }

        public double FalsePositiveProbability
        {
            // (1 - e^(-k * n / m)) ^ k
            get
            {
                return Math.Pow((1 - Math.Exp(-NumberOfHashes * (double)Count
                          / (double)bits.Length)), NumberOfHashes);
            }

        }
        public abstract object SyncRoot { get; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Count >= 0);
        }

        public BloomFilter(int bitsPerElement, int numberOfElements)
        {
            Contract.Requires<ArgumentOutOfRangeException>(bitsPerElement > 0);
            Contract.Requires<ArgumentOutOfRangeException>(numberOfElements > 0);

            this.bitsPerElement = bitsPerElement;
            bits = new BitArray(bitsPerElement * numberOfElements);
            Count = 0;
        }

        private byte[] ToBytes(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, element);
            return memoryStream.ToArray();
        }

        private int[] CreateHashes(byte[] bytes)
        {
            int[] result = new int[numberOfHashFunctions];
            int k = 0;
            foreach (string algoName in algorithmNames)
            {
                byte[] digest;
                using (HashAlgorithm algorithm = HashAlgorithm.Create(algoName))
                {
                    digest = algorithm.ComputeHash(bytes);
                }
                for (int i = 0; i < digest.Length / 4 && k < numberOfHashFunctions; i++)
                {
                    int h = 0;
                    for (int j = (i * 4); j < (i * 4) + 4; j++)
                    {
                        h <<= 8;
                        h |= ((int)digest[j]) & 0xFF;
                    }
                    result[k] = h;
                    k++;
                }
            }
            return result;
        }

        public void Add(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            int[] hashes = CreateHashes(ToBytes(element));
            foreach (int hash in hashes)
            {
                bits.Set((int)Math.Abs(hash % bits.Length), true);
            }
            Count++;
        }

        public void AddRange(IEnumerable<T> elements)
        {
            Contract.Requires<ArgumentNullException>(elements != null);

            foreach (var element in elements)
            {
                Add(element);
            }
        }

        private bool Contains(byte[] bytes)
        {
            Contract.Requires<ArgumentNullException>(bytes != null);
            Contract.Requires<ArgumentException>(bytes.Length > 0);

            int[] hashes = CreateHashes(bytes);
            foreach (int hash in hashes)
            {
                if (!bits.Get((int)Math.Abs(hash % bits.Length)))
                {
                    return false;
                }
            }
            return true;
        }

        [Pure]
        public bool Contains(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            return Contains(ToBytes(element));
        }

        public void Clear()
        {
            bits.SetAll(false);
            Count = 0;
        }
    }
}
