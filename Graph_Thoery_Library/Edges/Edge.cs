using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphThoeryLibrary.Vertices;

namespace GraphThoeryLibrary.Edges
{
    public struct Edge : IEdge
    {
        public IVertex Vertex1 { get; init; }
        public IVertex Vertex2 { get; init; }

        public Edge(IVertex Vertex1, IVertex Vertex2)
        {
            this.Vertex1 = Vertex1;
            this.Vertex2 = Vertex2;
        }
    }
}
