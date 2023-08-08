using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Edges;
using GraphLibrary.Vertices;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Data;

namespace GraphLibrary.Graphs
{
	public interface IWeightedOrientedGraph<TVertex, TEdge,  TWeight> : IOrientedGraph<TVertex, TEdge> 
		where TVertex : WeightedOrientedVertex<TWeight> 
		where TEdge : WeightedOrientedEdge<TWeight> 
		where TWeight : INumber<TWeight>
	{
		TWeight GetWeight(VertexName vertex);
		TWeight GetWeight(VertexName vertexOut, VertexName vertexIn);

		new IWeightedOrientedGraph<TVertex, TEdge, TWeight> Clear();
		new static abstract IWeightedOrientedGraph<TVertex, TEdge, TWeight> Create();
		new static abstract IWeightedOrientedGraph<TVertex, TEdge, TWeight> Create(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> ChangeVertexWeight(VertexName vertex, TWeight weight);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> ChangeEdgeWeight(VertexName vertexOut, VertexName vertexIn, TWeight weight);
	}
}
