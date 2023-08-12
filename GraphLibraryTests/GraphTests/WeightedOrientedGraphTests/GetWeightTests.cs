using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests.GraphTests.WeightedOrientedGraphTests
{
	/// <summary>
	/// <b> Tests for GetWeight methods in WeightedOrientedGraph </b>
	/// </summary>
	[TestClass]
	public class GetWeightTests
	{
		[TestMethod]
		public void GetVertexWeightTest1()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0, () => 1);

			// Act
			var weight = graph.GetWeight("1");

			// Assert
			Assert.AreEqual(0, weight);
		}

		[TestMethod]
		public void GetVertexWeightTest2()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0, () => 1);

			// Assert
			Assert.ThrowsException<VertexException>(() => graph.GetWeight("5"));
		}

		[TestMethod]
		public void GetEdgeWeightTest1()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0, () => 1);

			// Act
			var weight = graph.GetWeight("1", "2");

			// Assert
			Assert.AreEqual(1, weight);
		}

		[TestMethod]
		public void GetEdgeWeightTest2()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0, () => 1);

			Assert.ThrowsException<EdgeException>(() => graph.GetWeight("1", "3"));

		}

		[TestMethod]
		public void GetEdgeWeightTest3()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0, () => 1);

			Assert.ThrowsException<EdgeException>(() => graph.GetWeight("1", "6"));

		}
	}
}
