using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures.BinarySearchTreeSpace;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace DataStructures.BinarySearchTreeSpace.Tests
{
    [TestClass()]
    public class BinarySearchTreeTransposeTests : BinarySearchTreeTests
    {
        private BinarySearchTreeTranspose<int, int> binaryTreeTranspose = new BinarySearchTreeTranspose<int, int>();
        [TestMethod()]
        public void FindTestOnLeafNode()
        {
            UnLoadInput(binaryTreeTranspose);
            LoadInput(binaryTreeTranspose);
            int value;
            //Leaf Node
            bool actual = binaryTreeTranspose.Find(-6, out value);
            bool expected = true;
            int expectedValue = CalculateValue(-6);
            Assert.AreEqual(expected, actual, "Invalid result in finding a element with Leaf Node");
            Assert.AreEqual(expectedValue, value, "Invalid value in finding a element Leaf Node");

            actual = binaryTreeTranspose.ValidateTree();
            Assert.AreEqual(expected, actual, "Validation failed in transpose");
        }

        [TestMethod()]
        public void FindTestOnNodeWithOnlyLeftChild()
        {
            UnLoadInput(binaryTreeTranspose);
            LoadInput(binaryTreeTranspose);
            int input = -1;
            int value;
            //Leaf Node
            bool actual = binaryTreeTranspose.Find(input, out value);
            bool expected = true;
            int expectedValue = CalculateValue(input);
            Assert.AreEqual(expected, actual, "Invalid result in finding a element with one left child");
            Assert.AreEqual(expectedValue, value, "Invalid value in finding a element one left child");

            actual = binaryTreeTranspose.ValidateTree();
            Assert.AreEqual(expected, actual, "Validation failed in transpose with one left child");
        }

        [TestMethod()]
        public void FindTestOnNodeWithOnlyRightChild()
        {
            UnLoadInput(binaryTreeTranspose);
            LoadInput(binaryTreeTranspose);
            int input = 24;
            int value;
            //Leaf Node
            bool actual = binaryTreeTranspose.Find(input, out value);
            bool expected = true;
            int expectedValue = CalculateValue(input);
            Assert.AreEqual(expected, actual, "Invalid result in finding a element with one right child");
            Assert.AreEqual(expectedValue, value, "Invalid value in finding a element one right child");

            actual = binaryTreeTranspose.ValidateTree();
            Assert.AreEqual(expected, actual, "Validation failed in transpose with one right child");
        }

        [TestMethod()]
        public void FindTestOnNodeWithLeftandRightChild()
        {
            UnLoadInput(binaryTreeTranspose);
            LoadInput(binaryTreeTranspose);
            int input = 26;
            int value;
            //Leaf Node
            bool actual = binaryTreeTranspose.Find(input, out value);
            bool expected = true;
            int expectedValue = CalculateValue(input);
            Assert.AreEqual(expected, actual, "Invalid result in finding a element with left and right child");
            Assert.AreEqual(expectedValue, value, "Invalid value in finding a element with left and right child");

            actual = binaryTreeTranspose.ValidateTree();
            Assert.AreEqual(expected, actual, "Validation failed in transpose with left and right child");
        }

        [TestMethod()]
        public void FindTestOnRootNode()
        {
            UnLoadInput(binaryTreeTranspose);
            LoadInput(binaryTreeTranspose);
            int input = 10;
            int value;
            //Leaf Node
            bool actual = binaryTreeTranspose.Find(input, out value);
            bool expected = true;
            int expectedValue = CalculateValue(input);
            Assert.AreEqual(expected, actual, "Invalid result in finding a element with root node");
            Assert.AreEqual(expectedValue, value, "Invalid value in finding a element with root node");

            actual = binaryTreeTranspose.ValidateTree();
            Assert.AreEqual(expected, actual, "Validation failed in transpose with root node");
        }

        [TestMethod()]
        public void FindTestWithNoNode()
        {
            UnLoadInput(binaryTreeTranspose);
            int input = -1;
            int value;
            //Leaf Node
            bool actual = binaryTreeTranspose.Find(input, out value);
            bool expected = false;
            Assert.AreEqual(expected, actual, "Invalid result in finding a element a element without any nodes");

            actual = binaryTreeTranspose.ValidateTree();
            expected = true;
            Assert.AreEqual(expected, actual, "Validation failed in transpose without any nodes");
        }


        [TestMethod()]
        public void FindTestWithInvalidValue()
        {
            UnLoadInput(binaryTreeTranspose);
            int input = 55;
            int value;
            //Leaf Node
            bool actual = binaryTreeTranspose.Find(input, out value);
            bool expected = false;
            Assert.AreEqual(expected, actual, "Invalid result in finding a element a element with invalid value");

            actual = binaryTreeTranspose.ValidateTree();
            expected = true;
            Assert.AreEqual(expected, actual, "Validation failed in transpose with invalid value");
        }
    }
}
