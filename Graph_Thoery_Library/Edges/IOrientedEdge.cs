using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphThoeryLibrary.Vertices;

namespace GraphThoeryLibrary.Edges
{
    public interface IOrientedEdge
    {
		IOrientedVertex VertexIn { get; }
		IOrientedVertex VertexOut { get; }
    }
}
