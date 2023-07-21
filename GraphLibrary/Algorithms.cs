using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Edges;
using GraphLibrary.Vertices;
using GraphLibrary.Graphs;

namespace GraphLibrary
{
	public delegate void BfsDelegate<TVertex, TEdge>(IOrientedGraph<TVertex, TEdge> graph)
		where TVertex : OrientedVertex
		where TEdge : OrientedEdge;
	public static class Algorithms
	{
		static private void Bfs<TVertex, TEdge>(IOrientedGraph<TVertex, TEdge> graph)
		where TVertex : OrientedVertex
		where TEdge : OrientedEdge{
			var queue = new Queue<VertexName>();
			var visited = new HashSet<VertexName>();
			queue.Enqueue(graph.GetVerticies().First().Name);

			while (queue.Count != 0){
				var vertex = queue.Dequeue();
				if (visited.Contains(vertex)) continue;
				visited.Add(vertex);
				graph.GetOutAdjacentVertices(vertex).Where(v => !visited.Contains(v.Name)).ToList().ForEach(unvisited => queue.Enqueue(unvisited.Name));
			}
		}
		static private void Dfs<TVertex, TEdge>(IOrientedGraph<TVertex, TEdge> graph)
		where TVertex : OrientedVertex
		where TEdge : OrientedEdge
		{
			var stack = new Stack<VertexName>();
			var visited = new HashSet<VertexName>();
			stack.Push(graph.GetVerticies().First().Name);

			while (stack.Count != 0)
			{
				var vertex = stack.Pop();
				if (visited.Contains(vertex)) continue;
				visited.Add(vertex);
				graph.GetOutAdjacentVertices(vertex).Where(v => !visited.Contains(v.Name)).ToList().ForEach(unvisited => stack.Push(unvisited.Name));
			}
		}
	}
}
