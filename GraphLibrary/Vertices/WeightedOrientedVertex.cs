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
		public WeightedOrientedVertex(VertexName name, TWeight Weight) : base(name)
		{
			this.Weight = Weight;
		}

		virtual public WeightedOrientedVertex<TWeight> ChangeWeight(TWeight weight)
		{
			Weight = weight;
			return this;
		}

	}
}
