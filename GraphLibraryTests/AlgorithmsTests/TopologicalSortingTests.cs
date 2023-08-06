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
		public void DAGTest(){
			var graph = TestGraphs.GetTestGraphDAG1();
			var expected = new List<VertexName>() { "4", "5", "0", "2", "3", "1" };
			var actual = Algorithms.TopologicalSorting(graph);
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void NotDAGTest(){
			var graph = TestGraphs.GetTestGraphNotDAG1();
			Assert.ThrowsException<GraphIsNotDAGException>(() => Algorithms.TopologicalSorting(graph));
		}
	}
}
