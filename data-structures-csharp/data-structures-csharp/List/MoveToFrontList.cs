using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DataStructures.MoveToFrontListSpace
{
    /// <summary>
    /// Moves most recently accessed element to the root
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class MoveToFrontList<T> : IEnumerable<T>
        where T : IEquatable<T>
    {
        private List<T> list = new List<T>();

        public int Count { get { return list.Count; } }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            //Contract.Requires(arg != null);
        }

        /// <summary>
        /// Retrieves specific node and updates that node position
        /// </summary>
        /// <param name="node"></param>
        /// <returns>null if element isn't in the list</returns>
        public T Get(T node)
        {
            Contract.Requires<ArgumentNullException>(node != null);
            T element = list.Where(e => e.Equals(node)).FirstOrDefault();
            if (element == null)
            {
                return element;
            }
            list.Remove(element);
            list.Insert(0, element);
            return element;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public void Add(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            list.Add(element);
        }

        /// <summary>
        /// Removes element from list, throws exception
        /// </summary>
        /// <param name="element"></param>
        public void Remove(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            list.Remove(element);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
