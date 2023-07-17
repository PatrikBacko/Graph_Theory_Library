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
		public TWeight Weight { get; init; }

		public WeightedOrientedVertex(string name, TWeight weight) : base(name)
		{
			Weight = weight;
		}
	}
}
