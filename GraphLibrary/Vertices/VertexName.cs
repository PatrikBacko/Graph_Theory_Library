using GraphLibrary.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics.CodeAnalysis;

namespace GraphLibrary.Vertices
{
	/// <summary>
	/// Struct for vertex name, contain only one field - "string Value". Used in Library as ID for vertices.
	/// </summary>
	public struct VertexName
	{
		/// <summary>
		/// string value of vertex name
		/// </summary>
		public string Value;

		public VertexName(string value) {  Value = value; }

		/// <summary>
		/// Creates new instance of <see cref="OrientedVertex"/> with this as name. <br />
		/// </summary>
		/// <returns>a new instance of <see cref="OrientedVertex"/></returns>
		public OrientedVertex ToOrientedVertex()
		{
			return new OrientedVertex(this);
		}

		/// <summary>
		/// Creates new instance of <see cref="OrientedEdge"/> with this as vertexOut and vertexIn as vertexIn. <br />
		/// </summary>
		/// <param name="vertexIn"> Name of Vertex into which new edge ends. </param>
		/// <returns>a new instance of <see cref="OrientedEdge"/>. </returns>
		public OrientedEdge ToOrientedEdge(VertexName vertexIn){
			return new OrientedEdge(this, vertexIn);
		}

		/// <summary>
		/// Creates new instance of <see cref="WeightedOrientedVertex{TWeight}"/> with this as name. <br />
		/// </summary>
		/// <typeparam name="TWeight">Weight type</typeparam>
		/// <param name="weight">weight value of the new Vertex</param>
		/// <returns>a new instance of <see cref="WeightedOrientedVertex{TWeight}"/></returns>
		public WeightedOrientedVertex<TWeight> ToWeightedOrientedVertex<TWeight>(TWeight weight)
			where TWeight : INumber<TWeight>
		{
			return new WeightedOrientedVertex<TWeight>(this, weight);
		}

		/// <summary>
		/// Creates new instance of <see cref="WeightedOrientedEdge{TWeight}"/> with this as vertexOut and vertexIn as vertexIn. <br />
		/// </summary>
		/// <typeparam name="TWeight"> Weight type of a edge </typeparam>
		/// <param name="vertexIn"> Name of Vertex into which new edge ends. </param>
		/// <param name="weight">weight value of the new Vertex</param>
		/// <returns>a new instance of <see cref="WeightedOrientedEdge{TWeight}"/> </returns>
		public WeightedOrientedEdge<TWeight> ToWeightedOrientedEdge<TWeight>(VertexName vertexIn, TWeight weight) 
			where TWeight : INumber<TWeight>{
			return new WeightedOrientedEdge<TWeight>(this, vertexIn, weight);
		}

		/// <summary>
		/// Override of ToString method. <br />
		/// </summary>
		/// <returns>  field <see cref="VertexName"/>.Value that is string </returns>
		public override string ToString(){
			return Value;
		}

		/// <summary>
		/// Override of Equals method.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns> result of comparing <see cref="VertexName"/>.Value of this and obj.</returns>
		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return base.Equals(obj);
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		static public implicit operator VertexName(string value){
			return new VertexName(value);
		}

		/// <summary>
		/// Override of == operator.
		/// </summary>
		/// <param name="vertexName1"></param>
		/// <param name="vertexName2"></param>
		/// <returns> Result of Equals method </returns>
		static public bool operator ==(VertexName vertexName1, VertexName vertexName2){
			return vertexName1.Equals(vertexName2);
		}

		/// <summary>
		/// Override of != operator.
		/// </summary>
		/// <param name="vertexName1"></param>
		/// <param name="vertexName2"></param>
		/// <returns> negated result of Equals method </returns>
		static public bool operator !=(VertexName vertexName1, VertexName vertexName2)
		{
			return !vertexName1.Equals(vertexName2);
		}
	}
}
