using GraphLibrary.Edges;
using GraphLibrary.Vertices;
using GraphLibrary.Graphs.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Graphs.Delegates;

//maybe prejsť z eager na lazy a detekovať zmeny

namespace GraphLibrary.Graphs
{
    public class OrientedGraph<TVertex, TEdge> : IOrientedGraph<TVertex, TEdge>
		where TVertex : OrientedVertex//, new()
		where TEdge : OrientedEdge//, new()
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
			if (!IsVertex(vertex.Name))
				return false;

			var v = vertices[vertex.Name];
			if (v == vertex)
				return true;
			return false;
		}

		public bool IsEdge(VertexName vertexOut, VertexName vertexIn)
		{
			ConteinsVertex(vertexOut);
			ConteinsVertex(vertexIn);

			if (neighbors[vertexOut].ContainsKey(vertexIn))
				return true;
			return false;
		}
		public bool IsEdge(TEdge edge)
		{
			ConteinsVertex(edge.VertexOut);
			ConteinsVertex(edge.VertexIn);

			if (!IsEdge(edge.VertexOut, edge.VertexIn))
				return false;

			var e = neighbors[edge.VertexOut][edge.VertexIn];
			if (e == edge)
				return true;
			return false;
		}

		public IEnumerable<TVertex> GetVertices() => vertices.Values;
		public IEnumerable<TVertex> GetVerticesWith(OrientedVertexPredicate<TVertex> vertexPredicate) {
			List<TVertex> returnVertices = new List<TVertex>();
			foreach (var vertex in vertices.Values)
				if (vertexPredicate(vertex))
					returnVertices.Add(vertex);
			return returnVertices;
		}
		public IEnumerable<TEdge> GetEdges() {
			List<TEdge> edges = new List<TEdge>();
			foreach (var vertex in vertices.Values)
				foreach (var edge in neighbors[vertex.Name].Values)
					edges.Add(edge);
			return edges;
		}
		public IEnumerable<TEdge> GetEdgesWith(OrientedEdgePredicate<TEdge> edgePredicate) {
			List<TEdge> returnEdges = new List<TEdge>();
			foreach (var edge in GetEdges())
				if (edgePredicate(edge))
					returnEdges.Add(edge);
			return returnEdges;
		}

		public OrientedGraph<TVertex, TEdge> ApplyToVertices(OrientedVertexAction<TVertex> vertexAction) {
			var vertices = GetVertices();
			foreach (var vertex in vertices)
				vertexAction(vertex);
			return this;
		}
		public OrientedGraph<TVertex, TEdge> ApplyToVerticesWith(OrientedVertexPredicate<TVertex> vertexPredicate, OrientedVertexAction<TVertex> vertexAction) {
			var vertices = GetVerticesWith(vertexPredicate);
			foreach (var vertex in vertices)
				vertexAction(vertex);
			return this;
		}
		public OrientedGraph<TVertex, TEdge> ApplyToEdges(OrientedEdgeAction<TEdge> edgeAction) {
			var edges = GetEdges();
			foreach (var edge in edges)
				edgeAction(edge);
			return this;
		}
		public OrientedGraph<TVertex, TEdge> ApplyToEdgesWith(OrientedEdgePredicate<TEdge> edgePredicate, OrientedEdgeAction<TEdge> edgeAction) {
			var edges = GetEdgesWith(edgePredicate);
			foreach (var edge in edges)
				edgeAction(edge);
			return this;
		}

		public TVertex GetVertex(VertexName vertex) {
			ConteinsVertex(vertex);
			return vertices[vertex];
		}
		public TEdge GetEdge(VertexName vertexOut, VertexName vertexIn) {
			ContainsEdge(vertexOut, vertexIn);
			return neighbors[vertexOut][vertexIn];
		}

		public int GetVertexCount() => vertices.Count;
		public int GetEdgeCount() => edgeCount;
		public IEnumerable<TVertex> GetInAdjacentVertices(VertexName vertex) {
			ConteinsVertex(vertex);
			List<TVertex> adjVerticesIn = new List<TVertex>();
			foreach (var v in GetVertices())
				if (IsEdge(v.Name, vertex))
					adjVerticesIn.Add(v);
			return adjVerticesIn;
		}
		public IEnumerable<TVertex> GetOutAdjacentVertices(VertexName vertex) {
			ConteinsVertex(vertex);
			List<TVertex> adjVerticesOut = new List<TVertex>();
			foreach (var v in GetVertices())
				if (IsEdge(vertex, v.Name))
					adjVerticesOut.Add(v);
			return adjVerticesOut;
		}

		public IEnumerable<TEdge> GetInEdges(VertexName vertex) {
			ConteinsVertex(vertex);
			List<TEdge> edgesIn = new List<TEdge>();
			foreach (var v in GetVertices())
				if (IsEdge(v.Name, vertex))
					edgesIn.Add(GetEdge(v.Name, vertex));
			return edgesIn;
		}

		public IEnumerable<TEdge> GetOutEdges(VertexName vertex) {
			ConteinsVertex(vertex);
			List<TEdge> edgesOut = new List<TEdge>();
			foreach (var v in GetVertices())
				if (IsEdge(vertex, v.Name))
					edgesOut.Add(GetEdge(vertex, v.Name));
			return edgesOut;
		}

		public int GetInDegree(VertexName vertexName) {
			ConteinsVertex(vertexName);
			var vertex = GetVertex(vertexName);
			return vertex.DegreeIn;
		}
		public int GetOutDegree(VertexName vertexName) {
			ConteinsVertex(vertexName);
			var vertex = GetVertex(vertexName);
			return vertex.DegreeOut;
		}

		public OrientedGraph<TVertex, TEdge> AddVertex(TVertex vertex) {
			ValidVertexName(vertex.Name);
			if (IsVertex(vertex))
				throw new VertexException("Vertex already exists in Graph");
			vertices.Add(vertex.Name, vertex);
			neighbors.Add(vertex.Name, new Dictionary<VertexName, TEdge>());
			return this;
		}
		public OrientedGraph<TVertex, TEdge> AddVertices(IEnumerable<TVertex> vertices) {
			foreach (var vertex in vertices)
				AddVertex(vertex);
			return this;
		}

		public OrientedGraph<TVertex, TEdge> AddEdge(TEdge edge) {
			ValidVertexName(edge.VertexOut);
			ValidVertexName(edge.VertexIn);
			ConteinsVertex(edge.VertexOut);
			ConteinsVertex(edge.VertexIn);

			if (IsEdge(edge))
				throw new EdgeException("Edge already exists in Graph");

			neighbors[edge.VertexOut].Add(edge.VertexIn, edge);
			edgeCount++;

			vertices[edge.VertexOut].DegreeOut++;
			vertices[edge.VertexIn].DegreeIn++;
			return this;
		}
		public OrientedGraph<TVertex, TEdge> AddEdges(IEnumerable<TEdge> edges) {
			foreach (var edge in edges)
				AddEdge(edge);
			return this;
		}

		public OrientedGraph<TVertex, TEdge> RemoveVertex(VertexName vertexName) {
			ConteinsVertex(vertexName);
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

		public OrientedGraph<TVertex, TEdge> RemoveEdge(VertexName vertexOut, VertexName vertexIn) {
			ContainsEdge(vertexOut, vertexIn);
			neighbors[vertexOut].Remove(vertexIn);
			edgeCount--;
			vertices[vertexOut].DegreeOut--;
			vertices[vertexIn].DegreeIn--;
			return this;
		}
		public OrientedGraph<TVertex, TEdge> RemoveEdge(TEdge edge) {
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

		public static OrientedGraph<TVertex, TEdge> CreateGraph() => new OrientedGraph<TVertex, TEdge>();
		public static OrientedGraph<TVertex, TEdge> CreateGraph(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges) {
			var graph = new OrientedGraph<TVertex, TEdge>();
			graph.AddVertices(vertices);
			graph.AddEdges(edges);
			return graph;
		}


		public static OrientedGraph<TVertex, TEdge> operator +(OrientedGraph<TVertex, TEdge> graph, TVertex vertex) => graph.AddVertex(vertex);
		public static OrientedGraph<TVertex, TEdge> operator +(OrientedGraph<TVertex, TEdge> graph, TEdge edge) => graph.AddEdge(edge);

		public static OrientedGraph<TVertex, TEdge> operator -(OrientedGraph<TVertex, TEdge> graph, TVertex vertex) => graph.RemoveVertex(vertex);
		public static OrientedGraph<TVertex, TEdge> operator -(OrientedGraph<TVertex, TEdge> graph, TEdge edge) => graph.RemoveEdge(edge);

		protected void ConteinsVertex(VertexName vertex)
		{
			if (!IsVertex(vertex))
				throw new VertexException("Vertex doesn't exist in Graph");
		}
		protected void ContainsEdge(VertexName vertexOut, VertexName vertexIn)
		{
			if (!IsEdge(vertexOut, vertexIn))
				throw new EdgeException("Edge doesn't exist in Graph");
		}
		protected void ValidVertexName(VertexName vertex)
		{
			if (vertex.Value == "")
				throw new VertexException("Vertex name can't be empty string");
		}

		//public OrientedGraph<TVertex, TEdge> ReverseEdge(){
		//	return this;
		//}

		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.ApplyToVertices(OrientedVertexAction<TVertex> vertexAction) 
			=> ApplyToVertices(vertexAction);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.ApplyToVerticesWith(OrientedVertexPredicate<TVertex> vertexPredicate, OrientedVertexAction<TVertex> vertexAction) 
			=> ApplyToVerticesWith(vertexPredicate, vertexAction);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.ApplyToEdges(OrientedEdgeAction<TEdge> edgeAction) 
			=>	ApplyToEdges(edgeAction);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.ApplyToEdgesWith(OrientedEdgePredicate<TEdge> edgePredicate, OrientedEdgeAction<TEdge> edgeAction) 
			=>	ApplyToEdgesWith(edgePredicate, edgeAction);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.AddVertex(TVertex vertex) 
			=> AddVertex(vertex);
		//IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.AddVertex(VertexName vertex) => AddVertex(vertex);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.AddVertices(IEnumerable<TVertex> vertices) 
			=> AddVertices(vertices);

		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.AddEdge(TEdge edge) 
			=> AddEdge(edge);
		//IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.AddEdge(VertexName vertexOut, VertexName vertexIn) => AddEdge(vertexOut, vertexIn);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.AddEdges(IEnumerable<TEdge> edges) 
			=> AddEdges(edges);

		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveVertex(VertexName vertex) 
			=> RemoveVertex(vertex);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveVertex(TVertex vertex) 
			=> RemoveVertex(vertex);

		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveEdge(VertexName vertexOut, VertexName vertexIn) 
			=> RemoveEdge(vertexOut, vertexIn);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveEdge(TEdge edge) 
			=> RemoveEdge(edge);

		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.ClearGraph()
			=> ClearGraph();
		static IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.CreateGraph() 
			=> CreateGraph();
		static IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.CreateGraph(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges) 
			=> CreateGraph(vertices, edges);
	}
}
