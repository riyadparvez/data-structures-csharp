using System;


namespace DataStructures.HashSpace
{
    [Serializable]
    public class LinkedHashMap<TKey, TValue>
    {


        private class Entry<TKey, TValue>
        {
            // These fields comprise the doubly linked list used for iteration.
            Entry<TKey, TValue> before, after;

            Entry(int hash, TKey key, TValue value, Entry<TKey, TValue> next)
            {
                super(hash, key, value, next);
            }

            /**
             * Removes this entry from the linked list.
             */
            private void Remove()
            {
                before.after = after;
                after.before = before;
            }

            /**
             * Inserts this entry before the specified existing entry in the list.
             */
            private void InsertBefore(Entry<TKey, TValue> existingEntry)
            {
                after = existingEntry;
                before = existingEntry.before;
                before.after = this;
                after.before = this;
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
