using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Vertices
{
	public class WeightedOrientedVertex<TWeight> : OrientedVertex where TWeight : INumber<TWeight>
	{
		public TWeight Weight { get; protected set; }

		public WeightedOrientedVertex() : base()
		{
			Weight = TWeight.Zero;
		}
		public WeightedOrientedVertex(VertexName name, TWeight weight) : base(name)
		{
			Weight = weight;
		}

		virtual public WeightedOrientedVertex<TWeight> ChangeWeight(TWeight weight)
		{
			Weight = weight;
			return this;
		}
	}
}
