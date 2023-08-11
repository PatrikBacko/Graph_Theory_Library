using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Extensions.IOrientedGraphExtensions;

namespace GraphLibraryTests.AlgorithmsTests
{
	/// <summary>
	/// <b> Tests for BFS algorithm and its extensions. </b> <br />
	/// <br/>
	/// 
	/// <b> Methods: </b> <br />
	/// - Bfs <br />
	/// - BfsFromVertex <br />
	/// </summary>
	[TestClass]
	public class BfsTests
	{
		[TestMethod]
		public void BfsTest1()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(3);

			// Act
			var verticesList = new List<OrientedVertex>();
			var edgesList = new List<OrientedEdge>();
			Algorithms.Bfs(graph, v => verticesList.Add(v), e => edgesList.Add(e));
			var expectedVertices = new List<OrientedVertex>() { graph.GetVertex("0"), graph.GetVertex("1"), graph.GetVertex("2") };
			var expectedEdges = new List<OrientedEdge>() { graph.GetEdge("0", "1"), graph.GetEdge("1", "2") };

			// Assert
			CollectionAssert.AreEqual(expectedVertices, verticesList);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void BfsTest2()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetCycleTestOrientedGraph(3);

			// Act
			var verticesList = new List<OrientedVertex>();
			var edgesList = new List<OrientedEdge>();
			Algorithms.Bfs(graph, v => verticesList.Add(v), e => edgesList.Add(e));
			var expectedVertices = new List<OrientedVertex>() { graph.GetVertex("0"), graph.GetVertex("1"), graph.GetVertex("2") };
			var expectedEdges = new List<OrientedEdge>() { graph.GetEdge("0", "1"), graph.GetEdge("1", "2"), graph.GetEdge("2", "0") };

			// Assert
			CollectionAssert.AreEqual(expectedVertices, verticesList);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void BfsTest3()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetCycleTestOrientedGraph(3);
			graph.AddVertex("3").AddVertex("4")
				 .AddEdge("3", "4");

			// Act
			var verticesList = new List<OrientedVertex>();
			var edgesList = new List<OrientedEdge>();
			Algorithms.Bfs(graph, v => verticesList.Add(v), e => edgesList.Add(e));
			var expectedVertices = new List<OrientedVertex>() { graph.GetVertex("0"), graph.GetVertex("1"), graph.GetVertex("2"), graph.GetVertex("3"), graph.GetVertex("4") };
			var expectedEdges = new List<OrientedEdge>() { graph.GetEdge("0", "1"), graph.GetEdge("1", "2"), graph.GetEdge("2", "0"), graph.GetEdge("3", "4") };

			// Assert
			CollectionAssert.AreEqual(expectedVertices, verticesList);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void BfsTest4()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetVertexTestOrientedGraph(4);
			graph.AddEdge("0", "1").AddEdge("0", "2").AddEdge("1", "2").AddEdge("2", "3").AddEdge("1", "3");

			// Act
			var verticesList = new List<OrientedVertex>();
			var edgesList = new List<OrientedEdge>();
			Algorithms.Bfs(graph, v => verticesList.Add(v), e => edgesList.Add(e));
			var expectedVertices = new List<OrientedVertex>() { graph.GetVertex("0"), graph.GetVertex("1"), graph.GetVertex("2"), graph.GetVertex("3") };
			var expectedEdges = new List<OrientedEdge>() { graph.GetEdge("0", "1"), graph.GetEdge("0", "2"), graph.GetEdge("1", "3") };

			// Assert
			CollectionAssert.AreEqual(expectedVertices, verticesList);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void BfsTest5()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetVertexTestOrientedGraph(4);
			graph.AddEdge("0", "1").AddEdge("0", "3").AddEdge("1", "2").AddEdge("2", "3").AddEdge("1", "3");

			// Act
			var verticesList = new List<OrientedVertex>();
			var edgesList = new List<OrientedEdge>();
			Algorithms.Bfs(graph, v => verticesList.Add(v), e => edgesList.Add(e));
			var expectedVertices = new List<OrientedVertex>() { graph.GetVertex("0"), graph.GetVertex("1"), graph.GetVertex("3"), graph.GetVertex("2") };
			var expectedEdges = new List<OrientedEdge>() { graph.GetEdge("0", "1"), graph.GetEdge("0", "3"), graph.GetEdge("1", "2") };

			// Assert
			CollectionAssert.AreEqual(expectedVertices, verticesList);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void BfsFromVertexTest1()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetCycleTestOrientedGraph(3);
			graph.AddVertex("3").AddVertex("4")
				 .AddEdge("3", "4");

			// Act
			var verticesList = new List<OrientedVertex>();
			var edgesList = new List<OrientedEdge>();
			Algorithms.BfsFromVertex(graph,"2", v => verticesList.Add(v), e => edgesList.Add(e));
			var expectedVertices = new List<OrientedVertex>() { graph.GetVertex("2"), graph.GetVertex("0"), graph.GetVertex("1")  };
			var expectedEdges = new List<OrientedEdge>() { graph.GetEdge("2", "0"), graph.GetEdge("0", "1"), graph.GetEdge("1", "2") };

			// Assert
			CollectionAssert.AreEqual(expectedVertices, verticesList);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void BfsFromVertexTest2()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetCycleTestOrientedGraph(3);
			graph.AddVertex("3").AddVertex("4")
				 .AddEdge("3", "4");

			// Act
			var verticesList = new List<OrientedVertex>();
			var edgesList = new List<OrientedEdge>();
			Algorithms.BfsFromVertex(graph, "3", v => verticesList.Add(v), e => edgesList.Add(e));
			var expectedVertices = new List<OrientedVertex>() { graph.GetVertex("3"), graph.GetVertex("4") };
			var expectedEdges = new List<OrientedEdge>() { graph.GetEdge("3", "4") };

			// Assert
			CollectionAssert.AreEqual(expectedVertices, verticesList);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void BfsFromVertexTest3()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(3);
			
			// Act
			var verticesList = new List<OrientedVertex>();
			var edgesList = new List<OrientedEdge>();
			Algorithms.BfsFromVertex(graph, "2", v => verticesList.Add(v), e => edgesList.Add(e));
			var expectedVertices = new List<OrientedVertex>() { graph.GetVertex("2") };
			var expectedEdges = new List<OrientedEdge>() { };

			// Assert
			CollectionAssert.AreEqual(expectedVertices, verticesList);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}
	}
}
