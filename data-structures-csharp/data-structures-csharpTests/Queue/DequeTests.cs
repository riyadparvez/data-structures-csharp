using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp.DataStructures.MoveToFrontListSpace;
using CSharp.DataStructures.QueueSpace;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace data_structures_csharpTests.Queue
{
    [TestClass()]
    public class DequeTests
    {
        private Deque<int> _deque = new Deque<int>();
        private readonly int[] _nodes = new int[] { 1, 4, 5, 2, 3, 6 };

        [TestInitialize, TestCleanup]
        protected void UnLoadInput()
        {
            _deque = new Deque<int>();
        }

        protected void LoadInput()
        {
            foreach (var node in _nodes)
            {
                _deque.AddLast(node);
            }
        }

        [TestMethod]
        public void PeekFirstEmptyTest()
        {
            var noElement = _deque.PeekFirst;
            const int expectedElement = default(int);
            Assert.AreEqual(expectedElement, noElement, "Could not peek the first element on an empty list");
        }

        [TestMethod]
        public void PeekFirstTest()
        {
            LoadInput();
            var element = _deque.PeekFirst;
            var expectedElement = _nodes[0];
            Assert.AreEqual(expectedElement, element, "Could not peek the first element on a list");
        }

        [TestMethod]
        public void PeekLastEmptyTest()
        {
            var noElement = _deque.PeekLast;
            const int expectedElement = default(int);
            Assert.AreEqual(expectedElement, noElement, "Could not peek the last element on an empty list");
        }

        [TestMethod]
        public void PeekLastTest()
        {
            LoadInput();
            var element = _deque.PeekLast;
            var expectedElement = _nodes[_nodes.Length - 1];
            Assert.AreEqual(expectedElement, element, "Could not peek the last element on a list");
        }

        [TestMethod]
        public void AddFirstTest()
        {
            LoadInput();
            const int element = 11;
            _deque.AddFirst(element);
            var peekedElement = _deque.PeekFirst;
            Assert.AreEqual(element, peekedElement, "Could not peek the first element on a list after AddFirst");
        }

        [TestMethod]
        public void AddFirstCountTest()
        {
            const int element = 11;
            _deque.AddFirst(element);
            const int expectedSize = 1;
            Assert.AreEqual(expectedSize, _deque.Count, "Size does not match 1 after one insert");
        }

        [TestMethod]
        public void AddLastTest()
        {
            LoadInput();
            const int element = 11;
            _deque.AddLast(element);
            var peekedElement = _deque.PeekLast;
            Assert.AreEqual(element, peekedElement, "Could not peek the last element on a list after AddLast");
        }

        [TestMethod]
        public void AddLastCountTest()
        {
            const int element = 11;
            _deque.AddLast(element);
            const int expectedSize = 1;
            Assert.AreEqual(expectedSize, _deque.Count, "Size does not match 1 after one insert");
        }

        [TestMethod]
        public void RemoveFirstEmptyTest()
        {
            const int expectedElement = default(int);
            Assert.AreEqual(expectedElement, _deque.RemoveFirst(), "Did not remove get the right value on an empty list with RemoveFirst");
        }

        [TestMethod]
        public void RemoveFirstTest()
        {
            LoadInput();
            const int expectedElement = 11;
            _deque.AddFirst(expectedElement);
            Assert.AreEqual(expectedElement, _deque.RemoveFirst(), "Did not remove get the right value on a list with RemoveFirst");
        }

        [TestMethod]
        public void RemoveLastEmptyTest()
        {
            const int expectedElement = default(int);
            Assert.AreEqual(expectedElement, _deque.RemoveLast(), "Did not remove get the right value on an empty list with RemoveLast");
        }

        [TestMethod]
        public void RemovelastTest()
        {
            LoadInput();
            const int expectedElement = 11;
            _deque.AddLast(expectedElement);
            Assert.AreEqual(expectedElement, _deque.RemoveLast(), "Did not remove get the right value on a list with RemoveLast");
        }
    }
}
