using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tail
{
    class CircularQueue<T>
    {
        private const int DefaultCapacity = 4;
        private T[] elements;
        private int startindex = 0;
        private int endindex = 0;
        public int Count
        {
            get; private set;
        }
        public CircularQueue(int capacity = DefaultCapacity)
        {

        }
        public void Enqueue(T element)
        {
            if (this.Count >= this.elements.Length)
            {
                this.Grow();
            }
            this.elements[this.endindex] = element;
            this.endindex = (this.endindex + 1) % this.elements.Length;
            this.Count++;
        }
        public void Grow()
        {
            var newElements = new T[2 * this.elements.Length];
            this.CopyAllElementsTo(newElements);
            this.elements = newElements;
            this.startindex = 0;
            this.endindex = this.Count;
        }
        public void CopyAllElementsTo(T[] resultArr)
        {
            int SourceIndex = this.startindex;
            int destionationIndex = 0;
            for (int i = 0; i < this.Count; i++)
            {
                resultArr[destionationIndex] = this.elements[SourceIndex];
                SourceIndex = (SourceIndex + 1) % this.elements.Length;
                destionationIndex++;
            }
        }
        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The Queue is empty!");
            }
            var result = this.elements[startindex];
            this.startindex = (this.startindex + 1) % this.elements.Length;
            return result;
        }
        public T[] ToArray()
        {
            var resultArr = new T[this.Count];
            CopyAllElementsTo(resultArr);
            return resultArr;
        }
    }
}
