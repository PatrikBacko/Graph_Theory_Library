using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Graphs.Exceptions
{
	public class VertexException : Exception
	{
		public VertexException() : base() { }
		public VertexException(string message) : base(message) { }
		public VertexException(string message, Exception innerException) : base(message, innerException) { }
	}
}
