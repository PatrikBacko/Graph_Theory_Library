﻿using GraphLibrary.Graphs.Exceptions;
using GraphLibraryTests.TestGraphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests.GraphTests.OrientedGraphTests
{
    /// <summary>
    /// Tests for the GetEdges and GetEdges methods.
    /// </summary>
    [TestClass]
    public class GetEdgeTests
    {
        [TestMethod]
        public void GetEdgeTest1()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(3);
            var edge = new OrientedEdge("0", "1");
            graph.AddEdge(edge);

            // Act
            var result = graph.GetEdge("0", "1");

            // Assert
            Assert.AreEqual(edge, result);
        }

        [TestMethod]
        public void GetEdgeTest2()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(3);
            var edge = new OrientedEdge("0", "1");
            graph.AddEdge(edge);

            // Assert
            Assert.ThrowsException<EdgeException>(() => graph.GetEdge("1", "2"));
        }

		[TestMethod]
		public void GetEdgeTest3()
		{
			// Arrange
			var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(3);
			var edge = new OrientedEdge("0", "1");
			graph.AddEdge(edge);

			// Assert
			Assert.ThrowsException<EdgeException>(() => graph.GetEdge("1", "5"));
		}

		[TestMethod]
        public void GetEdgesTest1()
        {
            // Arrange
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(3);
            var edges = new List<OrientedEdge>()
            {
                new OrientedEdge("0", "1"),
                new OrientedEdge("1", "2")
            };

            // Act
            graph.AddEdges(edges);
            var result = graph.GetEdges();

            // Assert
            CollectionAssert.AreEqual(edges, result.ToList());
        }
    }
}
