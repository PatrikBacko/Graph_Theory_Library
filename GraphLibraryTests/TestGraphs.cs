using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests
{
	public static class TestGraphs
	{
		public static IOrientedGraph<OrientedVertex, OrientedEdge> VertexTestGraph1
			=> OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph()
					.AddVertex(new OrientedVertex("0"))
					.AddVertex(new OrientedVertex("1"))
					.AddVertex(new OrientedVertex("2"));

		public static IOrientedGraph<OrientedVertex, OrientedEdge> GetVertexTestGraph(int vertexCount){
  			var graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
				for (int i = 0; i < vertexCount; i++)
				{
					graph.AddVertex(new OrientedVertex(i.ToString()));
				}
				return graph;	
		}

		public static IOrientedGraph<OrientedVertex, OrientedEdge> DagTestGraph1
			=> OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph()
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
		public static IOrientedGraph<OrientedVertex, OrientedEdge> DagTestGraph2
			=> OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph()
				.AddVertex(new OrientedVertex("0"))
				.AddVertex(new OrientedVertex("1"))
				.AddVertex(new OrientedVertex("2"))
				.AddVertex(new OrientedVertex("3"))
				.AddEdge(new OrientedEdge("0", "1"))
				.AddEdge(new OrientedEdge("0", "2"))
				.AddEdge(new OrientedEdge("1", "3"))
				.AddEdge(new OrientedEdge("2", "3"));

		public static IOrientedGraph<OrientedVertex, OrientedEdge> NotDagTestGraph1
			=> OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph()
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
