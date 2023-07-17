using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Vertices;

namespace GraphLibrary.Edges
{
	public class OrientedEdge : Edge
	{
		public OrientedVertex VertexIn { get; init; }
		public OrientedVertex VertexOut { get; init; }

		public OrientedEdge(OrientedVertex vertexIn, OrientedVertex vertexOut)
		{
			VertexIn = vertexIn;
			VertexOut = vertexOut;
		}	
	}
}
