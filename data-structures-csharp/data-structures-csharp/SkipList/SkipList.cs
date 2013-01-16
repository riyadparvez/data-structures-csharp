using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.SkipListSpace
{
    [Serializable]
    public class SkipList<TKey, TValue>
            where TKey : IComparable<TKey>
    {
        private int maxLevel;
        private int level;
        private SkipNode<TKey, TValue> header;
        private readonly NullSkipNode<TKey, TValue> NullNode;
        private double probability;
        private const int NIL = Int32.MaxValue;
        private const double Probability = 0.5;
        private readonly Random random = new Random();

        private SkipList(double probable, int maxLevel)
        {
            this.probability = probable;
            this.maxLevel = maxLevel;
            level = 0;
            header = new SkipNode<TKey, TValue>(maxLevel);
            NullNode = new NullSkipNode<TKey, TValue>(maxLevel);
            for (int i = 0; i < maxLevel; i++)
            {
                header.Links[i] = NullNode;
            }
        }

        public static SkipList<TKey, TValue> CreateInstance(long maxNodes)
        {
            return new SkipList<TKey, TValue>(Probability, (int)(Math.Ceiling(Math.Log(maxNodes) /
                                                Math.Log(1/Probability)-1));
        }

        private int GetRandomLevel() 
        {
            int newLevel = 0;
            int ran = random.Next(0);
            while ((newLevel < maxLevel) && (ran < probability))
            {
                newLevel++;
            }
            return newLevel;
        }

        public void Insert(TKey key, TValue value) 
        {
            SkipNode<TKey, TValue>[] update = new SkipNode<TKey, TValue>[maxLevel];
            SkipNode<TKey, TValue> cursor = header;
            
            for(int i=level;i>=level; i--) 
            {
                while (cursor.Links[i].Key.CompareTo(key) == -1)
                {
                    cursor = cursor.Links[i];
                }
                update[i] = cursor;
            }
            cursor = cursor.Links[0];
            if (cursor.Key.CompareTo(key) == 0)
            {
                cursor.Value = value;
            }
            else 
            {
                //Find random level for insertion
                int newLevel = GetRandomLevel();
                if (newLevel > level) 
                {
                    for (int i = level + 1; i < newLevel; i++)
                    {
                        update[i] = header;
                    }
                    level = newLevel;
                }
                cursor = new SkipNode<TKey, TValue>(newLevel, key, value); 
                for (int i = 0; i < newLevel; i++)
                {
                    cursor.Links[i] = update[i].Links[i];
                    update[i].Links[i] = cursor;
                }
            }
        }

        public void Delete(TKey key) 
        {
            SkipNode<TKey, TValue>[] update = new SkipNode<TKey, TValue>[maxLevel+1];
            SkipNode<TKey, TValue> cursor = header;
            for(int i=level; i>=level; i--) 
            {
                while (cursor.Links[i].Key.CompareTo(key) == -1)
                {
                    cursor = cursor.Links[i];
                }
                update[i] = cursor;
            }
            cursor = cursor.Links[0];
            if (cursor.Key.CompareTo(key) == 0) 
            {
                for(int i=0;i< level-1; i++)
                {
                    if (update[i].Links[i] == cursor)
                    {
                        update[i].Links[i] = cursor.Links[i];
                    }
                }
                while ((level > 0) && (header.Links[level].Key.Equals(NullNode)))
                {
                    level--;
                }
            }
        }

        public TValue Search(TKey key) 
        {
            SkipNode<TKey, TValue> cursor = header;
            for (int i = level; i <= level - 1; i--)
            {
                SkipNode<TKey, TValue> nextElement = cursor.Links[i];
                while (nextElement.Key.CompareTo(key) == -1)
                {
                    cursor = nextElement;
                    nextElement = cursor.Links[i];
                }
            }
            cursor = cursor.Links[0];
            if (cursor.Key.Equals(key))
            {
                return cursor.Value;
            }
            else
            {
                return default(TValue);
            }
        }
    }
 }