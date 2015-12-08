using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp.DataStructures.BinarySearchTreeSpace;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.Contracts;

namespace CSharp.DataStructures.BinarySearchTreeSpace.Tests
{
    [TestClass()]
    public class BinarySearchTreeTests
    {
        private BinarySearchTree<int, int> binaryTree = new BinarySearchTree<int, int>();
        private int existingKey;
        private int existingValue;
        private int nonExistingKey;
        private int[] input = new int[] { 10, 0, 2, 26, -1, -6, 1, 24, 25, 44, 100 };

        protected void UnLoadInput(BinarySearchTree<int, int> binaryTree)
        {
            binaryTree = new BinarySearchTree<int, int>();
        }

        protected int CalculateValue(int i)
        {
            return (i * 10) / 5;
        }

        protected void LoadInput(BinarySearchTree<int, int> binaryTree)
        {
            Contract.Assert(binaryTree != null, "Tree cannot be null"); Random random = new Random();

            foreach (int i in input)
            {
                var value = CalculateValue(i);
                binaryTree.Add(i, value);
                if (i == 1)
                {
                    existingKey = 1;
                    existingValue = value;
                }
            }
            nonExistingKey = 99;

        }

        [TestMethod()]
        public void FindTest()
        {
            int value = 0;
            int key = 0;
            int expectedValue = 0;
            int actualValue = value;

            //Find value with out any nodes
            UnLoadInput(binaryTree);
            bool actual = binaryTree.Find(key, out actualValue);
            bool expected = false;
            Assert.AreEqual(expected, actual, "Invalid result in finding a element without any nodes");
            Assert.AreEqual(expectedValue, actualValue, "Invalid value in finding a element without any nodes");

            //Find the existing value with nodes
            LoadInput(binaryTree);
            actual = binaryTree.Find(existingKey, out actualValue);
            expected = true;
            expectedValue = existingValue;
            Assert.AreEqual(expected, actual, "Invalid result in finding a element with many nodes");
            Assert.AreEqual(expectedValue, actualValue, "Invalid value in finding a element with many nodes");

            //Find the nonexisting value with nodes
            actual = binaryTree.Find(nonExistingKey, out actualValue);
            expected = false;
            expectedValue = default(int);
            Assert.AreEqual(expected, actual, "Invalid result in finding a nonexisting element with many nodes");
            Assert.AreEqual(expectedValue, actualValue, "Invalid value in finding a nonexisting element with many nodes");

        }

