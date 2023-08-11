using GraphLibrary.Extensions.IOrientedGraphExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests.AlgorithmsTests
{
	/// <summary>
	/// <b> Tests for Depth First Search algorithm. </b> <br />
	/// <br />
	/// 
	/// <b> methods: </b> <br />
	/// - Dfs <br />
	/// - DfsFromVertex <br />
	/// - DfsFromVertexSpecial <br />
	/// </summary>
	[TestClass]
	public class DfsTests
	{
		[TestMethod]
		public void DfsTest1()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(3);

			// Act
			var verticesListIn = new List<OrientedVertex>();
			var verticesListOut = new List<OrientedVertex>();
			var edgesList = new List<OrientedEdge>();
			Algorithms.Dfs(graph, v => verticesListIn.Add(v), v => verticesListOut.Add(v), e => edgesList.Add(e));
			var expectedVerticesIn = new List<OrientedVertex>() { graph.GetVertex("0"), graph.GetVertex("1"), graph.GetVertex("2") };
			var expectedVerticesOut = expectedVerticesIn.Reverse<OrientedVertex>().ToList();
			var expectedEdges = new List<OrientedEdge>() { graph.GetEdge("0", "1"), graph.GetEdge("1", "2") };

			// Assert
			CollectionAssert.AreEqual(expectedVerticesIn, verticesListIn);
			CollectionAssert.AreEqual(expectedVerticesOut, verticesListOut);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void DfsTest2()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetCycleTestOrientedGraph(3);

			// Act
			var verticesListIn = new List<OrientedVertex>();
			var verticesListOut = new List<OrientedVertex>();

			var edgesList = new List<OrientedEdge>();

			Algorithms.Dfs(
				graph, 
				v => verticesListIn.Add(v), 
				v => verticesListOut.Add(v),
				e => edgesList.Add(e));

			var expectedVerticesIn = new List<OrientedVertex>() { graph.GetVertex("0"), graph.GetVertex("1"), graph.GetVertex("2") };
			var expectedVerticesOut = expectedVerticesIn.Reverse<OrientedVertex>().ToList();
			var expectedEdges = new List<OrientedEdge>() { graph.GetEdge("0", "1"), graph.GetEdge("1", "2")};

			// Assert
			CollectionAssert.AreEqual(expectedVerticesIn, verticesListIn);
			CollectionAssert.AreEqual(expectedVerticesOut, verticesListOut);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void DfsTest3()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetCycleTestOrientedGraph(3);
			graph.AddVertex("3").AddVertex("4")
				 .AddEdge("3", "4");

			// Act
			var verticesListIn = new List<VertexName>();
			var verticesListOut = new List<VertexName>();

			var edgesList = new List<(VertexName, VertexName)>();
			Algorithms.Dfs(
				graph, 
				v => verticesListIn.Add(v.Name),
				v=> verticesListOut.Add(v.Name) ,
				e => edgesList.Add((e.VertexOut, e.VertexIn)));

			var expectedVerticesIn = new List<VertexName>() { "0", "1", "2", "3", "4" };
			var expectedVerticesOut = new List<VertexName>() { "2", "1", "0", "4", "3" };
			var expectedEdges = new List<(VertexName, VertexName)>() { ("0", "1"), ("1", "2"), ("3", "4") };

			// Assert
			CollectionAssert.AreEqual(expectedVerticesIn, verticesListIn);
			CollectionAssert.AreEqual(expectedVerticesOut, verticesListOut);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void DfsTest4()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetVertexTestOrientedGraph(5);
			graph.AddEdge("0", "1").AddEdge("0", "2").AddEdge("1", "2").AddEdge("2", "3").AddEdge("1", "3");

			// Act
			var verticesListIn = new List<VertexName>();
			var verticesListOut = new List<VertexName>();
			var edgesList = new List<(VertexName, VertexName)>();

			Algorithms.Dfs(
				graph,
				v => verticesListIn.Add(v.Name),
				v => verticesListOut.Add(v.Name),
				e => edgesList.Add((e.VertexOut, e.VertexIn)));

			var expectedVerticesIn = new List<VertexName>() { "0", "1", "2", "3", "4" };
			var expectedVerticesOut = new List<VertexName>() { "3", "2", "1", "0", "4" };
			var expectedEdges = new List<(VertexName, VertexName)>() { ("0", "1"), ("1", "2"), ("2", "3"),  };

			// Assert
			CollectionAssert.AreEqual(expectedVerticesIn, verticesListIn);
			CollectionAssert.AreEqual(expectedVerticesOut, verticesListOut);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void DfsTest5()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetVertexTestOrientedGraph(5);
			graph.AddEdge("0", "1").AddEdge("0", "3").AddEdge("0", "4").AddEdge("1", "2").AddEdge("2", "3").AddEdge("1", "3").AddEdge("2", "4");

			// Act
			var verticesListIn = new List<VertexName>();
			var verticesListOut = new List<VertexName>();
			var edgesList = new List<(VertexName, VertexName)>();

			Algorithms.Dfs(
				graph,
				v => verticesListIn.Add(v.Name),
				v => verticesListOut.Add(v.Name),
				e => edgesList.Add((e.VertexOut, e.VertexIn)));

			var expectedVerticesIn = new List<VertexName>() { "0", "1", "2", "3", "4" };
			var expectedVerticesOut = new List<VertexName>() { "3", "4", "2", "1", "0" };
			var expectedEdges = new List<(VertexName, VertexName)>() { ("0", "1"), ("1", "2"), ("2", "3"), ("2", "4")};

			// Assert
			CollectionAssert.AreEqual(expectedVerticesIn, verticesListIn);
			CollectionAssert.AreEqual(expectedVerticesOut, verticesListOut);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void DfsFromVertexTest1()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetCycleTestOrientedGraph(3);
			graph.AddVertex("3").AddVertex("4")
				 .AddEdge("3", "4");

			// Act
			var verticesListIn = new List<VertexName>();
			var verticesListOut = new List<VertexName>();
			var edgesList = new List<(VertexName, VertexName)>();

			Algorithms.DfsFromVertex(
				graph,
				"2",
				v => verticesListIn.Add(v.Name),
				v => verticesListOut.Add(v.Name),
				e => edgesList.Add((e.VertexOut, e.VertexIn)));

			var expectedVerticesIn = new List<VertexName>() { "2", "0", "1" };
			var expectedVerticesOut = new List<VertexName>() { "1", "0", "2" };
			var expectedEdges = new List<(VertexName, VertexName)>() { ("2", "0"), ("0", "1") };

			// Assert
			CollectionAssert.AreEqual(expectedVerticesIn, verticesListIn);
			CollectionAssert.AreEqual(expectedVerticesOut, verticesListOut);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void DfsFromVertexTest2()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetCycleTestOrientedGraph(3);
			graph.AddVertex("3").AddVertex("4")
				 .AddEdge("3", "4");

			// Act
			var verticesListIn = new List<VertexName>();
			var verticesListOut = new List<VertexName>();
			var edgesList = new List<(VertexName, VertexName)>();

			Algorithms.DfsFromVertex(
				graph,
				"3",
				v => verticesListIn.Add(v.Name),
				v => verticesListOut.Add(v.Name),
				e => edgesList.Add((e.VertexOut, e.VertexIn)));

			var expectedVerticesIn = new List<VertexName>() { "3", "4" };
			var expectedVerticesOut = new List<VertexName>() { "4", "3" };
			var expectedEdges = new List<(VertexName, VertexName)>() { ("3", "4") };

			// Assert
			CollectionAssert.AreEqual(expectedVerticesIn, verticesListIn);
			CollectionAssert.AreEqual(expectedVerticesOut, verticesListOut);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void DfsFromVertexTest3()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(3);

			// Act
			var verticesListIn = new List<VertexName>();
			var verticesListOut = new List<VertexName>();
			var edgesList = new List<(VertexName, VertexName)>();

			Algorithms.DfsFromVertex(
				graph,
				"2",
				v => verticesListIn.Add(v.Name),
				v => verticesListOut.Add(v.Name),
				e => edgesList.Add((e.VertexOut, e.VertexIn)));

			var expectedVerticesIn = new List<VertexName>() { "2" };
			var expectedVerticesOut = new List<VertexName>() { "2" };
			var expectedEdges = new List<(VertexName, VertexName)>() {};

			// Assert
			CollectionAssert.AreEqual(expectedVerticesIn, verticesListIn);
			CollectionAssert.AreEqual(expectedVerticesOut, verticesListOut);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void DfsFromVertexSpecialTest1()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetCycleTestOrientedGraph(3);
			graph.AddVertex("3").AddVertex("4")
				 .AddEdge("3", "4");

			// Act
			var verticesListIn = new List<VertexName>();
			var verticesListOut = new List<VertexName>();
			var verticesListSpecial = new List<VertexName>();
			var edgesList = new List<(VertexName, VertexName)>();

			Algorithms.DfsFromVertexSpecial(
				graph,
				"2",
				v => verticesListIn.Add(v.Name),
				v => verticesListOut.Add(v.Name),
				v => verticesListSpecial.Add(v.Name),
				e => edgesList.Add((e.VertexOut, e.VertexIn)));

			var expectedVerticesIn = new List<VertexName>() { "2", "0", "1" };
			var expectedVerticesOut = new List<VertexName>() { "1", "0", "2" };
			var expectedVerticesSpecial = new List<VertexName>() { "2", "0", "1", "2" };
			var expectedEdges = new List<(VertexName, VertexName)>() { ("2", "0"), ("0", "1"), ("1", "2") };

			// Assert
			CollectionAssert.AreEqual(expectedVerticesIn, verticesListIn);
			CollectionAssert.AreEqual(expectedVerticesOut, verticesListOut);
			CollectionAssert.AreEqual(expectedVerticesSpecial, verticesListSpecial);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void DfsFromVertexSpecialTest2()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetCycleTestOrientedGraph(3);
			graph.AddVertex("3").AddVertex("4")
				 .AddEdge("3", "4");

			// Act
			var verticesListIn = new List<VertexName>();
			var verticesListOut = new List<VertexName>();
			var verticesListSpecial = new List<VertexName>();
			var edgesList = new List<(VertexName, VertexName)>();

			Algorithms.DfsFromVertexSpecial(
				graph,
				"3",
				v => verticesListIn.Add(v.Name),
				v => verticesListOut.Add(v.Name),
				v => verticesListSpecial.Add(v.Name),
				e => edgesList.Add((e.VertexOut, e.VertexIn)));

			var expectedVerticesIn = new List<VertexName>() { "3", "4" };
			var expectedVerticesOut = new List<VertexName>() { "4", "3" };
			var expectedVerticesSpecial = new List<VertexName>() { "3", "4" };
			var expectedEdges = new List<(VertexName, VertexName)>() { ("3", "4") };

			// Assert
			CollectionAssert.AreEqual(expectedVerticesIn, verticesListIn);
			CollectionAssert.AreEqual(expectedVerticesOut, verticesListOut);
			CollectionAssert.AreEqual(expectedVerticesSpecial, verticesListSpecial);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}

		[TestMethod]
		public void DfsFromVertexSpecialTest3()
		{
			// Arrange
			var graph = TestGraphs.TestOrientedGraphs.GetVertexTestOrientedGraph(6);
			graph.AddEdge("0", "1")
				.AddEdge("0", "3")
				.AddEdge("0", "4")
				.AddEdge("1", "2")
				.AddEdge("2", "3")
				.AddEdge("1", "3")
				.AddEdge("2", "4")
				.AddEdge("4", "5");

			// Act
			var verticesListIn = new List<VertexName>();
			var verticesListOut = new List<VertexName>();
			var verticesListSpecial = new List<VertexName>();
			var edgesList = new List<(VertexName, VertexName)>();

			Algorithms.DfsFromVertexSpecial(
				graph,
				"0",
				v => verticesListIn.Add(v.Name),
				v => verticesListOut.Add(v.Name),
				v => verticesListSpecial.Add(v.Name),
				e => edgesList.Add((e.VertexOut, e.VertexIn)));

			var expectedVerticesIn = new List<VertexName>() { "0", "1", "2", "3", "4", "5" };
			var expectedVerticesOut = new List<VertexName>() { "3", "5", "4", "2", "1", "0" };
			var expectedVerticesSpecial = new List<VertexName>() { "0", "1", "2", "3", "4", "5", "3", "3", "4"};
			var expectedEdges = new List<(VertexName, VertexName)>() { ("0", "1"), ("1", "2"), ("2", "3"), ("2", "4"), ("4", "5"), ("1", "3"), ("0", "3"), ("0", "4") };

			// Assert
			CollectionAssert.AreEqual(expectedVerticesIn, verticesListIn);
			CollectionAssert.AreEqual(expectedVerticesOut, verticesListOut);
			CollectionAssert.AreEqual(expectedVerticesSpecial, verticesListSpecial);
			CollectionAssert.AreEqual(expectedEdges, edgesList);
		}
	}
}
