using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Vertices;

namespace GraphLibrary.Edges
{
	/// <summary>
	/// Class representing weighted oriented edge inheriting from <see cref="OrientedEdge"/> <br />
	/// Weight type can be any type that implements <see cref="INumber{TWeight}"/> interface.
	/// </summary>
	/// <typeparam name="TWeight"> 
	/// type of weight of the edge 
	/// </typeparam>
	public class WeightedOrientedEdge<TWeight> : OrientedEdge 
		where TWeight : INumber<TWeight>
	{
		/// <summary>
		/// weight value of the edge
		/// </summary>
		public TWeight Weight { get; protected set; }
		public WeightedOrientedEdge() : base()
		{
			Weight = TWeight.Zero;
		}

		public WeightedOrientedEdge(VertexName vertexOut, VertexName vertexIn, TWeight weight) : base(vertexOut, vertexIn)
		{
			Weight = weight;
		}

		/// <summary>
		/// Changes weight of the edge, but weight type must be the same as the previous one.
		/// </summary>
		/// <param name="weight"> 
		/// value to which weight will be changes 
		/// </param>
		/// <returns> 
		/// itself with changed weight
		/// </returns>
		virtual public WeightedOrientedEdge<TWeight> ChangeWeight(TWeight weight)
		{
			Weight = weight;
			return this;
		}
	}
}
