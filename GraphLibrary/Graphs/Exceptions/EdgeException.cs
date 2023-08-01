using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Graphs.Exceptions
{
	public class EdgeException : Exception
	{
		public EdgeException() : base() { }
		public EdgeException(string message) : base(message) { }
		public EdgeException(string message, Exception innerException) : base(message, innerException) { }
	}
}
