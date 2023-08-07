using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests.OrientedGraphTests
{
    /// <summary>
    /// Tests for the GetOutAdjacentVertices and GetInAdjacementVertices methods 
    /// </summary>
    [TestClass]
    public class GetOutInVerticesTests
    {
        [TestMethod]
        public void GetOutVerticesTest1()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);

            // Act
            var expected = new List<OrientedVertex>();
            var actual = graph.GetOutAdjacentVertices("0").ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetOutVerticesTest2()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
            graph.AddEdge(new OrientedEdge("0", "1"));
            graph.AddEdge(new OrientedEdge("0", "2"));
            graph.AddEdge(new OrientedEdge("0", "4"));

            // Act
            var expected = new List<OrientedVertex>
            {
                graph.GetVertex("1"),
                graph.GetVertex("2"),
                graph.GetVertex("4"),
            };
            var actual = graph.GetOutAdjacentVertices("0").ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetInVerticesTest1()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);

            // Act
            var expected = new List<OrientedVertex>();
            var actual = graph.GetInAdjacentVertices("0").ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetInVerticesTest2()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
            graph.AddEdge(new OrientedEdge("1", "0"));
            graph.AddEdge(new OrientedEdge("2", "0"));
            graph.AddEdge(new OrientedEdge("4", "0"));

            // Act
            var expected = new List<OrientedVertex>
            {
                graph.GetVertex("1"),
                graph.GetVertex("2"),
                graph.GetVertex("4"),
            };
            var actual = graph.GetInAdjacentVertices("0").ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
