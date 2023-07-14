using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Thoery_Library
{
	public interface IEdge {
	IVertex Vertex1 { get; }
	IVertex Vertex2 { get; }
	}

	public interface IOrientedEdge{
	IVertex InVertex { get; }
	IVertex OutVertex { get; }
	}

	public class Edge : IEdge
	{
		public IVertex Vertex1 { get; set; }
		public IVertex Vertex2 { get; set; }
	}

	public class OrientedEdge : IOrientedEdge 
	{
		public IVertex InVertex { get; private set; }
		public IVertex OutVertex { get; private set; }
	}


	//public interface IWeightedEdge<TWeightType> where TWeightType : INumber<TWeightType> {

	//}
}
