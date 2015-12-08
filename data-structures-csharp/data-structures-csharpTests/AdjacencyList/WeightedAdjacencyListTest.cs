using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp.DataStructures.AdjacencyList;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp.DataStructures.WeightedAdjacencyList.Tests
{
    [TestClass()]
    public class WeightedAdjacencyListTest
    {
        private WeightedAdjacencyList<string> _weightedAdjacencyList = new WeightedAdjacencyList<string>();

        private readonly string[] _nodes = new string[] { "1", "2", "3", "4","5", "6" };

        [TestInitialize, TestCleanup]
        protected void UnLoadInput()
        {
            _weightedAdjacencyList = new WeightedAdjacencyList<string>();
        }

        protected void LoadInput()
        {
            //first ensure all the dict are initialized
            foreach (var node in _nodes)
            {
                _weightedAdjacencyList.AddVertex(node);
            }
            //make a link between nodes that have summed values that mod 3 are equals to 0
            foreach (var node in _nodes)
            {
                foreach (var innerNode in _nodes)
                {
                    //when node + innerNode is divisible by three add a link between them
                    //e.g. 1 will have edges with 2 and 5
                    var nodeInt = int.Parse(node);
                    var innerNodeInt = int.Parse(innerNode);
                    if ((nodeInt + innerNodeInt) % 3 != 0 || nodeInt == innerNodeInt) continue;
                    _weightedAdjacencyList.AddEdge(node, innerNode,1);
                    _weightedAdjacencyList.AddEdge(innerNode, node,1);
                }
            }
        }
        [TestMethod]
        public void NoVertexTest()
        {
            const int numberOfNodes = 0;
            Assert.AreEqual(numberOfNodes, _weightedAdjacencyList.Vertices.Count, "Number of nodes different than 0 on an empty list");
        }

        [TestMethod]
        public void AddSingleVertexTest()
        {
            const string node = "1";
            _weightedAdjacencyList.AddVertex(node);
            const int numberOfNodes = 1;
            Assert.AreEqual(numberOfNodes, _weightedAdjacencyList.Vertices.Count, "Number of nodes different than 1 on a list with one node");
        }

        [TestMethod]
        public void AddMultipleVertexesTest()
        {
            LoadInput();
            var numberOfNodes = _nodes.Length;
            Assert.AreEqual(numberOfNodes, _weightedAdjacencyList.Vertices.Count, "Number of nodes does not match the expected number");
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void AddEdgeNoNodeTest()
        {
            //test with one node, no edges
            const int expectedEdgeCount = 0;
            var nodeEdgeCount = _weightedAdjacencyList.GetNeighbours(null).Count();
            Assert.AreEqual(expectedEdgeCount, nodeEdgeCount, "Number of edges different from 0 on a list without edges");
        }

        [TestMethod]
        public void AddEdgeNoNeighbourCountTest()
        {
            //test with one node, no edges
            const string node = "1";
            _weightedAdjacencyList.AddVertex(node);
            const int expectedEdgeCount = 0;
            var nodeEdgeCount = _weightedAdjacencyList.GetNeighbours(node).Count();
            Assert.AreEqual(expectedEdgeCount, nodeEdgeCount, "Number of edges different from 0 on a list without edges");
        }

        [TestMethod]
        public void AddEdgeNeighbourCountTest()
        {
            //test with one node, no edges
            const string firstNode = "1";
            const string secondNode = "2";
            const int weight = 1;
            _weightedAdjacencyList.AddVertex(firstNode);
            _weightedAdjacencyList.AddVertex(secondNode);
            _weightedAdjacencyList.AddEdge(firstNode, secondNode, weight);
            const int expectedEdgeCount = 1;
            var nodeEdgeCount = _weightedAdjacencyList.GetNeighbours(firstNode).Count();
            Assert.AreEqual(expectedEdgeCount, nodeEdgeCount, "Number of edges different from 1 on a list with one edge");
        }

        [TestMethod]
        public void AddDoubleEdgeNeighbourCountTest()
        {
            //test with two nodes, two edges
            const string firstNode = "1";
            const string secondNode = "2";
            const int weight = 1;
            const int doubleWeight = weight*2;
            _weightedAdjacencyList.AddVertex(firstNode);
            _weightedAdjacencyList.AddVertex(secondNode);
            _weightedAdjacencyList.AddEdge(firstNode, secondNode, weight);
            _weightedAdjacencyList.AddEdge(secondNode, firstNode, doubleWeight);
            const int expectedEdgeCount = 2;
            var nodeEdgeCount = _weightedAdjacencyList.GetNeighbours(firstNode).Count();

            Assert.AreEqual(expectedEdgeCount, nodeEdgeCount, "Number of edges different than one for the first node");
        }

        [TestMethod]
        public void AddSeveralEdgesTest()
        {
            LoadInput();
            const string node = "1";
            const int expectedNumberOfEdgesForNode1 = 2;
            var nodeEdgeCount = _weightedAdjacencyList.GetNeighbours(node).Count;
            Assert.AreEqual(expectedNumberOfEdgesForNode1, nodeEdgeCount,
                "Number of edges for node 1 do not match the expected value");
        }

        [TestMethod]
        public void AddEdgeNoNeighbourWeightTest()
        {
            //test with one node, no edges
            const string node = "1";
            _weightedAdjacencyList.AddVertex(node);
            const int expectedEdgeCount = 0;
            var nodeWeightCount = _weightedAdjacencyList.GetAllWeights(node).Count();
            Assert.AreEqual(expectedEdgeCount, nodeWeightCount, "Number of weigths different from 0 on a list without edges");
        }

        [TestMethod]
        public void AddEdgeNeighbourWeightTest()
        {
            //test with one node, no edges
            const string firstNode = "1";
            const string secondNode = "2";
            const int weight = 1;
            _weightedAdjacencyList.AddVertex(firstNode);
            _weightedAdjacencyList.AddVertex(secondNode);
            _weightedAdjacencyList.AddEdge(firstNode, secondNode, weight);
            const int expectedEdgeCount = 1;
            var nodeWeightCount = _weightedAdjacencyList.GetAllWeights(firstNode).Count();
            Assert.AreEqual(expectedEdgeCount, nodeWeightCount, "Number of weigths different from 1 on a list with one edge");
        }

        [TestMethod]
        public void AddEdgeNeighbourWeightComparisonTest()
        {
            //test with one node, no edges
            const string firstNode = "1";
            const string secondNode = "2";
            const Decimal weight = 1;
            _weightedAdjacencyList.AddVertex(firstNode);
            _weightedAdjacencyList.AddVertex(secondNode);
            _weightedAdjacencyList.AddEdge(firstNode, secondNode, (double)weight);
            const int expectedEdgeCount = 1;
            var nodeWeightCount = _weightedAdjacencyList.GetAllWeights(firstNode).Count(weig => weight == (Decimal)weig);
            Assert.AreEqual(expectedEdgeCount, nodeWeightCount, "Number of weigths different from 1 on a list with one edge");
        }

        [TestMethod]
        public void AddDoubleEdgeNeighbourWeightTest()
        {
            //test with two nodes, two edges
            const string firstNode = "1";
            const string secondNode = "2";
            const int weight = 1;
            const int doubleWeight = weight * 2;
            _weightedAdjacencyList.AddVertex(firstNode);
            _weightedAdjacencyList.AddVertex(secondNode);
            _weightedAdjacencyList.AddEdge(firstNode, secondNode, weight);
            _weightedAdjacencyList.AddEdge(secondNode, firstNode, doubleWeight);
            const int expectedWeightCount = 2;
            var nodeWeightCount = _weightedAdjacencyList.GetAllWeights(firstNode).Count();

            Assert.AreEqual(expectedWeightCount, nodeWeightCount, "Number of weights different than two for the first node");
        }

        [TestMethod]
        public void AddSeveralEdgesWeightTest()
        {
            LoadInput();
            const string node = "1";
            const int expectedNumberOfWeigthsForNode1 = 2;
            var nodeWeightCount = _weightedAdjacencyList.GetAllWeights(node).Count;
            Assert.AreEqual(expectedNumberOfWeigthsForNode1, nodeWeightCount,
                "Number of weights for node 1 do not match the expected value");
        }

        [TestMethod]
        public void IsNeighbourOfNotConnectedNodeTest()
        {
            const string node = "1";
            _weightedAdjacencyList.AddVertex(node);
            const string notConnectedNode = "2";
            const int weight = 1;
            var notConnectedEdge = new WeightedAdjacencyList<string>.Node<string>(notConnectedNode, weight);
            var isNeigthbour = _weightedAdjacencyList.IsNeighbourOf(node, notConnectedEdge);
            Assert.IsFalse(isNeigthbour, "Two nodes that were connected are shown as connected");
        }

        [TestMethod]
        public void IsNeighbourOfConnectedNodeTest()
        {
            LoadInput();
            const string firstNode = "1";
            const string secondNode = "2";
            const int weight = 1;
            _weightedAdjacencyList.AddVertex(firstNode);
            _weightedAdjacencyList.AddVertex(secondNode);
            var edges = _weightedAdjacencyList.AddEdge(firstNode, secondNode, weight);
            var isNeigthbourFirst = _weightedAdjacencyList.IsNeighbourOf(firstNode, edges[0]);
            var isNeigthbourSecond = _weightedAdjacencyList.IsNeighbourOf(secondNode, edges[1]);
            Assert.IsTrue(isNeigthbourFirst, "Two nodes that are connected are shown as not connected from firstNode to secondNode");
            Assert.IsTrue(isNeigthbourSecond, "Two nodes that are connected are shown as not connected from secondNode to firstNode");
        }

        [TestMethod]
        public void GetNeighboursCountEmptyTest()
        {
            const string node = "1";
            _weightedAdjacencyList.AddVertex(node);
            var neightbours = _weightedAdjacencyList.GetNeighbours(node);
            const int expectedNumberOfNeighbours = 0;
            Assert.AreEqual(expectedNumberOfNeighbours, neightbours.Count, "Number of neighbours different from 0 for a node without edges");
        }

        [TestMethod]
        public void GetNeighboursCountTest()
        {
            LoadInput();
            const string firstNode = "1";
            var neightbours = _weightedAdjacencyList.GetNeighbours(firstNode);
            const int expectedNumberOfNeighbours = 2;
            Assert.AreEqual(expectedNumberOfNeighbours, neightbours.Count, "Number of neighbours different from expected");
        }
    }
}
