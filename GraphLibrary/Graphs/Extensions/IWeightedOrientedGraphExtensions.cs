using GraphLibrary.Edges;
using GraphLibrary.Graphs;
using GraphLibrary.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GraphLibrary.Extensions.IOrientedGraphExtensions;

namespace GraphLibrary.Extensions.IWeightedOrientedGraphExtensions
{
	public static class IWeightedOrientedGraphExtensions
	{
		public static IWeightedOrientedGraph<TVertex, TEdge, TWeight> AddVertex<TVertex, TEdge, TWeight>
		(this IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, VertexName vertexName, TWeight weight)
			where TVertex : WeightedOrientedVertex<TWeight>, new()
			where TEdge : WeightedOrientedEdge<TWeight>, new()
			where TWeight : INumber<TWeight>, new()
		{
			var vertex = new TVertex() { Name = vertexName };
			vertex.ChangeWeight(weight);
			return graph.AddVertex(vertex);
		}

		public static IWeightedOrientedGraph<TVertex, TEdge, TWeight> AddVertices<TVertex, TEdge, TWeight>
		(this IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, IEnumerable<(VertexName vertexName, TWeight weight)> vertices)
			where TVertex : WeightedOrientedVertex<TWeight>, new()
			where TEdge : WeightedOrientedEdge<TWeight>, new()
			where TWeight : INumber<TWeight>, new()
		{
			foreach (var (vertex, weight) in vertices)
			{
				graph.AddVertex(vertex, weight); ;
			}
			return graph;
		}

		public static IWeightedOrientedGraph<TVertex, TEdge, TWeight> AddEdge<TVertex, TEdge, TWeight>
		(this IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, VertexName vertexOut, VertexName vertexIn, TWeight weight)
			where TVertex : WeightedOrientedVertex<TWeight>, new()
			where TEdge : WeightedOrientedEdge<TWeight>, new()
			where TWeight : INumber<TWeight>, new()
		{
			var edge= new TEdge() { VertexOut = vertexOut, VertexIn = vertexIn };
			edge.ChangeWeight(weight);
			return graph.AddEdge(edge);
		}

		public static IWeightedOrientedGraph<TVertex, TEdge, TWeight> AddEdges<TVertex, TEdge, TWeight>
		(this IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, IEnumerable<(VertexName vertexOut, VertexName vertexIn, TWeight weight)> edges)
			where TVertex : WeightedOrientedVertex<TWeight>, new()
			where TEdge : WeightedOrientedEdge<TWeight>, new()
			where TWeight : INumber<TWeight>, new()
		{
			foreach (var edge in edges)
			{
				graph.AddEdge(edge.vertexOut, edge.vertexIn, edge.weight);
			}
			return graph;
		}
	}
}
