using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphThoeryLibrary.Edges;
using GraphThoeryLibrary.Edges.Edge;
using GraphThoeryLibrary.Vertices;

namespace GraphThoeryLibrary.Graphs
{
    public interface IGraph
    {
        List<IVertex> GetVertices();
        List<IEdge> GetEdges();
        List<IVertex> GetAdjacentVertices(IVertex vertex);
        List<IEdge> GetIncidentEdges(IVertex vertex);
        int GetDegree(IVertex vertex);
        int GetVertexCount();
        int GetEdgeCount();
        bool IsAdjacent(IVertex vertex1, IVertex vertex2);
        bool IsIncident(IVertex vertex, IEdge edge);
        bool IsIncident(IVertex vertex1, IVertex vertex2);
        bool IsIncident(IEdge edge1, IEdge edge2);

        IGraph AddVertex(IVertex vertex);
        IGraph AddEdge(IEdge edge);
        IGraph RemoveVertex(IVertex vertex);
        IGraph RemoveEdge(IEdge edge);

    }
}
