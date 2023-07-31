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
	public class GraphNeighborsList : IOrientedGraph
	{
		private class Degree {
			public int In { get; set; }
			public int Out { get; set; }
			public Degree(int In, int Out)
			{
				this.In = In;
				this.Out = Out;
			}
		}

		private Dictionary<Vertex, Degree> Degrees { get; set; }
		internal Dictionary<Vertex, HashSet<Vertex>> NeighboursList { get; private set; }
		private int EdgeCount { get; set; }

		public GraphNeighborsList()
		{
			Degrees = new Dictionary<Vertex, Degree>();
			NeighboursList = new Dictionary<Vertex, HashSet<Vertex>>();
			EdgeCount = 0;
		}

		private void CheckVertexInGraph(Vertex vertex) {
			if (!IsVertex(vertex))
				throw new VertexException("Vertex is not in graph");
		}
		private void CheckVertexNotInGraph(Vertex vertex)
		{
			if (IsVertex(vertex))
				throw new VertexException("Vertex is already in graph");
		}
		private void CheckEdgeInGraph(Vertex VertexIn, Vertex VertexOut) {
			if (!IsEdge(VertexIn, VertexOut))
				throw new EdgeException("Edge is not in graph");
		}
		private void CheckEdgeNotInGraph(Vertex VertexIn, Vertex VertexOut)
		{
			if (IsEdge(VertexIn, VertexOut))
				throw new EdgeException("Edge is already in graph");
		}

		public Vertex ReturnVertexWithName(string name) {
			Vertex vertex = new OrientedVertex(name);
			CheckVertexInGraph(vertex);
			return vertex;
		}

		public IEnumerable<Vertex> GetVertices() => NeighboursList.Keys;
		public IEnumerable<IOrientedEdge> GetEdges() =>
			NeighboursList.SelectMany(v => v.Value.Select(v2 => (IOrientedEdge)new OrientedEdge(v.Key, v2)));

		public IEnumerable<Vertex> GetInAdjacentVertices(Vertex vertex) {
			CheckVertexInGraph(vertex);
			return NeighboursList.Where(v => v.Value.Contains(vertex)).Select(v => v.Key);
		}
		public IEnumerable<Vertex> GetInAdjacentVertices(string vertexName)
		{
			Vertex vertex = new OrientedVertex(vertexName);
			return GetInAdjacentVertices(vertex);
		}

		public IEnumerable<Vertex> GetOutAdjacentVertices(Vertex vertex)
		{
			CheckVertexInGraph(vertex);
			return NeighboursList[vertex];
		}
		public IEnumerable<Vertex> GetOutAdjacentVertices(string vertexName)
		{
			Vertex vertex = new OrientedVertex(vertexName);
			return GetOutAdjacentVertices(vertex);
		}

		public IEnumerable<IOrientedEdge> GetInEdges(Vertex vertex)
		{
			CheckVertexInGraph(vertex);
			return NeighboursList.Where(v => v.Value.Contains(vertex)).Select(v => (IOrientedEdge)new OrientedEdge(v.Key, vertex));
		}
		public IEnumerable<IOrientedEdge> GetInEdges(string vertexName)
		{
			Vertex vertex = new OrientedVertex(vertexName);
			return GetInEdges(vertex);
		}

		public IEnumerable<IOrientedEdge> GetOutEdges(Vertex vertex)
		{
			CheckVertexInGraph(vertex);
			return NeighboursList[vertex].Select(v => (IOrientedEdge)new OrientedEdge(vertex, v));
		}
		public IEnumerable<IOrientedEdge> GetOutEdges(string vertexName)
		{
			Vertex vertex = new OrientedVertex(vertexName);
			return GetOutEdges(vertex);
		}



		public int GetInDegree(Vertex vertex) {
			CheckVertexInGraph(vertex);
			return Degrees[vertex].In;
		}
		public int GetInDegree(string vertexName)
		{
			Vertex vertex = new OrientedVertex(vertexName);
			return GetInDegree(vertex);
		}

		public int GetOutDegree(Vertex vertex) {
			CheckVertexInGraph(vertex);
			return Degrees[vertex].Out;
		}
		public int GetOutDegree(string vertexName)
		{
			Vertex vertex = new OrientedVertex(vertexName);
			return GetOutDegree(vertex);
		}

		public int GetVertexCount() => NeighboursList.Count;
		public int GetEdgeCount() => EdgeCount;



		public bool IsVertex(Vertex vertex) => NeighboursList.ContainsKey(vertex);
		public bool IsVertex(string vertexName)
		{
			Vertex vertex = new OrientedVertex(vertexName);
			return NeighboursList.ContainsKey(vertex);
		}

		//porozmýšlať či nechať vyhadzovať vertex exception alebo pridať vyhadzovanie inej exception alebo returnovať false keď nie vertex v grafe
		public bool IsEdge(Vertex vertexIn, Vertex vertexOut) {
			CheckVertexInGraph(vertexIn);
			CheckVertexInGraph(vertexOut);

			return NeighboursList[vertexOut].Contains(vertexIn);
		}
		public bool IsEdge(string NameOfVertexIn, string NameOfVertexOut)
		{
			Vertex vertexIn = new OrientedVertex(NameOfVertexIn);
			Vertex vertexOut = new OrientedVertex(NameOfVertexOut);
			
			return IsEdge(vertexIn, vertexOut);
		}
		public bool IsEdge(IOrientedEdge edge)
		{
			return IsEdge(edge.VertexIn, edge.VertexOut);
		}

		public IOrientedGraph ClearGraph() {
			NeighboursList.Clear();
			Degrees.Clear();
			EdgeCount = 0;

			return this;
		}

		public IOrientedGraph AddVertex(Vertex vertex){
			CheckVertexInGraph(vertex);

			Degrees[vertex] = new Degree(0, 0);
			NeighboursList[vertex] = new HashSet<Vertex>();

			return this;
		}
		public IOrientedGraph AddVertex(string vertexName)
		{
			Vertex vertex = new OrientedVertex(vertexName);
			return AddVertex(vertex);
		}

		public IOrientedGraph AddEdge(IOrientedEdge edge){
			return AddEdge(edge.VertexIn, edge.VertexOut);
		}
		public IOrientedGraph AddEdge(Vertex vertexIn, Vertex vertexOut)
		{
			CheckEdgeInGraph(vertexIn, vertexOut);

			Degrees[vertexOut].Out += 1;
			Degrees[vertexIn].In += 1;
			NeighboursList[vertexOut].Add(vertexIn);
			EdgeCount++;

			return this;
		}
		public IOrientedGraph AddEdge(string vertexInName, string vertexOutName)
		{
			Vertex vertexIn = new OrientedVertex(vertexInName);
			Vertex vertexOut = new OrientedVertex(vertexOutName);

			return AddEdge(vertexIn, vertexOut);
		}

		public IOrientedGraph RemoveVertex(Vertex vertex){
			CheckVertexNotInGraph(vertex);

			EdgeCount -= NeighboursList[vertex].Count;
			NeighboursList.Remove(vertex);
			foreach (var v in NeighboursList.Keys)
			{
				NeighboursList[v].Remove(vertex);
				Degrees[v].Out -= 1;
				EdgeCount--;
			}

			return this;
		}
		public IOrientedGraph RemoveVertex(string vertexName)
		{
			Vertex vertex = new OrientedVertex(vertexName);

			return RemoveVertex(vertex);
		}

		public IOrientedGraph RemoveEdge(IOrientedEdge edge){
			return RemoveEdge(edge.VertexIn, edge.VertexOut);
		}
		public IOrientedGraph RemoveEdge(Vertex vertexIn, Vertex vertexOut){
			CheckEdgeNotInGraph(vertexIn, vertexOut);

			EdgeCount--;
			Degrees[vertexIn].In -= 1;
			Degrees[vertexOut].Out -= 1;
			NeighboursList[vertexIn].Remove(vertexOut);

			return this;
		}
		public IOrientedGraph RemoveEdge(string vertexInName, string vertexOutName){
			Vertex vertexIn = new OrientedVertex(vertexInName);
			Vertex vertexOut = new OrientedVertex(vertexOutName);

			return RemoveEdge(vertexIn, vertexOut);
		}
    }
}
