using GraphLibrary.Edges;
using GraphLibrary.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Graphs
{
	public interface IOrientedGraph<TVertex, TEdge> 
		where TVertex : OrientedVertex
		where TEdge : OrientedEdge
	{
		IOrientedGraph<TVertex, TEdge> ClearGraph();

		int GetVertexCount();
		int GetEdgeCount();

		IEnumerable<TVertex> GetVerticies();
		IEnumerable<TVertex> GetVerticesWith(OrientedVertexPredicate<TVertex> vertexPredicate);
		IEnumerable<TEdge> GetEdges();
		IEnumerable<TEdge> GetEdgesWith(OrientedEdgePredicate<TEdge> edgePredicate);

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

		IOrientedGraph<TVertex, TEdge> RemoveEdge(VertexName vertexOut, VertexName vertexIn);
		IOrientedGraph<TVertex, TEdge> RemoveEdge(TEdge edge);

		static abstract IOrientedGraph<TVertex, TEdge> CreateGraph();
		static abstract IOrientedGraph<TVertex, TEdge> CreateGraph(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges);
		//IOrientedGraph<TVertex, TEdge> ReverseEdge(TEdge edge);
		//IOrientedGraph<TVertex, TEdge> ReverseEdges();
		//IOrientedGraph<TVertex, TEdge> ReverseEdges(IEnumerable<TEdge> edges);

		static IOrientedGraph<TVertex, TEdge> operator +(IOrientedGraph<TVertex, TEdge> graph, TVertex vertex) => graph.AddVertex(vertex);
		static IOrientedGraph<TVertex, TEdge> operator +(IOrientedGraph<TVertex, TEdge> graph, TEdge edge) => graph.AddEdge(edge);

		static IOrientedGraph<TVertex, TEdge> operator -(IOrientedGraph<TVertex, TEdge> graph, TVertex vertex) => graph.RemoveVertex(vertex);
		static IOrientedGraph<TVertex, TEdge> operator -(IOrientedGraph<TVertex, TEdge> graph, TEdge edge) => graph.RemoveEdge(edge);
	}
}
