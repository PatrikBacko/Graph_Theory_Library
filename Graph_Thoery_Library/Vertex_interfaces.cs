using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Thoery_Library
{
	public interface IVertex{

	}

	public interface IWeightedVertex<TWeightType> where TWeightType : INumber<TWeightType>
	{

	}
}