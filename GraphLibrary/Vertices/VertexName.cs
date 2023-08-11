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
	public struct VertexName
	{
		public string Value;

		public VertexName(string value) {  Value = value; }

		public OrientedEdge ToOrientedEdge(VertexName vertexIn){
			return new OrientedEdge(this, vertexIn);
		}

		public OrientedVertex ToOrientedVertex(){
			return new OrientedVertex(this);
		}
		
		public WeightedOrientedEdge<TWeight> ToWeightedOrientedEdge<TWeight>(VertexName vertexIn, TWeight weight) 
			where TWeight : INumber<TWeight>{
			return new WeightedOrientedEdge<TWeight>(this, vertexIn, weight);
		}

		public WeightedOrientedVertex<TWeight> ToWeightedOrientedVertex<TWeight>(TWeight weight) 
			where TWeight : INumber<TWeight>{
			return new WeightedOrientedVertex<TWeight>(this, weight);
		}

		public override string ToString(){
			return Value;
		}

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

		static public bool operator ==(VertexName vertexName1, VertexName vertexName2){
			return vertexName1.Equals(vertexName2);
		}
		static public bool operator !=(VertexName vertexName1, VertexName vertexName2)
		{
			return !vertexName1.Equals(vertexName2);
		}
	}
}
