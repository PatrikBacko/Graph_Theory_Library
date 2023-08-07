using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Graphs.Exceptions;

namespace GraphLibraryTests.OrientedGraphTests
{
    /// <summary>
    /// Tests for the IsEdge method and all its overloads.
    /// </summary>
    [TestClass]
    public class IsEdgeTests
    {
        [TestMethod]
        public void IsEdgeTest1()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(3);
            graph.AddEdge(new OrientedEdge("1", "2"));
            // Act
            var result = graph.IsEdge("1", "2");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsEdgeTest2()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(3);
            var edge = new OrientedEdge("1", "2");
            graph.AddEdge(edge);
            // Act
            var result = graph.IsEdge(edge);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsEdgeTest3()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(3);
            var edge = new OrientedEdge("1", "2");
            graph.AddEdge(new OrientedEdge("1", "2"));
            // Act
            var result = graph.IsEdge(edge);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsEdgeTest4()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(3);
            var edge = new OrientedEdge("1", "0");
            graph.AddEdge(edge);
            // Act
            var result = graph.IsEdge("1", "2");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsEdgeTest5()
        {
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(3);

            Assert.ThrowsException<VertexException>(() => graph.IsEdge("1", "5"));
        }
    }
}
