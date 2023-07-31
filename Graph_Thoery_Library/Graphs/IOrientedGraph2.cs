using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using global::GraphThoeryLibrary.Edges;
using global::GraphThoeryLibrary.Vertices;
using GraphThoeryLibrary.Edges;
using GraphThoeryLibrary.Vertices;

namespace GraphThoeryLibrary.Graphs
{
	public interface IOrientedGraph2
	{
		IEnumerable<OrientedVertex> GetVertices();
		IEnumerable<IOrientedEdge> GetEdges();

		IEnumerable<OrientedVertex> GetInAdjacentVertices(OrientedVertex vertex);
		IEnumerable<OrientedVertex> GetInAdjacentVertices(string nameOfVertex);

		IEnumerable<OrientedVertex> GetOutAdjacentVertices(OrientedVertex vertex);
		IEnumerable<OrientedVertex> GetOutAdjacentVertices(string nameOfVertex);

		IEnumerable<IOrientedEdge> GetInEdges(OrientedVertex vertex);
		IEnumerable<IOrientedEdge> GetInEdges(string nameOfVertex);

		IEnumerable<IOrientedEdge> GetOutEdges(OrientedVertex vertex);
		IEnumerable<IOrientedEdge> GetOutEdges(string nameOfVertex);

		OrientedVertex ReturnVertexWithName(string nameOfVertex);

		int GetInDegree(OrientedVertex vertex);
		int GetInDegree(string nameOfVertex);

		int GetOutDegree(OrientedVertex vertex);
		int GetOutDegree(string nameOfVertex);

		int GetVertexCount();
		int GetEdgeCount();

		bool IsVertex(OrientedVertex vertex);
		bool IsVertex(string nameOfVertex);

		bool IsEdge(OrientedVertex vertexIn, OrientedVertex vertexOut);  //edge: vertexIn -> vertexOut
		bool IsEdge(string NameOfVertexIn, string NameOfVertexOut);  //edge: vertexIn -> vertexOut
		bool IsEdge(IOrientedEdge edge);  //edge: vertexIn -> vertexOut


		IOrientedGraph2 ClearGraph();
		IOrientedGraph2 AddVertex(OrientedVertex vertex);
		IOrientedGraph2 AddVertex(string nameOfVertex);

		IOrientedGraph2 AddEdge(IOrientedEdge edge);
		IOrientedGraph2 AddEdge(OrientedVertex vertexOut, OrientedVertex vertexIn); //adds edge: vertexOut -> vertexIn
		IOrientedGraph2 AddEdge(string nameOfVertexIn, string nameOfVertexOut); //adds edge: vertexOut -> vertexIn


		IOrientedGraph2 RemoveVertex(OrientedVertex vertex);
		IOrientedGraph2 RemoveVertex(string nameOfVertex);

		IOrientedGraph2 RemoveEdge(IOrientedEdge edge);
		IOrientedGraph2 RemoveEdge(OrientedVertex vertexOut, OrientedVertex vertexIn); //removes edge: vertexOut -> vertexIn
		IOrientedGraph2 RemoveEdge(string nameOfVertexOut, string nameOfVertexIn); //removes edge: vertexOut -> vertexIn


		static IOrientedGraph2 operator +(IOrientedGraph2 graph, OrientedVertex vertex) => graph.AddVertex(vertex);
		static IOrientedGraph2 operator +(IOrientedGraph2 graph, IOrientedEdge edge) => graph.AddEdge(edge);
		static IOrientedGraph2 operator +(IOrientedGraph2 graph, (OrientedVertex, OrientedVertex) edge) => graph.AddEdge(edge.Item1, edge.Item2);

		static IOrientedGraph2 operator -(IOrientedGraph2 graph, OrientedVertex vertex) => graph.RemoveVertex(vertex);
		static IOrientedGraph2 operator -(IOrientedGraph2 graph, IOrientedEdge edge) => graph.RemoveEdge(edge);
		static IOrientedGraph2 operator -(IOrientedGraph2 graph, (OrientedVertex, OrientedVertex) edge) => graph.RemoveEdge(edge.Item1, edge.Item2);

	}

}
