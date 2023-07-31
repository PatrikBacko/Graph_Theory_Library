using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GraphThoeryLibrary.Edges;
using GraphThoeryLibrary.Vertices;

namespace GraphThoeryLibrary.Graphs
{
    public interface IOrientedGraph
    {
        IEnumerable<Vertex> GetVertices();
		IEnumerable<IOrientedEdge> GetEdges();

		IEnumerable<Vertex> GetInAdjacentVertices(Vertex vertex);
		IEnumerable<Vertex> GetInAdjacentVertices(string nameOfVertex);

		IEnumerable<Vertex> GetOutAdjacentVertices(Vertex vertex);
		IEnumerable<Vertex> GetOutAdjacentVertices(string nameOfVertex);

		IEnumerable<IOrientedEdge> GetInEdges(Vertex vertex);
		IEnumerable<IOrientedEdge> GetInEdges(string nameOfVertex);

		IEnumerable<IOrientedEdge> GetOutEdges(Vertex vertex);
		IEnumerable<IOrientedEdge> GetOutEdges(string nameOfVertex);

		Vertex ReturnVertexWithName(string nameOfVertex);

		int GetInDegree(Vertex vertex);
		int GetInDegree(string nameOfVertex);

		int GetOutDegree(Vertex vertex);
		int GetOutDegree(string nameOfVertex);

		int GetVertexCount();
        int GetEdgeCount();

        bool IsVertex(Vertex vertex);
		bool IsVertex(string nameOfVertex);

		bool IsEdge(Vertex vertexIn, Vertex vertexOut);  //edge: vertexIn -> vertexOut
		bool IsEdge(string NameOfVertexIn, string NameOfVertexOut);  //edge: vertexIn -> vertexOut
		bool IsEdge(IOrientedEdge edge);  //edge: vertexIn -> vertexOut


		IOrientedGraph ClearGraph();
		IOrientedGraph AddVertex(Vertex vertex);
		IOrientedGraph AddVertex(string nameOfVertex);

		IOrientedGraph AddEdge(IOrientedEdge edge);
		IOrientedGraph AddEdge(Vertex vertexOut, Vertex vertexIn); //adds edge: vertexOut -> vertexIn
		IOrientedGraph AddEdge(string nameOfVertexIn, string nameOfVertexOut); //adds edge: vertexOut -> vertexIn


		IOrientedGraph RemoveVertex(Vertex vertex);
		IOrientedGraph RemoveVertex(string nameOfVertex);

		IOrientedGraph RemoveEdge(IOrientedEdge edge);
        IOrientedGraph RemoveEdge(Vertex vertexOut, Vertex vertexIn); //removes edge: vertexOut -> vertexIn
		IOrientedGraph RemoveEdge(string nameOfVertexOut, string nameOfVertexIn); //removes edge: vertexOut -> vertexIn


		static IOrientedGraph operator +(IOrientedGraph graph, Vertex vertex) => graph.AddVertex(vertex);
        static IOrientedGraph operator +(IOrientedGraph graph, IOrientedEdge edge) => graph.AddEdge(edge);
        static IOrientedGraph operator +(IOrientedGraph graph, (Vertex, Vertex) edge) => graph.AddEdge(edge.Item1, edge.Item2);

        static IOrientedGraph operator -(IOrientedGraph graph, Vertex vertex) => graph.RemoveVertex(vertex);
        static IOrientedGraph operator -(IOrientedGraph graph, IOrientedEdge edge) => graph.RemoveEdge(edge);
        static IOrientedGraph operator -(IOrientedGraph graph, (Vertex, Vertex) edge) => graph.RemoveEdge(edge.Item1, edge.Item2);

    }
}
