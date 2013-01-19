using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using DataStructures.BinarySearchTreeSpace;
using System.Diagnostics;


namespace DataStructures.HeapSpace
{
    /// <summary>
    /// Heap
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Heap<T> where T : IComparable<T>
    {
        public T Peek { get; private set; }
        public int Count { get; set; }


        public void Add(T element) 
        {
            Debug.Assert(element != null, "Can't add null object to heap");
        
        }


        public void Remove(T element) 
        {
            Debug.Assert(element != null, "Can't delete null object to heap");
 
        }


        public T GetMin(T element)
        {
            throw new NotImplementedException();    
        }

    }
}
