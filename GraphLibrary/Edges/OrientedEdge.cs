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
		public VertexName VertexIn { get; init; }
		public VertexName VertexOut { get; init; }

		public OrientedEdge(VertexName vertexIn, VertexName vertexOut)
		{
			VertexIn = vertexIn;
			VertexOut = vertexOut;
		}	
	}
}
