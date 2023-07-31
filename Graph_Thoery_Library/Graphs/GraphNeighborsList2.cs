using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphThoeryLibrary.Edges;
using GraphThoeryLibrary.Graphs.Exceptions;
using GraphThoeryLibrary.Vertices;

namespace GraphThoeryLibrary.Graphs
{
	internal class GraphNeighborsList2 : IOrientedGraph2
	{
		private Dictionary<string, OrientedVertex> Vertices { get; set; }
		internal Dictionary<string, HashSet<string>> NeighboursList { get; private set; }
		private int EdgeCount { get; set; }

		private void CheckVertexInGraph(OrientedVertex vertex)
		{
			if (!IsVertex(vertex))
				throw new VertexException("Vertex is not in graph");
		}
		private void CheckVertexNotInGraph(OrientedVertex vertex)
		{
			if (IsVertex(vertex))
				throw new VertexException("Vertex is already in graph");
		}
		private void CheckEdgeInGraph(OrientedVertex VertexIn, OrientedVertex VertexOut)
		{
			if (!IsEdge(VertexIn, VertexOut))
				throw new EdgeException("Edge is not in graph");
		}
		private void CheckEdgeNotInGraph(OrientedVertex VertexIn, OrientedVertex VertexOut)
		{
			if (IsEdge(VertexIn, VertexOut))
				throw new EdgeException("Edge is already in graph");
		}

		public bool IsVertex(OrientedVertex vertex) => Vertices.ContainsKey(vertex.Name);
		public bool IsVertex(string vertexName) => Vertices.ContainsKey(vertexName);

		public bool IsEdge(OrientedVertex VertexIn, OrientedVertex VertexOut) => NeighboursList[VertexIn.Name].Contains(VertexOut.Name);



	}
}
