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

		private Dictionary<IOrientedVertex, Degree> Degrees { get; set; }
		internal Dictionary<IOrientedVertex, HashSet<IOrientedVertex>> NeighboursList { get; private set; }
		private int EdgeCount { get; set; }

		public GraphNeighborsList()
		{
			Degrees = new Dictionary<IOrientedVertex, Degree>();
			NeighboursList = new Dictionary<IOrientedVertex, HashSet<IOrientedVertex>>();
			EdgeCount = 0;
		}

		private void CheckVertexInGraph(IOrientedVertex vertex) {
			if (!IsVertex(vertex))
				throw new VertexException("Vertex is not in graph");
		}
		private void CheckVertexNotInGraph(IOrientedVertex vertex)
		{
			if (IsVertex(vertex))
				throw new VertexException("Vertex is already in graph");
		}
		private void CheckEdgeInGraph(IOrientedVertex VertexIn, IOrientedVertex VertexOut) {
			if (!IsEdge(VertexIn, VertexOut))
				throw new EdgeException("Edge is not in graph");
		}
		private void CheckEdgeNotInGraph(IOrientedVertex VertexIn, IOrientedVertex VertexOut)
		{
			if (IsEdge(VertexIn, VertexOut))
				throw new EdgeException("Edge is already in graph");
		}

		public IOrientedVertex ReturnVertexWithName(string name) {
			IOrientedVertex vertex = new OrientedVertex(name);
			CheckVertexInGraph(vertex);
			return vertex;
		}

		public IEnumerable<IOrientedVertex> GetVertices() => NeighboursList.Keys;
		public IEnumerable<IOrientedEdge> GetEdges() =>
			NeighboursList.SelectMany(v => v.Value.Select(v2 => (IOrientedEdge)new OrientedEdge(v.Key, v2)));

		public IEnumerable<IOrientedVertex> GetInAdjacentVertices(IOrientedVertex vertex) {
			CheckVertexInGraph(vertex);
			return NeighboursList.Where(v => v.Value.Contains(vertex)).Select(v => v.Key);
		}
		public IEnumerable<IOrientedVertex> GetInAdjacentVertices(string vertexName)
		{
			IOrientedVertex vertex = new OrientedVertex(vertexName);
			return GetInAdjacentVertices(vertex);
		}

		public IEnumerable<IOrientedVertex> GetOutAdjacentVertices(IOrientedVertex vertex)
		{
			CheckVertexInGraph(vertex);
			return NeighboursList[vertex];
		}
		public IEnumerable<IOrientedVertex> GetOutAdjacentVertices(string vertexName)
		{
			IOrientedVertex vertex = new OrientedVertex(vertexName);
			return GetOutAdjacentVertices(vertex);
		}

		public IEnumerable<IOrientedEdge> GetInEdges(IOrientedVertex vertex)
		{
			CheckVertexInGraph(vertex);
			return NeighboursList.Where(v => v.Value.Contains(vertex)).Select(v => (IOrientedEdge)new OrientedEdge(v.Key, vertex));
		}
		public IEnumerable<IOrientedEdge> GetInEdges(string vertexName)
		{
			IOrientedVertex vertex = new OrientedVertex(vertexName);
			return GetInEdges(vertex);
		}

		public IEnumerable<IOrientedEdge> GetOutEdges(IOrientedVertex vertex)
		{
			CheckVertexInGraph(vertex);
			return NeighboursList[vertex].Select(v => (IOrientedEdge)new OrientedEdge(vertex, v));
		}
		public IEnumerable<IOrientedEdge> GetOutEdges(string vertexName)
		{
			IOrientedVertex vertex = new OrientedVertex(vertexName);
			return GetOutEdges(vertex);
		}



		public int GetInDegree(IOrientedVertex vertex) {
			CheckVertexInGraph(vertex);
			return Degrees[vertex].In;
		}
		public int GetInDegree(string vertexName)
		{
			IOrientedVertex vertex = new OrientedVertex(vertexName);
			return GetInDegree(vertex);
		}

		public int GetOutDegree(IOrientedVertex vertex) {
			CheckVertexInGraph(vertex);
			return Degrees[vertex].Out;
		}
		public int GetOutDegree(string vertexName)
		{
			IOrientedVertex vertex = new OrientedVertex(vertexName);
			return GetOutDegree(vertex);
		}

		public int GetVertexCount() => NeighboursList.Count;
		public int GetEdgeCount() => EdgeCount;



		public bool IsVertex(IOrientedVertex vertex) => NeighboursList.ContainsKey(vertex);
		public bool IsVertex(string vertexName)
		{
			IOrientedVertex vertex = new OrientedVertex(vertexName);
			return NeighboursList.ContainsKey(vertex);
		}

		//porozmýšlať či nechať vyhadzovať vertex exception alebo pridať vyhadzovanie inej exception alebo returnovať false keď nie vertex v grafe
		public bool IsEdge(IOrientedVertex vertexIn, IOrientedVertex vertexOut) {
			CheckVertexInGraph(vertexIn);
			CheckVertexInGraph(vertexOut);

			return NeighboursList[vertexOut].Contains(vertexIn);
		}
		public bool IsEdge(string NameOfVertexIn, string NameOfVertexOut)
		{
			IOrientedVertex vertexIn = new OrientedVertex(NameOfVertexIn);
			IOrientedVertex vertexOut = new OrientedVertex(NameOfVertexOut);
			
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

		public IOrientedGraph AddVertex(IOrientedVertex vertex){
			CheckVertexInGraph(vertex);

			Degrees[vertex] = new Degree(0, 0);
			NeighboursList[vertex] = new HashSet<IOrientedVertex>();

			return this;
		}
		public IOrientedGraph AddVertex(string vertexName)
		{
			IOrientedVertex vertex = new OrientedVertex(vertexName);
			return AddVertex(vertex);
		}

		public IOrientedGraph AddEdge(IOrientedEdge edge){
			return AddEdge(edge.VertexIn, edge.VertexOut);
		}
		public IOrientedGraph AddEdge(IOrientedVertex vertexIn, IOrientedVertex vertexOut)
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
			IOrientedVertex vertexIn = new OrientedVertex(vertexInName);
			IOrientedVertex vertexOut = new OrientedVertex(vertexOutName);

			return AddEdge(vertexIn, vertexOut);
		}

		public IOrientedGraph RemoveVertex(IOrientedVertex vertex){
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
			IOrientedVertex vertex = new OrientedVertex(vertexName);

			return RemoveVertex(vertex);
		}

		public IOrientedGraph RemoveEdge(IOrientedEdge edge){
			return RemoveEdge(edge.VertexIn, edge.VertexOut);
		}
		public IOrientedGraph RemoveEdge(IOrientedVertex vertexIn, IOrientedVertex vertexOut){
			CheckEdgeNotInGraph(vertexIn, vertexOut);

			EdgeCount--;
			Degrees[vertexIn].In -= 1;
			Degrees[vertexOut].Out -= 1;
			NeighboursList[vertexIn].Remove(vertexOut);

			return this;
		}
		public IOrientedGraph RemoveEdge(string vertexInName, string vertexOutName){
			IOrientedVertex vertexIn = new OrientedVertex(vertexInName);
			IOrientedVertex vertexOut = new OrientedVertex(vertexOutName);

			return RemoveEdge(vertexIn, vertexOut);
		}
    }
}
