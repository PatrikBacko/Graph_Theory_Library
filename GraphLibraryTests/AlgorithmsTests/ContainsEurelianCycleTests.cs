using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Extensions.IOrientedGraphExtensions;

namespace GraphLibraryTests.AlgorithmsTests
{
	/// <summary>
	/// Tests for ContainsEurelianCycle method
	/// </summary>
	[TestClass]
	public class ContainsEurelianCycleTests
	{
		[TestMethod]
		public void ContainsEurelianCycleTest1()
		{
			// Arrange
			var Graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(5);

			// Act
			var actual = Algorithms.ContainsEurelianCycle(Graph);

			// Assert
			Assert.IsFalse(actual);
		}

		[TestMethod]
		public void ContainsEurelianCycleTest2()
		{
			// Arrange
			var Graph = TestGraphs.TestOrientedGraphs.GetCompleteTestOrientedGraph(5);

			// Act
			var actual = Algorithms.ContainsEurelianCycle(Graph);

			// Assert
			Assert.IsTrue(actual);
		}

		[TestMethod]
		public void ContainsEurelianCycleTest3()
		{
			// Arrange
			var Graph = TestGraphs.TestOrientedGraphs.GetVertexTestOrientedGraph(6);
			Graph.AddEdge("0", "1").AddEdge("1", "2").AddEdge("2", "0")
				.AddEdge("3", "4").AddEdge("4", "5").AddEdge("5", "3");

			// Act
			var actual = Algorithms.ContainsEurelianCycle(Graph);

			// Assert
			Assert.IsFalse(actual);
		}

		[TestMethod]
		public void ContainsEurelianCycleTest4()
		{
			// Arrange
			var Graph = TestGraphs.TestOrientedGraphs.GetVertexTestOrientedGraph(6);
			Graph.AddEdge("0", "1").AddEdge("1", "2").AddEdge("2", "0")
				.AddEdge("3", "4").AddEdge("4", "5").AddEdge("5", "3")
				.AddEdge("0", "5");

			// Act
			var actual = Algorithms.ContainsEurelianCycle(Graph);

			// Assert
			Assert.IsFalse(actual);
		}

		[TestMethod]
		public void ContainsEurelianCycleTest5()
		{
			// Arrange
			var Graph = TestGraphs.TestOrientedGraphs.GetVertexTestOrientedGraph(6);
			Graph.AddEdge("0", "1").AddEdge("1", "2").AddEdge("2", "0")
				.AddEdge("3", "4").AddEdge("4", "5").AddEdge("5", "3")
				.AddEdge("0", "5").AddEdge("5", "0");

			// Act
			var actual = Algorithms.ContainsEurelianCycle(Graph);

			// Assert
			Assert.IsTrue(actual);
		}
	}
}
