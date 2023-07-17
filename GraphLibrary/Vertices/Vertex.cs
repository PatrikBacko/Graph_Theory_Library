using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Vertices
{
	public abstract class Vertex
	{
		string Name { get; init; }

		public Vertex(string name)
		{
			Name = name;
		}
	}
}
