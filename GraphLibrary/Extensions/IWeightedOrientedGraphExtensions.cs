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
		#region AddVertexMethods

		/// <summary>
		/// Extension method to add vertex to graph only with name and weight <br />
		/// (Be Aware, it is possible it wont work as intented with different type of vertex than <see cref="WeightedOrientedVertex{TWeight}"/>)
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of vertex. Must be inherited from <see cref="WeightedOrientedVertex{TWeight}"/>
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of edge. Must be inherited from <see cref="WeightedOrientedEdge{TWeight}"/>
		/// </typeparam>
		/// <typeparam name="TWeight">
		/// Type of weight. Must be inherited from <see cref="INumber{TWeight}"/>
		/// </typeparam>
		/// <param name="graph">
		/// Graph to add vertex to
		/// </param>
		/// <param name="vertexName">
		/// Name of vertex to add
		/// </param>
		/// <param name="weight">
		/// Weight of vertex to add
		/// </param>
		/// <returns>
		/// Itself with added vertex
		/// </returns>
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

		/// <summary>
		/// Extension method to add vertices to a graph only with <see cref="IEnumerable{ValueTuple{VertexName, TWeight}}"/> with vertexNames and weights <br />
		/// (Be Aware, it is possible it wont work as intented with different type of vertex than <see cref="WeightedOrientedVertex{TWeight}"/>)
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of vertex. Must be inherited from <see cref="WeightedOrientedVertex{TWeight}"/>
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of edge. Must be inherited from <see cref="WeightedOrientedEdge{TWeight}"/>
		/// </typeparam>
		/// <typeparam name="TWeight">
		/// TWype of weight. Must be inherited from <see cref="INumber{TWeight}"/>
		/// </typeparam>
		/// <param name="graph">
		/// graph to add vertices to
		/// </param>
		/// <param name="vertices">
		/// <see cref="IEnumerable{ValueTuple{VertexName, TWeight}}"/> of vertexNames and weights to add
		/// </param>
		/// <returns>
		/// Itself with added vertices
		/// </returns>
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
		#endregion

		#region AddEdgeMethods
		/// <summary>
		/// Extension method to add edge to graph only with names of source and dest vertices and weight <br />
		/// (Be Aware, it is possible it wont work as intented with different type of edge than <see cref="WeightedOrientedEdge{TWeight}{TWeight}"/>)
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of vertex. Must be inherited from <see cref="WeightedOrientedVertex{TWeight}"/>
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of edge. Must be inherited from <see cref="WeightedOrientedEdge{TWeight}"/>
		/// </typeparam>
		/// <typeparam name="TWeight"> 
		/// Type of weight. Must be inherited from <see cref="INumber{TWeight}"/>
		/// </typeparam>
		/// <param name="graph">
		/// graph to add edge to
		/// </param>
		/// <param name="vertexOut">
		/// Name of vertex from which edge goes out
		/// </param>
		/// <param name="vertexIn">
		/// Name of vertex to which edge goes in
		/// </param>
		/// <param name="weight">
		/// Weight of edge to add
		/// </param>
		/// <returns>
		/// Itself with added edge
		/// </returns>
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

		/// <summary>
		/// Extension method to add edges to graph only with <see cref="IEnumerable{ValueTuple{VertexName, VertexName, TWeight}}"/> with vertexNames of dest and source vertices and weights <br />
		/// (Be Aware, it is possible it wont work as intented with different type of edge than <see cref="WeightedOrientedEdge{TWeight}"/>)
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of vertex. Must be inherited from <see cref="WeightedOrientedVertex{TWeight}"/>
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of edge. Must be inherited from <see cref="WeightedOrientedEdge{TWeight}"/>
		/// </typeparam>
		/// <typeparam name="TWeight">
		/// Type of weight. Must be inherited from <see cref="INumber{TWeight}"/>
		/// </typeparam>
		/// <param name="graph">
		/// Graph to add edges to
		/// </param>
		/// <param name="edges">
		/// Edges to add
		/// </param>
		/// <returns>
		/// Itself with added edges
		/// </returns>
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
		#endregion
	}
}
