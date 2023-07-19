using GraphLibrary.Vertices;
using GraphLibrary.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GraphLibrary.Graphs
{
	internal class WeightedOrientedGraph<TVertex, TEdge, TWeight> : OrientedGraph<TVertex, TEdge>, IWeightedOrientedGraph<TVertex, TEdge, TWeight>
		where TVertex : WeightedOrientedVertex<TWeight>
		where TEdge : WeightedOrientedEdge<TWeight>
		where TWeight : INumber<TWeight>
	{

		public WeightedOrientedGraph() : base() { }

		public TWeight GetWeight(VertexName vertex) => GetVertex(vertex).Weight;
		public TWeight GetWeight(VertexName vertexOut, VertexName vertexIn) => GetEdge(vertexOut, vertexIn).Weight;

		public WeightedOrientedGraph<TVertex, TEdge, TWeight> ChangeVertexWeight(VertexName vertex, TWeight weight)
		{
			GetVertex(vertex).ChangeWeight(weight);
			return this;
		}

		public WeightedOrientedGraph<TVertex, TEdge, TWeight> ChangeEdgeWeight(VertexName vertexOut, VertexName vertexIn, TWeight weight)
		{
			GetEdge(vertexOut, vertexIn).ChangeWeight(weight);
			return this;
		}

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ChangeVertexWeight(VertexName vertex, TWeight weight) 
			=> ChangeVertexWeight(vertex, weight);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ChangeEdgeWeight(VertexName vertexOut, VertexName vertexIn, TWeight weight) 
			=> ChangeEdgeWeight(vertexOut, vertexIn, weight);

	}
}
