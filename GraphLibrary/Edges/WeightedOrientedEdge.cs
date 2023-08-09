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
		public TWeight Weight { get; protected set; }

		public WeightedOrientedEdge(VertexName vertexOut, VertexName vertexIn, TWeight weight) : base(vertexOut, vertexIn)
		{
			Weight = weight;
		}

		virtual public WeightedOrientedEdge<TWeight> ChangeWeight(TWeight weight)
		{
			Weight = weight;
			return this;
		}
	}
}
