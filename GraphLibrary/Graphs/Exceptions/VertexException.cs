using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Graphs.Exceptions
{
	/// <summary>
	/// Exception for problems with vertices in a graph. (e.g. vertex not found ...)
	/// </summary>
	public class VertexException : Exception
	{
		public VertexException() : base() { }
		public VertexException(string message) : base(message) { }
		public VertexException(string message, Exception innerException) : base(message, innerException) { }
	}
}
