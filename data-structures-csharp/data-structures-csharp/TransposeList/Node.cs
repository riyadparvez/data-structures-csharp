using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.TransposeListSpace
{
    [Serializable]
    public class Node<T> 
        public T Data { get; set; }
        public Node<T> Previous { get; set; }
        public Node<T> Next { get; set; }

        public Node()
        {
        }

        public Node(T data) 
        {
            Data = data;
        }

        public override int GetHashCode() 
        {
            unchecked 
            {
                int hash = 17;
                hash = 23 * Data.GetHashCode();
                return hash;
            }
        }


        public override bool Equals(object obj)
        {
            Node<T> otherObject = obj as Node<T>;
            if(otherObject == null)
            {
                return false;
            }
            return Data.Equals(otherObject.Data);
        }
    }
}
