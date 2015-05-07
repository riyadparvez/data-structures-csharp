using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.MoveToFrontListSpace.Tests
{
    [TestClass()]
    public class MoveToFrontListTests
    {
        private MoveToFrontList<int> _moveToFrontList = new MoveToFrontList<int>();
        private readonly int[] _nodes = new int[] { 1, 4, 5, 2, 3, 6 };

        [TestInitialize, TestCleanup]
        protected void UnLoadInput()
        {
            _moveToFrontList = new MoveToFrontList<int>();
        }

        protected void LoadInput()
        {
            foreach (var node in _nodes)
            {
                _moveToFrontList.Add(node);
            }
        }

        [TestMethod]
        public void AddElementTest()
        {
            const int elementToAdd = 1;
            _moveToFrontList.Add(elementToAdd);
            const int expectedSize = 1;
            Assert.AreEqual(expectedSize, _moveToFrontList.Count, "Size do not match the expected value after one insert");
        }

        [TestMethod]
        public void AddSameElementTest()
        {
            const int elementToAdd = 1;
            _moveToFrontList.Add(elementToAdd);
            _moveToFrontList.Add(elementToAdd);
            const int expectedSize = 2;
            Assert.AreEqual(expectedSize, _moveToFrontList.Count, "Size do not match the expected value after double insert");
        }

        [TestMethod]
        public void AddElementsFrontTest()
        {
            for (var i = _nodes.Length - 1; i >= 0; i--)
            {
                _moveToFrontList.Add(_nodes[i]);
            }
            const int firstIndexToCheck = 0;
            const int secondIndexToCheck = 1;

            var firstNodeValue = _nodes[firstIndexToCheck];
            var secondNodeValue = _nodes[secondIndexToCheck];

            var myArray = new int[_moveToFrontList.Count];
            _moveToFrontList.CopyTo(myArray, 0);

            var firstValue = myArray[_nodes.Length - 1 - firstIndexToCheck];
            var secondValue = myArray[_nodes.Length - 1 - secondIndexToCheck];

            Assert.AreEqual(firstNodeValue,firstValue, "Values not brough to the front(first element)");
            Assert.AreEqual(secondNodeValue, secondValue, "Values not brough to the front(second element)");
        }

        [TestMethod]
        public void ClearTest()
        {
            LoadInput();
            _moveToFrontList.Clear();
            const int expectedCount = 0;
            Assert.AreEqual(expectedCount, _moveToFrontList.Count, "Cleared list has size <> 0");
        }

        [TestMethod]
        public void ContainTest()
        {
            const int elementToAdd = 1;
            _moveToFrontList.Add(elementToAdd);
            Assert.IsTrue(_moveToFrontList.Contains(elementToAdd), "List does not contain the added element");
        }

        [TestMethod]
        public void RemoveTest()
        {
            const int element = 1;
            _moveToFrontList.Add(element);
            _moveToFrontList.Remove(element);
            Assert.IsFalse(_moveToFrontList.Contains(element), "List still contains the rimoved element");
        }


        [TestMethod]
        public void RemoveNonAddedTest()
        {
            const int addedElement = 1;
            _moveToFrontList.Add(addedElement);
            const int toRemoveElement = 2;
            const int expectedSize = 1;
            _moveToFrontList.Remove(toRemoveElement);
            Assert.AreEqual(expectedSize, _moveToFrontList.Count, "Removing a non-present element makes the count non correct");
        }

        

    }
}
