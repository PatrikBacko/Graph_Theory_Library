using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Numerics;
using GraphLibrary.Edges;
using GraphLibrary.Vertices;

namespace GraphLibrary.Graphs.JsonConverters
{
	public class WeightedOrientedEdgeConverter<TWeight> : JsonConverter<WeightedOrientedEdge<TWeight>>
		where TWeight : INumber<TWeight>
	{
		public override WeightedOrientedEdge<TWeight>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			reader.Read();
			var VertexOut = JsonSerializer.Deserialize<VertexName>(ref reader, options);
			reader.Read();
			var VertexIn = JsonSerializer.Deserialize<VertexName>(ref reader, options);
			reader.Read();
			var weight = JsonSerializer.Deserialize<TWeight>(ref reader, options);
			reader.Read();

			return new WeightedOrientedEdge<TWeight>(VertexOut, VertexIn, weight!);
		}

		public override void Write(Utf8JsonWriter writer, WeightedOrientedEdge<TWeight> value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("From");
			JsonSerializer.Serialize(writer, value.VertexOut, options);
			writer.WritePropertyName("To");
			JsonSerializer.Serialize(writer, value.VertexIn, options);
			writer.WritePropertyName("Weight");
			JsonSerializer.Serialize(writer, value.Weight, options);
			writer.WriteEndObject();
		}
	}
}
