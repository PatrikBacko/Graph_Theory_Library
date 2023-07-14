using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Thoery_Library
{

	public interface IVertex
	{
		string Name { get; }
	}

	public class Vertex : IVertex{
		public string Name { get; init; }

		protected int Index { get; set; }

		public Vertex(string Name) {
		this.Name = Name;
		Index = -1;
		}
	}


	//public interface IVertex<TName>
	//{
	//	TName Name { get; set; }
	//}

	//public interface IWeightedVertex<TWeightType> where TWeightType : INumber<TWeightType>
	//{

	//}

	//public class Vertex<TName> : IVertex<TName>
	//{
	//	public TName Name { get; set; }
	//}

	//public class WeightedVertex<TWeightType> : IWeightedVertex<TWeightType> where TWeightType : INumber<TWeightType> 
	//{
		
	//}
}