using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace DataStructures.SkipListSpace
{
    /// <summary>
    /// Key value pair implemented in Skip List
    /// </summary>
    /// <typeparam name="TKey">Key in key value pair</typeparam>
    /// <typeparam name="TValue">Value in key value pair</typeparam>
    [Serializable]
    public partial class SkipList<TKey, TValue> : IEnumerable<TValue>, ICollection<TValue>
            where TKey : IComparable<TKey>
    {
        private readonly object lockObject = new object();

        private readonly int maxLevel;
        private int level;
        private SkipNode<TKey, TValue> header;
        private readonly NullSkipNode<TKey, TValue> NullNode;
        private double probability;
        private const double Probability = 0.5;
        private readonly Random random = new Random();

        public int Count { get; private set; }
        public bool IsSynchronized 
        { 
            get { return false; } 
        }
        public object SyncRoot 
        { 
            get { return lockObject; } 
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(probability > 0);
            Contract.Invariant(probability < 1);
            Contract.Invariant(maxLevel > 0);
        }

        private SkipList(double probable, int maxLevel)
        {
            Contract.Requires<ArgumentOutOfRangeException>(maxLevel > 0);
            Contract.Requires<ArgumentOutOfRangeException>(probable < 1);
            Contract.Requires<ArgumentOutOfRangeException>(probable > 0);

            this.probability = probable;
            this.maxLevel = maxLevel;
            level = 0;
            header = new SkipNode<TKey, TValue>(maxLevel);
            NullNode = new NullSkipNode<TKey, TValue>(maxLevel);
            //Initially all the forward links node of dummy header is NullNode
            for (int i = 0; i < maxLevel; i++)
            {
                header.Links[i] = NullNode;
            }
        }

        public static SkipList<TKey, TValue> CreateInstance(long maxNodes)
        {
            return new SkipList<TKey, TValue>(Probability, (int)(Math.Ceiling(Math.Log(maxNodes) /
                                                Math.Log(1 / Probability) - 1)));
        }

        private int GetRandomLevel()
        {
            Contract.Ensures(Contract.Result<int>() >= 0);
            Contract.Ensures(Contract.Result<int>() < maxLevel);

            int newLevel = 0;
            double ran = random.NextDouble();
            while ((newLevel < maxLevel) && (ran < probability))
            {
                newLevel++;
            }
            return newLevel;
        }

        /// <summary>
        /// Insert key value pair in list
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Insert(TKey key, TValue value)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key");

            SkipNode<TKey, TValue>[] update = new SkipNode<TKey, TValue>[maxLevel];
            //Start search for each level from dummy header node
            SkipNode<TKey, TValue> cursor = header;

            //Start from current max initialized header
            for (int i = level; i >= 0; i--)
            {
                //Find the node which is previous node to current code in any level
                while (cursor.Links[i].Key.CompareTo(key) == -1)
                {
                    cursor = cursor.Links[i];
                }
                //Put the predecessor node link in the level of update list
                update[i] = cursor;
            }
            //Check level 0 next whether we already have that element
            cursor = cursor.Links[0];
            if (cursor.Key.CompareTo(key) == 0)
            {
                //Assign new value to corresponding key
                cursor.Value = value;
            }
            else
            {
                //If this is new node, then
                //Find random level for insertion
                int newLevel = GetRandomLevel();
                //New node level is greater then current level
                //Update intermediate nodes
                if (newLevel > level)
                {
                    //This is a special case, where dummy header links aren't initialized yet
                    for (int i = level + 1; i < newLevel; i++)
                    {
                        //These levels of header aren't initialized yet
                        update[i] = header;
                    }
                    //update current level, until this level from bottom header link is initialized
                    level = newLevel;
                }
                //New node which will be inserted into new level, also needs newLevel number of forward edges
                cursor = new SkipNode<TKey, TValue>(newLevel, key, value);
                //Insert the node
                for (int i = 0; i < newLevel; i++)
                {
                    //Update edges of all the levels below to that level
                    //New node is set to successor node its predecessor
                    cursor.Links[i] = update[i].Links[i];
                    //Update forward edges of predecessor to currently inserted node
                    update[i].Links[i] = cursor;
                }
                Count++;
            }
        }

        /// <summary>
        /// Delete from skip list if element exists
        /// </summary>
        /// <param name="key">Key to be deleted</param>
        public void Delete(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            SkipNode<TKey, TValue>[] update = new SkipNode<TKey, TValue>[maxLevel + 1];
            SkipNode<TKey, TValue> cursor = header;

            for (int i = level; i >= 0; i--)
            {
                while (cursor.Links[i].Key.CompareTo(key) == -1)
                {
                    cursor = cursor.Links[i];
                }
                update[i] = cursor;
            }

            cursor = cursor.Links[0];
            //Check is this the element we want to delete?
            if (cursor.Key.CompareTo(key) == 0)
            {
                for (int i = 0; i < level; i++)
                {
                    //If next element is our to be deleted element
                    //Check prev node point to next node
                    if (update[i].Links[i] == cursor)
                    {
                        update[i].Links[i] = cursor.Links[i];
                    }
                }
                //Readjust levels of initialized links of dummy header
                while ((level > 0) && (header.Links[level].Key.Equals(NullNode)))
                {
                    level--;
                }
                Count--;
            }
            //Element isn't in the list, just return
        }

        /// <summary>
        /// Search for values, given a key
        /// </summary>
        /// <param name="key">Key to be searched</param>
        /// <returns>Value otherwise type default</returns>
        public TValue Search(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            SkipNode<TKey, TValue> cursor = header;
            for (int i = 0; i < level; i++)
            {
                SkipNode<TKey, TValue> nextElement = cursor.Links[i];
                while (nextElement.Key.CompareTo(key) == -1)
                {
                    cursor = nextElement;
                    nextElement = cursor.Links[i];
                }
            }
            //Got previous element of current key in the list
            //So next element must our searched element if list contains that element
            cursor = cursor.Links[0];
            if (cursor.Key.Equals(key))
            {
                return cursor.Value;
            }
            else
            {
                //Element is not in the list, return default
                return default(TValue);
            }
        }

        public void CopyTo(Array array, int index)
        {
            Contract.Requires<ArgumentNullException>(array != null, "array");
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0, "index");
            Contract.Requires<ArgumentException>(array.Length < Count);

            int i = index;

            var node = header.Links[0];
            while (node != null)
            {
                array.SetValue(node.Value, i);
                node = node.Links[0];
                i++;
            }
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            var node = header.Links[0];
            while (node != null)
            {
                yield return node.Value;
                node = node.Links[0];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}