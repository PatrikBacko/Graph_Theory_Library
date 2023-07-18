using GraphLibrary.Edges;
using GraphLibrary.Vertices;
using GraphLibrary.Graphs.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace GraphLibrary.Graphs
{
	public class OrientedGraph<TVertex, TEdge> : IOrientedGraph<TVertex, TEdge> where TVertex : OrientedVertex, new() where TEdge : OrientedEdge, new()
	{
		private Dictionary<VertexName, TVertex> vertices;
		private Dictionary<VertexName, Dictionary<VertexName, TEdge>> neighbors;
		private int edgeCount;

		public OrientedGraph() {
			vertices = new Dictionary<VertexName, TVertex>();
			neighbors = new Dictionary<VertexName, Dictionary<VertexName, TEdge>>();
			edgeCount = 0;
		}

		public bool IsVertex(VertexName vertex) {
			if (vertices.ContainsKey(vertex))
				return true;
			return false;
		}
		public bool IsVertex(TVertex vertex) {
			if (IsVertex(vertex.Name))
				return true;

			var v = vertices[vertex.Name];
			if (v == vertex)
				return true;
			return false;
		}
		private void CheckVertex(VertexName vertex) {
			if (!IsVertex(vertex))
				throw new VertexException("Vertex doesn't exist in Graph");
		}

		public bool IsEdge(VertexName vertexOut, VertexName vertexIn) {
			CheckVertex(vertexOut);
			CheckVertex(vertexIn);

			if (neighbors[vertexOut].ContainsKey(vertexIn))
				return true;
			return false;
		}
		public bool IsEdge(TEdge edge) {
			CheckVertex(edge.VertexOut);
			CheckVertex(edge.VertexIn);

			var e = neighbors[edge.VertexOut][edge.VertexIn];
			if (e == edge)
				return true;
			return false;
		}
		private void CheckEdge(VertexName vertexOut, VertexName vertexIn) {
			if (!IsEdge(vertexOut, vertexIn))
				throw new EdgeException("Edge doesn't exist in Graph");
		}

		public int GetVertexCount() => vertices.Count;
		public int GetEdgeCount() => edgeCount;

		public IEnumerable<TVertex> GetVerticies() => vertices.Values;
		public IEnumerable<TEdge> GetEdges() {
			List<TEdge> edges = new List<TEdge>();
			foreach (var vertex in vertices.Values)
				foreach (var edge in neighbors[vertex.Name].Values)
					edges.Add(edge);
			return edges;
		}

		public TVertex GetVertex(VertexName vertex) {
			CheckVertex(vertex);
			return vertices[vertex];
		}
		public TEdge GetEdge(VertexName vertexOut, VertexName vertexIn) {
			CheckEdge(vertexOut, vertexIn);
			return neighbors[vertexOut][vertexIn];
		}

		public IEnumerable<TVertex> GetInAdjacentVertices(VertexName vertex) {
			CheckVertex(vertex);
			List<TVertex> adjVerticesIn = new List<TVertex>();
			foreach (var v in GetVerticies())
				if (IsEdge(v.Name, vertex))
					adjVerticesIn.Add(v);
			return adjVerticesIn;
		}
		public IEnumerable<TVertex> GetOutAdjacentVertices(VertexName vertex) {
			CheckVertex(vertex);
			List<TVertex> adjVerticesOut = new List<TVertex>();
			foreach (var v in GetVerticies())
				if (IsEdge(vertex, v.Name))
					adjVerticesOut.Add(v);
			return adjVerticesOut;
		}

		public IEnumerable<TEdge> GetInEdges(VertexName vertex) {
			CheckVertex(vertex);
			List<TEdge> edgesIn = new List<TEdge>();
			foreach (var v in GetVerticies())
				if (IsEdge(v.Name, vertex))
					edgesIn.Add(GetEdge(v.Name, vertex));
			return edgesIn;
		}

		public IEnumerable<TEdge> GetOutEdges(VertexName vertex) {
			CheckVertex(vertex);
			List<TEdge> edgesOut = new List<TEdge>();
			foreach (var v in GetVerticies())
				if (IsEdge(vertex, v.Name))
					edgesOut.Add(GetEdge(vertex, v.Name));
			return edgesOut;
		}

		public int GetInDegree(VertexName vertexName) {
			CheckVertex(vertexName);
			var vertex = GetVertex(vertexName);
			return vertex.DegreeIn;
		}
		public int GetOutDegree(VertexName vertexName) {
			CheckVertex(vertexName);
			var vertex = GetVertex(vertexName);
			return vertex.DegreeOut;
		}

		public OrientedGraph<TVertex, TEdge> AddVertex(TVertex vertex) {
			if (IsVertex(vertex))
				throw new VertexException("Vertex already exists in Graph");
			vertices.Add(vertex.Name, vertex);
			neighbors.Add(vertex.Name, new Dictionary<VertexName, TEdge>());
			return this;
		}
		public OrientedGraph<TVertex, TEdge> AddVertex(VertexName vertexName) {
			if (IsVertex(vertexName))
				throw new VertexException("Vertex already exists in Graph");

			var vertex = new TVertex() { Name = vertexName };
			vertices.Add(vertexName, vertex);
			neighbors.Add(vertexName, new Dictionary<VertexName, TEdge>());
			return this;
		}

		public OrientedGraph<TVertex, TEdge> AddEdge(TEdge edge) {
			CheckVertex(edge.VertexOut);
			CheckVertex(edge.VertexIn);

			if (IsEdge(edge))
				throw new EdgeException("Edge already exists in Graph");

			neighbors[edge.VertexOut].Add(edge.VertexIn, edge);
			edgeCount++;

			vertices[edge.VertexOut].DegreeOut++;
			vertices[edge.VertexIn].DegreeIn++;
			return this;
		}
		public OrientedGraph<TVertex, TEdge> AddEdge(VertexName vertexOut, VertexName vertexIn) {
			CheckVertex(vertexOut);
			CheckVertex(vertexIn);

			if (IsEdge(vertexOut, vertexIn))
				throw new EdgeException("Edge already exists in Graph");

			var edge = new TEdge() { VertexOut = vertexOut, VertexIn = vertexIn };
			neighbors[vertexOut].Add(vertexIn, edge);
			edgeCount++;

			vertices[edge.VertexOut].DegreeOut++;
			vertices[edge.VertexIn].DegreeIn++;

			return this;
		}

		public OrientedGraph<TVertex, TEdge> RemoveVertex(VertexName vertexName) {
			CheckVertex(vertexName);
			edgeCount -= neighbors[vertexName].Count;
			vertices.Remove(vertexName);
			neighbors.Remove(vertexName);
			foreach (var vertex in vertices.Values) {
				if (IsEdge(vertex.Name, vertexName)) {
					neighbors[vertex.Name].Remove(vertexName);
					edgeCount--;
					vertex.DegreeOut--;
				}
			}
			return this;
		}
		public OrientedGraph<TVertex, TEdge> RemoveVertex(TVertex vertex) {
			if (!IsVertex(vertex))
				throw new VertexException("Vertex doesn't exist in Graph");
			return RemoveVertex(vertex.Name);
		}

		public OrientedGraph<TVertex, TEdge> RemoveEdge(VertexName vertexOut, VertexName vertexIn){
			CheckEdge(vertexOut, vertexIn);
			neighbors[vertexOut].Remove(vertexIn);
			edgeCount--;
			vertices[vertexOut].DegreeOut--;
			vertices[vertexIn].DegreeIn--;
			return this;
		}
		public OrientedGraph<TVertex, TEdge> RemoveEdge(TEdge edge){
			if (!IsEdge(edge))
				throw new EdgeException("Edge doesn't exist in Graph");
			return RemoveEdge(edge.VertexOut, edge.VertexIn);
		}

		public OrientedGraph<TVertex, TEdge> ClearGraph()
		{
			vertices.Clear();
			neighbors.Clear();
			edgeCount = 0;
			return this;
		}


		public static OrientedGraph<TVertex, TEdge> operator +(OrientedGraph<TVertex, TEdge> graph, TVertex vertex) => graph.AddVertex(vertex);
		public static OrientedGraph<TVertex, TEdge> operator +(OrientedGraph<TVertex, TEdge> graph, TEdge edge) => graph.AddEdge(edge);

		public static OrientedGraph<TVertex, TEdge> operator -(OrientedGraph<TVertex, TEdge> graph, TVertex vertex) => graph.RemoveVertex(vertex);
		public static OrientedGraph<TVertex, TEdge> operator -(OrientedGraph<TVertex, TEdge> graph, TEdge edge) => graph.RemoveEdge(edge);


		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.AddVertex(TVertex vertex) => AddVertex(vertex);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.AddVertex(VertexName vertex) => AddVertex(vertex);

		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.AddEdge(TEdge edge) => AddEdge(edge);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.AddEdge(VertexName vertexOut, VertexName vertexIn) => AddEdge(vertexOut, vertexIn);

		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveVertex(VertexName vertex) => RemoveVertex(vertex);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveVertex(TVertex vertex) => RemoveVertex(vertex);

		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveEdge(VertexName vertexOut, VertexName vertexIn) => RemoveEdge(vertexOut, vertexIn);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveEdge(TEdge edge) => RemoveEdge(edge);

		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.ClearGraph() => ClearGraph();
	}
}
