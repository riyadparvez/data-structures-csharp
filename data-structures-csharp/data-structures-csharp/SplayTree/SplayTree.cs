using System;


namespace DataStructures.SplayTreeSpace
{
    public class SplayTree<T>
        where T : IComparable<T>, IEquatable<T>
    {
        public Node<T> Root { get; set; }

    }
}
