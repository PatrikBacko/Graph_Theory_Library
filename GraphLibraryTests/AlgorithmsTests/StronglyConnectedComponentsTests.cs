using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Extensions.IOrientedGraphExtensions;
using GraphLibrary.MaybeLater;

namespace GraphLibraryTests.AlgorithmsTests
{
	/// <summary>
	/// Tests for the StronglyConnectedComponents method
	/// </summary>
	[TestClass]
	public class StronglyConnectedComponentsTests
	{
		[TestMethod]
		public void StronglyConnectedComponentsTest1()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetCycleTestOrientedGraph(4);

			// Act
			var components = Algorithms.GetStronglyConnectedComponents(graph);
			var expectedComponents = new List<List<VertexName>>()
			{
				new List<VertexName>() { "0", "3", "2", "1" }
			};

			// Assert
			for (int i = 0; i < components.Count; i++)
			{
				CollectionAssert.AreEqual(expectedComponents[i], components[i]);
			}
		}

		[TestMethod]
		public void StronglyConnectedComponentsTest2()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(4);

			// Act
			var components = Algorithms.GetStronglyConnectedComponents(graph);
			var expectedComponents = new List<List<VertexName>>()
			{
				new List<VertexName>() { "0" },
				new List<VertexName>() { "1" },
				new List<VertexName>() { "2" },
				new List<VertexName>() { "3" },
			};

			// Assert
			for (int i = 0; i < components.Count; i++)
			{
				CollectionAssert.AreEqual(expectedComponents[i], components[i]);
			}
		}

		[TestMethod]
		public void StronglyConnectedComponentsTest3()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetCycleTestOrientedGraph(4);
			graph.AddVertex("4").AddVertex("5").AddVertex("6")
				.AddEdge("4","5").AddEdge("5", "4").AddEdge("3", "6");

			// Act
			var components = Algorithms.GetStronglyConnectedComponents(graph);
			var expectedComponents = new List<List<VertexName>>()
			{
				new List<VertexName>() { "4", "5" },
				new List<VertexName>() { "0", "3", "2", "1" },
				new List<VertexName>() { "6" }
			};

			// Assert
			for (int i = 0; i < components.Count; i++)
			{
				CollectionAssert.AreEqual(expectedComponents[i], components[i]);
			}
		}
	}
}
