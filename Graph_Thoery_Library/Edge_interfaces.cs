using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Thoery_Library
{
	public interface IEdge {

	}

	public interface IWeightedEdge<TWeightType> where TWeightType : INumber<TWeightType> {

	}
}
