using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using GraphLibrary.Vertices;
using GraphLibrary.Edges;
using GraphLibrary.Graphs;


namespace GraphLibrary
{
	public static class Serialization
	{
		public static string Serialize<TOrientedGraph, TVertex, TEdge>(this TOrientedGraph graph)
			where TOrientedGraph : IOrientedGraph<TVertex, TEdge>
			where TVertex : OrientedVertex
			where TEdge : OrientedEdge
		{
			var options = new JsonSerializerOptions
			{
				ReferenceHandler = ReferenceHandler.Preserve,
				WriteIndented = true,
				IncludeFields = true
			};

			return JsonSerializer.Serialize<TOrientedGraph>(graph, options);
		}
	}
}
