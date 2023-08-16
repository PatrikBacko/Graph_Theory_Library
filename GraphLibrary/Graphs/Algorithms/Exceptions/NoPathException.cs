using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Algorithms.Exceptions
{
	public class NoPathException : AlgorithmException
	{
		public NoPathException() : base() { }
		public NoPathException(string message) : base(message) { }
		public NoPathException(string message, Exception innerException) : base(message, innerException) { }
	}
}
