using GraphLibrary.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Vertices;
using GraphLibrary.Edges;
using GraphLibrary.Extensions.StringExtensions;

namespace GraphLibrary.Extensions.IOrientedGraphExtensions
{
	public static class IOrientedGraphExtensions
	{
		public static IOrientedGraph<TVertex, TEdge> AddVertex<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, VertexName vertexName)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			return graph.AddVertex(new TVertex() { Name = vertexName});
		}

		public static IOrientedGraph<TVertex, TEdge> AddVertices<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, IEnumerable<VertexName> vertices)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			foreach (var vertex in vertices)
			{
				graph.AddVertex(vertex);
			}
			return graph;
		}

		public static IOrientedGraph<TVertex, TEdge> AddEdge<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, VertexName vertexOut, VertexName vertexIn)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			return graph.AddEdge(new TEdge() { VertexOut = vertexOut, VertexIn = vertexIn });
		}

		public static IOrientedGraph<TVertex, TEdge> AddEdges<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, IEnumerable<(VertexName vertexOut, VertexName vertexIn)> edges)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			foreach (var edge in edges)
			{
				graph.AddEdge(edge.Item1, edge.Item2);
			}
			return graph;
		}

		public static IOrientedGraph<TVertex, TEdge> ReverseEdge<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, TEdge edge)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			graph.RemoveEdge(edge);
			graph.AddEdge(edge.VertexIn, edge.VertexOut);
			return graph;
		}

		public static IOrientedGraph<TVertex, TEdge> ReverseEdge<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, VertexName vertexOut, VertexName vertexIn)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
			=> graph.ReverseEdge(graph.GetEdge(vertexOut, vertexIn));
		


		public static IOrientedGraph<TVertex, TEdge> ReverseEdges<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, IEnumerable<TEdge> edges)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			foreach (var edge in edges)
				graph.ReverseEdge(edge);
			return graph;
		}

		public static IOrientedGraph<TVertex, TEdge> ReverseEdges<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, IEnumerable<(VertexName vertexOut, VertexName vertexIn)> edges)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			foreach (var (vertexOut, vertexIn) in edges)
				graph.ReverseEdge(vertexOut, vertexIn);
			return graph;
		}
		public static IOrientedGraph<TVertex, TEdge> ReverseGraph<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			var newGraph = new OrientedGraph<TVertex, TEdge>();
			foreach (var vertex in graph.GetVertices())
			{
				newGraph.AddVertex(vertex);
			}
			foreach (var edge in graph.GetEdges())
			{
				newGraph.AddEdge(edge.VertexIn, edge.VertexOut);
			}
			return newGraph;
		}
	}
}
