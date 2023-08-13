using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Vertices
{
	/// <summary>
	/// Class for oriented vertex inheriting from <see cref="Vertex"/><br />
	/// </summary>
	public class OrientedVertex : Vertex
	{
		/// <summary>
		/// property for out degree of vertex in oriented graph
		/// </summary>
		public int DegreeOut { get; internal set; }

		/// <summary>
		/// property for in degree of vertex in oriented graph
		/// </summary>
		public int DegreeIn { get; internal set; }
		public OrientedVertex() : base(){
			DegreeIn = 0;
			DegreeOut = 0;
		}
		public OrientedVertex(VertexName name) : base(name)
		{
			DegreeIn = 0;
			DegreeOut = 0;
		}

	}
}
