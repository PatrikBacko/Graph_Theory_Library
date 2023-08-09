using GraphLibrary.Edges;
using GraphLibrary.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace GraphLibrary.Graphs.JsonConverters
{
	public class OrientedGraphConverter<TVertex, TEdge> : JsonConverter<OrientedGraph<TVertex, TEdge>>
		where TVertex : OrientedVertex
		where TEdge : OrientedEdge
	{
		public override void Write(Utf8JsonWriter writer, OrientedGraph<TVertex, TEdge> value, JsonSerializerOptions options)
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
		public override OrientedGraph<TVertex, TEdge>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			reader.Read();
			var vertices = JsonSerializer.Deserialize<List<TVertex>>(ref reader, options);
			reader.Read();
			var edges = JsonSerializer.Deserialize<List<TEdge>>(ref reader, options);
			reader.Read();

			return OrientedGraph<TVertex, TEdge>.Create().AddVertices(vertices!).AddEdges(edges!);
		}
	}
}
