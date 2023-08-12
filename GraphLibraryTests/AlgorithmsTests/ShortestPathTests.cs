using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Extensions.IWeightedOrientedGraphExtensions;

namespace GraphLibraryTests.AlgorithmsTests
{
	/// <summary>
	/// <b> Tests for shortestPath algorithm in OrientedGraph </b>
	/// </summary>
	[TestClass]
	public class ShortestPathTests
	{
		[TestMethod]
		public void ShortestPathTest1()
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
		public void ShortestPathTest2()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0.0, () => 1);
			graph.AddEdge("4", "0", 5.0);

			// Act
			var path = Algorithms.ShortestPath(graph, "0", "4", out var pathWeight);
			var expectedPath = new List<VertexName> { "0", "1", "2", "3", "4" };
			var expectedPathWeight = 4.0;

			// Assert
			CollectionAssert.AreEqual(expectedPath, path);
			Assert.AreEqual(expectedPathWeight, pathWeight);
		}
	}
}
