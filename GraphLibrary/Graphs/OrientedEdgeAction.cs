using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Edges;

namespace GraphLibrary.Graphs
{
	public delegate void OrientedEdgeAction<TEdge>(TEdge edge)
		where TEdge : OrientedEdge;
}
