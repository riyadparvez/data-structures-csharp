using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures.ListSpace;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.SortedList.Tests
{
    [TestClass()]
    public class SortedListTests
    {
        private SortedList<int> _sortedList = new SortedList<int>();
        private readonly int[] _nodes = new int[] { 1, 2, 3, 4, 5, 6 };

        [TestInitialize, TestCleanup]
        protected void UnLoadInput()
        {
            _sortedList = _sortedList = new SortedList<int>();
        }

        protected void LoadInput()
        {
            foreach (var node in _nodes)
            {
                _sortedList.Add(node);
            }
        }

        [TestMethod]
        public void AddElementTest()
        {
            const int elementToAdd = 1;
            _sortedList.Add(elementToAdd);
            const int expectedSize = 1;
            Assert.AreEqual(expectedSize, _sortedList.Count, "Size do not match the expected value after one insert");
        }

        [TestMethod]
        public void AddSameElementTest()
        {
            const int elementToAdd = 1;
            _sortedList.Add(elementToAdd);
            _sortedList.Add(elementToAdd);
            const int expectedSize = 2;
            Assert.AreEqual(expectedSize, _sortedList.Count, "Size do not match the expected value after double insert");
        }

        [TestMethod]
        public void AddElementsOrderedTest()
        {
            for (var i = _nodes.Length - 1; i >= 0; i--)
            {
                _sortedList.Add(i);
            }
            const int firstIndexToCheck = 0;
            const int secondIndexToCheck = 1;
            var size = _sortedList.Count;
            var firstElement = _sortedList.ElementAt(firstIndexToCheck);
            var secondElement = _sortedList.ElementAt(secondIndexToCheck);

            Assert.IsTrue(firstElement <= secondElement, "List not sorted");
        }

        [TestMethod]
        public void ClearTest()
        {
            LoadInput();
            _sortedList.Clear();
            const int expectedCount = 0;
            Assert.AreEqual(expectedCount, _sortedList.Count, "Cleared list has size <> 0");
        }

        [TestMethod]
        public void ContainTest()
        {
            const int elementToAdd = 1;
            _sortedList.Add(elementToAdd);
            Assert.IsTrue(_sortedList.Contains(elementToAdd), "List does not contain the added element");
        }

        [TestMethod]
        public void RemoveTest()
        {
            const int element = 1;
            _sortedList.Add(element);
            _sortedList.Remove(element);
            Assert.IsFalse(_sortedList.Contains(element), "List still contains the rimoved element");
        }


        [TestMethod]
        public void RemoveNonAddedTest()
        {
            const int addedElement = 1;
            _sortedList.Add(addedElement);
            const int toRemoveElement = 2;
            const int expectedSize = 1;
            _sortedList.Remove(toRemoveElement);
            Assert.AreEqual(expectedSize, _sortedList.Count, "Removing a non-present element makes the count non correct");
        }
    }
}
