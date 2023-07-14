using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphThoeryLibrary.Vertices;

namespace GraphThoeryLibrary.Edges
{
    public struct OrientedEdge : IOrientedEdge
    {
        public IVertex InVertex { get; init; }
        public IVertex OutVertex { get; init; }

        public OrientedEdge(IVertex InVertex, IVertex OutVertex)
		{
			this.InVertex = InVertex;
			this.OutVertex = OutVertex;
		}
    }
}
