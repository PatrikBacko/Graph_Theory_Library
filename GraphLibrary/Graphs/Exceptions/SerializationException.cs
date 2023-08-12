using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Graphs.Exceptions
{
	public class SerializationException : Exception
	{
		public SerializationException() : base() { }
		public SerializationException(string message) : base(message) { }
		public SerializationException(string message, Exception innerException) : base(message, innerException) { }
	}
}
