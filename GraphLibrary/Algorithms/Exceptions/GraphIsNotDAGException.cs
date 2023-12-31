﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Algorithms.Exceptions
{
	public class GraphIsNotDAGException : AlgorithmException
	{
		public GraphIsNotDAGException() : base() { }
		public GraphIsNotDAGException(string message) : base(message) { }
		public GraphIsNotDAGException(string message, Exception innerException) : base(message, innerException) { }
	}
}
