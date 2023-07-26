using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GraphLibrary.Edges;
using GraphLibrary.Vertices;
using GraphLibrary.Graphs;
using GraphLibrary.Extensions.StringExtensions;
using GraphLibrary.Extensions.IOrientedGraphExtensions;


namespace GraphLibrary
{
	enum VertexState { OPENED, CLOSED, UNVISITED}
	public static class Algorithms
	{
		static public void Bfs<TVertex, TEdge>(IOrientedGraph<TVertex, TEdge> graph, VertexName sourceVertex, OrientedVertexAction<TVertex> vertexAction, OrientedEdgeAction<TEdge> edgeAction)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			var queue = new Queue<VertexName>();
			var visited = new Dictionary<VertexName, VertexState>();

			if (!graph.IsVertex(sourceVertex)) throw new ArgumentException("Source Vertex is not in the given Graph");
			queue.Enqueue(sourceVertex);

			while (queue.Count != 0) {
				var vertex = queue.Dequeue();
				if (visited.ContainsKey(vertex)) continue;
				visited.Add(vertex, VertexState.OPENED);
				graph.GetOutAdjacentVertices(vertex).Where(v => !visited.ContainsKey(v.Name)).ToList().ForEach(unvisited => queue.Enqueue(unvisited.Name));
				vertexAction(graph.GetVertex(vertex));
				graph.GetOutEdges(vertex).ToList().ForEach(e => edgeAction(e));
				visited[vertex] = VertexState.CLOSED;
			}
		}
		static public void Bfs<TVertex, TEdge>(IOrientedGraph<TVertex, TEdge> graph, OrientedVertexAction<TVertex> vertexAction, OrientedEdgeAction<TEdge> edgeAction)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			var vertices = graph.GetVertices().Select(v => v.Name).ToHashSet();
			while (vertices.Count != 0)
			{
				var vertex = vertices.First();
				Bfs(graph, vertex, v => { vertices.Remove(v.Name); vertexAction(v); }, edgeAction);
			}
		}
			static public void Dfs<TVertex, TEdge>(IOrientedGraph<TVertex, TEdge> graph, VertexName sourceVertex, OrientedVertexAction<TVertex> vertexAction, OrientedEdgeAction<TEdge> edgeAction)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			if (!graph.IsVertex(sourceVertex)) throw new ArgumentException("Source Vertex is not in the given Graph");

			var stack = new Stack<VertexName>();
			var visited = new Dictionary<VertexName, VertexState>();
			stack.Push(sourceVertex);

			while (stack.Count != 0)
			{
				var vertex = stack.Pop();
				if (visited.ContainsKey(vertex)) continue;
				visited.Add(vertex, VertexState.OPENED);
				graph.GetOutAdjacentVertices(vertex).Where(v => !visited.ContainsKey(v.Name)).ToList().ForEach(unvisited => stack.Push(unvisited.Name));
				vertexAction(graph.GetVertex(vertex));
				graph.GetOutEdges(vertex).ToList().ForEach(e => edgeAction(e));
				visited[vertex] = VertexState.CLOSED;
			}
		}
		static public void Dfs<TVertex, TEdge>(IOrientedGraph<TVertex, TEdge> graph, OrientedVertexAction<TVertex> vertexAction, OrientedEdgeAction<TEdge> edgeAction)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			var vertices = graph.GetVertices().Select(v => v.Name).ToHashSet();
			while (vertices.Count != 0)
			{
				var vertex = vertices.First();
				Dfs(graph, vertex, v => { vertices.Remove(v.Name); vertexAction(v); }, edgeAction) ;
			}
		}

		static public List<List<VertexName>> GetStronglyConnectedComponents<TVertex, TEdge>(IOrientedGraph<TVertex, TEdge> graph)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			var components = new List<List<VertexName>>();
			var unvisited = graph.GetVertices().Select(v => v.Name).ToHashSet();
			var stack = new Stack<VertexName>();

			var reversedGraph = graph.ReverseGraph();

			Dfs(graph, v => stack.Push(v.Name), e => { });

			while (stack.Count != 0){
				var vertex = stack.Pop();
				if (!unvisited.Contains(vertex)) continue;
				var component = new List<VertexName>();
				Dfs(reversedGraph, vertex, v => { unvisited.Remove(v.Name); component.Add(v.Name); }, e => { });
				components.Add(component);
			}

			return components;
		}

		static public bool ContainsEurelianCycle<TVertex, TEdge>(IOrientedGraph<TVertex, TEdge> graph)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			if (GetStronglyConnectedComponents(graph).Count > 1) return false;
			if (graph.GetVerticesWith(v => (v.DegreeIn != v.DegreeOut)).Count() > 0) return false;
			return true;	
		}

		static public List<VertexName> ShortestPath<TVertex, TEdge, TWeight>(IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, VertexName sourceVertex, VertexName destinationVertex, out TWeight pathWeight)
			where TVertex : WeightedOrientedVertex<TWeight>
			where TEdge : WeightedOrientedEdge<TWeight>
			where TWeight : INumber<TWeight>
		{
			var (distances, predecessors) = BellmanFordShortestPath(graph, sourceVertex);
			var path = new List<VertexName>();

			pathWeight = distances[destinationVertex];

			var vertex = destinationVertex;
			for (int i = 0; i < graph.GetVertexCount(); i++)
			{
				if(vertex == sourceVertex)
				{
					path.Add(vertex);
					path.Reverse();
					return path;
				}
				path.Add(vertex);
				vertex = predecessors[vertex];
			}

			throw new Exception("There is no path between source and destination vertices");
		}

		static public (Dictionary<VertexName, TWeight> distances, Dictionary<VertexName, VertexName> predecessors) BellmanFordShortestPath<TVertex, TEdge, TWeight>(IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, VertexName sourceVertex)
			where TVertex : WeightedOrientedVertex<TWeight>
			where TEdge : WeightedOrientedEdge<TWeight>
			where TWeight : INumber<TWeight>
		{
			if (!graph.IsVertex(sourceVertex)) throw new ArgumentException("Source Vertex is not in the given Graph");

			var distances = new Dictionary<VertexName, TWeight>();
			var predecessors = new Dictionary<VertexName, VertexName>();
			var edges = graph.GetEdges().ToList();

			var vertices = graph.GetVertices().Select(v => v.Name).ToHashSet();
			var maxWeight = edges.Select(e => e.Weight).Aggregate((a, b) => a + b);
			var nullVertex = new VertexName("");

			// probably vytvoriť nové exceptions pre toto 
			if (graph.IsVertex(nullVertex)) throw new Exception("Vertex with empty name is in the given Graph");
			if (edges.Any(e => e.Weight > maxWeight)) throw new Exception("Weight type´s number range is too small for edge weights");


			foreach (var vertex in vertices)
			{
				distances.Add(vertex, maxWeight);
				predecessors.Add(vertex, nullVertex);
			}

			distances[sourceVertex] = TWeight.Zero;

			for (int i = 0; i < vertices.Count - 1; i++)
			{
				foreach (var edge in edges)
				{
					var u = edge.VertexOut;
					var v = edge.VertexIn;
					var w = edge.Weight;

					if (distances[u] + w < distances[v])
					{
						distances[v] = distances[u] + w;
						predecessors[v] = u;
					}
				}
			}

			foreach (var edge in edges)
			{
				var u = edge.VertexOut;
				var v = edge.VertexIn;
				var w = edge.Weight;

				if (distances[u] + w < distances[v])
				{
					// Negarive cycle excepiton or something like that
					throw new Exception("Graph contains a negative-weight cycle");
				}
			}

			return (distances, predecessors);
		}
	}
}
