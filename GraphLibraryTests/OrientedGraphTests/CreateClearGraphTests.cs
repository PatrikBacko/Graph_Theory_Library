using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibraryTests.TestGraphs;

namespace GraphLibraryTests.OrientedGraphTests
{
    /// <summary>
    /// Tests for Create and Clear methods and all overloads.
    /// </summary>
    [TestClass]
	public class CreateClearGraphTests
	{
		[TestMethod]
		public void ClearGraphTest1()
		{
			// Arrange
			var graph = TestOrientedGraphs.GetCompleteTestOrientedGraph(10);

			// Act
			graph.Clear();

			// Assert
			CollectionAssert.AreEqual(new List<OrientedVertex>(), graph.GetVertices().ToList());
			CollectionAssert.AreEqual(new List<OrientedEdge>(), graph.GetEdges().ToList());
		}

		[TestMethod]
		public void CreateGraphTest1()
		{
			// Act
			var graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();

			// Assert
			CollectionAssert.AreEqual(new List<OrientedVertex>(), graph.GetVertices().ToList());
			CollectionAssert.AreEqual(new List<OrientedEdge>(), graph.GetEdges().ToList());
		}

		[TestMethod]
		public void CreateGraphTest2()
		{
			// Arrange
			var vertices = new List<OrientedVertex>() 
				{
				new OrientedVertex("0"),
				new OrientedVertex("1"),
				new OrientedVertex("2"),
				new OrientedVertex("3")
				};

			var edges = new List<OrientedEdge>()
			{
				new OrientedEdge("0", "1"),
				new OrientedEdge("0", "3"),
				new OrientedEdge("1", "0"),
				new OrientedEdge("2", "3"),
				new OrientedEdge("3", "0")
			};

			//Act
			var graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create(vertices, edges);

			// Assert
			CollectionAssert.AreEqual(vertices, graph.GetVertices().ToList());
			CollectionAssert.AreEqual(edges, graph.GetEdges().ToList());
		}

	}
}
