using System;
using CSharp.DataStructures.ListSpace;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp.DataStructures.CircularQueueSpace.Tests
{
    [TestClass()]
    public class CircularQueueTests
    {
        private CircularQueue<int> _circularQueue = new CircularQueue<int>();
        private readonly int[] _nodes = new int[] { 1, 2, 3, 4, 5, 6 };

        [TestInitialize, TestCleanup]
        protected void UnLoadInput()
        {
            _circularQueue = new CircularQueue<int>();
        }

        protected void LoadInput()
        {
            _circularQueue = new CircularQueue<int>(_nodes.Length);
            foreach (var node in _nodes)
            {
                _circularQueue.Enqueue(node);
            }
        }

        [TestMethod]
        public void EnqueueEmptyTest()
        {
            const int expectedValue = 5;
            _circularQueue.Enqueue(expectedValue);
            Assert.AreEqual(expectedValue, _circularQueue.Dequeue(), "Could not extract the right element from an empty queue");
        }

        [TestMethod]
        public void EnqueueTwiceTest()
        {
            const int expectedValue = 5;
            _circularQueue.Enqueue(expectedValue);
            _circularQueue.Enqueue(expectedValue);
            Assert.AreEqual(2, _circularQueue.Count, "Could not extract the right element from an empty queue");
        }

        [TestMethod]
        public void DequeueTest()
        {
            LoadInput();
            var expectedValue = _nodes[0];
            Assert.AreEqual(expectedValue, _circularQueue.Dequeue(), "Could not extract the right element from a queue");
        }

        [TestMethod]
        public void EnqueueSingleTest()
        {
            _circularQueue = new CircularQueue<int>(_nodes.Length);
            LoadInput();
            const int expectedValue = 5;
            _circularQueue.Enqueue(expectedValue);
            Assert.AreEqual(expectedValue, _circularQueue.Dequeue(), "Could not extract the right element inserted in a full queue");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DequeueEmptyTest()
        {
            _circularQueue.Dequeue();
        }

        [TestMethod]
        public void NextIndexEmptyTest()
        {
            var next = _circularQueue.NextIndex(_circularQueue.Count);
            const int expectedNext = 0;
            Assert.AreEqual(expectedNext, next, "Next index on empty queue do not match");
        }

        [TestMethod]
        public void NextIndexFullTest()
        {
            _circularQueue = new CircularQueue<int>(_nodes.Length);
            var i = 0;

            var next = _circularQueue.NextIndex(null);
            var expectedNext = i;
            Assert.AreEqual(expectedNext, next, "Could not get the right index at the element - " + i);
            _circularQueue.Enqueue(i++);
            
            next = _circularQueue.NextIndex(i-1);
            expectedNext = i;
            Assert.AreEqual(expectedNext, next, "Could not get the right index at the element - " + i);
            _circularQueue.Enqueue(i++);
            
            next = _circularQueue.NextIndex(i-1);
            expectedNext = i;
            Assert.AreEqual(expectedNext, next, "Could not get the right index at the element - " + i);
            _circularQueue.Enqueue(i++);
            
            next = _circularQueue.NextIndex(i-1);
            expectedNext = i;
            Assert.AreEqual(expectedNext, next, "Could not get the right index at the element - " + i);
            _circularQueue.Enqueue(i++);
            
            next = _circularQueue.NextIndex(i-1);
            expectedNext = i;
            Assert.AreEqual(expectedNext, next, "Could not get the right index at the element - " + i);
            _circularQueue.Enqueue(i++);
            
            next = _circularQueue.NextIndex(i-1);
            expectedNext = i;
            Assert.AreEqual(expectedNext, next, "Could not get the right index at the element - " + i);
        }

        [TestMethod]
        public void EnqueueFullTest()
        {
            _circularQueue = new CircularQueue<int>(_nodes.Length);
            var i = 0;

            var next = _circularQueue.NextIndex(null);
            _circularQueue.Enqueue(i++);
            if (next != null)
                Assert.AreEqual(i - 1, _circularQueue.QueueList[next.Value], "Could not get the right value at the element - " + (i - 1));

            next = _circularQueue.NextIndex(i - 1);
            _circularQueue.Enqueue(i++);
            if (next != null)
                Assert.AreEqual(i - 1, _circularQueue.QueueList[next.Value], "Could not get the right value at the element - " + (i - 1));

            next = _circularQueue.NextIndex(i - 1);
            _circularQueue.Enqueue(i++);
            if (next != null)
                Assert.AreEqual(i - 1, _circularQueue.QueueList[next.Value], "Could not get the right value at the element - " + (i - 1));

            next = _circularQueue.NextIndex(i - 1);
            _circularQueue.Enqueue(i++);
            if (next != null)
                Assert.AreEqual(i - 1, _circularQueue.QueueList[next.Value], "Could not get the right value at the element - " + (i - 1));

            next = _circularQueue.NextIndex(i - 1);
            _circularQueue.Enqueue(i++);
            if (next != null)
                Assert.AreEqual(i - 1, _circularQueue.QueueList[next.Value], "Could not get the right value at the element - " + (i - 1));

            next = _circularQueue.NextIndex(i - 1);
            _circularQueue.Enqueue(i++);
            if (next != null)
                Assert.AreEqual(i - 1, _circularQueue.QueueList[next.Value], "Could not get the right value at the element - " + (i - 1));
        }
    }
}
