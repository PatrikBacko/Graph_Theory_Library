using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Extensions.StringExtensions;

namespace GraphLibrary.Vertices
{
	//TODO: is in graph property needed? (also for edges)
	public class OrientedVertex : Vertex
	{
		public int DegreeOut { get; internal set; }
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
