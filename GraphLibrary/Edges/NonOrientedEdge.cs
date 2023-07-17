using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Vertices;

namespace GraphLibrary.Edges
{
	public class NonOrientedEdge : Edge
	{
		NonOrientedVertex Vertex1 { get; init; }
		NonOrientedVertex Vertex2 { get; init; }

		public NonOrientedEdge(NonOrientedVertex vertex1, NonOrientedVertex vertex2)
		{
			Vertex1 = vertex1;
			Vertex2 = vertex2;
		}
	}
}
