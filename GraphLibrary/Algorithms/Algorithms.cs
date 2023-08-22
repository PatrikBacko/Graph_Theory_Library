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
		#region Bfs
		/// <summary>
		/// Breadth First Search from a given Vertex. <br />
		/// If there are vertices that are not connected to the sourceVertex by a path, they will not be visited. <br />
		/// When vertex is visited, vertexAction is called on it. <br />
		/// When algorithm goes through an edge, edgeAction is called on it. 
		/// (When the end of Edge is a vertex which was already visited, EdgeAction will not be performed on that edge)
		/// </summary>
		/// <typeparam name="TVertex"> 
		/// Type of Vertex in the Graph. <br />
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of Edge in the Graph. <br />
		/// </typeparam>
		/// <param name="graph">
		/// Graph on which algorithm is performed. <br />
		/// </param>
		/// <param name="sourceVertex">
		/// Vertex from which algorithm starts. <br />
		/// </param>
		/// <param name="vertexAction">
		/// Action performed on a Vertex when it is visited. <br />
		/// </param>
		/// <param name="edgeAction">
		/// Action performed on an Edge when it is visited. <br />
		/// </param>
		/// <exception cref="ArgumentException">
		/// Exception thrown when sourceVertex is not in the graph. <br />
		/// </exception>
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

		/// <summary>
		/// Breadth First Search on a Graph, visits all the vertices, but innitial vertex can´t be chosen <br />
		/// When vertex is visited, vertexAction is called on it. <br />
		/// When algorithm goes through an edge, edgeAction is called on it.
		/// (When the end of Edge is a vertex which was already visited, EdgeAction will not be performed on that edge)
		/// </summary>
		/// <typeparam name="TVertex">
		/// type of Vertex in the Graph. <br />
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// type of Edge in the Graph. <br />
		/// </typeparam>
		/// <param name="graph">
		/// Graph on which algorithm is performed. <br />
		/// </param>
		/// <param name="vertexAction">
		/// Action performed on a Vertex when it is visited. <br />
		/// </param>
		/// <param name="edgeAction">
		/// Action performed on an Edge when it is visited. <br />
		/// </param>
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
		#endregion

		#region Dfs
		/// <summary>
		/// Depth First Search on a Graph, visits all the vertices, but innitial vertex can´t be chosen <br />
		/// When vertex is opened, vertexActionOpened is called on it. <br />
		/// When vertex is closed, vertexActionClosed is called on it. <br />
		/// When algorithm goes through an edge, edgeAction is called on it.
		/// (When the end of Edge is a vertex which was already visited, EdgeAction will not be performed on that edge) <br />
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of Vertex in the Graph.
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of Edge in the Graph.
		/// </typeparam>
		/// <param name="graph">
		/// Graph on which algorithm is performed.
		/// </param>
		/// <param name="vertexActionOpened">
		/// Action performed on a Vertex when it is opened.
		/// </param>
		/// <param name="vertexActionClosed">
		/// Action performed on a Vertex when it is closed.
		/// </param>
		/// <param name="edgeAction">
		/// Action performed on an Edge when it is visited.
		/// </param>
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

		/// <summary>
		/// Depth First Search on a Graph from a given Vertex. <br />
		/// If there are vertices that are not connected to the sourceVertex by a path, they will not be visited. <br /> <br />
		/// When vertex is opened, vertexActionOpened is called on it. <br />
		/// When vertex is closed, vertexActionClosed is called on it. <br />
		/// When algorithm goes through an edge, edgeAction is called on it.
		/// (When the end of Edge is a vertex which was already visited, EdgeAction will not be performed on that edge) <br />
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of Vertex in the Graph.
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of Edge in the Graph.
		/// </typeparam>
		/// <param name="graph">
		/// Graph on which algorithm is performed.
		/// </param>
		/// <param name="vertexActionOpened">
		/// Action performed on a Vertex when it is opened.
		/// </param>
		/// <param name="vertexActionClosed">
		/// Action performed on a Vertex when it is closed.
		/// </param>
		/// <param name="edgeAction">
		/// Action performed on an Edge when it is visited.
		/// </param>
		/// <param name="visited">
		/// Dict of visited vertices and their states.
		/// </param>
		/// <exception cref="ArgumentException">
		/// Exception thrown when sourceVertex is not in the given Graph.
		/// </exception>
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

		/// <summary>
		/// Depth First Search on a Graph from a given Vertex. <br />
		/// If there are vertices that are not connected to the sourceVertex by a path, they will not be visited. <br />
		/// When vertex is opened, vertexActionOpened is called on it. <br />
		/// When vertex is closed, vertexActionClosed is called on it. <br />
		/// Every time before algorithm goes into a vertex, even if it was already visited, vertexActionEveryTime is called on it. <br />
		/// but if it is opened or closed, algorithm will not call dfs on its children. <br />
		/// When algorithm goes through an edge, edgeAction is called on it.
		/// (When the end of Edge is a vertex which was already visited, EdgeAction will be performed anyways) <br />
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of Vertex in the Graph.
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of Edge in the Graph.
		/// </typeparam>
		/// <param name="graph">
		/// Graph on which algorithm is performed.
		/// </param>
		/// <param name="sourceVertex">
		/// Vertex from which algorithm starts.
		/// </param>
		/// <param name="vertexActionOpened">
		/// Action performed on a Vertex when it is opened.
		/// </param>
		/// <param name="vertexActionClosed">
		/// Action performed on a Vertex when it is closed.
		/// </param>
		/// <param name="vertexActionEveryTime">
		/// Action performed on a Vertex every time it is visited, even if it was already opened or closed.
		/// </param>
		/// <param name="edgeAction">
		/// Action performed on an Edge when it is visited.
		/// </param>
		/// <param name="visited">
		/// Dict of visited vertices and their states.
		/// </param>
		/// <exception cref="ArgumentException">
		/// Exception thrown when sourceVertex is not in the given Graph.
		/// </exception>
		static public void DfsFromVertexSpecial<TVertex, TEdge>
		(IOrientedGraph<TVertex, TEdge> graph, VertexName sourceVertex, VertexAction<TVertex> vertexActionOpened,
		VertexAction<TVertex> vertexActionClosed, VertexAction<TVertex> vertexActionEveryTime, EdgeAction<TEdge> edgeAction, Dictionary<VertexName, VertexState>? visited = null)
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
				vertexActionEveryTime, 
				edgeAction, 
				visited); 
		}

		/// <summary>
		/// Recursive part of Depth First Search on a Graph from a given Vertex. <br />
		/// Used by all other Dfs methods. <br />
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of Vertex in the Graph.
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of Edge in the Graph.
		/// </typeparam>
		/// <param name="graph">
		/// Graph on which algorithm is performed.
		/// </param>
		/// <param name="sourceVertex">
		/// Vertex from which algorithm starts.
		/// </param>
		/// <param name="vertexActionOpened">
		/// Action performed on a Vertex when it is opened.
		/// </param>
		/// <param name="vertexActionClosed">
		/// Action performed on a Vertex when it is closed.
		/// </param>
		/// <param name="vertexActionEveryTime">
		/// Action performed on a Vertex every time it is visited, even if it was already opened or closed.
		/// </param>
		/// <param name="edgeAction">
		/// Action performed on an Edge when it is visited.
		/// </param>
		/// <param name="visited">
		/// Dict of visited vertices and their states.
		/// </param>
		static private void DfsRecursion<TVertex, TEdge>
		(IOrientedGraph<TVertex, TEdge> graph, VertexName sourceVertex, VertexAction<TVertex> vertexActionOpened,
		VertexAction<TVertex> vertexActionClosed, VertexAction<TVertex> vertexActionEveryTime,EdgeAction<TEdge> edgeAction, Dictionary<VertexName, VertexState> visited)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			var sourceGraphVertex = graph.GetVertex(sourceVertex);

			vertexActionEveryTime(sourceGraphVertex);
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
					vertexActionEveryTime, 
					edgeAction, 
					visited);
			}
			vertexActionClosed(sourceGraphVertex);
			visited[sourceVertex] = VertexState.CLOSED;
		}
		#endregion

		#region Strongly Connected Components
		/// <summary>
		/// Algorithm for finding strongly connected components in a Graph. <br />
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of Vertex in the Graph.
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of Edge in the Graph.
		/// </typeparam>
		/// <param name="graph">
		/// Graph on which algorithm is performed.
		/// </param>
		/// <returns>
		/// List of strongly connected components in a Graph.
		/// </returns>
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
		#endregion

		#region Contains Eurelian Cycle
		/// <summary>
		/// Checks if a Graph contains Eurelian Cycle. <br />
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of Vertex in the Graph.
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of Edge in the Graph.
		/// </typeparam>
		/// <param name="graph">
		/// Graph on which algorithm is performed.
		/// </param>
		/// <returns>
		/// bool value indicating if a Graph contains Eurelian Cycle.
		/// </returns>
		static public bool ContainsEurelianCycle<TVertex, TEdge>(IOrientedGraph<TVertex, TEdge> graph)
			where TVertex : OrientedVertex, new()
			where TEdge : OrientedEdge, new()
		{
			if (GetStronglyConnectedComponents(graph).Count > 1) return false;
			if (graph.GetVerticesWith(v => v.DegreeIn != v.DegreeOut).Count() > 0) return false;
			return true;
		}
		#endregion

		#region Shortest Path
		/// <summary>
		/// Algorithm for finding shortest path between two vertices in a Graph. <br />
		/// If Graph has no negative edges, Dijkstra algorithm is used. <br />
		/// Otherwise Bellman-Ford algorithm is used. <br />
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of Vertex in the Graph.
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of Edge in the Graph.
		/// </typeparam>
		/// <typeparam name="TWeight">
		/// Type of Weight of Edges in the Graph.
		/// </typeparam>
		/// <param name="graph">
		/// Graph on which algorithm is performed.
		/// </param>
		/// <param name="sourceVertex">
		/// Vertex from which the path starts.
		/// </param>
		/// <param name="destinationVertex">
		/// Vertex at which the path ends.
		/// </param>
		/// <param name="pathWeight">
		/// Output parameter for the weight of the path.
		/// </param>
		/// <returns>
		/// List of vertices on the shortest path.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Exception thrown When destination or source vertex is not in the given Graph.
		/// </exception>
		/// <exception cref="NoPathException">
		/// Exception thrown when no path exists between source and destination vertex.
		/// </exception>
		/// <exception cref="GraphHasNegativeCycleException">
		/// Exception thrown when Graph contains negative cycle.
		/// </exception>
		static public List<VertexName> ShortestPath<TVertex, TEdge, TWeight>
		(IWeightedOrientedGraph<TVertex, TEdge, TWeight> graph, VertexName sourceVertex, VertexName destinationVertex, out TWeight pathWeight)
			where TVertex : WeightedOrientedVertex<TWeight>
			where TEdge : WeightedOrientedEdge<TWeight>
			where TWeight : INumber<TWeight>
		{
			if (!graph.IsVertex(destinationVertex)) throw new ArgumentException("Destination Vertex is not in the given Graph");

			var (distances, predecessors) = ShortestPathsAllVertices(graph, sourceVertex);

			if (!distances.ContainsKey(destinationVertex))
				throw new NoPathException("There is no path from source to destination vertex in given Graph");

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

		/// <summary>
		/// Algorithm for finding shortest path from sourceVertex to every vertex in a Graph. <br />
		/// If Graph has no negative edges, Dijkstra algorithm is used. <br />
		/// Otherwise Bellman-Ford algorithm is used. <br />
		/// Algorithm returns distances dict and predecessors dict. 
		/// In distances dict, key is vertex name and value is distance from source vertex. <br />
		/// In predecessors dict, key is vertex name and value is predecessor of the vertex on the shortest path from source vertex. <br />
		/// If Vertex is not reachable from source vertex, neither of dicts will contain its name <br />
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of Vertex in the Graph.
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of Edge in the Graph.
		/// </typeparam>
		/// <typeparam name="TWeight">
		/// Type of Weight of Edges in the Graph.
		/// </typeparam>
		/// <param name="graph">
		/// Graph on which algorithm is performed.
		/// </param>
		/// <param name="sourceVertex">
		/// Vertex from which the path starts.
		/// </param>
		/// <returns>
		/// Tuple of two dictionaries. <br />
		/// First dictionary (distances) contains distances from source vertex to every vertex in the Graph. <br />
		/// Second dictionary (precedessors) contains predecessors of every vertex on the shortest path from source vertex. <br />
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Exception thrown When source vertex is not in the given Graph.
		/// </exception>
		/// <exception cref="GraphHasNegativeCycleException">
		/// Exception thrown when Graph contains negative cycle.
		/// </exception>
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

		/// <summary>
		/// Djikstra algorithm for finding shortest path from sourceVertex to every vertex in a Graph. <br />
		/// Cant be used if Graph contains negative edges. <br />
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of Vertex in the Graph.
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of Edge in the Graph.
		/// </typeparam>
		/// <typeparam name="TWeight">
		/// Type of Weight of Edges in the Graph.
		/// </typeparam>
		/// <param name="graph">
		/// Graph on which algorithm is performed.
		/// </param>
		/// <param name="sourceVertex">
		/// Vertex from which the path starts.
		/// </param>
		/// <returns>
		/// Tuple of two dictionaries. <br />
		/// First dictionary (distances) contains distances from source vertex to every vertex in the Graph. <br />
		/// Second dictionary (precedessors) contains predecessors of every vertex on the shortest path from source vertex. <br />
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Exception thrown When source vertex is not in the given Graph,
		/// or when Graph contains negative edges.
		/// </exception>
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

		/// <summary>
		/// Bellman-Ford algorithm for finding shortest path from sourceVertex to every vertex in a Graph. <br />
		/// Can be used if Graph contains negative edges. <br />
		/// Can detect negative cycles. <br />
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of Vertex in the Graph.
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of Edge in the Graph.
		/// </typeparam>
		/// <typeparam name="TWeight">
		/// Type of Weight of Edges in the Graph.
		/// </typeparam>
		/// <param name="graph">
		/// Graph on which algorithm is performed.
		/// </param>
		/// <param name="sourceVertex">
		/// Vertex from which the path starts.
		/// </param>
		/// <returns>
		/// Tuple of two dictionaries. <br />
		/// First dictionary (distances) contains distances from source vertex to every vertex in the Graph. <br />
		/// Second dictionary (precedessors) contains predecessors of every vertex on the shortest path from source vertex. <br />
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Exception thrown When source vertex is not in the given Graph.
		/// </exception>
		/// <exception cref="GraphHasNegativeCycleException">
		/// Exception thrown When Graph contains negative cycle.
		/// </exception>
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

			for (int i = 0; i <= vertices.Count - 1; i++)
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
					throw new GraphHasNegativeCycleException("Graph contains a negative-weight cycle");
			}

			return (distances, predecessors);
		}
		#endregion

		#region Topological Sorting

		/// <summary>
		/// Algorithm for finding topological sorting of a Directed Acyclic Graph. <br />
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of Vertex in the Graph.
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of Edge in the Graph.
		/// </typeparam>
		/// <param name="graph">
		/// Graph on which algorithm is performed.
		/// </param>
		/// <returns>
		/// List of vertices in topological order.
		/// </returns>
		/// <exception cref="GraphIsNotDAGException">
		/// Exception thrown When Graph is not a DAG.
		/// </exception>
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

		/// <summary>
		/// Algorithm for checking if a Graph is a DAG. <br />
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of Vertex in the Graph.
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of Edge in the Graph.
		/// </typeparam>
		/// <param name="graph">
		/// Graph on which algorithm is performed.
		/// </param>
		/// <returns>
		/// bool value indicating if Graph is a DAG.
		/// </returns>
		static public bool IsDag<TVertex, TEdge>
		(IOrientedGraph<TVertex, TEdge> graph)
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			return HasTopologicalSorting(graph, out var topologicalSorting);
		}

		/// <summary>
		/// Private method for finding topological sorting of a Directed Acyclic Graph. <br />
		/// returns true if Graph is a DAG, false otherwise. <br />
		/// out parameter topologicalSorting is set to null if Graph is not a DAG. <br />
		/// </summary>
		/// <typeparam name="TVertex">
		/// Type of Vertex in the Graph.
		/// </typeparam>
		/// <typeparam name="TEdge">
		/// Type of Edge in the Graph.
		/// </typeparam>
		/// <param name="graph">
		/// Graph on which algorithm is performed.
		/// </param>
		/// <param name="topologicalSorting">
		/// Out parameter for topological sorting of the Graph.
		/// Is set to null if Graph is not a DAG.
		/// </param>
		/// <returns>
		/// bool value indicating if Graph is a DAG. (and thus has topological order)
		/// </returns>
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
		#endregion
	}
}
