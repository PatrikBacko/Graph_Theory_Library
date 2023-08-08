using GraphLibrary.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GraphLibraryTests.TestGraphs
{
	public class TestWeightedOrientedGraphs
	{

		static public IWeightedOrientedGraph<WeightedOrientedVertex<int>, WeightedOrientedEdge<int>, int> ClearTestWeightedOrientedGraph
			=> WeightedOrientedGraph<WeightedOrientedVertex<int>, WeightedOrientedEdge<int>, int>.Create();

		static public IWeightedOrientedGraph<WeightedOrientedVertex<TWeight>, WeightedOrientedEdge<TWeight>, TWeight> GetVertexTestWeightedOrientedGraph<TWeight>
		(int vertexCount, Func<TWeight> vertexWeightFunction)
			where TWeight : INumber<TWeight>
		{
			var graph = WeightedOrientedGraph<WeightedOrientedVertex<TWeight>, WeightedOrientedEdge<TWeight>, TWeight>.Create();

			for (int i = 0; i < vertexCount; i++)
			{
				graph.AddVertex(new WeightedOrientedVertex<TWeight>(i.ToString(), vertexWeightFunction()));
			}

			return graph;
		}

		static public IWeightedOrientedGraph<WeightedOrientedVertex<TWeight>, WeightedOrientedEdge<TWeight>, TWeight> GetCompleteTestWeightedOrientedGraph<TWeight>
		(int vertexCount, Func<TWeight> vertexWeightFunction, Func<TWeight> edgeWeightFunction)
			where TWeight : INumber<TWeight>
		{
			var graph = WeightedOrientedGraph<WeightedOrientedVertex<TWeight>, WeightedOrientedEdge<TWeight>, TWeight>.Create();

			for (int i = 0; i < vertexCount; i++)
			{
				graph.AddVertex(new WeightedOrientedVertex<TWeight>(i.ToString(), vertexWeightFunction()));
			}

			for (int i = 0; i < vertexCount; i++)
			{
				for (int j = 0; j < vertexCount; j++)
				{
					if (i != j)
					{
						graph.AddEdge(new WeightedOrientedEdge<TWeight>(i.ToString(), j.ToString(), edgeWeightFunction()));
					}
				}
			}

			return graph;
		}

		static public IWeightedOrientedGraph<WeightedOrientedVertex<TWeight>, WeightedOrientedEdge<TWeight>, TWeight> GetPathTestWeightedOrientedGraph<TWeight>
		(int vertexCount, Func<TWeight> vertexWeightFunction, Func<TWeight> edgeWeightFunction)
			where TWeight : INumber<TWeight>
		{
			var graph = WeightedOrientedGraph<WeightedOrientedVertex<TWeight>, WeightedOrientedEdge<TWeight>, TWeight>.Create();

			for (int i = 0; i < vertexCount; i++)
			{
				graph.AddVertex(new WeightedOrientedVertex<TWeight>(i.ToString(), vertexWeightFunction()));
			}

			for (int i = 0; i < vertexCount - 1; i++)
			{
				graph.AddEdge(new WeightedOrientedEdge<TWeight>(i.ToString(), (i + 1).ToString(), edgeWeightFunction()));
			}

			return graph;
		}
	}
}
