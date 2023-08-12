using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Algorithms.Exceptions
{
	public class AlgorithmException : Exception
	{
		public AlgorithmException() : base() { }
		public AlgorithmException(string message) : base(message) { }
		public AlgorithmException(string message, Exception innerException) : base(message, innerException) { }

	}
}
