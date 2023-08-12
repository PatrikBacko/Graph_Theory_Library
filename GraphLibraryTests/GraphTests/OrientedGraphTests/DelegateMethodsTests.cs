using GraphLibraryTests.TestGraphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests.GraphTests.OrientedGraphTests
{
    /// <summary>
    /// <b> Tests for methods which use delegates. </b> <br />
    /// <br />
    /// 
    /// <b> Methods: </b> <br />
    /// - GetVerticesWith <br />
    /// - GetEdgesWith <br />
    /// - ApplyToVertices <br />
    /// - ApplyToEdges <br />
    /// - ApplyToVerticesWith <br />
    /// - ApplyToEdgesWith <br />
    /// - RemoveVerticesWith <br />
    /// - RemoveEdgesWith <br />
    /// <br />
    /// 
    /// <b> Delegates: </b>  <br />
    /// - VertexPredicate <br />
    /// - EdgePredicate <br />
    /// - VertexAction <br />
    /// - EdgeAction <br />
    /// </summary>
    [TestClass]
    public class DelegateMethodsTests
    {
        [TestMethod]
        public void GetVerticesWithTest1()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(10);

            // Act
            var expected = graph.GetVertices().Where(v => v.Name == "0").ToList();
            var actual = graph.GetVerticesWith(v => v.Name == "0").ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetVerticesWithTest2()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetPathTestOrientedGraph(10);

            // Act
            var expected = graph.GetVertices().Where(v => graph.GetOutDegree(v.Name) > 0).ToList();
            var actual = graph.GetVerticesWith(v => graph.GetOutDegree(v.Name) > 0).ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetEdgesWithTest1()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetCompleteTestOrientedGraph(10);

            // Act
            var expected = graph.GetEdges().Where(e => e.VertexIn == "4").ToList();
            var actual = graph.GetEdgesWith(e => e.VertexIn == "4").ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetEdgesWithTest2()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetPathTestOrientedGraph(10);

            // Act
            var expected = graph.GetEdges().Where(e => graph.GetInDegree(e.VertexOut) > 0).ToList();
            var actual = graph.GetEdgesWith(e => graph.GetInDegree(e.VertexOut) > 0).ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveVerticesWithTest1()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(10);

            // Act
            var expected = graph.GetVertices().Where(v => v.Name != "4").ToList();
            graph.RemoveVerticesWith(v => v.Name == "4");
            var actual = graph.GetVertices().ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveEdgesWithTest1()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetPathTestOrientedGraph(10);

            // Act
            var expected = graph.GetEdges().Where(e => graph.GetInDegree(e.VertexIn) + graph.GetInDegree(e.VertexOut) >= 2).ToList();
            graph.RemoveEdgesWith(e => graph.GetInDegree(e.VertexIn) + graph.GetInDegree(e.VertexOut) < 2);
            var actual = graph.GetEdges().ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ApplyToVerticesTest1()
        {
            // Arrange
            var graph = TestWeightedOrientedGraphs.GetVertexTestWeightedOrientedGraph(10, () => 1);

            // Act
            graph.ApplyToVertices(v => v.ChangeWeight(5));
            var actual = graph.GetVertices().ToList();

            // Assert

            Assert.IsTrue(actual.All(v => v.Weight == 5) && actual.Count == graph.GetVertexCount());
        }

        [TestMethod]
        public void ApplyToEdgesTest1()
        {
            // Arrange
            var graph = TestWeightedOrientedGraphs.GetCompleteTestWeightedOrientedGraph(10, () => 1, () => 1);

            // Act
            graph.ApplyToEdges(e => e.ChangeWeight(5));
            var actual = graph.GetEdges().ToList();

            // Assert
            Assert.IsTrue(actual.All(e => e.Weight == 5) && actual.Count == graph.GetEdgeCount());
        }

        [TestMethod]
        public void ApplyToVerticesWithTest1()
        {
            // Arrange
            var graph = TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(10, () => 1, () => 1);

            //Act
            graph.ApplyToVerticesWith(v => v.Name == "0", v => v.ChangeWeight(5));
            var actual = graph.GetVertices().ToList();

            // Assert
            Assert.IsTrue(actual.Where(v => v.Name == "0").All(v => v.Weight == 5));
        }

        [TestMethod]
        public void ApplyToEdgesWithTest1()
        {
            // Arrange
            var graph = TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(10, () => 1, () => 1);

            //Act
            graph.ApplyToEdgesWith(v => v.VertexIn == "4", v => v.ChangeWeight(5));
            var actual = graph.GetEdges().ToList();

            // Assert
            Assert.IsTrue(actual.Where(e => e.VertexIn == "4").All(e => e.Weight == 5));
        }
    }
}
