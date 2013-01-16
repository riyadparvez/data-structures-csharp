using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using DataStructures.BinarySearchTreeSpace;


namespace DataStructures.HeapSpace
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Heap<T> where T : IComparable<T>
    {
        public Node<T> Peek { get; private set; }
        public int Count { get; set; }


        public void Add(T element) 
        { 
        
        }


        public void Remove(T element) 
        {
 
        }


        public T GetMin(T element)
        {
            throw new NotImplementedException();    
        }

    }
}
