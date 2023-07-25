using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Vertices;

namespace GraphLibrary.Graphs
{
	public delegate void OrientedVertexAction<TVertex>(TVertex vertex) where TVertex : OrientedVertex;
}
