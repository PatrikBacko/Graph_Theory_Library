using GraphLibrary.Vertices;
using GraphLibrary.Edges;
using GraphLibrary.Graphs.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Data;
using System.Diagnostics;
using GraphLibrary.Graphs.JsonConverters;
using System.Text.Json;
using GraphLibrary.Graphs.Exceptions;

namespace GraphLibrary.Graphs
{
	public class WeightedOrientedGraph<TVertex, TEdge, TWeight> : OrientedGraph<TVertex, TEdge>, IWeightedOrientedGraph<TVertex, TEdge, TWeight>
		where TVertex : WeightedOrientedVertex<TWeight>
		where TEdge : WeightedOrientedEdge<TWeight>
		where TWeight : INumber<TWeight>
	{

		public WeightedOrientedGraph() : base() { }

		#region GetWeightMethods
		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.GetWeight(VertexName)"/>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with given name doesn't exist in graph
		/// </exception>
		public virtual TWeight GetWeight(VertexName vertex) 
			=> GetVertex(vertex).Weight;

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.GetWeight(VertexName, VertexName)"/>
		/// <exception cref="EdgeException">
		/// Edge with given vertices doesn't exist in graph
		/// or one of the vertices doesn't exist in graph
		/// </exception>
		public virtual TWeight GetWeight(VertexName vertexOut, VertexName vertexIn) 
			=> GetEdge(vertexOut, vertexIn).Weight;
		#endregion

		#region ChangeWeightMethods
		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.ChangeVertexWeight(VertexName, TWeight)"/>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with given name doesn't exist in graph
		/// </exception>
		public virtual WeightedOrientedGraph<TVertex, TEdge, TWeight> ChangeVertexWeight(VertexName vertex, TWeight weight)
		{
			GetVertex(vertex).ChangeWeight(weight);
			return this;
		}

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.ChangeEdgeWeight(VertexName, VertexName, TWeight)"/>
		/// <exception cref="EdgeException">
		/// Exception thrown when edge with given vertices doesn't exist in graph
		/// or one of the vertices doesn't exist in graph
		/// </exception>
		public virtual WeightedOrientedGraph<TVertex, TEdge, TWeight> ChangeEdgeWeight(VertexName vertexOut, VertexName vertexIn, TWeight weight)
		{
			GetEdge(vertexOut, vertexIn).ChangeWeight(weight);
			return this;
		}
		#endregion

		#region CoverMethodsOfIOrientedGraphMethods
		// Cover methods of IOrientedGraph methods because of the fluent syntax

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.AddVertex(TVertex)"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.AddVertex(TVertex)"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> AddVertex(TVertex vertex)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.AddVertex(vertex);
		
		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.AddVertices(IEnumerable{TVertex})"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.AddVertices(IEnumerable{TVertex})"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> AddVertices(IEnumerable<TVertex> vertices)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.AddVertices(vertices);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.AddEdge(TEdge)"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.AddEdge(TEdge)"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> AddEdge(TEdge edge)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.AddEdge(edge);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.AddEdges(IEnumerable{TEdge})"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.AddEdges(IEnumerable{TEdge})"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> AddEdges(IEnumerable<TEdge> edges)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.AddEdges(edges);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.RemoveVertex(VertexName)"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.RemoveVertex(VertexName)"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertex(VertexName vertex)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveVertex(vertex);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.RemoveVertex(TVertex)"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.RemoveVertex(TVertex)"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertex(TVertex vertex)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveVertex(vertex);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.RemoveVertices(IEnumerable{VertexName})"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.RemoveVertices(IEnumerable{VertexName})"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertices(IEnumerable<VertexName> vertices)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveVertices(vertices);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.RemoveVertices(IEnumerable{TVertex})"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.RemoveVertices(IEnumerable{TVertex})"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertices(IEnumerable<TVertex> vertices)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveVertices(vertices);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.RemoveVerticesWith(VertexPredicate{TVertex})"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.RemoveVerticesWith(VertexPredicate{TVertex})"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVerticesWith(VertexPredicate<TVertex> vertexPredicate)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveVerticesWith(vertexPredicate);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.RemoveEdge(TEdge)"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.RemoveEdge(TEdge)"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdge(TEdge edge)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveEdge(edge);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.RemoveEdge(VertexName, VertexName)"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.RemoveEdge(VertexName, VertexName)"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdge(VertexName vertexOut, VertexName vertexIn)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveEdge(vertexOut, vertexIn);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.RemoveEdges(IEnumerable{TEdge})"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.RemoveEdges(IEnumerable{TEdge})"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdges(IEnumerable<TEdge> edges)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveEdges(edges);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.RemoveEdges(IEnumerable{ValueTuple{VertexName, VertexName})"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.RemoveEdges(IEnumerable{ValueTuple{VertexName, VertexName})"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdges(IEnumerable<(VertexName vertexOut, VertexName vertexIn)> edges)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveEdges(edges);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.RemoveEdgesWith(EdgePredicate{TEdge})"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.RemoveEdgesWith(EdgePredicate{TEdge})"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdgesWith(EdgePredicate<TEdge> edgePredicate)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveEdgesWith(edgePredicate);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.ApplyToVertices(VertexAction{TVertex})"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.ApplyToVertices(VertexAction{TVertex})"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToVertices(VertexAction<TVertex> vertexAction)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>)base.ApplyToVertices(vertexAction);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.ApplyToVerticesWith(VertexPredicate{TVertex}, VertexAction{TVertex})"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.ApplyToVerticesWith(VertexPredicate{TVertex}, VertexAction{TVertex})"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToVerticesWith
		(VertexPredicate<TVertex> vertexPredicate, VertexAction<TVertex> vertexAction)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.ApplyToVerticesWith(vertexPredicate, vertexAction);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.ApplyToEdges(EdgeAction{TEdge})"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.ApplyToEdges(EdgeAction{TEdge})"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToEdges(EdgeAction<TEdge> edgeAction)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.ApplyToEdges(edgeAction);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.ApplyToEdgesWith(EdgePredicate{TEdge}, EdgeAction{TEdge})"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.ApplyToEdgesWith(EdgePredicate{TEdge}, EdgeAction{TEdge})"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToEdgesWith
		(EdgePredicate<TEdge> edgePredicate, EdgeAction<TEdge> edgeAction)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.ApplyToEdgesWith(edgePredicate, edgeAction);

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.Clear()"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.Clear()"/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> Clear()
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>)base.Clear();

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.Create()"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.Create()"/>
		public static new WeightedOrientedGraph<TVertex, TEdge, TWeight> Create()
			=> new WeightedOrientedGraph<TVertex, TEdge, TWeight>();

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.Create(IEnumerable{TVertex}, IEnumerable{TEdge})"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.Create(IEnumerable{TVertex}, IEnumerable{TEdge})"/>
		public static new WeightedOrientedGraph<TVertex, TEdge, TWeight> Create(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges)
		{
			var graph = Create();
			graph.AddVertices(vertices);
			graph.AddEdges(edges);
			return graph;
		}
		#endregion

