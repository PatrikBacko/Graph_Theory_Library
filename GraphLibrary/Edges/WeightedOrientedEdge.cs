using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Vertices;

namespace GraphLibrary.Edges
{
	public class WeightedOrientedEdge<TWeight> : OrientedEdge where TWeight : INumber<TWeight>
	{
		public TWeight Weight { get; set; }

		public WeightedOrientedEdge(OrientedVertex source, OrientedVertex destination, TWeight weight) : base(source, destination)
		{
			Weight = weight;
		}
	}
}
