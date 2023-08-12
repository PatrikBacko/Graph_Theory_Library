using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Extensions.IWeightedOrientedGraphExtensions;
using GraphLibrary.Algorithms.Exceptions;

namespace GraphLibraryTests.AlgorithmsTests
{
	/// <summary>
	/// <b> Tests for shortestPath algorithm in OrientedGraph </b>
	/// </summary>
	[TestClass]
	public class ShortestPathTests
	{
		[TestMethod]
		public void ShortestPathDjikstraTest1()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0.0, () => 1 );

			// Act
			var path = Algorithms.ShortestPath(graph, "0", "4", out var pathWeight);
			var expectedPath = new List<VertexName> { "0", "1", "2", "3", "4" };
			var expectedPathWeight = 4.0;

			// Assert
			CollectionAssert.AreEqual(expectedPath, path);
			Assert.AreEqual(expectedPathWeight, pathWeight);
		}

		[TestMethod]
		public void ShortestPathDjikstraTest2()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0.0, () => 1);
			graph.AddEdge("0", "4", 5.0);

			// Act
			var path = Algorithms.ShortestPath(graph, "0", "4", out var pathWeight);
			var expectedPath = new List<VertexName> { "0", "1", "2", "3", "4" };
			var expectedPathWeight = 4.0;

			// Assert
			CollectionAssert.AreEqual(expectedPath, path);
			Assert.AreEqual(expectedPathWeight, pathWeight);
		}

		[TestMethod]
		public void ShortestPathDjikstraTest3()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0.0, () => 1);
			graph.AddEdge("0", "4", 3.5);

			// Act
			var path = Algorithms.ShortestPath(graph, "0", "4", out var pathWeight);
			var expectedPath = new List<VertexName> { "0", "4" };
			var expectedPathWeight = 3.5;

			// Assert
			CollectionAssert.AreEqual(expectedPath, path);
			Assert.AreEqual(expectedPathWeight, pathWeight);
		}

		[TestMethod]
		public void ShortestPathDjikstraTest4()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0.0, () => 1);
			graph.AddEdge("0", "3", 4.0);

			// Act
			var path = Algorithms.ShortestPath(graph, "0", "4", out var pathWeight);
			var expectedPath = new List<VertexName> { "0", "1", "2", "3", "4" };
			var expectedPathWeight = 4.0;

			// Assert
			CollectionAssert.AreEqual(expectedPath, path);
			Assert.AreEqual(expectedPathWeight, pathWeight);
		}

		[TestMethod]
		public void ShortestPathExceptionTest1()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0.0, () => 1);

			// Assert
			Assert.ThrowsException<ArgumentException>(() => Algorithms.ShortestPath(graph, "5", "2", out var pathWeight));
		}

		[TestMethod]
		public void ShortestPathExceptionTest2()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0.0, () => 1);

			// Assert
			Assert.ThrowsException<ArgumentException>(() => Algorithms.ShortestPath(graph, "0", "5", out var pathWeight));
		}

		[TestMethod]
		public void ShortestPathExceptionTest3()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0.0, () => 1);

			// Assert
			Assert.ThrowsException<NoPathException>(() => Algorithms.ShortestPath(graph, "1", "0", out var pathWeight));
		}

		[TestMethod]
		public void ShortestPathExceptionTest4()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0.0, () => 1);
			graph.RemoveEdge("1", "2");

			// Assert
			Assert.ThrowsException<NoPathException>(() => Algorithms.ShortestPath(graph, "0", "4", out var pathWeight));
		}

		[TestMethod]
		public void ShortestPathBellmanTest1()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0.0, () => 1);
			graph.AddEdge("0", "2", -1);

			// Act
			var path = Algorithms.ShortestPath(graph, "0", "4", out var pathWeight);
			var expectedPath = new List<VertexName> { "0", "2", "3", "4" };
			var expectedPathWeight = 1.0;

			// Assert
			CollectionAssert.AreEqual(expectedPath, path);
			Assert.AreEqual(expectedPathWeight, pathWeight);
		}

		[TestMethod]
		public void ShortestPathBellmanTest2()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(4, () => 0.0, () => -1);
			graph.AddEdge("3", "0", -1);
			graph.AddVertex("4", 0);
			graph.AddEdge("3", "4", 1);


			// Assert
			Assert.ThrowsException<GraphHasNegativeCycleException>(() => Algorithms.ShortestPath(graph, "0", "4", out var pathWeight));
		}


	}
}
