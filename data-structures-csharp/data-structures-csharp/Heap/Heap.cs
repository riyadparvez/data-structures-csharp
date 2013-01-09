using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures.BinarySearchTreeSpace;


namespace DataStructures.HeapSpace
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Heap<T> where T : IComparable<T>
    {
        public Node<T> Peek { get; private set; }
        public int Count { get; set; }


        public void Add() 
        { 
        
        }


        public T GetMin(T element)
        {
            
        }

    }
}
