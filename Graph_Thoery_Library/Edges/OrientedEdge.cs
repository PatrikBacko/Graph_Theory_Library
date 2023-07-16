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
        public IOrientedVertex VertexIn { get; init; }
        public IOrientedVertex VertexOut { get; init; }

        public OrientedEdge(IOrientedVertex VertexIn, IOrientedVertex VertexOut)
		{
			this.VertexIn = VertexIn;
			this.VertexOut = VertexOut;
		}
    }
}
