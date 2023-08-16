using GraphLibrary.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Vertices;
using GraphLibrary.Edges;

namespace GraphLibrary.Extensions.IOrientedGraphExtensions
{
	/// <summary>
	/// Extension methods for <see cref="IOrientedGraph{TVertex, TEdge}"/>
	/// Idealy IWeightedOrientedGraph<OrientedVertex, OrientedEdge>
	/// </summary>
	public static class IOrientedGraphExtensions
	{
		#region AddVertexMethods
		/// <summary>
		/// Extension method to add vertex to graph only with name <br />
		/// (Be Aware, it is possible it wont work as intented with different type of vertex than <see cref="OrientedVertex"/>)
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of vertex. Must be inherited from <see cref="OrientedVertex"/>
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of edge. Must be inherited from <see cref="OrientedEdge"/>
		/// <param name="graph">
		/// Graph to add vertex to
		/// </param>
		/// <param name="vertexName">
		/// Name of vertex to add
		/// </param>
		/// <returns>
		/// Itself with added vertex
		/// </returns>
		public static IOrientedGraph<TVertex, TEdge> AddVertex<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, VertexName vertexName)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			return graph.AddVertex(new TVertex() { Name = vertexName});
		}

		/// <summary>
		/// Extension method to add vertices to a graph only with <see cref="IEnumerable{ValueTuple{VertexName, TWeight}}"/> with vertexNames <br />
		/// (Be Aware, it is possible it wont work as intented with different type of vertex than <see cref="OrientedVertex"/>)
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of vertex. Must be inherited from <see cref="OrientedVertex"/>
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of edge. Must be inherited from <see cref="OrientedVertex"/>
		/// <param name="graph">
		/// graph to add vertices to
		/// </param>
		/// <param name="vertices">
		/// <see cref="IEnumerable{ValueTuple{VertexName}"/> of vertexNames to add
		/// </param>
		/// <returns>
		/// Itself with added vertices
		/// </returns>
		public static IOrientedGraph<TVertex, TEdge> AddVertices<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, IEnumerable<VertexName> vertices)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			foreach (var vertex in vertices)
			{
				graph.AddVertex(vertex); ;	
			}
			return graph;
		}
		#endregion

		#region AddEdgeMethods

		/// <summary>
		/// Extension method to add edge to graph only with names of source and dest vertices <br />
		/// (Be Aware, it is possible it wont work as intented with different type of edge than <see cref="OrientedEdge"/>)
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of vertex. Must be inherited from <see cref="OrientedVertex"/>
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of edge. Must be inherited from <see cref="OrientedEdge"/>
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
		/// <returns>
		/// Itself with added edge
		/// </returns>
		public static IOrientedGraph<TVertex, TEdge> AddEdge<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, VertexName vertexOut, VertexName vertexIn)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			return graph.AddEdge(new TEdge() { VertexOut = vertexOut, VertexIn = vertexIn });
		}

		/// <summary>
		/// Extension method to add edges to graph only with <see cref="IEnumerable{ValueTuple{VertexName, VertexName}}"/> with vertexNames of dest and source vertices <br />
		/// (Be Aware, it is possible it wont work as intented with different type of edge than <see cref="OrientedEdge"/>)
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of vertex. Must be inherited from <see cref="OrientedVertex"/>
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of edge. Must be inherited from <see cref="OrientedEdge"/>
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
		public static IOrientedGraph<TVertex, TEdge> AddEdges<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, IEnumerable<(VertexName vertexOut, VertexName vertexIn)> edges)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			foreach (var edge in edges)
			{
				graph.AddEdge(edge.vertexOut, edge.vertexIn);
			}
			return graph;
		}
		#endregion

		#region ReverseMethods

		/// <summary>
		/// Extension method to reverse edge in graph <br />
		/// (Be Aware, it is possible it wont work as intented with different type of edge than <see cref="OrientedEdge"/>)
		/// </summary>
		/// <typeparam name="TVertex">
		/// Vertex type. Must be inherited from <see cref="OrientedVertex"/>
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Edge type. Must be inherited from <see cref="OrientedEdge"/>
		/// </typeparam>
		/// <param name="graph">
		/// Graph to reverse edge in
		/// </param>
		/// <param name="edge">
		/// Edge to reverse
		/// </param>
		/// <returns>
		/// Itself with reversed edge
		/// </returns>
		public static IOrientedGraph<TVertex, TEdge> ReverseEdge<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, TEdge edge)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			graph.RemoveEdge(edge);
			graph.AddEdge(edge.VertexIn, edge.VertexOut);
			return graph;
		}

		/// <summary>
		/// Extension method to reverse edge in graph with given source and dest names of vertices <br />
		/// (Be Aware, it is possible it wont work as intented with different type of edge than <see cref="OrientedEdge"/>)
		/// </summary>
		/// <typeparam name="TVertex">
		/// Vertex type. Must be inherited from <see cref="OrientedVertex"/>
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Edge type. Must be inherited from <see cref="OrientedEdge"/>
		/// </typeparam>
		/// <param name="graph">
		/// Graph to reverse edge in
		/// </param>
		/// <param name="vertexIn">
		/// Vertex name to which edge goes in
		/// </param>
		/// <param name="vertexOut">
		/// Vertex name from which edge goes out
		/// </param>
		/// <returns>
		/// Itself with reversed edge
		/// </returns>
		public static IOrientedGraph<TVertex, TEdge> ReverseEdge<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, VertexName vertexOut, VertexName vertexIn)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			graph.RemoveEdge(vertexOut, vertexIn);
			graph.AddEdge(vertexIn, vertexOut);
			return graph;
		}


		/// <summary>
		/// Extension method to reverse edges in graph with given <see cref="IEnumerable{TEdge}"/> of edges to be reversed <br />
		/// (Be Aware, it is possible it wont work as intented with different type of edge than <see cref="OrientedEdge"/>)
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of vertex. Must be inherited from <see cref="OrientedVertex"/>
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of edge. Must be inherited from <see cref="OrientedEdge"/>
		/// </typeparam>
		/// <param name="graph">
		/// Graph to reverse edges in
		/// </param>
		/// <param name="edges">
		/// edges to be reversed
		/// </param>
		/// <returns>
		/// Itself with reversed edges
		/// </returns>
		public static IOrientedGraph<TVertex, TEdge> ReverseEdges<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, IEnumerable<TEdge> edges)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			foreach (var edge in edges)
				graph.ReverseEdge(edge);
			return graph;
		}

		/// <summary>
		/// Extension method to reverse edges in graph with given <see cref="IEnumerable{ValueTuple{VertexName, VertexName})"/> 
		/// of source and dest vertices of edges to be reversed <br />
		/// (Be Aware, it is possible it wont work as intented with different type of edge than <see cref="OrientedEdge"/>)
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of vertex. Must be inherited from <see cref="OrientedVertex"/>
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of edge. Must be inherited from <see cref="OrientedEdge"/>
		/// </typeparam>
		/// <param name="graph">
		/// Graph to reverse edges in
		/// </param>
		/// <param name="edges">
		/// edges to be reversed
		/// </param>
		/// <returns>
		/// Itself with reversed edges
		/// </returns>
		public static IOrientedGraph<TVertex, TEdge> ReverseEdges<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph, IEnumerable<(VertexName vertexOut, VertexName vertexIn)> edges)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			foreach (var (vertexOut, vertexIn) in edges)
				graph.ReverseEdge(vertexOut, vertexIn);
			return graph;
		}

		/// <summary>
		/// Gets new instance of graph with reversed edges
		/// (Be Aware, it is possible it wont work as intented with different type of edge than <see cref="OrientedEdge"/>
		/// and different type of graph than <see cref="IOrientedGraph{TVertex, TEdge}"/>)
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of vertex. Must be inherited from <see cref="OrientedVertex"/>
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of edge. Must be inherited from <see cref="OrientedEdge"/>
		/// </typeparam>
		/// <param name="graph">
		/// Graph to reverse edges in
		/// </param>
		/// <returns>
		/// New instance of graph with reversed edges
		/// </returns>
		public static IOrientedGraph<TVertex, TEdge> GetReversedGraph<TVertex, TEdge>(this IOrientedGraph<TVertex, TEdge> graph)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			var newGraph = new OrientedGraph<TVertex, TEdge>();
			foreach (var vertex in graph.GetVertices())
			{
				newGraph.AddVertex(vertex.Name);
			}
			foreach (var edge in graph.GetEdges())
			{
				newGraph.AddEdge(edge.VertexIn, edge.VertexOut);
			}
			return newGraph;
		}
		#endregion
	}
}
