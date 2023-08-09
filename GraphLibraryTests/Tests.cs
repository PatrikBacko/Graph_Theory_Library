using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using GraphLibrary.Edges;
using System.Numerics;
using GraphLibrary.Vertices;


namespace GraphLibraryTests
{
	[TestClass]
	public class SerializationTests
	{
		[TestMethod]
		public void SerializeTest1()
		{
			//var graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create()
			//	.AddVertices(new List<OrientedVertex> { new OrientedVertex("0"), new OrientedVertex("1"), new OrientedVertex("2") })
			//	.AddEdges(new List<OrientedEdge> { new OrientedEdge("0", "1"), new OrientedEdge("2", "1") });

			//var graph2 = (WeightedOrientedGraph<WeightedOrientedVertex<int>, WeightedOrientedEdge<int>, int>) TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph<int>(5, () => 1, () => 2);

			//JsonSerializerOptions options = new JsonSerializerOptions
			//{
			//	WriteIndented = true,
			//	IncludeFields = true,
			//	Converters = { new OrientedGraphConverter<OrientedVertex, OrientedEdge>(), new VertexNameConverter() }
			//};

			//JsonSerializerOptions options2 = new JsonSerializerOptions
			//{
			//	WriteIndented = true,
			//	IncludeFields = true,
			//	Converters = { new WeightedOrientedGraphConverter<WeightedOrientedVertex<int>, WeightedOrientedEdge<int>, int>(), new VertexNameConverter() }
			//};

			//var str = JsonSerializer.Serialize(graph, options);
			//var graphDeserialized = JsonSerializer.Deserialize<OrientedGraph<OrientedVertex, OrientedEdge>>(str, options);

			//var str2 = JsonSerializer.Serialize(graph2, options2);
			//var graphDeserialized2 = JsonSerializer.Deserialize<WeightedOrientedGraph<WeightedOrientedVertex<int>, WeightedOrientedEdge<int>, int>>(str2, options2);

			//int i = 42;
		}
	}

	
}
