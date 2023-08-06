using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Algorithms.Exceptions
{
	public class GraphHasNegativeCycleException : GraphException
	{
		public GraphHasNegativeCycleException() : base() { }
		public GraphHasNegativeCycleException(string message) : base(message) { }
		public GraphHasNegativeCycleException(string message, Exception innerException) : base(message, innerException) { }
	}
}
