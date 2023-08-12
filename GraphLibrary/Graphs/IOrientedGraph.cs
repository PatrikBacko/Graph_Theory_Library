using GraphLibrary.Edges;
using GraphLibrary.Graphs.Delegates;
using GraphLibrary.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GraphLibrary.Graphs
{
    public interface IOrientedGraph<TVertex, TEdge> 
		where TVertex : OrientedVertex
		where TEdge : OrientedEdge
	{
		IOrientedGraph<TVertex, TEdge> Clear();

		int GetVertexCount();
		int GetEdgeCount();

		IEnumerable<TVertex> GetVertices();
		IEnumerable<TVertex> GetVerticesWith(VertexPredicate<TVertex> vertexPredicate);
		IEnumerable<TEdge> GetEdges();
		IEnumerable<TEdge> GetEdgesWith(EdgePredicate<TEdge> edgePredicate);

		IOrientedGraph<TVertex, TEdge> ApplyToVertices(VertexAction<TVertex> vertexAction);
		IOrientedGraph<TVertex, TEdge> ApplyToVerticesWith(VertexPredicate<TVertex> vertexPredicate, VertexAction<TVertex> vertexAction);

		IOrientedGraph<TVertex, TEdge> ApplyToEdges(EdgeAction<TEdge> edgeAction);
		IOrientedGraph<TVertex, TEdge> ApplyToEdgesWith(EdgePredicate<TEdge> edgePredicate, EdgeAction<TEdge> edgeAction);

		TVertex GetVertex(VertexName vertex);
		TEdge GetEdge(VertexName vertexOut, VertexName vertexIn);

		bool IsVertex(VertexName vertexName);
		bool IsVertex(TVertex vertex);
		bool IsEdge(VertexName vertexOut, VertexName vertexIn);
		bool IsEdge(TEdge edge);

		IEnumerable<TVertex> GetInAdjacentVertices(VertexName vertex);
		IEnumerable<TVertex> GetOutAdjacentVertices(VertexName vertex);
		IEnumerable<TEdge> GetInEdges(VertexName vertex);
		IEnumerable<TEdge> GetOutEdges(VertexName vertex);

		int GetInDegree(VertexName vertex);
		int GetOutDegree(VertexName vertex);

		IOrientedGraph<TVertex, TEdge> AddVertex(TVertex vertex);
		//IOrientedGraph<TVertex, TEdge> AddVertex(VertexName vertex);
		IOrientedGraph<TVertex, TEdge> AddVertices(IEnumerable<TVertex> vertices);


		IOrientedGraph<TVertex, TEdge> AddEdge(TEdge edge);
		//IOrientedGraph<TVertex, TEdge> AddEdge(VertexName vertexOut, VertexName vertexIn);
		IOrientedGraph<TVertex, TEdge> AddEdges(IEnumerable<TEdge> edges);

		IOrientedGraph<TVertex, TEdge> RemoveVertex(VertexName vertex);
		IOrientedGraph<TVertex, TEdge> RemoveVertex(TVertex vertex);
		IOrientedGraph<TVertex, TEdge> RemoveVertices(IEnumerable<TVertex> vertices);
		IOrientedGraph<TVertex, TEdge> RemoveVertices(IEnumerable<VertexName> vertices);
		IOrientedGraph<TVertex, TEdge> RemoveVerticesWith(VertexPredicate<TVertex> vertices);

		IOrientedGraph<TVertex, TEdge> RemoveEdge(VertexName vertexOut, VertexName vertexIn);
		IOrientedGraph<TVertex, TEdge> RemoveEdge(TEdge edge);
		IOrientedGraph<TVertex, TEdge> RemoveEdges(IEnumerable<TEdge> edges);
		IOrientedGraph<TVertex, TEdge> RemoveEdges(IEnumerable<(VertexName vertexOut, VertexName vertexIn)> edges);
		IOrientedGraph<TVertex, TEdge> RemoveEdgesWith(EdgePredicate<TEdge> edges);


		static abstract IOrientedGraph<TVertex, TEdge> Create();
		static abstract IOrientedGraph<TVertex, TEdge> Create(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges);

		string SerializeToJson();
		void SaveToJson(string path);


		static abstract IOrientedGraph<TVertex, TEdge> DeserializeFromJson(string jsonString);
		static abstract IOrientedGraph<TVertex, TEdge> LoadFromJson(string path);

		static IOrientedGraph<TVertex, TEdge> operator +(IOrientedGraph<TVertex, TEdge> graph, TVertex vertex) => graph.AddVertex(vertex);
		static IOrientedGraph<TVertex, TEdge> operator +(IOrientedGraph<TVertex, TEdge> graph, TEdge edge) => graph.AddEdge(edge);

		static IOrientedGraph<TVertex, TEdge> operator -(IOrientedGraph<TVertex, TEdge> graph, TVertex vertex) => graph.RemoveVertex(vertex);
		static IOrientedGraph<TVertex, TEdge> operator -(IOrientedGraph<TVertex, TEdge> graph, TEdge edge) => graph.RemoveEdge(edge);
	}
}
