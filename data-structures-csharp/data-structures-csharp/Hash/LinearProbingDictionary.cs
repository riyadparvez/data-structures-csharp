using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;


namespace DataStructures.HashSpace
{
    /// <summary>
    /// A hash table which uses user specified step size probing
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class LinearProbingDictionary<Tkey, TValue> : IDictionary<Tkey, TValue>
    {
        private const int DefaultCapacity = 1023;
        private int capacity;
        private readonly int stepSize;
        private int count;
        private bool[] isFilled;
        private Pair<Tkey, TValue>[] pairs;

        public ICollection<Tkey> Keys
        {
            get { return pairs.Select(p => p.Key).ToList().AsReadOnly(); }
        }

        public ICollection<TValue> Values
        {
            get { return pairs.Select(p => p.Value).ToList().AsReadOnly(); }
        }

        public int Count
        {
            get { return count; }
        }

        public int Capacity
        {
            get { return capacity; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }


        public LinearProbingDictionary(int capacity, int stepSize)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity > 0);
            Contract.Requires<ArgumentOutOfRangeException>(stepSize > 0);

            this.capacity = capacity;
            this.stepSize = stepSize;
            pairs = new Pair<Tkey, TValue>[capacity];
            isFilled = new bool[capacity];
        }

        public LinearProbingDictionary(int stepSize)
            : this(DefaultCapacity, stepSize)
        {
        }

        public void Add(Tkey key, TValue value)
        {

        }

        /// <summary>
        /// Returns index of th item
        /// </summary>
        /// <param name="key"></param>
        /// <returns>-1 if not present</returns>
        private int GetIndex(Tkey key)
        {
            if (key == null)
            {
                return -1;
            }

            int index = key.GetHashCode() % capacity;
            int currentIndex = index;
            do
            {
                if (pairs[currentIndex] == null)
                {
                    return -1;
                }
                if (pairs[currentIndex].Key.Equals(key))
                {
                    return currentIndex;
                }
                else
                {
                    currentIndex = (currentIndex + stepSize) % capacity;
                }
            } while (currentIndex != index);
            return -1;
        }

        public bool ContainsKey(Tkey key)
        {
            if (GetIndex(key) == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Remove(Tkey key)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key");
            int index;
            if ((index = GetIndex(key)) != -1)
            {
                pairs[index] = null;
                count--;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryGetValue(Tkey key, out TValue value)
        {
            int index = GetIndex(key);
            if (index != -1)
            {
                value = pairs[index].Value;
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }

        public TValue this[Tkey key]
        {
            get
            {
                Contract.Requires<ArgumentNullException>(key != null, "key");
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(KeyValuePair<Tkey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            count = 0;
            isFilled = Array.ConvertAll<bool, bool>(isFilled, b => b = false);
            pairs = Array.ConvertAll<Pair<Tkey, TValue>, Pair<Tkey, TValue>>(pairs, pair => pair = null);
        }

        public bool Contains(KeyValuePair<Tkey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<Tkey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void Remove(KeyValuePair<Tkey, TValue> item)
        {
            if (!ContainsKey(item.Key))
            {
                return;
            }

        }

        public IEnumerator<KeyValuePair<Tkey, TValue>> GetEnumerator()
        {
            foreach (var pair in pairs)
            {
                yield return new KeyValuePair<Tkey, TValue>(pair.Key, pair.Value);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class Pair<TKey, TValue>
        {
            public Tkey Key { get; set; }
            public TValue Value { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                return Key.Equals(obj);
            }

            public override int GetHashCode()
            {
                int hash = 31;
                unchecked
                {
                    hash = hash + 17 * Key.GetHashCode();
                }
                return hash;
            }
        }
    }
}
