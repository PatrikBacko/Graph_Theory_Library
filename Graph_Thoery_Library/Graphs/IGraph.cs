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
        List<Vertex> GetVertices();
        List<IEdge> GetEdges();
        List<Vertex> GetAdjacentVertices(Vertex vertex);
        List<IEdge> GetIncidentEdges(Vertex vertex);
        int GetDegree(Vertex vertex);
        int GetVertexCount();
        int GetEdgeCount();
        bool IsAdjacent(Vertex vertex1, Vertex vertex2);
        bool IsIncident(Vertex vertex, IEdge edge);
        bool IsIncident(Vertex vertex1, Vertex vertex2);
        bool IsIncident(IEdge edge1, IEdge edge2);

        IGraph AddVertex(Vertex vertex);
        IGraph AddEdge(IEdge edge);
        IGraph RemoveVertex(Vertex vertex);
        IGraph RemoveEdge(IEdge edge);

    }
}
