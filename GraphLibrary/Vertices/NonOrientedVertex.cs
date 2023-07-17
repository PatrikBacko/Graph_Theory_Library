using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Vertices
{
	public class NonOrientedVertex : Vertex
	{
		public int Degree { get; internal set; }
		public NonOrientedVertex(string name) : base(name)
		{
			Degree = 0;
		}
	}
}
