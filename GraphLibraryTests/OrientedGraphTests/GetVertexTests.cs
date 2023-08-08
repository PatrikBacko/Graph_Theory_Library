using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Graphs.Exceptions;
using GraphLibraryTests.TestGraphs;

namespace GraphLibraryTests.OrientedGraphTests
{
    /// <summary>
    /// Tests for the GetVertex and GetVertcies methods.
    /// </summary>
    [TestClass]
    public class GetVertexTests
    {
        [TestMethod]
        public void GetVertexTest1()
        {
            // Arrange
            var graph = TestOrientedGraphs.ClearTestOrientedGraph;
            var vertex = new OrientedVertex("0");
            graph.AddVertex(vertex);

            // Act
            var result = graph.GetVertex("0");

            // Assert
            Assert.AreEqual(vertex, result);
        }

        [TestMethod]
        public void GetVertexTest2()
        {
            // Arrange
            var graph = TestOrientedGraphs.ClearTestOrientedGraph;
            var vertex = new OrientedVertex("0");
            graph.AddVertex(vertex);

            // Assert
            Assert.ThrowsException<VertexException>(() => graph.GetVertex("1"));
        }

        public void GetVerticesTest1()
        {
            // Arrange
            var graph = TestOrientedGraphs.VertexTestOrientedGraph1;
            var vertices = new List<OrientedVertex>()
            {
                new OrientedVertex("0"),
                new OrientedVertex("1"),
                new OrientedVertex("2")
            };

            // Act
            var result = graph.GetVertices();

            // Assert
            CollectionAssert.AreEqual(vertices, result.ToList());
        }

        public void GetVerticesTest2()
        {
            // Arrange
            var graph = TestOrientedGraphs.VertexTestOrientedGraph1;
            var vertices = new List<OrientedVertex>()
            {
                new OrientedVertex("2"),
                new OrientedVertex("0"),
                new OrientedVertex("1")
            };

            // Act
            var result = graph.GetVertices();

            // Assert
            CollectionAssert.AreEqual(vertices, result.ToList());
        }
    }
}
