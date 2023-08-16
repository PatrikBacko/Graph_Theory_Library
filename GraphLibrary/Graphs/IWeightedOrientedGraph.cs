using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Edges;
using GraphLibrary.Vertices;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Data;
using GraphLibrary.Graphs.Delegates;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace GraphLibrary.Graphs
{
	public interface IWeightedOrientedGraph<TVertex, TEdge,  TWeight> : IOrientedGraph<TVertex, TEdge>
		where TVertex : WeightedOrientedVertex<TWeight> 
		where TEdge : WeightedOrientedEdge<TWeight> 
		where TWeight : INumber<TWeight>
	{
		#region GetWeightMethods
		/// <summary>
		/// Get weight of vertex with given name
		/// </summary>
		/// <param name="vertex">
		/// Name of vertex
		/// </param>
		/// <returns>
		/// Weight of vertex with given name
		/// </returns>
		TWeight GetWeight(VertexName vertex);

		/// <summary>
		/// Get weight of edge with given vertices
		/// </summary>
		/// <param name="vertexOut">
		/// name of source vertex of edge
		/// </param>
		/// <param name="vertexIn">
		/// name of destination vertex of edge
		/// </param>
		/// <returns>
		/// Weight of edge with given vertices
		/// </returns>
		TWeight GetWeight(VertexName vertexOut, VertexName vertexIn);
		#endregion

		#region ChangeWeightMethods
		/// <summary>
		/// Change weight of vertex with given name,
		/// weight type must remain the same
		/// </summary>
		/// <param name="vertex">
		/// Vertex name of vertex which weight will be changed
		/// </param>
		/// <param name="weight">
		/// New value of weight
		/// </param>
		/// <returns>
		/// Itself with changed weight of vertex
		/// </returns>
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> ChangeVertexWeight(VertexName vertex, TWeight weight);

		/// <summary>
		/// Change weight of edge with given vertices,
		/// weight type must remain the same
		/// </summary>
		/// <param name="vertexOut">
		/// name of source vertex of edge
		/// </param>
		/// <param name="vertexIn">
		/// name of destination vertex of edge
		/// </param>
		/// <param name="weight">
		/// new value of weight
		/// </param>
		/// <returns>
		/// Itself with changed weight of edge
		/// </returns>
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> ChangeEdgeWeight(VertexName vertexOut, VertexName vertexIn, TWeight weight);
		#endregion

		#region CoverMethodsOfIOrientedGraphMethods
		// Cover methods of IOrientedGraph methods because of the fluent syntax

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.ApplyToVertices(VertexAction{TVertex})"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToVertices(VertexAction<TVertex> vertexAction);

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.ApplyToVerticesWith(VertexPredicate{TVertex}, VertexAction{TVertex})"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToVerticesWith(VertexPredicate<TVertex> vertexPredicate, VertexAction<TVertex> vertexAction);


		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.ApplyToEdges(EdgeAction{TEdge})"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToEdges(EdgeAction<TEdge> edgeAction);

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.ApplyToEdgesWith(EdgePredicate{TEdge}, EdgeAction{TEdge})"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToEdgesWith(EdgePredicate<TEdge> edgePredicate, EdgeAction<TEdge> edgeAction);


		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.AddVertex(TVertex)"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> AddVertex(TVertex vertex);

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.AddVertices(IEnumerable{TVertex}"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> AddVertices(IEnumerable<TVertex> vertices);


		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.AddEdge(TEdge)"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> AddEdge(TEdge edge);

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.AddEdges(IEnumerable{TEdge})"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> AddEdges(IEnumerable<TEdge> edges);


		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.RemoveVertex(VertexName)"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertex(VertexName vertex);

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.RemoveVertex(TVertex)"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertex(TVertex vertex);

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.RemoveVertices(IEnumerable{TVertex})"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertices(IEnumerable<TVertex> vertices);

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.RemoveVertices(IEnumerable{VertexName)"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertices(IEnumerable<VertexName> vertices);

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.RemoveVerticesWith(VertexPredicate{TVertex})"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVerticesWith(VertexPredicate<TVertex> vertices);


		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.RemoveEdge(VertexName, VertexName)"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdge(VertexName vertexOut, VertexName vertexIn);

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.RemoveEdge(TEdge)"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdge(TEdge edge);

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.RemoveEdges(IEnumerable{TEdge})"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdges(IEnumerable<TEdge> edges);

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.RemoveEdges(IEnumerable{ValueTuple{VertexName, VertexName}})"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdges(IEnumerable<(VertexName vertexOut, VertexName vertexIn)> edges);

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.RemoveEdgesWith(EdgePredicate{TEdge})"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdgesWith(EdgePredicate<TEdge> edges);


		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.Clear"/>
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> Clear();

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.Create()"/>
		new static abstract IWeightedOrientedGraph<TVertex, TEdge, TWeight> Create();

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.Create(IEnumerable{TVertex}, IEnumerable{TEdge})"/>
		new static abstract IWeightedOrientedGraph<TVertex, TEdge, TWeight> Create(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges);


		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.LoadFromJson(string)"/>
		static abstract new IWeightedOrientedGraph<TVertex, TEdge, TWeight> LoadFromJson(string path);

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.DeserializeFromJson(string)"/>
		static abstract new IWeightedOrientedGraph<TVertex, TEdge, TWeight> DeserializeFromJson(string jsonString);

		/// <inheritdoc cref="IOrientedGraph{TVertex, TEdge}.DeserializeFromJson(string, JsonSerializerOptions)"/>
		static abstract new IWeightedOrientedGraph<TVertex, TEdge, TWeight> DeserializeFromJson(string jsonString, JsonSerializerOptions options);
		#endregion

		#region Operators
		/// <summary>
		/// Overriden operator + <br />
		/// Functions same as <see cref="AddVertex(TVertex)"/> <br />
		/// </summary>
		/// <param name="graph">
		/// graph to which vertex is added
		/// </param>
		/// <param name="vertex">
		/// vertex to be added to the graph
		/// </param>
		/// <returns>
		/// Itself after adding vertex
		/// </returns>
		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> operator +(IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TVertex vertex) => graph.AddVertex(vertex);

		/// <summary>
		/// Overriden operator + <br />
		/// Functions same as <see cref="AddEdge(TEdge)"/> <br />
		/// </summary>
		/// <param name="graph">
		/// graph to which edge is added
		/// </param>
		/// <param name="edge">
		/// edge to be added to the graph
		/// </param>
		/// <returns>
		/// Itself after adding edge
		/// </returns>
		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> operator +(IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TEdge edge) => graph.AddEdge(edge);


		/// <summary>
		/// Overriden operator - <br />
		/// Functions same as <see cref="RemoveVertex(TVertex)"/> <br />
		/// </summary>
		/// <param name="graph">
		/// graph from which vertex is removed
		/// </param>
		/// <param name="vertex">
		/// vertex to be removed from the graph
		/// </param>
		/// <returns>
		/// Itself after removing vertex
		/// </returns>
		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> operator -(IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TVertex vertex) => graph.RemoveVertex(vertex);

		/// <summary>
		/// Overriden operator - <br />
		/// Functions same as <see cref="RemoveEdge(TEdge)"/> <br />
		/// </summary>
		/// <param name="graph">
		/// graph from which edge is removed
		/// </param>
		/// <param name="edge">
		/// edge to be removed from the graph
		/// </param>
		/// <returns>
		/// Itself after removing edge
		/// </returns>
		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> operator -(IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TEdge edge) => graph.RemoveEdge(edge);
		#endregion
	}
}
