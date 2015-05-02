using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace DataStructures.HashSpace
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class LinkedDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TValue : LinkedDictionary<TKey, TValue>.Value<TKey, TValue>
    {
        private Dictionary<TKey, Value<TKey, TValue>> dic;
        private readonly Value<TKey, TValue> headValue = new Value<TKey, TValue>(default(TValue));
        private Value<TKey, TValue> lastAdded;
        private Pair<TKey, TValue> lastAddedPair;

        public LinkedDictionary(int capacity)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity > 0);

            dic = new Dictionary<TKey, Value<TKey, TValue>>(capacity);
            lastAdded = headValue;
            dic.Add(default(TKey), headValue);
        }

        public bool ContainsValue(TValue value)
        {
            return dic.ContainsValue(new Value<TKey, TValue>(value));
        }

        public void Add(TKey key, TValue value)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            var newValue = new Value<TKey, TValue>(value, lastAddedPair);
            var newPair = new Pair<TKey, TValue>(key, newValue);
            lastAdded.next = newPair;
            lastAdded = newValue;
            lastAddedPair = newPair;
            dic.Add(newPair.key, newPair.value);
        }

        public bool Remove(TKey key)
        {
            if (!dic.ContainsKey(key))
            {
                return false;
            }
            if (lastAdded.Equals(dic[key]))
            {
                lastAddedPair = lastAdded.prev;
            }
            var prev = lastAdded.prev;
            //TODO: remove element
            return false;
        }

        public bool ContainsKey(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            
            return dic.ContainsKey(key);
        }

        public ICollection<TKey> Keys
        {
            get 
            {
                return dic.Keys; 
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if(!dic.ContainsKey(item.Key))
            {
                return false;
            }
            if(lastAdded.Equals(dic[item.Key]))
            {
                lastAddedPair = lastAdded.prev;
            }
            var prev = lastAdded.prev;
            //TODO: remove element
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);
            var valueDic = new Value<TKey, TValue>(value);
            return dic.TryGetValue(key, out valueDic);
        }

        public ICollection<TValue> Values
        {
            //TODO: fix
            get { return null; }
        }

        public TValue this[TKey key]
        {
            get
            {
                Contract.Requires<ArgumentNullException>(key != null);

                return dic[key].value;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(key != null);

                dic[key] = value;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            dic.Clear();
            lastAdded = headValue;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return (dic.ContainsKey(item.Key) && dic[item.Key].Equals(item.Value));
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Contract.Requires<ArgumentException>(array.Length >= dic.Count+arrayIndex);
        }

        public int Count
        {
            get { return dic.Count; }
        }

        public bool IsReadOnly { get; private set; }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var key = headValue.next;
            while (key != null)
            {
                var tempKey = key;
                key = key.value.next;
                yield return new KeyValuePair<TKey, TValue>(tempKey.key, dic[tempKey.key].value);
            }
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

      

        public class Pair<TKey, TValue> where TValue : Value<TKey, TValue>
        {
            public TKey key;
            public Value<TKey, TValue> value;

            public Pair(TKey key = default(TKey), Value<TKey, TValue> value = null)
            {
                this.key = key;
                this.value = value;
            }
        }
        
        public class Value<TKey, TValue> where TValue:Value<TKey,TValue>
        {
            public TValue value;
            public Pair<TKey, TValue> next;
            public Pair<TKey, TValue> prev;

            public Value(TValue value, Pair<TKey, TValue> prev = null, Pair<TKey, TValue> next = null)
            {
                this.value = value;
                this.prev = prev;
                this.next = next;
            }

            public override int GetHashCode()
            {
                return value.GetHashCode();
            }

            public override bool Equals(object otherObj) 
            {
                var otherKey = otherObj as Value<TKey, TValue>;
                if(otherKey == null)
                {
                    return false;
                }
                return otherKey.value.Equals(value);
            }
        }
    }
}
