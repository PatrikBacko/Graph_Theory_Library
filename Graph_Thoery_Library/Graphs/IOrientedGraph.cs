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
        IEnumerable<IOrientedVertex> GetVertices();
		IEnumerable<IOrientedEdge> GetEdges();

		IEnumerable<IOrientedVertex> GetInAdjacentVertices(IOrientedVertex vertex);
		IEnumerable<IOrientedVertex> GetInAdjacentVertices(string nameOfVertex);

		IEnumerable<IOrientedVertex> GetOutAdjacentVertices(IOrientedVertex vertex);
		IEnumerable<IOrientedVertex> GetOutAdjacentVertices(string nameOfVertex);

		IEnumerable<IOrientedEdge> GetInEdges(IOrientedVertex vertex);
		IEnumerable<IOrientedEdge> GetInEdges(string nameOfVertex);

		IEnumerable<IOrientedEdge> GetOutEdges(IOrientedVertex vertex);
		IEnumerable<IOrientedEdge> GetOutEdges(string nameOfVertex);

		IOrientedVertex ReturnVertexWithName(string nameOfVertex);

		int GetInDegree(IOrientedVertex vertex);
		int GetInDegree(string nameOfVertex);

		int GetOutDegree(IOrientedVertex vertex);
		int GetOutDegree(string nameOfVertex);

		int GetVertexCount();
        int GetEdgeCount();

        bool IsVertex(IOrientedVertex vertex);
		bool IsVertex(string nameOfVertex);

		bool IsEdge(IOrientedVertex vertexIn, IOrientedVertex vertexOut);  //edge: vertexIn -> vertexOut
		bool IsEdge(string NameOfVertexIn, string NameOfVertexOut);  //edge: vertexIn -> vertexOut
		bool IsEdge(IOrientedEdge edge);  //edge: vertexIn -> vertexOut


		IOrientedGraph ClearGraph();
		IOrientedGraph AddVertex(IOrientedVertex vertex);
		IOrientedGraph AddVertex(string nameOfVertex);

		IOrientedGraph AddEdge(IOrientedEdge edge);
		IOrientedGraph AddEdge(IOrientedVertex vertexOut, IOrientedVertex vertexIn); //adds edge: vertexOut -> vertexIn
		IOrientedGraph AddEdge(string nameOfVertexIn, string nameOfVertexOut); //adds edge: vertexOut -> vertexIn


		IOrientedGraph RemoveVertex(IOrientedVertex vertex);
		IOrientedGraph RemoveVertex(string nameOfVertex);

		IOrientedGraph RemoveEdge(IOrientedEdge edge);
        IOrientedGraph RemoveEdge(IOrientedVertex vertexOut, IOrientedVertex vertexIn); //removes edge: vertexOut -> vertexIn
		IOrientedGraph RemoveEdge(string nameOfVertexOut, string nameOfVertexIn); //removes edge: vertexOut -> vertexIn


		static IOrientedGraph operator +(IOrientedGraph graph, IOrientedVertex vertex) => graph.AddVertex(vertex);
        static IOrientedGraph operator +(IOrientedGraph graph, IOrientedEdge edge) => graph.AddEdge(edge);
        static IOrientedGraph operator +(IOrientedGraph graph, (IOrientedVertex, IOrientedVertex) edge) => graph.AddEdge(edge.Item1, edge.Item2);

        static IOrientedGraph operator -(IOrientedGraph graph, IOrientedVertex vertex) => graph.RemoveVertex(vertex);
        static IOrientedGraph operator -(IOrientedGraph graph, IOrientedEdge edge) => graph.RemoveEdge(edge);
        static IOrientedGraph operator -(IOrientedGraph graph, (IOrientedVertex, IOrientedVertex) edge) => graph.RemoveEdge(edge.Item1, edge.Item2);

    }
}
