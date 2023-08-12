using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests.GraphTests.WeightedOrientedGraphTests
{
	/// <summary>
	/// <b> Tests for ChangeWeight methods in WeightedOrientedGraph </b>
	/// </summary>
	[TestClass]
	public class ChangeWeightTests
	{
		[TestMethod]
		public void ChengeVertexWeightTest1()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0, () => 1);

			// Act
			graph.ChangeVertexWeight("0", 2);
			var expectedWeight = 2;
			var actualWeight = graph.GetWeight("0");

			// Assert
			Assert.AreEqual(expectedWeight, actualWeight);
		}

		[TestMethod]
		public void ChengeVertexWeightTest2()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0, () => 1);

			// Assert
			Assert.ThrowsException<VertexException>(() => graph.ChangeVertexWeight("5", 2));
		}

		[TestMethod]
		public void ChangeEdgeWeightTest1()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0, () => 1);

			// Act
			graph.ChangeEdgeWeight("0", "1", 2);
			var expectedWeight = 2;
			var actualWeight = graph.GetWeight("0", "1");

			// Assert
			Assert.AreEqual(expectedWeight, actualWeight);
		}

		[TestMethod]
		public void ChengeEdgeWeightTest2()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0, () => 1);

			// Assert
			Assert.ThrowsException<EdgeException>(() => graph.ChangeEdgeWeight("0", "2", 2));
		}
		[TestMethod]
		public void ChengeEdgeWeightTest3()
		{
			// Arrange
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => 0, () => 1);

			// Assert
			Assert.ThrowsException<EdgeException>(() => graph.ChangeEdgeWeight("6", "0", 2));
		}
	}
}
