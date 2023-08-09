using GraphLibrary.Vertices;
using GraphLibrary.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Data;

namespace GraphLibrary.Graphs
{
	public class WeightedOrientedGraph<TVertex, TEdge, TWeight> : OrientedGraph<TVertex, TEdge>, IWeightedOrientedGraph<TVertex, TEdge, TWeight>, IOrientedGraph<TVertex, TEdge>
		where TVertex : WeightedOrientedVertex<TWeight>
		where TEdge : WeightedOrientedEdge<TWeight>
		where TWeight : INumber<TWeight>
	{

		public WeightedOrientedGraph() : base() { }

		public virtual TWeight GetWeight(VertexName vertex) => GetVertex(vertex).Weight;
		public virtual TWeight GetWeight(VertexName vertexOut, VertexName vertexIn) => GetEdge(vertexOut, vertexIn).Weight;

		public virtual WeightedOrientedGraph<TVertex, TEdge, TWeight> ChangeVertexWeight(VertexName vertex, TWeight weight)
		{
			GetVertex(vertex).ChangeWeight(weight);
			return this;
		}

		public virtual WeightedOrientedGraph<TVertex, TEdge, TWeight> ChangeEdgeWeight(VertexName vertexOut, VertexName vertexIn, TWeight weight)
		{
			GetEdge(vertexOut, vertexIn).ChangeWeight(weight);
			return this;
		}

		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> Clear() => (WeightedOrientedGraph<TVertex, TEdge, TWeight>)base.Clear();

		public static new WeightedOrientedGraph<TVertex, TEdge, TWeight> Create() => new WeightedOrientedGraph<TVertex, TEdge, TWeight>();
		public static new WeightedOrientedGraph<TVertex, TEdge, TWeight> Create(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges)
		{
			var graph = Create();
			graph.AddVertices(vertices);
			graph.AddEdges(edges);
			return graph;
		}

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ChangeVertexWeight(VertexName vertex, TWeight weight) 
			=> ChangeVertexWeight(vertex, weight);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ChangeEdgeWeight(VertexName vertexOut, VertexName vertexIn, TWeight weight) 
			=> ChangeEdgeWeight(vertexOut, vertexIn, weight);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.Clear() => Clear();

		//static IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.Create() => Create();
		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.Create() => Create();
		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.Create(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges) => Create(vertices, edges);
	}
}
