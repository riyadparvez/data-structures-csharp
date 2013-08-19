using System;
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
    public class LinkedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private Dictionary<TKey, Value<TKey, TValue>> dic;
        private readonly Value<TKey, TValue> headValue = new Value<TKey, TValue>(default(TValue));
        private Value<TKey, TValue> lastAdded;

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

            var newValue = new Value<TKey, TValue>(value, lastAdded);
            var newPair = new Pair<TKey, Value<TKey, TValue>>(key, newValue);
            lastAdded.next = newPair;
            lastAdded = newPair;
            dic.Add(newPair.key, newPair.value);
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

        public bool Remove(TKey key)
        {
            if(!dic.ContainsKey(key))
            {
                return false;
            }
            if(lastAdded.Equals(dic[key]))
            {
                lastAdded = lastAdded.prev;
            }
            var prev = ;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return dic.TryGetValue(new Value<TKey>(key), out value);
        }

        public ICollection<TValue> Values
        {
            get { return dic.Values; }
        }

        public TValue this[TKey key]
        {
            get
            {
                Contract.Requires<ArgumentNullException>(key != null);

                return dic[new Value<TKey>(key)];
            }
            set
            {
                Contract.Requires<ArgumentNullException>(key != null);

                dic[new Value<TKey>(key)] = value;
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
            return (dic.ContainsKey(new Value<TKey>(item.Key)) ? 
                    dic[new Value<TKey>(item.Key)].Equals(item.Value) : false);          
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Contract.Requires<ArgumentException>(array.Length >= dic.Count+arrayIndex);
        }

        public int Count
        {
            get { return dic.Count; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var key = headValue.next;
            while (key != null)
            {
                var tempKey = key;
                key = key.next;
                yield return new KeyValuePair<TKey, TValue>(tempKey.value, dic[tempKey]);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Pair<TKey, Value<TKey, TValue>>
        {
            public TKey key;
            public Value<TKey, TValue> value;

            public Pair(TKey key = default(TKey), Value<TKey, TValue> value = null)
            {
                this.key = key;
                this.value = value;
            }
        }

        private class Value<TKey, TValue>
        {
            public TValue value;
            public Pair<TKey, Value<TKey, TValue>> next;
            public Pair<TKey, Value<TKey, TValue>> prev;

            public Value(TValue value, Pair<TKey, Value<TKey, TValue>> prev = null, Pair<TKey, Value<TKey, TValue>> next = null)
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
