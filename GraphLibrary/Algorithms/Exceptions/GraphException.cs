using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Algorithms.Exceptions
{
	public class GraphException : Exception
	{
		public GraphException() : base() { }
		public GraphException(string message) : base(message) { }
		public GraphException(string message, Exception innerException) : base(message, innerException) { }

	}
}
