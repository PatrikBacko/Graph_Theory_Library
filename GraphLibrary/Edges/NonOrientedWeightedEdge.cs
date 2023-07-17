using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Vertices;

namespace GraphLibrary.Edges
{
	public class NonOrientedWeightedEdge<TWeight> : NonOrientedEdge where TWeight : INumber<TWeight>
	{
		public TWeight Weight { get; init; }

		public NonOrientedWeightedEdge(NonOrientedVertex vertex1, NonOrientedVertex vertex2, TWeight weight) : base(vertex1, vertex2)
		{
			Weight = weight;
		}
	}
}
