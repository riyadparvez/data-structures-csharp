using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp.DataStructures.AdjacencyList.Tests
{
    [TestClass()]
    public class AdjacencyListTests
    {
        private AdjacencyList<int> _adjacencyList = new AdjacencyList<int>();

        private readonly int[] _nodes = new int[] { 1, 2, 3, 4, 5, 6 };
        //private Dictionary<int,HashSet<int>> _dictHashSet = new Dictionary<int, HashSet<int>>();

        [TestInitialize, TestCleanup]
        protected void UnLoadInput()
        {
            _adjacencyList = new AdjacencyList<int>();
        }

        protected void LoadInput()
        {
            //first ensure all the dict are initialized
            foreach (var node in _nodes)
            {
                _adjacencyList.AddVertex(node);
            }
            //make a link between nodes that have summed values that mod 3 are equals to 0
            foreach (var node in _nodes)
            {
                foreach (var innerNode in _nodes)
                {
                    //when node + innerNode is divisible by three add a link between them
                    //e.g. 1 will have edges with 2 and 5
                    if ((node + innerNode) % 3 != 0 || node == innerNode) continue;
                    _adjacencyList.AddEdge(node, innerNode);
                    _adjacencyList.AddEdge(innerNode, node);
                }
            }
        }
        [TestMethod]
        public void NoVertexTest()
        {
            const int numberOfNodes = 0;
            Assert.AreEqual(numberOfNodes, _adjacencyList.Vertices.Count, "Number of nodes different than 0 on an empty list");
        }

        [TestMethod]
        public void AddSingleVertexTest()
        {
            const int node = 1;
            _adjacencyList.AddVertex(node);
            const int numberOfNodes = 1;
            Assert.AreEqual(numberOfNodes, _adjacencyList.Vertices.Count, "Number of nodes different than 1 on a list with one node");
        }

        [TestMethod]
        public void AddMultipleVertexesTest()
        {
            LoadInput();
            var numberOfNodes = _nodes.Length;
            Assert.AreEqual(numberOfNodes, _adjacencyList.Vertices.Count, "Number of nodes does not match the expected number");
        }

        [TestMethod]
        public void AddEdgeNoNodeTest()
        {
            //test with one node, no edges
            const int expectedEdgeCount = 0;
            var nodeEdgeCount = _adjacencyList.GetNeighbours(int.MinValue).Count();
            Assert.AreEqual(expectedEdgeCount, nodeEdgeCount, "Number of edges different from 0 on a list without edges");
        }

        [TestMethod]
        public void AddEdgeOutOfRangeTest()
        {
            //test with one node, no edges
            const int node = 1;
            _adjacencyList.AddVertex(node);
            const int expectedEdgeCount = 0;
            var nodeEdgeCount = _adjacencyList.GetEdgeList().Count(tuple => tuple.Item1 == node);
            Assert.AreEqual(expectedEdgeCount, nodeEdgeCount, "Number of edges different from 0 on a list without edges");
        }

        [TestMethod]
        public void AddDoubleEdgeTest()
        {
        //test with two nodes, two edges
            const int firstNode = 1;
            const int secondNode = 2;
            _adjacencyList.AddVertex(firstNode);
            _adjacencyList.AddVertex(secondNode);
            _adjacencyList.AddEdge(firstNode, secondNode);
            _adjacencyList.AddEdge(secondNode, firstNode);
            const int expectedNumberOfEdges = 1;
            var abc = _adjacencyList.GetEdgeList();
            var firstNodeEdgeCount = _adjacencyList.GetEdgeList().Count(tuple => tuple.Item1 == firstNode);
            var secondNodeEdgeCount = _adjacencyList.GetEdgeList().Count(tuple => tuple.Item1 == secondNode);

            Assert.AreEqual(expectedNumberOfEdges, firstNodeEdgeCount, "Number of edges different than one for the first node");
            Assert.AreEqual(expectedNumberOfEdges, secondNodeEdgeCount, "Number of edges different than one for the second node");
        }

        [TestMethod]
        public void AddSeveralEdgesTest()
        {
            LoadInput();
            const int node = 1;
            const int expectedNumberOfEdgesForNode1 = 2;
            var nodeEdgeCount = _adjacencyList.GetEdgeList().Count(tuple => tuple.Item1 == node);
            Assert.AreEqual(expectedNumberOfEdgesForNode1, nodeEdgeCount,
                "Number of edges for node 1 do not match the expected value");
        }

        [TestMethod]
        public void IsNeighbourOfNotConnectedNodeTest()
        {
            const int node = 1;
            _adjacencyList.AddVertex(node);
            const int notConnectedNode = 2;
            var isNeigthbour = _adjacencyList.IsNeighbourOf(node, notConnectedNode);
            Assert.IsFalse(isNeigthbour, "Two nodes that were connected are shown as connected");
        }

        [TestMethod]
        public void IsNeighbourOfConnectedNodeTest()
        {
            LoadInput();
            const int firstNode = 1;
            const int secondNode = 2;
            var isNeigthbour = _adjacencyList.IsNeighbourOf(firstNode, secondNode);
            Assert.IsTrue(isNeigthbour, "Two nodes that are connected are shown as not connected");
        }

        [TestMethod]
        public void GetNeighboursCountEmptyTest()
        {
            const int node = 1;
            _adjacencyList.AddVertex(node);
            var neightbours = _adjacencyList.GetNeighbours(node);
            const int expectedNumberOfNeighbours = 0;
            Assert.AreEqual(expectedNumberOfNeighbours, neightbours.Count, "Number of neighbours different from 0 for a node without edges");
        }

        [TestMethod]
        public void GetNeighboursCountTest()
        {
            LoadInput();
            const int firstNode = 1;
            var neightbours = _adjacencyList.GetNeighbours(firstNode);
            const int expectedNumberOfNeighbours = 2;
            Assert.AreEqual(expectedNumberOfNeighbours, neightbours.Count, "Number of neighbours different from expected");
        }
    }
}
