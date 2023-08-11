using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Extensions.IOrientedGraphExtensions;

namespace GraphLibraryTests.ExtensionsTests
{
	/// <summary>
	/// <b> Testing Extension methods for IOrientedGraph </b> <br />
	/// <br />
	/// 
	/// <b> methods and all overloads: </b> <br />
	/// - AddVertex <br />
	/// - AddVertices <br />
	/// - AddEdge <br />
	/// - AddEdges <br />
	/// - ReverseEdge <br />
	/// - ReverseEdges <br />
	/// - GetReversedGraph <br />
	/// </summary>
	[TestClass]
	public class IOrientedGraphExtensionsTests
	{
		[TestMethod]
		public void AddVertexTest1()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(5);

			// Act
			graph.AddVertex("6");

			// Assert
			Assert.IsTrue(graph.IsVertex("6"));
		}

		[TestMethod]
		public void AddVertexTest2()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(5);

			// Assert
			Assert.ThrowsException<VertexException>(() => graph.AddVertex("2"));
		}

		[TestMethod]
		public void AddVertexTest3()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(5);

			// Assert
			Assert.ThrowsException<VertexException>(() => graph.AddVertex(""));
		}

		[TestMethod]
		public void AddVerticesTest1()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.ClearTestOrientedGraph;
			var vertices = new List<VertexName> { "0", "1", "2", "3" };

			// Act
			graph.AddVertices(vertices);

			// Assert
			Assert.IsTrue(graph.IsVertex("0"));
			Assert.IsTrue(graph.IsVertex("1"));
			Assert.IsTrue(graph.IsVertex("2"));
			Assert.IsTrue(graph.IsVertex("3"));
			Assert.IsFalse(graph.IsVertex("4"));
		}

		[TestMethod]
		public void AddVerticesTest2()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.ClearTestOrientedGraph;
			var vertices = new List<VertexName> { "0", "1", "2", "2" };

			// Assert
			Assert.ThrowsException<VertexException>(() => graph.AddVertices(vertices));
		}

		[TestMethod]
		public void AddEdgeTest1()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetVertexTestOrientedGraph(5);

			// Act
			graph.AddEdge("0", "1");

			// Assert
			Assert.IsTrue(graph.IsEdge("0", "1"));
		}

		[TestMethod]
		public void AddEdgeTest2()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(5);

			// Assert
			Assert.ThrowsException<EdgeException>(() => graph.AddEdge("0", "1"));
		}

		[TestMethod]
		public void AddEdgeTest3()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(5);

			// Assert
			Assert.ThrowsException<EdgeException>(() => graph.AddEdge("0", "6"));
		}

		[TestMethod]
		public void AddEdgesTest1()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetVertexTestOrientedGraph(5);
			var edges = new List<(VertexName, VertexName)> { ("0", "1"), ("1", "2"), ("2", "3"), ("3", "4") };

			// Act
			graph.AddEdges(edges);

			// Assert
			Assert.IsTrue(graph.IsEdge("0", "1"));
			Assert.IsTrue(graph.IsEdge("1", "2"));
			Assert.IsTrue(graph.IsEdge("2", "3"));
			Assert.IsTrue(graph.IsEdge("3", "4"));
		}

		[TestMethod]
		public void AddEdgesTest2()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetVertexTestOrientedGraph(5);
			var edges = new List<(VertexName, VertexName)> { ("0", "1"), ("1", "2"), ("2", "3"), ("2", "3") };

			// Assert
			Assert.ThrowsException<EdgeException>(() => graph.AddEdges(edges));
		}

		[TestMethod]
		public void AddEdgesTest3()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetVertexTestOrientedGraph(5);
			var edges = new List<(VertexName, VertexName)> { ("0", "1"), ("1", "2"), ("2", "3"), ("3", "5") };

			// Assert
			Assert.ThrowsException<EdgeException>(() => graph.AddEdges(edges));
		}

		[TestMethod]
		public void ReverseEdgeTest1()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(5);

			// Act
			graph.ReverseEdge("0", "1");

			// Assert
			Assert.IsTrue(graph.IsEdge("1", "0"));
		}

		[TestMethod]
		public void ReverseEdgeTest2()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(5);

			// Assert
			Assert.ThrowsException<EdgeException>(() => graph.ReverseEdge("1", "0"));
		}

		[TestMethod]
		public void ReverseEdgeTest3()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(5);

			// Assert
			Assert.ThrowsException<EdgeException>(() => graph.ReverseEdge("0", "6"));
		}

		[TestMethod]
		public void ReverseEdgeTest4()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(5);
			var Edge = graph.GetEdge("0", "1");

			// Act
			graph.ReverseEdge(Edge);

			// Assert
			Assert.IsTrue(graph.IsEdge("1", "0"));

			Assert.IsFalse(graph.IsEdge("0", "1"));
		}

		[TestMethod]
		public void ReverseEdgesTest1()
		{
			// Arrange
			var Graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(4);
			var Edges = new List<(VertexName, VertexName)> { ("0", "1"), ("1", "2"), ("2", "3") };

			// Act
			Graph.ReverseEdges(Edges);

			// Assert
			Assert.IsTrue(Graph.IsEdge("1", "0"));
			Assert.IsTrue(Graph.IsEdge("2", "1"));
			Assert.IsTrue(Graph.IsEdge("3", "2"));

			Assert.IsFalse(Graph.IsEdge("0", "1"));
			Assert.IsFalse(Graph.IsEdge("1", "2"));
			Assert.IsFalse(Graph.IsEdge("2", "3"));
		}

		[TestMethod]
		public void ReverseEdgesTest2()
		{
			// Arrange
			var Graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(3);
			var Edges = new List<OrientedEdge> { Graph.GetEdge("0", "1"), Graph.GetEdge("1", "2") };

			// Act
			Graph.ReverseEdges(Edges);

			// Assert
			Assert.IsTrue(Graph.IsEdge("1", "0"));
			Assert.IsTrue(Graph.IsEdge("2", "1"));

			Assert.IsFalse(Graph.IsEdge("0", "1"));
			Assert.IsFalse(Graph.IsEdge("1", "2"));
		}

		[TestMethod]

		public void GetReversedGraphTest1()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(4);

			// Act
			var reversedGraph = graph.GetReversedGraph();

			//Assert
			Assert.IsTrue(reversedGraph.IsEdge("1", "0"));
			Assert.IsTrue(reversedGraph.IsEdge("2", "1"));
			Assert.IsTrue(reversedGraph.IsEdge("3", "2"));

			Assert.IsFalse(reversedGraph.IsEdge("0", "1"));
			Assert.IsFalse(reversedGraph.IsEdge("1", "2"));
			Assert.IsFalse(reversedGraph.IsEdge("2", "3"));
		}

	}
}
