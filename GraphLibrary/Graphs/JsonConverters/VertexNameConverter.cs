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
	public class VertexNameConverter : JsonConverter<VertexName>
	{
		public override VertexName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return new VertexName(reader.GetString()!);
		}

		public override void Write(Utf8JsonWriter writer, VertexName value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString());
		}
	}
}
