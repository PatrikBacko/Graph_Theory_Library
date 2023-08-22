using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Vertices
{
	/// <summary>
	/// Abstract class representing vertices.
	/// </summary>
	public abstract class Vertex
	{
		/// <summary>
		/// bool value indicating whether the vertex is in the graph. 
		/// if it is in some graph, then it cannot be added to different graph.
		/// </summary>
		internal bool IsInGraph { get; set; } = false;
		/// <summary>
		/// Name and identification of the vertex.
		/// </summary>
		public VertexName Name { get; init; }


		public Vertex() { }

		public Vertex(VertexName name)
		{
			Name = name;
		}
	}
}
