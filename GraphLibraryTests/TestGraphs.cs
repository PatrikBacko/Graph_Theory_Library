using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests
{
	public static class TestGraphs
	{
		public static IOrientedGraph<OrientedVertex, OrientedEdge> GetTestGraphDAG1(){
			return OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph()
				.AddVertex(new OrientedVertex("0"))
				.AddVertex(new OrientedVertex("1"))
				.AddVertex(new OrientedVertex("2"))
				.AddVertex(new OrientedVertex("3"))
				.AddVertex(new OrientedVertex("4"))
				.AddVertex(new OrientedVertex("5"))
				.AddEdge(new OrientedEdge("2", "3"))
				.AddEdge(new OrientedEdge("3", "1"))
				.AddEdge(new OrientedEdge("4", "0"))
				.AddEdge(new OrientedEdge("4", "1"))
				.AddEdge(new OrientedEdge("5", "0"))
				.AddEdge(new OrientedEdge("5", "2"));
		}
		public static IOrientedGraph<OrientedVertex, OrientedEdge> GetTestGraphDAG2()
		{
			return OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph()
				.AddVertex(new OrientedVertex("0"))
				.AddVertex(new OrientedVertex("1"))
				.AddVertex(new OrientedVertex("2"))
				.AddVertex(new OrientedVertex("3"))
				.AddEdge(new OrientedEdge("0", "1"))
				.AddEdge(new OrientedEdge("0", "2"))
				.AddEdge(new OrientedEdge("1", "3"))
				.AddEdge(new OrientedEdge("2", "3"));
		}

		public static IOrientedGraph<OrientedVertex, OrientedEdge> GetTestGraphNotDAG1()
		{
			return OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph()
				.AddVertex(new OrientedVertex("0"))
				.AddVertex(new OrientedVertex("1"))
				.AddVertex(new OrientedVertex("2"))
				.AddVertex(new OrientedVertex("3"))
				.AddVertex(new OrientedVertex("4"))
				.AddVertex(new OrientedVertex("5"))
				.AddEdge(new OrientedEdge("2", "3"))
				.AddEdge(new OrientedEdge("3", "1"))
				.AddEdge(new OrientedEdge("4", "0"))
				.AddEdge(new OrientedEdge("4", "1"))
				.AddEdge(new OrientedEdge("5", "0"))
				.AddEdge(new OrientedEdge("5", "2"))
				.AddEdge(new OrientedEdge("3", "5"));
		}
	}
}