		#region JsonSaveAndSerializationMethods
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.SerializeToJson()"/>
		public override string SerializeToJson()
			=> SerializeToJson(new JsonSerializerOptions()
				{ 
					Converters = { 
						new WeightedOrientedGraphConverter<TVertex, TEdge, TWeight>(),
						new WeightedOrientedVertexConverter<TWeight>(),
						new WeightedOrientedEdgeConverter<TWeight>(),
						new VertexNameConverter() 
					} 
				}
			);

		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.SerializeToJson(JsonSerializerOptions)"/>
		public override string SerializeToJson(JsonSerializerOptions options)
		{
			try
			{
				return JsonSerializer.Serialize(this, options);
			}
			catch (NotSupportedException e)
			{
				throw new SerializationException("Serialization could not be done because of a problem with Graph", e);
			}

		}
		#endregion

		#region JsonLoadAndDeserializationMethods

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.LoadFromJson(string)"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.LoadFromJson(string)"/>
		public static new WeightedOrientedGraph<TVertex, TEdge, TWeight> LoadFromJson(string Path)
			=> DeserializeFromJson(File.ReadAllText(Path));

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.DeserializeFromJson(string)"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.DeserializeFromJson(string)"/>
		public new static WeightedOrientedGraph<TVertex, TEdge, TWeight> DeserializeFromJson(string jsonString)
			=> DeserializeFromJson(jsonString, new JsonSerializerOptions()
				{
					Converters =
						{
							new WeightedOrientedGraphConverter<TVertex, TEdge, TWeight>(),
							new WeightedOrientedVertexConverter<TWeight>(),
							new WeightedOrientedEdgeConverter<TWeight>(),
							new VertexNameConverter()
						}
				});

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.DeserializeFromJson(string, JsonSerializerOptions)"/>
		/// <inheritdoc cref="OrientedGraph{TVertex, TEdge}.DeserializeFromJson(string, JsonSerializerOptions)"/>
		public new static WeightedOrientedGraph<TVertex, TEdge, TWeight> DeserializeFromJson(string jsonString, JsonSerializerOptions options)
		{
			try
			{
				return JsonSerializer.Deserialize<WeightedOrientedGraph<TVertex, TEdge, TWeight>>(jsonString, options)
					?? throw new DeserializationException("Deserialization could not be done because null was returned");
			}
			catch (Exception e)
			{
				if (e is JsonException || e is ArgumentNullException || e is NotSupportedException)
					throw new DeserializationException("Deserialization could not be done, check inner exception for more details", e);
				throw;
			}
		}
		#endregion

