using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp.DataStructures.HeapSpace;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.Contracts;

namespace CSharp.DataStructures.Heap.Tests
{
    [TestClass()]
    public class HeapTests
    {
        private Heap<int> _heap = new Heap<int>();
        private readonly int[] _input = new int[] { 10, 0, 2, 26, -1, -6, 1, 24, 25, 44, 100 };

        protected void UnLoadInput()
        {
            _heap = new Heap<int>();
            Contract.Assert(_heap != null, "Heap initialisation failed");
        }

        protected void LoadInput()
        {
            Contract.Assert(_heap != null, "Heap cannot be null");
            _heap = new Heap<int>();
            foreach (var i in _input)
            {
                _heap.Add(i);
            }
        }

        [TestMethod()]
        public void AddTest()
        {
            const int key = 0;
            var expectedValue = _input[key];

            //Add first element
            UnLoadInput();
            _heap.Add(_input[key]);
            var actualCount = _heap.Count;
            var expectedCount = 1;
            Assert.IsTrue(_heap.Peek == expectedValue, "The added element was not found in the heap");
            Assert.AreEqual(_heap.GetMin(), expectedValue, "The extracted element: "+ _heap.GetMin() + " does not equal the inserted one " +expectedValue);
            Assert.AreEqual(expectedCount, actualCount, "Invalid count in adding first element");

            //Add with many data
            LoadInput();
            actualCount = _heap.Count;
            expectedCount = _input.Length;
            Assert.AreEqual(expectedCount, actualCount, "Invalid count in initialising the heap");
        }

        [TestMethod()]
        public void RemoveTest()
        {
            var count = 0;
            UnLoadInput();

            //Removing a nonexisting Item
            var actual = _heap.RemoveMin();
            var expected = default(int);
            Assert.AreEqual(expected, actual, "Removing non existing item failed.");

            //Test for the existing node
            if (_heap.Count == 0)
            {
                LoadInput();
            }
            //Removing a existing Item
            count = _heap.Count;
            int value;
            expected = _input.Min();
            actual = _heap.RemoveMin();
            var actualCount = _heap.Count;
            var expectedCount = count - 1;
            Assert.AreEqual(expected, actual, "Removing existing item failed.");
            Assert.AreEqual(expectedCount, actualCount, "Removing existing item failed- Count mismatch");
        }

        [TestMethod()]
        public void GetMinTest()
        {
            var count = 0;
            UnLoadInput();

            //Removing a nonexisting Item
            var actual = _heap.GetMin();
            var expected = default(int);
            Assert.AreEqual(expected, actual, "Getting non existing minimum item failed.");

            //Test for the existing node
            if (_heap.Count == 0)
            {
                LoadInput();
            }
            //Removing a existing Item
            count = _heap.Count;
            expected = _input.Min();
            actual = _heap.GetMin();
            var actualCount = _heap.Count;
            var expectedCount = count;
            Assert.AreEqual(expected, actual, "Getting existing minimum item failed.");
            Assert.AreEqual(expectedCount, actualCount, "Getting existing minimum item failed- Count mismatch");
        }



        [TestMethod()]
        public void GetPeekTest()
        {
            var count = 0;
            UnLoadInput();

            //Removing a nonexisting Item
            var actual = _heap.GetMin();
            var expected = default(int);
            Assert.AreEqual(expected, actual, "Peeking non existing item failed.");

            //Test for the existing node
            if (_heap.Count == 0)
            {
                LoadInput();
            }
            //Removing a existing Item
            count = _heap.Count;
            int value;
            expected = _input.Min();
            actual = _heap.Peek;
            var actualCount = _heap.Count;
            var expectedCount = count;
            Assert.AreEqual(expected, actual, "Peeking existing item failed.");
            Assert.AreEqual(expectedCount, actualCount, "Peeking existing item failed- Count mismatch");
        }
    }
}
