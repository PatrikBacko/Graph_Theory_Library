using GraphLibrary.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
