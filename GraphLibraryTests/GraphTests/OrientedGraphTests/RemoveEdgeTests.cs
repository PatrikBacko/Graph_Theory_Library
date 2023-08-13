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
    /// Tests for removing edges from graph
    /// 
    /// methods and all its overloads:
    /// - RemoveEdge
    /// - RemoveEdges
    /// </summary>
    [TestClass]
    public class RemoveEdgeTests
    {
        [TestMethod]
        public void RemoveEdgeTest1()

        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
            var edge = new OrientedEdge("0", "1");
            graph.AddEdge(edge);
            graph.RemoveEdge(edge);

            Assert.IsFalse(graph.IsEdge(edge));
            Assert.IsFalse(graph.IsEdge(edge.VertexOut, edge.VertexIn));
            Assert.IsFalse(graph.IsEdge("0", "1"));
        }

        [TestMethod]
        public void RemoveEdgeTest2()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
            var edge = new OrientedEdge("0", "1");
            graph.AddEdge(edge);
            graph.RemoveEdge(edge);

            Assert.ThrowsException<EdgeException>(() => graph.RemoveEdge(edge));
        }

        [TestMethod]
        public void RemoveEdgeTest3()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
            var edge = new OrientedEdge("0", "1");
            graph.AddEdge(edge);
            graph.RemoveEdge(edge);

            Assert.ThrowsException<EdgeException>(() => graph.GetEdge("0", "1"));
        }

        [TestMethod]
        public void RemoveEdgeTest4()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
            var edge = new OrientedEdge("0", "1");

            Assert.ThrowsException<EdgeException>(() => graph.RemoveEdge("0", "1"));
        }

        [TestMethod]
        public void RemoveEdgeTest5()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
            var edge = new OrientedEdge("0", "1");

            Assert.ThrowsException<EdgeException>(() => graph.RemoveEdge("0", "10"));
        }

        [TestMethod]
        public void RemoveEdgeTest6()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
            var edge1 = new OrientedEdge("0", "1");
            var edge2 = new OrientedEdge("1", "4");
            var edge3 = new OrientedEdge("3", "2");
            var edge4 = new OrientedEdge("2", "3");

            graph.AddEdge(edge1).AddEdge(edge2).AddEdge(edge3).AddEdge(edge4);

            var expected = new List<OrientedEdge>() { edge1, edge4 };
            var actual = graph.RemoveEdge(edge2).RemoveEdge("3", "2").GetEdges().ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod] public void RemoveEdgeTest7()
        {
            // Arrange
            var graph1 = TestGraphs.TestOrientedGraphs.GetVertexTestOrientedGraph(3);
			var graph2 = TestGraphs.TestOrientedGraphs.GetVertexTestOrientedGraph(3);
            var edge = new OrientedEdge("0", "1");

            // Act
            graph1.AddEdge(edge);
            graph1.RemoveEdge(edge);
            graph2.AddEdge(edge);

            // Assert
            Assert.IsFalse(graph1.IsEdge(edge));
            Assert.IsTrue(graph2.IsEdge(edge));
            Assert.ThrowsException<EdgeException>(() => graph1.AddEdge(edge));
		}

		[TestMethod]
        public void RemoveEdgesTest1()
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
            graph.RemoveEdges(edges);
            var expected = new List<OrientedEdge>();
            var actual = graph.GetEdges().ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveEdgesTest2()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.GetVertexTestOrientedGraph(10);
            var edges = new List<(VertexName, VertexName)>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i != j)
                    {
                        graph.AddEdge(new OrientedEdge(i.ToString(), j.ToString()));
                        edges.Add((i.ToString(), j.ToString()));
                    }
                }
            }
            graph.RemoveEdges(edges);
            var expected = new List<OrientedEdge>();
            var actual = graph.GetEdges().ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveEdgesTest3()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
            var edge1 = new OrientedEdge("0", "1");
            var edge2 = new OrientedEdge("1", "4");
            var edge3 = new OrientedEdge("3", "2");
            var edge4 = new OrientedEdge("2", "3");

            graph.AddEdge(edge1).AddEdge(edge2).AddEdge(edge3).AddEdge(edge4);
            var edges = new List<OrientedEdge>() { edge2, edge3 };

            var expected = new List<OrientedEdge>() { edge1, edge4 };
            var actual = graph.RemoveEdges(edges).GetEdges().ToList();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
