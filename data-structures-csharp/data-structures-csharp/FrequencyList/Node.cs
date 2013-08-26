using System;
using System.Diagnostics.Contracts;

namespace DataStructures.FrequencyListSpace
{
    public partial class FrequencyList<T>
    {
        [Serializable]
        private class Node<T>
            where T : IComparable<T>
        {
            public int AccessCount { get; set; }
            public T Value { get; set; }
            public Node<T> Previous { get; set; }
            public Node<T> Next { get; set; }

            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(AccessCount >= 0);
            }

            public Node()
            {
            }

            public Node(T data)
            {
                Contract.Requires<ArgumentNullException>(data != null);
                Value = data;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;
                    hash = 23 * Value.GetHashCode();
                    return hash;
                }
            }

            public override bool Equals(object obj)
            {
                Node<T> otherObject = obj as Node<T>;
                if (otherObject == null)
                {
                    return false;
                }
                return Value.Equals(otherObject.Value);
            }
        }
    }
}
