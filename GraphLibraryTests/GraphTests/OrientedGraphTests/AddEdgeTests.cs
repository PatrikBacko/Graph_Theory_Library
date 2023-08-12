using GraphLibrary.Graphs.Exceptions;
using GraphLibraryTests.TestGraphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests.GraphTests.OrientedGraphTests
{
    /// <summary>
    /// Tests for adding edges to graph
    /// 
    /// methods and all its overloads:
    /// - AddEdge
    /// - AddEdges
    /// </summary>
    [TestClass]
    public class AddEdgeTests
    {
        [TestMethod]
        public void AddEdgeTest1()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.VertexTestOrientedGraph1;

            var edge = new OrientedEdge("0", "1");
            graph.AddEdge(edge);

            Assert.IsTrue(graph.IsEdge(edge));
            Assert.IsTrue(graph.IsEdge(edge.VertexOut, edge.VertexIn));
            Assert.IsTrue(graph.IsEdge("0", "1"));

            Assert.AreEqual(graph.GetEdge(edge.VertexOut, edge.VertexIn), edge);
            Assert.AreEqual(graph.GetEdge("0", "1"), edge);
        }

        [TestMethod]
        public void AddEdgeTest2()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.VertexTestOrientedGraph1;

            var edge = new OrientedEdge("0", "1");
            graph.AddEdge(edge);
            Assert.ThrowsException<EdgeException>(() => graph.AddEdge(edge));
        }

        [TestMethod]
        public void AddEdgeTest3()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.VertexTestOrientedGraph1;

            var edge = new OrientedEdge("0", "1");
            graph.AddEdge(edge);

            Assert.ThrowsException<EdgeException>(() => graph.AddEdge(new OrientedEdge("0", "1")));
        }

        [TestMethod]
        public void AddEdgeTest4()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.VertexTestOrientedGraph1;

            var edge = new OrientedEdge("0", "4");

            Assert.ThrowsException<EdgeException>(() => graph.AddEdge(edge));
        }
        [TestMethod]
        public void AddEdgeTest5()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.VertexTestOrientedGraph1;

            var edge = new OrientedEdge("0", "0");

            Assert.ThrowsException<EdgeException>(() => graph.AddEdge(edge));
        }

        [TestMethod]
        public void AddEdgesTest1()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.GetVertexTestOrientedGraph(10);
            var edges = new List<OrientedEdge>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i != j)
                        edges.Add(new OrientedEdge(i.ToString(), j.ToString()));
                }
            }
            graph.AddEdges(edges);
            var expected = edges;
            var actual = graph.GetEdges().ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddEdgesTest2()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
            var edges = new List<OrientedEdge>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i != j)
                        edges.Add(new OrientedEdge(i.ToString(), j.ToString()));
                }
            }
            graph.AddEdges(edges);

            Assert.IsTrue(graph.IsEdge("0", "1"));
            Assert.IsTrue(graph.IsEdge("2", "4"));
            Assert.IsTrue(graph.IsEdge("4", "3"));
        }

        [TestMethod]
        public void AddEdgesTest3()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.GetVertexTestOrientedGraph(10);
            var edges = new List<OrientedEdge>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    edges.Add(new OrientedEdge(i.ToString(), j.ToString()));
                }
            }
            Assert.ThrowsException<EdgeException>(() => graph.AddEdges(edges));
        }
    }
}
