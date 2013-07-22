using System;
using System.Collections.Generic;


namespace DataStructures.HashSpace
{
    [Serializable]
    public class LinkedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private Dictionary<Key<TKey>, TValue> dic;
        private Key<TKey> head = new Key<TKey>();

        public LinkedDictionary()
        {

        }

        public bool ContainsValue(TValue value)
        {
        }

        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public ICollection<TKey> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        public ICollection<TValue> Values
        {
            get { throw new NotImplementedException(); }
        }

        public TValue this[TKey key]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            dic.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
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

            public Key(T key, Key<T> prev, Key<T> next)
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
