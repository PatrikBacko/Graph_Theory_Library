using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Extensions.IWeightedOrientedGraphExtensions;

namespace GraphLibraryTests.ExtensionsTests
{
	/// <summary>
	/// <b> Testing Extension methods for IWeightedOrientedGraph </b> <br />
	/// <br />
	/// 
	/// <b> methods and all overloads: </b> <br />
	/// - AddVertex <br />
	/// - AddVertices <br />
	/// - AddEdge <br />
	/// - AddEdges <br />
	/// </summary>
	[TestClass]
	public class IWeightedOrientedGraphExtensionsTests
	{
		[TestMethod]
		public void AddVertexTest1()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0.0, () => 0);

			// Act
			graph.AddVertex("5", 5);

			// Assert
			Assert.IsTrue(graph.IsVertex("5"));
			Assert.AreEqual(graph.GetWeight("5"), 5.0);
		}

		[TestMethod]
		public void AddVertexTest2()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0.0, () => 0);

			// Assert
			Assert.ThrowsException<VertexException>(() => graph.AddVertex("2", 2));
		}

		[TestMethod]
		public void AddVertexTest3()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0.0, () => 0);

			// Assert
			Assert.ThrowsException<VertexException>(() => graph.AddVertex("", 0));
		}

		[TestMethod]
		public void AddVerticesTest1()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.ClearTestWeightedOrientedGraph;
			var vertices = new List<(VertexName, int)> { ("0", 0), ("1", 1), ("2", -2), ("3", 3) };

			// Act
			graph.AddVertices(vertices);

			// Assert
			Assert.IsTrue(graph.IsVertex("0"));
			Assert.AreEqual(graph.GetWeight("0"), 0);

			Assert.IsTrue(graph.IsVertex("1"));
			Assert.AreEqual(graph.GetWeight("1"), 1);

			Assert.IsTrue(graph.IsVertex("2"));
			Assert.AreEqual(graph.GetWeight("2"), -2);

			Assert.IsTrue(graph.IsVertex("3"));
			Assert.AreEqual(graph.GetWeight("3"), 3);

			Assert.IsFalse(graph.IsVertex("4"));
		}

		[TestMethod]
		public void AddVerticesTest2()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.ClearTestWeightedOrientedGraph;
			var vertices = new List<(VertexName, int)> { ("0", 0), ("1", 1), ("1", 2), ("3", -3) };

			// Assert
			Assert.ThrowsException<VertexException>(() => graph.AddVertices(vertices));
		}

		[TestMethod]
		public void AddEdgeTest1()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetVertexTestWeightedOrientedGraph(5, () => 0.0m);

			// Act
			graph.AddEdge("0", "1", 5);

			// Assert
			Assert.IsTrue(graph.IsEdge("0", "1"));
			Assert.AreEqual(graph.GetWeight("0", "1"), 5.0m);
		}

		[TestMethod]
		public void AddEdgeTest2()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0, () => 0);

			// Assert
			Assert.ThrowsException<EdgeException>(() => graph.AddEdge("0", "1", 4));
		}

		[TestMethod]
		public void AddEdgeTest3()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0, () => 0);

			// Assert
			Assert.ThrowsException<EdgeException>(() => graph.AddEdge("0", "6", 5));
		}

		[TestMethod]
		public void AddEdgesTest1()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetVertexTestWeightedOrientedGraph(5, () => 0.0m);
			var edges = new List<(VertexName, VertexName, decimal)> { ("0", "1", 1.5m), ("1", "2", -5), ("2", "3", 10), ("3", "4", -5.1m) };

			// Act
			graph.AddEdges(edges);

			// Assert
			Assert.IsTrue(graph.IsEdge("0", "1"));
			Assert.AreEqual(graph.GetWeight("0", "1"), 1.5m);

			Assert.IsTrue(graph.IsEdge("1", "2"));
			Assert.AreEqual(graph.GetWeight("1", "2"), -5m);

			Assert.IsTrue(graph.IsEdge("2", "3"));
			Assert.AreEqual(graph.GetWeight("2", "3"), 10m);

			Assert.IsTrue(graph.IsEdge("3", "4"));
			Assert.AreEqual(graph.GetWeight("3", "4"), -5.1m);
		}

		[TestMethod]
		public void AddEdgesTest2()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetVertexTestWeightedOrientedGraph(5, () => 0);
			var edges = new List<(VertexName, VertexName, int)> { ("0", "1", 1), ("1", "2", -5), ("2", "3", 10), ("2", "3", -7) };

			// Assert
			Assert.ThrowsException<EdgeException>(() => graph.AddEdges(edges));
		}

		[TestMethod]
		public void AddEdgesTest3()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetVertexTestWeightedOrientedGraph(5, () => 0);
			var edges = new List<(VertexName, VertexName, int)> { ("0", "1", 1), ("1", "2", -5), ("2", "3", 10), ("2", "5", -7) };

			// Assert
			Assert.ThrowsException<EdgeException>(() => graph.AddEdges(edges));
		}
	}
}
