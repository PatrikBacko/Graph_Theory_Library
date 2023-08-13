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
		public VertexName VertexOut { get; init; }
		public VertexName VertexIn { get; init; }

		public OrientedEdge() : base()
		{
			VertexOut = new VertexName();
			VertexIn = new VertexName();
		}
		public OrientedEdge(VertexName vertexOut, VertexName vertexIn)
		{
			VertexOut = vertexOut;
			VertexIn = vertexIn;
		}
	}
}
