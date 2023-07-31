using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphThoeryLibrary.Graphs.Exceptions
{
	internal class EdgeException : Exception
	{
		public EdgeException()
		{
		}

		public EdgeException(string message)
			: base(message)
		{
		}

		public EdgeException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
