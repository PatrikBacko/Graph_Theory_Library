using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphThoeryLibrary.Vertices;

namespace GraphThoeryLibrary.Edges
{
    public interface IEdge
    {
        IVertex Vertex1 { get; }
        IVertex Vertex2 { get; }
    }
}
