using System;
using System.Collections.Generic;


namespace DataStructures.HashSpace
{
    [Serializable]
    public class LinkedHashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private Pair<TKey, TValue> header;

        void Initialize()
        {
            header = new Pair<TKey, TValue>(-1, default(TKey), default(TValue), null);
            header.previous = header.next = header;
        }

        public bool ContainsValue(TValue value)
        {
            // Overridden to take advantage of faster iterator
            if (value == null)
            {
                for (var e = header.next; e != header; e = e.next)
                {
                    if (e.value == null)
                    {
                        return true;
                    }
                }
            }
            else
            {
                for (var e = header.next; e != header; e = e.next)
                {
                    if (value.Equals(e.value))
                    {
                        return true;
                    }
                }
            }
            return false;
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
            throw new NotImplementedException();
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
            get { throw new NotImplementedException(); }
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
        private class Pair<TKey, TValue>
        {
            public TKey key;
            public TValue value;
            // These fields comprise the doubly linked list used for iteration.
            public Pair<TKey, TValue> previous;
            public Pair<TKey, TValue> next;

            public Pair(int hash, TKey key, TValue value, Pair<TKey, TValue> next)
            {
                this.key = key;
                this.value = value;
            }

            /**
             * Removes this entry from the linked list.
             */
            private void Remove()
            {
                previous.next = next;
                next.previous = previous;
            }

            /**
             * Inserts this entry before the specified existing entry in the list.
             */
            private void InsertBefore(Pair<TKey, TValue> existingEntry)
            {
                next = existingEntry;
                previous = existingEntry.previous;
                previous.next = this;
                next.previous = this;
            }

            /**
             * This method is invoked by the superclass whenever the value
             * of a pre-existing entry is read by Map.get or modified by Map.set.
             * If the enclosing Map is access-ordered, it moves the entry
             * to the end of the list; otherwise, it does nothing.
             */
            void recordAccess(LinkedHashMap<TKey, TValue> lm)
            {
                if (lm.accessOrder)
                {
                    lm.modCount++;
                    Remove();
                    InsertBefore(lm.header);
                }
            }
        }

    }
}
