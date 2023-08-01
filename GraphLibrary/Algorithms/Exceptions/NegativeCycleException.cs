using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Algorithms.Exceptions
{
	public class NegativeCycleException : Exception
	{
		public NegativeCycleException() : base() { }
		public NegativeCycleException(string message) : base(message) { }
		public NegativeCycleException(string message, Exception innerException) : base(message, innerException) { }
	}
}
