using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphThoeryLibrary.Vertices;

namespace GraphThoeryLibrary.Edges
{
    public class OrientedEdge : IOrientedEdge
    {
        public Vertex VertexIn { get; init; }
        public Vertex VertexOut { get; init; }

        public OrientedEdge(Vertex VertexIn, Vertex VertexOut)
		{
			this.VertexIn = VertexIn;
			this.VertexOut = VertexOut;
		}
    }
}
