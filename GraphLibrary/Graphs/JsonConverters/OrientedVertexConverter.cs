using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using GraphLibrary.Vertices;

namespace GraphLibrary.Graphs.JsonConverters
{
	public class OrientedVertexConverter : JsonConverter<OrientedVertex>
	{
		public override OrientedVertex? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			reader.Read();
			var name = JsonSerializer.Deserialize<VertexName>(ref reader, options);
			reader.Read();

			return new OrientedVertex(name);
		}

		public override void Write(Utf8JsonWriter writer, OrientedVertex value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("Name");
			JsonSerializer.Serialize(writer, value.Name, options);
			writer.WriteEndObject();
		}
	}
}
