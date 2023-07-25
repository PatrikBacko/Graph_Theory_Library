using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Edges;
using GraphLibrary.Vertices;
using GraphLibrary.Graphs;
using GraphLibrary.Extensions.StringExtensions;
using GraphLibrary.Extensions.IOrientedGraphExtensions;
using System.Runtime.InteropServices;

namespace GraphLibrary
{
	enum VertexState { OPENED, CLOSED, UNVISITED}
	public static class Algorithms
	{
		static public void Bfs<TVertex, TEdge>(IOrientedGraph<TVertex, TEdge> graph, VertexName startVertex, OrientedVertexAction<TVertex> vertexAction, OrientedEdgeAction<TEdge> edgeAction)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			var queue = new Queue<VertexName>();
			var visited = new Dictionary<VertexName, VertexState>();

			if (!graph.IsVertex(startVertex)) throw new ArgumentException("Start Vertex is not in the given Graph");
			queue.Enqueue(startVertex);

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
			static public void Dfs<TVertex, TEdge>(IOrientedGraph<TVertex, TEdge> graph, VertexName startVertex, OrientedVertexAction<TVertex> vertexAction, OrientedEdgeAction<TEdge> edgeAction)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			if (!graph.IsVertex(startVertex)) throw new ArgumentException("Start Vertex is not in the given Graph");

			var stack = new Stack<VertexName>();
			var visited = new Dictionary<VertexName, VertexState>();
			stack.Push(startVertex);

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
	}
}
