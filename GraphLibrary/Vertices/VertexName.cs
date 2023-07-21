using GraphLibrary.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GraphLibrary.Vertices
{
	public struct VertexName
	{
		public string Value;

		public VertexName(string value) {  Value = value; }

		public OrientedEdge ToOrientedEdge(VertexName vertexIn){
			return new OrientedEdge(this, vertexIn);
		}

		public OrientedVertex ToorientedVertex(){
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
	}
}
