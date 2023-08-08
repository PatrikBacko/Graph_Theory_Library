using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibraryTests.TestGraphs;

namespace GraphLibraryTests.OrientedGraphTests
{
    /// <summary>
    ///	Tests for the GetOutDegree and GetInDegree methods
    /// </summary>
    [TestClass]
	public class GetOutInDegreeTests
	{
		[TestMethod]
		public void GetOutDegreeTest1()
		{
			// Arrange
			var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
			graph.AddEdge(new OrientedEdge("1", "0"));

			// Act
			var expected = 0;
			var actual = graph.GetOutDegree("0");

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void GetOutDegreeTest2()
		{
			// Arrange
			var graph = TestOrientedGraphs.GetCompleteTestOrientedGraph(10);

			// Act
			var expected = 9;
			var actual = graph.GetOutDegree("0");

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void GetOutDegreeTest3()
		{
			// Arrange
			var graph = TestOrientedGraphs.GetPathTestOrientedGraph(5);

			// Act
			var expected = 1;
			var actual = graph.GetOutDegree("0");

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void GetOutDegreeTest4()
		{
			// Arrange
			var graph = TestOrientedGraphs.GetCompleteTestOrientedGraph(10);
			graph.RemoveEdge("0", "5");
			graph.RemoveEdge("0", "2");
			graph.RemoveEdge("7", "0");

			// Act
			var expected = 7;
			var actual = graph.GetOutDegree("0");

			// Assert
			Assert.AreEqual(expected, actual);
		}


		[TestMethod]
		public void GetInDegreeTest1()
		{
			// Arrange
			var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(5);
			graph.AddEdge(new OrientedEdge("1", "0"));

			// Act
			var expected = 1;
			var actual = graph.GetInDegree("0");

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void GetInDegreeTest2()
		{
			// Arrange
			var graph = TestOrientedGraphs.GetCompleteTestOrientedGraph(10);

			// Act
			var expected = 9;
			var actual = graph.GetInDegree("0");

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void GetInDegreeTest3()
		{
			// Arrange
			var graph = TestOrientedGraphs.GetPathTestOrientedGraph(5);

			// Act
			var expected = 0;
			var actual = graph.GetInDegree("0");

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void GetInDegreeTest4()
		{
			// Arrange
			var graph = TestOrientedGraphs.GetCompleteTestOrientedGraph(10);
			graph.RemoveEdge("8", "0");
			graph.RemoveEdge("1", "0");
			graph.RemoveEdge("0", "4");


			// Act
			var expected = 7;
			var actual = graph.GetInDegree("0");

			// Assert
			Assert.AreEqual(expected, actual);
		}
	}
}
