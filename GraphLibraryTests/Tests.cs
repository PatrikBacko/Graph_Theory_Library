using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using GraphLibrary.Edges;


namespace GraphLibraryTests
{
	[TestClass]
	public class SerializationTests
	{
		[TestMethod]
		public void SerializeTest1()
		{
			var graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create()
				.AddVertices(new List<OrientedVertex> { new OrientedVertex("0"), new OrientedVertex("1"), new OrientedVertex("2") })
				.AddEdges(new List<OrientedEdge> { new OrientedEdge("0", "1"), new OrientedEdge("2", "1") });

			var graph2 = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph<int>(5, () =>1, () => 2);

			JsonSerializerOptions options = new JsonSerializerOptions
			{
				WriteIndented = true,
				IncludeFields = true,
				Converters = { new GraphConverter<OrientedGraph<OrientedVertex, OrientedEdge>, OrientedVertex, OrientedEdge>(), new VertexNameConverter() }
				//Converters = { new VertexNameConverter(), new DictionaryVertexNameOrientedVertexConverter() }
			};

			var str = JsonSerializer.Serialize(graph, options);
			var graphDeserialized = JsonSerializer.Deserialize<OrientedGraph<OrientedVertex, OrientedEdge>>(str, options);

			var str2 = JsonSerializer.Serialize(graph2, options);
			var graphDeserialized2 = JsonSerializer.Deserialize<OrientedGraph<OrientedVertex, OrientedEdge>>(str2, options);

			int i = 6;
		}
	}

	public class GraphConverter<TGraph, TVertex, TEdge> : JsonConverter<TGraph>
		where TGraph : IOrientedGraph<TVertex, TEdge>
		where TVertex : OrientedVertex
		where TEdge : OrientedEdge
	{
		public override void Write(Utf8JsonWriter writer, TGraph value, JsonSerializerOptions options)
		{
			var vertices = value.GetVertices().ToList();
			var edges = value.GetEdges().ToList();

			writer.WriteStartObject();
			writer.WritePropertyName("Vertices");
			JsonSerializer.Serialize(writer, vertices, options);
			writer.WritePropertyName("Edges");
			JsonSerializer.Serialize(writer, edges, options);
			writer.WriteEndObject();
		}
		public override TGraph? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			reader.Read();
			var vertices = JsonSerializer.Deserialize<List<TVertex>>(ref reader, options);
			reader.Read();
			var edges = JsonSerializer.Deserialize<List<TEdge>>(ref reader, options);
			reader.Read();
			return (TGraph) TGraph.Create().AddVertices(vertices).AddEdges(edges);
		}

		//public override bool CanConvert(Type typeToConvert)
		//{
		//	return base.CanConvert(typeToConvert);
		//}
	}
	public class VertexNameConverter : JsonConverter<VertexName>
	{
		public override VertexName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return new VertexName(reader.GetString());
		}

		public override void Write(Utf8JsonWriter writer, VertexName value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString());
		}
	}

	//public class DictionaryVertexNameOrientedVertexConverter : JsonConverter<Dictionary<VertexName, OrientedVertex>>
	//{
	//	public override Dictionary<VertexName, OrientedVertex> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	//	{
	//		var dict = new Dictionary<VertexName, OrientedVertex>();
	//		reader.Read();
	//		while (reader.TokenType != JsonTokenType.EndObject)
	//		{
	//			var key = reader.GetString();
	//			reader.Read();
	//			var value = JsonSerializer.Deserialize<OrientedVertex>(ref reader, options);
	//			dict.Add(new VertexName(key), value);
	//			reader.Read();
	//		}
	//		return dict;
	//	}

	//	public override void Write(Utf8JsonWriter writer, Dictionary<VertexName, OrientedVertex> value, JsonSerializerOptions options)
	//	{
	//		writer.WriteStartObject();
	//		foreach (var item in value)
	//		{
	//			writer.WritePropertyName(item.Key.ToString());
	//			JsonSerializer.Serialize(writer, item.Value, options);
	//		}
	//		writer.WriteEndObject();
	//	}
	//}

}
