using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphThoeryLibrary.Graphs.Exceptions
{
    internal class VertexException : Exception
    {
		public VertexException()
		{
		}

		public VertexException(string message)
			: base(message)
		{
		}

		public VertexException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
