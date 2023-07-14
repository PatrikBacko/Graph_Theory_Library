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
        IEnumerable<IVertex> GetVertices();
		IEnumerable<IOrientedEdge> GetEdges();
		IEnumerable<IVertex> GetInAdjacentVertices(IVertex vertex);
        IEnumerable<IVertex> GetOutAdjacentVertices(IVertex vertex);
		IEnumerable<IOrientedEdge> GetInEdges(IVertex vertex);
		IEnumerable<IOrientedEdge> GetOutEdges(IVertex vertex);
        int GetInDegree(IVertex vertex);
        int GetOutDegree(IVertex vertex);
        int GetVertexCount();
        int GetEdgeCount();
        bool IsEdge(IVertex vertex1, IVertex vertex2);  //edge: vertex1 -> vertex2 

        IOrientedGraph AddVertex(IVertex vertex);
        IOrientedGraph AddEdge(IOrientedEdge edge);
		IOrientedGraph AddEdge(IVertex vertex1, IVertex vertex2); //adds edge: vertex1 -> vertex2

		IOrientedGraph RemoveVertex(IVertex vertex);
        IOrientedGraph RemoveEdge(IOrientedEdge edge);
        IOrientedGraph RemoveEdge(IVertex vertex1, IVertex vertex2); //removes edge: vertex1 -> vertex2

        static IOrientedGraph operator +(IOrientedGraph graph, IVertex vertex) => graph.AddVertex(vertex);
        static IOrientedGraph operator +(IOrientedGraph graph, IOrientedEdge edge) => graph.AddEdge(edge);
        static IOrientedGraph operator +(IOrientedGraph graph, (IVertex, IVertex) edge) => graph.AddEdge(edge.Item1, edge.Item2);

        static IOrientedGraph operator -(IOrientedGraph graph, IVertex vertex) => graph.RemoveVertex(vertex);
        static IOrientedGraph operator -(IOrientedGraph graph, IOrientedEdge edge) => graph.RemoveEdge(edge);
        static IOrientedGraph operator -(IOrientedGraph graph, (IVertex, IVertex) edge) => graph.RemoveEdge(edge.Item1, edge.Item2);

    }
}
