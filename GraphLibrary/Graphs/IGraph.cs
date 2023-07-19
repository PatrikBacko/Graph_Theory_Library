﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Graphs
{
	public interface IGraph
	{
		int GetVertexCount();
		int GetEdgeCount();
		IGraph ClearGraph();
	}
}