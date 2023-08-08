using GraphLibrary.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests.OrientedGraphTests
{
    /// <summary>
    /// Tests for the IsVertex method and all its overloads.
    /// </summary>
    [TestClass]
    public class IsVertexTests
    {
        [TestMethod]
        public void IsVertexTest1()
        {
            // Arrange
            var graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
            graph.AddVertex(new OrientedVertex("1"));

            // Act
            var result = graph.IsVertex("1");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsVertexTest2()
        {
            // Arrange
            var graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
            var vertex = new OrientedVertex("1");
            graph.AddVertex(vertex);

            // Act
            var result = graph.IsVertex(vertex);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsVertexTest3()
        {
            // Arrange
            var graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
            var vertex = new OrientedVertex("1");
            graph.AddVertex(new OrientedVertex("1"));

            // Act
            var result = graph.IsVertex(vertex);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsVertexTest4()
        {
            // Arrange
            var graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
            var vertex = new OrientedVertex("2");
            graph.AddVertex(new OrientedVertex("1"));

            // Act
            var result = graph.IsVertex(vertex);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsVertexTest5()
        {
            // Arrange
            var graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
            graph.AddVertex(new OrientedVertex("1"));

            // Act
            var result = graph.IsVertex("2");

            // Assert
            Assert.IsFalse(result);
        }
    }
}
