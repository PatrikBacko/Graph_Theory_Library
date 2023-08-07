using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests.OrientedGraphTests
{
    /// <summary>
    ///	Tests for the GetOutEdges and GetInEdges methods
    /// </summary>
    [TestClass]
    public class GetOutInEdgesTests
    {
        [TestMethod]
        public void GetOutEdgesTest1()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
            graph.AddEdge(new OrientedEdge("1", "0"));

            // Act
            var expected = new List<OrientedEdge>();
            var actual = graph.GetOutEdges("0").ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetOutEdgesTest2()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
            graph.AddEdge(new OrientedEdge("0", "1"));
            graph.AddEdge(new OrientedEdge("0", "2"));
            graph.AddEdge(new OrientedEdge("0", "4"));
            graph.AddEdge(new OrientedEdge("1", "0"));

            // Act
            var expected = new List<OrientedEdge>()
            {
                graph.GetEdge("0", "1"),
                graph.GetEdge("0", "2"),
                graph.GetEdge("0", "4"),
            };
            var actual = graph.GetOutEdges("0").ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetInEdgesTest1()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
            graph.AddEdge(new OrientedEdge("0", "1"));

            // Act
            var expected = new List<OrientedEdge>();
            var actual = graph.GetInEdges("0").ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetInEdgesTest2()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
            graph.AddEdge(new OrientedEdge("1", "0"));
            graph.AddEdge(new OrientedEdge("2", "0"));
            graph.AddEdge(new OrientedEdge("4", "0"));
            graph.AddEdge(new OrientedEdge("0", "1"));

            // Act
            var expected = new List<OrientedEdge>()
            {
                graph.GetEdge("1", "0"),
                graph.GetEdge("2", "0"),
                graph.GetEdge("4", "0"),
            };
            var actual = graph.GetInEdges("0").ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
