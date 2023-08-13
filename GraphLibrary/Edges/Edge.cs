using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Vertices;

namespace GraphLibrary.Edges
{
	/// <summary>
	/// Abstract class for edge
	/// </summary>
	public abstract class Edge
	{
		/// <summary>
		/// bool value indicating whether edge is in graph or not, if it is in graph, edge cannot be added to a different graph
		/// </summary>
		internal bool IsInGraph { get; set; } = false;

	}
}