        [TestMethod()]
        public void AddTest()
        {
            int value = 0;
            int key = 0;
            int expectedValue = 0;
            int actualValue = value;

            //Add first element
            UnLoadInput(binaryTree);
            bool actual = binaryTree.Add(key, value);
            bool expected = true;
            expectedValue = value;
            int actualCount = binaryTree.Count();
            int expectedCount = 1;
            Assert.IsTrue(binaryTree.Find(key, out actualValue), "Coundn't find the element after adding the first element");
            Assert.AreEqual(expected, actual, "Invalid result in adding first element");
            Assert.AreEqual(expectedValue, actualValue, "Invalid value in adding first element");
            Assert.AreEqual(expectedCount, actualCount, "Invalid count in adding first element");

            //Add with many data
            LoadInput(binaryTree);
            if (binaryTree.Find(key, out actualValue))   //Already existing key wont be added once again
            {
                expectedCount = binaryTree.Count;
            }
            else
            {
                expectedCount = binaryTree.Count + 1;
            }
            actual = binaryTree.Add(key, value);
            expected = true;
            expectedValue = value;
            actualCount = binaryTree.Count();
            Assert.IsTrue(binaryTree.Find(key, out actualValue), "Coundn't find the element after adding the elements");
            Assert.AreEqual(expected, actual, "Invalid result in adding first element");
            Assert.AreEqual(expectedValue, actualValue, "Invalid value in adding first element");
            Assert.AreEqual(expectedCount, actualCount, "Invalid count in adding first element");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PredecessorTestWithNoData()
        {
            //Finding predecessor with no data
            UnLoadInput(binaryTree);
            binaryTree.Predecessor(0);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PredecessorTestOnLeafNode()
        {
            //Finding predecessor with no data
            LoadInput(binaryTree);
            binaryTree.Predecessor(-6);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PredecessorTestOnNodeWithNoPredecessors()
        {
            //Finding predecessor with no data
            LoadInput(binaryTree);
            binaryTree.Predecessor(24);
        }

        [TestMethod()]
        public void PredecessorTest()
        {
            LoadInput(binaryTree);
            //Finding the node with predecessor
            var node = binaryTree.Predecessor(0);
            int expected = -1;
            Assert.IsNotNull(node, "Failed in identifying the predecessor");
            Assert.AreEqual(expected, node.Key, "Didn't identify the exact predecessor");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SuccessorTestWithNoData()
        {
            //Finding predecessor with no data
            UnLoadInput(binaryTree);
            binaryTree.Successor(0);
        }

        [TestMethod()]
        public void SuccessorTest()
        {
            LoadInput(binaryTree);
            //Finding the node with predecessor
            var node = binaryTree.Successor(0);
            int expected = 1;
            Assert.IsNotNull(node, "Failed in identifying the successor");
            Assert.AreEqual(expected, node.Key, "Didn't identify the exact successor");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SuccessorTestOnLeafNode()
        {
            //Finding predecessor with no data
            LoadInput(binaryTree);
            binaryTree.Successor(-6);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SuccessorTestOnNodeWithNoPredecessors()
        {
            //Finding predecessor with no data
            LoadInput(binaryTree);
            binaryTree.Successor(2);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Random random = new Random();
            int count = 0;

            //Removing a nonexisting Item
            bool actual = binaryTree.Remove(nonExistingKey);
            bool expected = false;
            Assert.AreEqual(expected, actual, "Removing non existing item failed.");

            //Test for the existing node
            if (binaryTree.Count == 0)
            {
                LoadInput(binaryTree);
            }
            //Removing a existing Item
            count = binaryTree.Count();
            int value;
            binaryTree.Find(existingKey, out value);
            actual = binaryTree.Remove(existingKey);
            int actualCount = binaryTree.Count();
            expected = true;
            int expectedCount = count - 1;
            Assert.AreEqual(expected, actual, "Removing existing item failed.");
            Assert.AreEqual(expectedCount, actualCount, "Removing existibg item - Count mismatch");

            if (binaryTree.Find(existingKey, out value))
            {
                Assert.Fail("Node still exists after removing.");
            }
        }

        [TestMethod()]
        public void GetAllNodesTest()
        {
            //With no data
            UnLoadInput(binaryTree);
            IList<KeyValuePair<int, int>> returnedValues = binaryTree.GetAllNodes();
            int expectedCount = 0;
            Assert.AreEqual(expectedCount, returnedValues.Count, "Returning values when no data is in the tree");

            //With Data
            LoadInput(binaryTree);
            returnedValues = binaryTree.GetAllNodes();
            expectedCount = input.Distinct().Count();
            Assert.AreEqual(expectedCount, returnedValues.Count, "Not returning exact number when data is in the tree");
        }

        /// <summary>
        /// Test Method for the range
        /// </summary>
        [TestMethod()]
        public void GetAllNodesTestWithRange()
        {
            //With no data
            UnLoadInput(binaryTree);
            IList<KeyValuePair<int, int>> returnedValues = binaryTree.GetAllNodes(0, 100);
            int expectedCount = 0;
            Assert.AreEqual(expectedCount, returnedValues.Count, "Returning values when no data is in the tree");

            //With data
            LoadInput(binaryTree);
            int min = -6; //Minimum of all values tree
            int max = 100; //Max of all values in tree
            returnedValues = binaryTree.GetAllNodes(min, max);
            expectedCount = input.Distinct().Count();
            Assert.AreEqual(expectedCount, returnedValues.Count, "Returning invalid values count in range");

            int start = 0;
            int end = 48;
            returnedValues = binaryTree.GetAllNodes(start, end);
            expectedCount = input.Where(x => x.CompareTo(start) >= 0 && x.CompareTo(end) <= 0).Distinct().Count();
            Assert.AreEqual(expectedCount, returnedValues.Count, "Returning invalid values count in range");

        }

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            //With no data
            UnLoadInput(binaryTree);
            var node = binaryTree.GetEnumerator();
            Assert.IsNotNull(node, "Null is returned when no data");

            LoadInput(binaryTree);
            node = binaryTree.GetEnumerator();
            Assert.IsNotNull(node, "Null is returned when data");
        }

        [TestMethod()]
        public void ValidateTreeTest()
        {
            bool actual = binaryTree.ValidateTree();
            bool expected = true;
            Assert.AreEqual(expected, actual, "Validation of Tree failed");
        }

        [TestMethod()]
        public void GetAllNodesInPreOrderTest()
        {
            //With no data
            UnLoadInput(binaryTree);
            IList<KeyValuePair<int, int>> returnedValues = binaryTree.GetAllNodes();
            int expectedCount = 0;
            Assert.AreEqual(expectedCount, returnedValues.Count, "Returning values when no data is in the tree");

            //With Data
            LoadInput(binaryTree);
            returnedValues = binaryTree.GetAllNodes();
            expectedCount = input.Distinct().Count();
            Assert.AreEqual(expectedCount, returnedValues.Count, "Not returning exact number when data is in the tree");
        }
    }
}
