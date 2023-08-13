using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Vertices;

namespace GraphLibrary.Edges
{
	/// <summary>
	/// Class for oriented edge inheriting from <see cref="Edge"/> <br />
	/// </summary>
	public class OrientedEdge : Edge
	{
		/// <summary>
		/// Vertex from which edge goes out (from which edge starts)
		/// </summary>
		public VertexName VertexOut { get; init; }

		/// <summary>
		/// Vertex to which edge goes in (to which edge ends)
		/// </summary>
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
