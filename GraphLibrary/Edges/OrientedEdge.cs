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
		public VertexName VertexIn { get; init; }	//internal set for reverse maybe ?
		public VertexName VertexOut { get; init; }

		public OrientedEdge(VertexName vertexOut, VertexName vertexIn)
		{
			VertexIn = vertexIn;
			VertexOut = vertexOut;
		}
	}
}
