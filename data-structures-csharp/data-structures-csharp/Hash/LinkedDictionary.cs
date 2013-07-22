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
        private Dictionary<Key<TKey>, TValue> dic;
        private readonly Key<TKey> head = new Key<TKey>(default(TKey));
        private Key<TKey> lastAdded;

        public LinkedDictionary(int capacity)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity > 0);
            
            dic = new Dictionary<Key<TKey>, TValue>(capacity);
            lastAdded = head;
        }

        public bool ContainsValue(TValue value)
        {
            return dic.ContainsValue(value);
        }

        public void Add(TKey key, TValue value)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            Key<TKey> newKey = new Key<TKey>(key, lastAdded);
            lastAdded.next = newKey;
            lastAdded = newKey;
            dic.Add(newKey, value);
        }

        public bool ContainsKey(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            
            return dic.ContainsKey(new Key<TKey>(key));
        }

        public ICollection<TKey> Keys
        {
            get 
            {
                var list = new List<Key<TKey>>(dic.Keys);
                return list.Select(l => l.key).ToList(); 
            }
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return dic.TryGetValue(new Key<TKey>(key), out value);
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

                return dic[new Key<TKey>(key)];
            }
            set
            {
                Contract.Requires<ArgumentNullException>(key != null);

                dic[new Key<TKey>(key)] = value;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            dic.Clear();
            lastAdded = head;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return (dic.ContainsKey(new Key<TKey>(item.Key)) ? dic[new Key<TKey>(item.Key)].Equals(item.Value) : false);          
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        private class Key<T>
        {
            public T key;
            public Key<T> next;
            public Key<T> prev;

            public Key(T key, Key<T> prev = null, Key<T> next = null)
            {
                this.key = key;
                this.prev = prev;
                this.next = next;
            }

            public override int GetHashCode()
            {
                return key.GetHashCode();
            }

            public override bool Equals(object otherObj) 
            {
                var otherKey = otherObj as Key<T>;
                if(otherKey == null)
                {
                    return false;
                }
                return otherKey.key.Equals(key);
            }
        }

    }
}
