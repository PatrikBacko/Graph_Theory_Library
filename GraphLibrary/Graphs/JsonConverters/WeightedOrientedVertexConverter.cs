using GraphLibrary.MaybeLater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Numerics;
using GraphLibrary.Vertices;

namespace GraphLibrary.Graphs.JsonConverters
{
	public class WeightedOrientedVertexConverter<TWeight> : JsonConverter<WeightedOrientedVertex<TWeight>>
		where TWeight : INumber<TWeight>
	{
		public override void Write(Utf8JsonWriter writer, WeightedOrientedVertex<TWeight> value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("Name");
			JsonSerializer.Serialize(writer, value.Name, options);
			writer.WritePropertyName("Weight");
			JsonSerializer.Serialize(writer, value.Weight, options);
			writer.WriteEndObject();
		}
		public override WeightedOrientedVertex<TWeight>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			reader.Read();
			var name = JsonSerializer.Deserialize<VertexName>(ref reader, options);
			reader.Read();
			var weight = JsonSerializer.Deserialize<TWeight>(ref reader, options);
			reader.Read();

			return new WeightedOrientedVertex<TWeight>(name, weight!);
		}
	}
}