		#region Operators
		/// <summary>
		/// Overriden operator + <br />
		/// Functions same as <see cref="AddVertex(TVertex)"/> <br />
		/// </summary>
		/// <param name="graph">
		/// graph to which vertex is added
		/// </param>
		/// <param name="vertex">
		/// vertex to be added to the graph
		/// </param>
		/// <returns>
		/// Itself after adding vertex
		/// </returns>
		public static WeightedOrientedGraph<TVertex, TEdge, TWeight> operator +(WeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TVertex vertex)
			=> graph.AddVertex(vertex);

		/// <summary>
		/// Overriden operator + <br />
		/// Functions same as <see cref="AddEdge(TEdge)"/> <br />
		/// </summary>
		/// <param name="graph">
		/// graph to which edge is added
		/// </param>
		/// <param name="edge">
		/// edge to be added to the graph
		/// </param>
		/// <returns>
		/// Itself after adding edge
		/// </returns>
		public static WeightedOrientedGraph<TVertex, TEdge, TWeight> operator +(WeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TEdge edge)
			=> graph.AddEdge(edge);

		/// <summary>
		/// Overriden operator - <br />
		/// Functions same as <see cref="RemoveVertex(TVertex)"/> <br />
		/// </summary>
		/// <param name="graph">
		/// graph from which vertex is removed
		/// </param>
		/// <param name="vertex">
		/// vertex to be removed from the graph
		/// </param>
		/// <returns>
		/// Itself after removing vertex
		/// </returns>
		public static WeightedOrientedGraph<TVertex, TEdge, TWeight> operator -(WeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TVertex vertex)
			=> graph.RemoveVertex(vertex);

		/// <summary>
		/// Overriden operator - <br />
		/// Functions same as <see cref="RemoveEdge(TEdge)"/> <br />
		/// </summary>
		/// <param name="graph">
		/// graph from which edge is removed
		/// </param>
		/// <param name="edge">
		/// edge to be removed from the graph
		/// </param>
		/// <returns>
		/// Itself after removing edge
		/// </returns>
		public static WeightedOrientedGraph<TVertex, TEdge, TWeight> operator -(WeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TEdge edge)
			=> graph.RemoveEdge(edge);
		#endregion

		#region InterfaceImplementations
		// IWeightedOrientedGraph implementations, because of fluent syntax
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ApplyToVertices(VertexAction<TVertex> vertexAction)
			=> ApplyToVertices(vertexAction);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ApplyToVerticesWith
		(VertexPredicate<TVertex> vertexPredicate, VertexAction<TVertex> vertexAction)
			=> ApplyToVerticesWith(vertexPredicate, vertexAction);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ApplyToEdges(EdgeAction<TEdge> edgeAction)
			=> ApplyToEdges(edgeAction);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ApplyToEdgesWith
		(EdgePredicate<TEdge> edgePredicate, EdgeAction<TEdge> edgeAction)
			=> ApplyToEdgesWith(edgePredicate, edgeAction);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.AddVertex(TVertex vertex)
			=> AddVertex(vertex);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.AddVertices(IEnumerable<TVertex> vertices)
			=> AddVertices(vertices);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.AddEdge(TEdge edge)
			=> AddEdge(edge);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.AddEdges(IEnumerable<TEdge> edges)
			=> AddEdges(edges);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveVertex(TVertex vertex)
			=> RemoveVertex(vertex);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveVertices(IEnumerable<TVertex> vertices)
			=> RemoveVertices(vertices);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveVertex(VertexName vertex)
			=> RemoveVertex(vertex);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveVertices(IEnumerable<VertexName> vertices)
			=> RemoveVertices(vertices);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveVerticesWith(VertexPredicate<TVertex> vertexPredicate)
			=> RemoveVerticesWith(vertexPredicate);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveEdge(TEdge edge)
			=> RemoveEdge(edge);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveEdges(IEnumerable<TEdge> edges)
			=> RemoveEdges(edges);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveEdge(VertexName vertexOut, VertexName vertexIn)
			=> RemoveEdge(vertexOut, vertexIn);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveEdges(IEnumerable<(VertexName vertexOut, VertexName vertexIn)> edges)
			=> RemoveEdges(edges);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveEdgesWith(EdgePredicate<TEdge> edgePredicate)
			=> RemoveEdgesWith(edgePredicate);
		
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ChangeVertexWeight(VertexName vertex, TWeight weight) 
			=> ChangeVertexWeight(vertex, weight);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ChangeEdgeWeight(VertexName vertexOut, VertexName vertexIn, TWeight weight) 
			=> ChangeEdgeWeight(vertexOut, vertexIn, weight);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.Clear() => Clear();

		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.Create() => Create();
		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.Create(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges) => Create(vertices, edges);

		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.LoadFromJson(string path)
			=> LoadFromJson(path);
		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.DeserializeFromJson(string jsonString)
			=> DeserializeFromJson(jsonString);
		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.DeserializeFromJson(string jsonString, JsonSerializerOptions options)
			=> DeserializeFromJson(jsonString, options);
		#endregion
	}
}
