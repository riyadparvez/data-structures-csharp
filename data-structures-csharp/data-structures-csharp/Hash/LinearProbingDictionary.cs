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
        private Pair<Tkey, TValue>[] pairs;

        public ICollection<Tkey> Keys
        {
            get 
            { 
                return pairs.Where(p => (p != null))
                              .Select(p => p.Key)
                              .ToList()
                              .AsReadOnly(); 
            }
        }

        public ICollection<TValue> Values
        {
            get 
            { 
                return pairs.Where(p => (p!=null))
                              .Select(p => p.Value)
                              .ToList()
                              .AsReadOnly(); 
            }
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

        public LinearProbingDictionary(int capacity, int stepSize = 1)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity > 0);
            Contract.Requires<ArgumentOutOfRangeException>(stepSize > 0);

            this.capacity = capacity;
            this.stepSize = stepSize;
            pairs = new Pair<Tkey, TValue>[capacity];
        }

        public LinearProbingDictionary(int stepSize)
            : this(DefaultCapacity, stepSize)
        {
        }

        public void Add(Tkey key, TValue value)
        {
            Pair<Tkey, TValue> pair = new Pair<Tkey, TValue>(key, value);
            int pos = pair.GetHashCode();
            int visit = 0;
            while(pairs[pos] != null)
            {
                if(visit >= capacity)
                {
                    throw new Exception("Dictionary is full");
                }
                pos = (pos+stepSize) % capacity;
                visit++;
            }
            
            pairs[pos] = pair;
            count++;
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
            return (GetIndex(key) != -1) ;
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
            Remove(new KeyValuePair<Tkey, TValue>());
            return false;

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
                var index = GetIndex(key);
                
                if(index == -1)
                {
                    return default(TValue);
                }
                return pairs[index].Value;
            }
            set
            {
                var index = GetIndex(key) ;
                if (index == -1)
                {
                    Add(key, value);
                }
                else
                {
                    pairs[index].Value = value;
                }
            }
        }

        public void Add(KeyValuePair<Tkey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            count = 0;
            pairs = new Pair<Tkey, TValue>[capacity];
        }

        public bool Contains(KeyValuePair<Tkey, TValue> item)
        {
            return ContainsKey(item.Key) ? item.Value.Equals(pairs[GetIndex(item.Key)].Value) : false;
        }

        public void CopyTo(KeyValuePair<Tkey, TValue>[] array, int arrayIndex)
        {
            foreach (var pair in pairs)
            {
                if(pair != null)
                {
                    array[arrayIndex] = new KeyValuePair<Tkey, TValue>(pair.Key, pair.Value);
                    arrayIndex++;
                }
            }
        }

        public bool Remove(KeyValuePair<Tkey, TValue> item)
        {
            int index = GetIndex(item.Key);
            if (index == -1)
            {
                return false;
            }
            pairs[index] = null;
            count--;
            return true;
        }

        public IEnumerator<KeyValuePair<Tkey, TValue>> GetEnumerator()
        {
            foreach (var pair in pairs)
            {
                if(pair == null)
                {
                    continue;
                }
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

            public Pair(Tkey key, TValue value)
            {
                Key = key;
                Value = value;
            }

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
