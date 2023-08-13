using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Vertices
{
	/// <summary>
	/// Class for weighted oriented vertex inheriting from <see cref="OrientedVertex"/>. <br />
	/// Weight type can be any type that implements <see cref="INumber{TSelf}"/> interface.
	/// </summary>
	/// <typeparam name="TWeight">weight type</typeparam>
	public class WeightedOrientedVertex<TWeight> : OrientedVertex where TWeight : INumber<TWeight>
	{
		/// <summary>
		/// Weight value of the vertex
		/// </summary>
		public TWeight Weight { get; protected set; }

		public WeightedOrientedVertex() : base()
		{
			Weight = TWeight.Zero;
		}
		public WeightedOrientedVertex(VertexName name, TWeight Weight) : base(name)
		{
			this.Weight = Weight;
		}

		/// <summary>
		/// Changes weight of the vertex, but weight type must be the same as the previous one.
		/// </summary>
		/// <param name="weight"> value to which weight will be changed </param>
		/// <returns> this </returns>
		virtual public WeightedOrientedVertex<TWeight> ChangeWeight(TWeight weight)
		{
			Weight = weight;
			return this;
		}
	}
}
