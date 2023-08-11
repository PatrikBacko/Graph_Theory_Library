using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using GraphLibrary.Edges;
using System.Numerics;
using GraphLibrary.Vertices;
using GraphLibrary.Extensions.IOrientedGraphExtensions;

namespace GraphLibraryTests
{
	[TestClass]
	public class Tests
	{
		[TestMethod]
		public void Test1()
		{
			var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(5);

			graph.AddEdge("1", "3");
			graph.ReverseEdge("1", "3");

			var graph2 = graph.GetReversedGraph();

			double MojaLáskaKuKiki = double.PositiveInfinity;
		}
	}
}
