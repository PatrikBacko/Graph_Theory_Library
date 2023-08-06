using GraphLibrary.Algorithms.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GraphLibraryTests.AlgorithmsTests
{
	[TestClass]
	public class TopologicalSortingTests
	{
		[TestMethod]
		public void DAGTest1(){
			var graph = TestGraphs.GetTestGraphDAG1();
			var expected = new List<VertexName>() { "5", "4", "2", "3", "1", "0" };
			var actual = Algorithms.TopologicalSorting(graph);
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void DAGTest2()
		{
			var graph = TestGraphs.GetTestGraphDAG2();
			var expected = new List<VertexName>() { "0", "2", "1", "3" };
			var actual = Algorithms.TopologicalSorting(graph);
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void NotDAGTest1(){
			var graph = TestGraphs.GetTestGraphNotDAG1();
			Assert.ThrowsException<GraphIsNotDAGException>(() => Algorithms.TopologicalSorting(graph));
		}
	}
}
