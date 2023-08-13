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
		TWeight GetWeight(VertexName vertex);
		TWeight GetWeight(VertexName vertexOut, VertexName vertexIn);
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToVertices(VertexAction<TVertex> vertexAction);
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToVerticesWith(VertexPredicate<TVertex> vertexPredicate, VertexAction<TVertex> vertexAction);

		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToEdges(EdgeAction<TEdge> edgeAction);
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToEdgesWith(EdgePredicate<TEdge> edgePredicate, EdgeAction<TEdge> edgeAction);

		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> AddVertex(TVertex vertex);
		//IOrientedGraph<TVertex, TEdge> AddVertex(VertexName vertex);
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> AddVertices(IEnumerable<TVertex> vertices);


		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> AddEdge(TEdge edge);
		//IOrientedGraph<TVertex, TEdge> AddEdge(VertexName vertexOut, VertexName vertexIn);
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> AddEdges(IEnumerable<TEdge> edges);

		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertex(VertexName vertex);
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertex(TVertex vertex);
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertices(IEnumerable<TVertex> vertices);
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertices(IEnumerable<VertexName> vertices);
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVerticesWith(VertexPredicate<TVertex> vertices);

		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdge(VertexName vertexOut, VertexName vertexIn);
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdge(TEdge edge);
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdges(IEnumerable<TEdge> edges);
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdges(IEnumerable<(VertexName vertexOut, VertexName vertexIn)> edges);
		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdgesWith(EdgePredicate<TEdge> edges);


		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> Clear();
		new static abstract IWeightedOrientedGraph<TVertex, TEdge, TWeight> Create();
		new static abstract IWeightedOrientedGraph<TVertex, TEdge, TWeight> Create(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> ChangeVertexWeight(VertexName vertex, TWeight weight);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> ChangeEdgeWeight(VertexName vertexOut, VertexName vertexIn, TWeight weight);

		static abstract new IWeightedOrientedGraph<TVertex, TEdge, TWeight> LoadFromJson(string path);
		static abstract new IWeightedOrientedGraph<TVertex, TEdge, TWeight> DeserializeFromJson(string jsonString);
		static abstract new IWeightedOrientedGraph<TVertex, TEdge, TWeight> DeserializeFromJson(string jsonString, JsonSerializerOptions options);



		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> operator +(IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TVertex vertex) => graph.AddVertex(vertex);
		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> operator +(IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TEdge edge) => graph.AddEdge(edge);

		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> operator -(IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TVertex vertex) => graph.RemoveVertex(vertex);
		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> operator -(IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TEdge edge) => graph.RemoveEdge(edge);
	}
}
