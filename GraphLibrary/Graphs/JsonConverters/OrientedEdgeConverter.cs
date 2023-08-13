using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using GraphLibrary.Vertices;
using GraphLibrary.Edges;


namespace GraphLibrary.Graphs.JsonConverters
{
	public class OrientedEdgeConverter : JsonConverter<OrientedEdge>
	{
		public override OrientedEdge? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			reader.Read();
			var VertexOut = JsonSerializer.Deserialize<VertexName>(ref reader, options);
			reader.Read();
			var VertexIn = JsonSerializer.Deserialize<VertexName>(ref reader, options);
			reader.Read();

			return new OrientedEdge(VertexOut, VertexIn);
		}

		public override void Write(Utf8JsonWriter writer, OrientedEdge value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("VertexOut");
			JsonSerializer.Serialize(writer, value.VertexOut, options);
			writer.WritePropertyName("VertexIn");
			JsonSerializer.Serialize(writer, value.VertexIn, options);
			writer.WriteEndObject();
		}
	}
}
