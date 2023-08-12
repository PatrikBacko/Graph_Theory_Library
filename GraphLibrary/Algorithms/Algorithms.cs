using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GraphLibrary.Edges;
using GraphLibrary.Vertices;
using GraphLibrary.Graphs;
using GraphLibrary.Extensions.IOrientedGraphExtensions;
using GraphLibrary.Graphs.Delegates;
using GraphLibrary.Algorithms.Exceptions;
using System.Linq.Expressions;

namespace GraphLibrary.Algorithms
{
	public enum VertexState { OPENED, CLOSED, UNVISITED }
	public static class Algorithms
	{

		static public void BfsFromVertex<TVertex, TEdge>
		(IOrientedGraph<TVertex, TEdge> graph, VertexName sourceVertex, VertexAction<TVertex> vertexAction, EdgeAction<TEdge> edgeAction)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			var queue = new Queue<VertexName>();
			var visited = new Dictionary<VertexName, VertexState>();
			
			if (!graph.IsVertex(sourceVertex)) throw new ArgumentException("Source Vertex is not in the given Graph");
			queue.Enqueue(sourceVertex);
			visited.Add(sourceVertex, VertexState.OPENED);

			while (queue.Count > 0)
			{
				var vertex = queue.Dequeue();

				graph.GetOutEdges(vertex).Where(e => !visited.ContainsKey(e.VertexIn)).ToList().ForEach(e2 => edgeAction(e2));
				graph.GetOutAdjacentVertices(vertex)
					.Where(v => !visited.ContainsKey(v.Name))
					.ToList()
					.ForEach(unvisited => {	queue.Enqueue(unvisited.Name);
											visited.Add(unvisited.Name, VertexState.OPENED); });

				vertexAction(graph.GetVertex(vertex));
				visited[vertex] = VertexState.CLOSED;
			}
		}
		static public void Bfs<TVertex, TEdge>
		(IOrientedGraph<TVertex, TEdge> graph, VertexAction<TVertex> vertexAction, EdgeAction<TEdge> edgeAction)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			var vertices = graph.GetVertices().Select(v => v.Name).ToHashSet();
			while (vertices.Count != 0)
			{
				var vertex = vertices.First();
				BfsFromVertex(
					graph, 
					vertex, 
					v => { vertices.Remove(v.Name); vertexAction(v); }, 
					edgeAction);
			}
		}

		static public void Dfs<TVertex, TEdge>
		(IOrientedGraph<TVertex, TEdge> graph, VertexAction<TVertex> vertexActionOpened, VertexAction<TVertex> vertexActionClosed, EdgeAction<TEdge> edgeAction)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			var visited = new Dictionary<VertexName, VertexState>();
			var unvisited = graph.GetVertices().Select(v => v.Name).ToHashSet();
			while (unvisited.Count > 0)
			{
				var vertex = unvisited.First();
				DfsFromVertex(	
					graph, 
					vertex, 
					vertexActionOpened, 
					v => { vertexActionClosed(v); unvisited.Remove(v.Name); }, 
					edgeAction, 
					visited);
			}
		}
		static public void DfsFromVertex<TVertex, TEdge>
		(IOrientedGraph<TVertex, TEdge> graph, VertexName sourceVertex, VertexAction<TVertex> vertexActionOpened,
		VertexAction<TVertex> vertexActionClosed, EdgeAction<TEdge> edgeAction, Dictionary<VertexName, VertexState>? visited = null)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			if (!graph.IsVertex(sourceVertex)) throw new ArgumentException("Source Vertex is not in the given Graph");

			if (visited is null)
				visited = new Dictionary<VertexName, VertexState>();

			DfsRecursion(	
				graph, 
				sourceVertex, 
				vertexActionOpened, 
				vertexActionClosed, 
				v => { }, 
				e => { if (!visited.ContainsKey(e.VertexIn)) edgeAction(e); }, 
				visited);
		}
		static public void DfsFromVertexSpecial<TVertex, TEdge>
		(IOrientedGraph<TVertex, TEdge> graph, VertexName sourceVertex, VertexAction<TVertex> vertexActionOpened,
		VertexAction<TVertex> vertexActionClosed, VertexAction<TVertex> beforeVisitedCheckAction, EdgeAction<TEdge> edgeAction, Dictionary<VertexName, VertexState>? visited = null)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			if (!graph.IsVertex(sourceVertex)) throw new ArgumentException("Source Vertex is not in the given Graph");

			if (visited is null)
				visited = new Dictionary<VertexName, VertexState>();

			DfsRecursion(	
				graph, 
				sourceVertex, 
				vertexActionOpened, 
				vertexActionClosed, 
				beforeVisitedCheckAction, 
				edgeAction, 
				visited); 
		}
		static private void DfsRecursion<TVertex, TEdge>
		(IOrientedGraph<TVertex, TEdge> graph, VertexName sourceVertex, VertexAction<TVertex> vertexActionOpened,
		VertexAction<TVertex> vertexActionClosed, VertexAction<TVertex> beforeVisitedCheckAction,EdgeAction<TEdge> edgeAction, Dictionary<VertexName, VertexState> visited)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			var sourceGraphVertex = graph.GetVertex(sourceVertex);

			beforeVisitedCheckAction(sourceGraphVertex);
			if (visited.ContainsKey(sourceVertex)) return;

			vertexActionOpened(sourceGraphVertex);
			visited.Add(sourceVertex, VertexState.OPENED);

			foreach (var vertex in graph.GetOutAdjacentVertices(sourceVertex))
			{
				edgeAction(graph.GetEdge(sourceVertex, vertex.Name));
				DfsRecursion(	
					graph, 
					vertex.Name, 
					vertexActionOpened, 
					vertexActionClosed, 
					beforeVisitedCheckAction, 
					edgeAction, 
					visited);
			}
			vertexActionClosed(sourceGraphVertex);
			visited[sourceVertex] = VertexState.CLOSED;
		}

		static public List<List<VertexName>> GetStronglyConnectedComponents<TVertex, TEdge>(IOrientedGraph<TVertex, TEdge> graph)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			var components = new List<List<VertexName>>();
			var stack = new Stack<VertexName>();

			var reversedGraph = graph.GetReversedGraph();

			Dfs(graph, v=> { }, v => stack.Push(v.Name), e => { });

			var visited = new Dictionary<VertexName, VertexState>();
			while (stack.Count != 0)
			{
				var vertex = stack.Pop();
				if (visited.ContainsKey(vertex)) continue;
				var component = new List<VertexName>();
				DfsFromVertex(reversedGraph, vertex, v => component.Add(v.Name), v => { }, e => { }, visited);
				components.Add(component);
			}

			return components;
		}

		static public bool ContainsEurelianCycle<TVertex, TEdge>(IOrientedGraph<TVertex, TEdge> graph)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			if (GetStronglyConnectedComponents(graph).Count > 1) return false;
			if (graph.GetVerticesWith(v => v.DegreeIn != v.DegreeOut).Count() > 0) return false;
			return true;
		}

		//TODO: ShortestPath add tests
		static public List<VertexName> ShortestPath<TVertex, TEdge, TWeight>
		(IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, VertexName sourceVertex, VertexName destinationVertex, out TWeight pathWeight)
			where TVertex : WeightedOrientedVertex<TWeight>
			where TEdge : WeightedOrientedEdge<TWeight>
			where TWeight : INumber<TWeight>
		{
			if (!graph.IsVertex(destinationVertex)) throw new ArgumentException("Destination Vertex is not in the given Graph");

			var (distances, predecessors) = ShortestPathsAllVertices(graph, sourceVertex);

			if (!distances.ContainsKey(destinationVertex))
				throw new ArgumentException("There is no path from source to destination vertex in given Graph");

			pathWeight = distances[destinationVertex];

			var path = new List<VertexName>();
			var currentVertex = destinationVertex;

			while (currentVertex != sourceVertex)
			{
				path.Add(currentVertex);
				currentVertex = predecessors[currentVertex];
			}

			path.Add(sourceVertex);
			path.Reverse();

			return path;
		}
		static public (Dictionary<VertexName, TWeight> distances, Dictionary<VertexName, VertexName> predecessors) ShortestPathsAllVertices<TVertex, TEdge, TWeight>
		(IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, VertexName sourceVertex)
			where TVertex : WeightedOrientedVertex<TWeight>
			where TEdge : WeightedOrientedEdge<TWeight>
			where TWeight : INumber<TWeight>
		{
			if (!graph.IsVertex(sourceVertex)) throw new ArgumentException("Source Vertex is not in the given Graph");

			if (graph.GetEdgesWith(e => e.Weight < TWeight.Zero).Count() > 0)
				return BellmanFordShortestPath(graph, sourceVertex);
			else
				return DjikstraShortestPath(graph, sourceVertex);
		}

		static private (Dictionary<VertexName, TWeight>, Dictionary<VertexName, VertexName>) DjikstraShortestPath<TVertex, TEdge, TWeight>
		(IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, VertexName sourceVertex)
			where TVertex : WeightedOrientedVertex<TWeight>
			where TEdge : WeightedOrientedEdge<TWeight>
			where TWeight : INumber<TWeight>
		{
			if (!graph.IsVertex(sourceVertex))
				throw new ArgumentException("Source Vertex is not in the given Graph");
			if (graph.GetEdgesWith(e => e.Weight < TWeight.Zero).Count() > 0)
				throw new ArgumentException("Graph contains negative edges");

			var priorityQueue = new PriorityQueue<VertexName, TWeight>();
			var distances = new Dictionary<VertexName, TWeight>();
			var predecessors = new Dictionary<VertexName, VertexName>();

			priorityQueue.Enqueue(sourceVertex, TWeight.Zero);
			distances[sourceVertex] = TWeight.Zero;

			while (priorityQueue.Count != 0)
			{
				var vertex = priorityQueue.Dequeue();

				foreach (var edge in graph.GetOutEdges(vertex))
				{
					var newDistance = distances[vertex] + edge.Weight;
					if ((!distances.ContainsKey(edge.VertexIn)) || (newDistance < distances[edge.VertexIn]))
					{
						distances[edge.VertexIn] = newDistance;
						predecessors[edge.VertexIn] = vertex;
						priorityQueue.Enqueue(edge.VertexIn, newDistance);
					}
				}
			}
			return (distances, predecessors);
		}
		static private (Dictionary<VertexName, TWeight>, Dictionary<VertexName, VertexName>) BellmanFordShortestPath<TVertex, TEdge, TWeight>
		(IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, VertexName sourceVertex)
			where TVertex : WeightedOrientedVertex<TWeight>
			where TEdge : WeightedOrientedEdge<TWeight>
			where TWeight : INumber<TWeight>
		{
			if (!graph.IsVertex(sourceVertex)) throw new ArgumentException("Source Vertex is not in the given Graph");

			var distances = new Dictionary<VertexName, TWeight>();
			var predecessors = new Dictionary<VertexName, VertexName>();

			var edges = graph.GetEdges().ToList();
			var vertices = graph.GetVertices().Select(v => v.Name).ToHashSet();

			distances[sourceVertex] = TWeight.Zero;

			for (int i = 0; i < vertices.Count - 1; i++)
			{
				foreach (var edge in edges)
				{
					if (!distances.ContainsKey(edge.VertexOut)) continue;

					var newDistance = distances[edge.VertexOut] + edge.Weight;
					if ((!distances.ContainsKey(edge.VertexIn)) || (newDistance < distances[edge.VertexIn]))
					{
						distances[edge.VertexIn] = newDistance;
						predecessors[edge.VertexIn] = edge.VertexOut;
					}
				}
			}

			foreach (var edge in edges)
			{
				if ((!distances.ContainsKey(edge.VertexOut)) || (!distances.ContainsKey(edge.VertexIn))) continue;

				if (distances[edge.VertexOut] + edge.Weight < distances[edge.VertexIn])
				{
					throw new GraphHasNegativeCycleException("Graph contains a negative-weight cycle");
				}
			}

			return (distances, predecessors);
		}

		static public List<VertexName> TopologicalSorting<TVertex, TEdge>
		(IOrientedGraph<TVertex, TEdge> graph)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			List<VertexName> topologicalSorting;

			if (!HasTopologicalSorting(graph, out topologicalSorting!))
				throw new GraphIsNotDAGException("Graph is not a DAG");

			return topologicalSorting;
		}

		static public bool IsDag<TVertex, TEdge>
		(IOrientedGraph<TVertex, TEdge> graph)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			return HasTopologicalSorting(graph, out var topologicalSorting);
		}

		static private bool HasTopologicalSorting<TVertex, TEdge>
		(IOrientedGraph<TVertex, TEdge> graph, out List<VertexName>? topologicalSorting)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			var stack = new Stack<VertexName>();
			var visited = new Dictionary<VertexName, VertexState>();
			var unvisited = graph.GetVertices().Select(v => v.Name).ToHashSet();

			var isDag = true;

			while (unvisited.Count > 0 && isDag)
			{
				var vertex = unvisited.First();

				DfsFromVertexSpecial(graph,
					vertex,
					v => { unvisited.Remove(v.Name); }, 
					v => { stack.Push(v.Name); }, 
					v => { if (visited.ContainsKey(v.Name) && visited[v.Name] == VertexState.OPENED) isDag = false; },
					e => { },
					visited);
			}

			if (isDag)
				topologicalSorting = stack.ToList();
			else
				topologicalSorting = null;
			return isDag;
		}
	}
}
