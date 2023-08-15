using GraphLibrary.Edges;
using GraphLibrary.Vertices;
using GraphLibrary.Graphs.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Graphs.Delegates;
using System.Text.Json;
using GraphLibrary.Graphs.JsonConverters;
using System.Xml.Linq;

namespace GraphLibrary.Graphs
{
	/// <summary>
	/// Oriented graph implementation by list of neighbors. (implented by dicts of dicts)
	/// </summary>
	/// <typeparam name="TVertex">
	/// Type of vertices in the graph. Must be derived from <see cref="OrientedVertex"/>
	/// </typeparam>
	/// <typeparam name="TEdge">
	/// Type of edges in the graph. Must be derived from <see cref="OrientedEdge"/>
	/// </typeparam>
	public class OrientedGraph<TVertex, TEdge> : IOrientedGraph<TVertex, TEdge>
		where TVertex : OrientedVertex
		where TEdge : OrientedEdge
	{
		#region ProtectedFields
		/// <summary>
		/// Dictionary of vertices in the graph.
		/// </summary>
		protected Dictionary<VertexName, TVertex> _vertices;
		/// <summary>
		/// list of neighbors implemented as dictionary of dictionaries.
		/// </summary>
		protected Dictionary<VertexName, Dictionary<VertexName, TEdge>> _neighbors;
		protected int _edgeCount;
		#endregion

		#region Constructors
		public OrientedGraph()
		{
			_vertices = new Dictionary<VertexName, TVertex>();
			_neighbors = new Dictionary<VertexName, Dictionary<VertexName, TEdge>>();
			_edgeCount = 0;
		}
		#endregion

		#region GetVertexMethods
		/// <inheritdoc/>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception>
		public virtual TVertex GetVertex(VertexName vertex)
		{
			ContainsVertex(vertex);
			return _vertices[vertex];
		}
		#endregion

		#region GetEdgeMethods
		/// <inheritdoc/>
		/// <exception cref="EdgeException">
		/// Exception thrown when edge with the given vertices does not exist in the graph, 
		/// or when vertices do not exist in the graph
		/// </exception>
		public virtual TEdge GetEdge(VertexName vertexOut, VertexName vertexIn)
		{
			try
			{
				ContainsEdge(vertexOut, vertexIn);
				return _neighbors[vertexOut][vertexIn];
			}
			catch (VertexException e)
			{
				throw new EdgeException("One or both vertices are not in the graph", e);
			}
		}
		#endregion

		#region IsVertexMethods
		/// <inheritdoc/>
		public virtual bool IsVertex(VertexName vertex)
		{
			if (_vertices.ContainsKey(vertex))
				return true;
			return false;
		}
		/// <inheritdoc/>
		public virtual bool IsVertex(TVertex vertex)
		{
			if (!IsVertex(vertex.Name))
				return false;

			var v = _vertices[vertex.Name];
			if (v == vertex)
				return true;
			return false;
		}
		#endregion

		#region IsEdgeMethods
		/// <inheritdoc/>
		public virtual bool IsEdge(VertexName vertexOut, VertexName vertexIn)
		{
			ContainsVertex(vertexOut);
			ContainsVertex(vertexIn);

			if (_neighbors[vertexOut].ContainsKey(vertexIn))
				return true;
			return false;
		}
		/// <inheritdoc/>
		public virtual bool IsEdge(TEdge edge)
		{
			if (!IsEdge(edge.VertexOut, edge.VertexIn))
				return false;

			var e = _neighbors[edge.VertexOut][edge.VertexIn];
			if (e == edge)
				return true;
			return false;
		}
		#endregion

		#region GetVerticesMethods
		/// <inheritdoc/>
		/// <remarks>
		/// Time complexity: O(1)
		/// </remarks>
		public virtual IEnumerable<TVertex> GetVertices() 
			=> _vertices.Values;
		/// <inheritdoc/>
		/// <remarks>
		/// Time complexity: O(number of vertices)
		/// </remarks>
		public virtual IEnumerable<TVertex> GetVerticesWith(VertexPredicate<TVertex> vertexPredicate)
		{
			List<TVertex> returnVertices = new List<TVertex>();
			foreach (var vertex in _vertices.Values)
				if (vertexPredicate(vertex))
					returnVertices.Add(vertex);
			return returnVertices;
		}
		#endregion

		#region GetEdgesMethods
		/// <inheritdoc/>
		/// <remarks>
		/// Time complexity: O(number of edges)
		/// </remarks>
		public virtual IEnumerable<TEdge> GetEdges()
		{
			List<TEdge> edges = new List<TEdge>();
			foreach (var vertex in _vertices.Values)
				foreach (var edge in _neighbors[vertex.Name].Values)
					edges.Add(edge);
			return edges;
		}
		/// <inheritdoc/>
		/// <remarks>
		/// Time complexity: O(number of edges)
		/// </remarks>
		public virtual IEnumerable<TEdge> GetEdgesWith(EdgePredicate<TEdge> edgePredicate)
		{
			List<TEdge> returnEdges = new List<TEdge>();
			foreach (var edge in GetEdges())
				if (edgePredicate(edge))
					returnEdges.Add(edge);
			return returnEdges;
		}
		#endregion

		#region GetAdjacentVerticesMethods
		/// <inheritdoc/>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception>
		/// <remarks>
		/// Time complexity: O(number of edges)
		/// </remarks>
		public virtual IEnumerable<TVertex> GetInAdjacentVertices(VertexName vertex)
		{
			ContainsVertex(vertex);
			List<TVertex> adjVerticesIn = new List<TVertex>();
			foreach (var v in GetVertices())
				if (IsEdge(v.Name, vertex))
					adjVerticesIn.Add(v);
			return adjVerticesIn;
		}
		/// <inheritdoc/>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception
		/// <remarks>
		/// Time complexity: O(out Degree of given vertex)
		/// </remarks>
		public virtual IEnumerable<TVertex> GetOutAdjacentVertices(VertexName vertex)
		{
			ContainsVertex(vertex);
			List<TVertex> adjVerticesOut = new List<TVertex>();
			foreach (var e in _neighbors[vertex].Values)
				adjVerticesOut.Add(GetVertex(e.VertexIn));
			return adjVerticesOut;
		}
		#endregion

		#region GetAdjacentEdgesMethods
		/// <inheritdoc/>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception>
		/// <remarks>
		/// Time complexity: O(number of edges)
		/// </remarks>
		public virtual IEnumerable<TEdge> GetInEdges(VertexName vertex)
		{
			ContainsVertex(vertex);
			List<TEdge> edgesIn = new List<TEdge>();
			foreach (var v in GetVertices())
				if (IsEdge(v.Name, vertex))
					edgesIn.Add(GetEdge(v.Name, vertex));
			return edgesIn;
		}
		/// <inheritdoc/>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception>
		/// <remarks>
		/// Time complexity: O(out Degree of given vertex)
		/// </remarks>
		public virtual IEnumerable<TEdge> GetOutEdges(VertexName vertex)
		{
			ContainsVertex(vertex);
			List<TEdge> edgesOut = new List<TEdge>();
			foreach (var v in _neighbors[vertex].Values)
				edgesOut.Add(v);
			return edgesOut;
		}
		#endregion

		#region GetCountMethods
		/// <inheritdoc/>
		/// <remarks>
		/// Time complexity: O(1)
		/// </remarks>
		public virtual int GetVertexCount() => _vertices.Count;

		/// <inheritdoc/>
		/// <remarks>
		/// Time complexity: O(1)
		/// </remarks>
		public virtual int GetEdgeCount() => _edgeCount;
		#endregion

		#region GetDegreeMethods
		/// <inheritdoc/>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception>
		/// <remarks>
		/// Time complexity: O(1)
		/// </remarks>
		public virtual int GetInDegree(VertexName vertexName) {
			ContainsVertex(vertexName);
			var vertex = GetVertex(vertexName);
			return vertex.DegreeIn;
		}
		/// <inheritdoc/>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception>
		/// <remarks>
		/// Time complexity: O(1)
		/// </remarks>
		public virtual int GetOutDegree(VertexName vertexName) {
			ContainsVertex(vertexName);
			var vertex = GetVertex(vertexName);
			return vertex.DegreeOut;
		}
		#endregion

		#region AddVertexMethods
		/// <inheritdoc/>
		/// <exception cref="VertexException">
		/// Exception thrown :
		/// - when vertex with the same name already exists in the graph,
		/// - when instance of the given vertex belongs to a different graph,
		/// - when name of the given vertex is empty string
		/// </exception>
		/// <remarks>
		/// If instance of the given vertex belongs to a different graph, <see cref="EdgeException"/> will be thrown <br />
		/// </remarks>
		public virtual OrientedGraph<TVertex, TEdge> AddVertex(TVertex vertex)
		{
			ValidVertexName(vertex.Name);
			if (IsVertex(vertex.Name))
				throw new VertexException("Vertex with the same name already exists in Graph");
			if (vertex.IsInGraph)
				throw new VertexException("Vertex already belongs to a different Graph");
			vertex.IsInGraph = true;
			_vertices.Add(vertex.Name, vertex);
			_neighbors.Add(vertex.Name, new Dictionary<VertexName, TEdge>());
			return this;
		}
		/// <inheritdoc/>
		/// <exception cref="VertexException">
		/// Exception thrown :
		/// - when vertex with the same name already exists in the graph,
		/// - when instance of the given vertex belongs to a different graph,
		/// - when name of the given vertex is empty string
		/// </exception>
		public virtual OrientedGraph<TVertex, TEdge> AddVertices(IEnumerable<TVertex> vertices)
		{
			foreach (var vertex in vertices)
				AddVertex(vertex);
			return this;
		}
		#endregion

		#region AddEdgeMethods
		/// <inheritdoc/>
		/// <exception cref="EdgeException">
		/// Exception thrown :
		/// - whenedge with same end vertices already exists in the graph,
		/// - when instance of the given edge belongs to a different graph,
		/// - when one of end vertices of the given edge does not exist in the graph
		/// </exception>
		/// <remarks>
		/// If instance of the given edge belongs to a different graph, <see cref="EdgeException"/> will be thrown <br />
		/// </remarks>
		public virtual OrientedGraph<TVertex, TEdge> AddEdge(TEdge edge)
		{
			try
			{
				ValidVertexName(edge.VertexOut);
				ValidVertexName(edge.VertexIn);

				if (IsEdge(edge.VertexOut, edge.VertexIn))
					throw new EdgeException("Edge already exists in Graph");

				if (edge.VertexIn == edge.VertexOut)
					throw new EdgeException("Edge cannot be a loop");

				if (edge.IsInGraph)
					throw new EdgeException("This edge already belongs to a different Graph");
				edge.IsInGraph = true;

				_neighbors[edge.VertexOut].Add(edge.VertexIn, edge);
				_edgeCount++;

				_vertices[edge.VertexOut].DegreeOut++;
				_vertices[edge.VertexIn].DegreeIn++;
				return this;
			}
			catch (VertexException e)
			{
				throw new EdgeException("Edge cannot be added because of a problem with vertices", e);
			}
		}
		/// <inheritdoc/>
		/// <exception cref="EdgeException">
		/// Exception thrown :
		/// - whenedge with same end vertices already exists in the graph,
		/// - when instance of the given edge belongs to a different graph,
		/// - when one of end vertices of the given edge does not exist in the graph
		/// </exception>
		public virtual OrientedGraph<TVertex, TEdge> AddEdges(IEnumerable<TEdge> edges)
		{
			foreach (var edge in edges)
				AddEdge(edge);
			return this;
		}
		#endregion

		#region RemoveVertexMethods
		/// <inheritdoc/>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception>
		public virtual OrientedGraph<TVertex, TEdge> RemoveVertex(VertexName vertexName)
		{
			ContainsVertex(vertexName);

			foreach (var vertex in _vertices.Values)
			{
				if (IsEdge(vertex.Name, vertexName))
					RemoveEdge(vertexName, vertex.Name);
			}

			var vertexToRemove = GetVertex(vertexName);
			vertexToRemove.IsInGraph = false;
			vertexToRemove.DegreeIn = 0;
			vertexToRemove.DegreeOut = 0;

			_edgeCount -= _neighbors[vertexName].Count;
			_vertices.Remove(vertexName);
			_neighbors.Remove(vertexName);


			return this;
		}
		/// <inheritdoc/>
		/// <exception cref="VertexException">
		/// exception thrown when vertex does not exist in the graph
		/// </exception>
		public virtual OrientedGraph<TVertex, TEdge> RemoveVertex(TVertex vertex)
		{
			if (!IsVertex(vertex))
				throw new VertexException("Vertex doesn't exist in Graph");
			return RemoveVertex(vertex.Name);
		}
		/// <inheritdoc/>
		/// <exception cref="VertexException">
		/// Exception thrown when one of the vertices does not exist in the graph
		/// </exception>
		public virtual OrientedGraph<TVertex, TEdge> RemoveVertices(IEnumerable<TVertex> vertices)
		{
			foreach (var vertex in vertices)
				RemoveVertex(vertex);
			return this;
		}
		/// <inheritdoc/>
		/// <exception cref="VertexException">
		/// Exception thrown when one of the vertex names does not exist in the graph
		/// </exception>
		public virtual OrientedGraph<TVertex, TEdge> RemoveVertices(IEnumerable<VertexName> vertices)
		{
			foreach (var vertex in vertices)
				RemoveVertex(vertex);
			return this;
		}
		/// <inheritdoc/>
		public virtual OrientedGraph<TVertex, TEdge> RemoveVerticesWith(VertexPredicate<TVertex> vertexPredicate)
		{
			var vertices = GetVerticesWith(vertexPredicate);
			foreach (var vertex in vertices)
				RemoveVertex(vertex);
			return this;
		}
		#endregion

		#region RemoveEdgeMethods
		/// <inheritdoc/>
		/// <exception cref="EdgeException">
		/// Exception thrown when edge between given vertices does not exist in the graph
		/// or when one of the vertices does not exist in the graph
		/// </exception>
		public virtual OrientedGraph<TVertex, TEdge> RemoveEdge(VertexName vertexOut, VertexName vertexIn)
		{
			try
			{
				ContainsEdge(vertexOut, vertexIn);
				GetEdge(vertexOut, vertexIn).IsInGraph = false;

				_neighbors[vertexOut].Remove(vertexIn);
				_edgeCount--;
				_vertices[vertexOut].DegreeOut--;
				_vertices[vertexIn].DegreeIn--;

				return this;
			}
			catch (VertexException e)
			{
				throw new EdgeException("Edge cannot be removed because of a problem with vertices", e);
			}
		}
		/// <inheritdoc/>
		/// <exception cref="EdgeException">
		/// Exception thrown when edge does not exist in the graph,
		/// or one of the vertices of the edge does not exist in the graph
		/// </exception>
		public virtual OrientedGraph<TVertex, TEdge> RemoveEdge(TEdge edge)
		{
			if (!IsEdge(edge))
				throw new EdgeException("Edge doesn't exist in Graph");
			return RemoveEdge(edge.VertexOut, edge.VertexIn);
		}
		/// <inheritdoc/>
		/// <exception cref="EdgeException">
		/// Exception thrown same as in <see cref="RemoveEdge(TEdge)"/>
		/// </exception>
		public virtual OrientedGraph<TVertex, TEdge> RemoveEdges(IEnumerable<TEdge> edges)
		{
			foreach (var edge in edges)
				RemoveEdge(edge);
			return this;
		}
		/// <inheritdoc/>
		/// <exception cref="EdgeException">
		/// Exception thrown same as in <see cref="RemoveEdge(VertexName, VertexName)"/>
		/// </exception>
		public virtual OrientedGraph<TVertex, TEdge> RemoveEdges(IEnumerable<(VertexName vertexOut, VertexName vertexIn)> edges)
		{
			foreach (var (vertexIn, vertexOut) in edges)
				RemoveEdge(vertexOut, vertexIn);
			return this;
		}
		/// <inheritdoc/>
		public virtual OrientedGraph<TVertex, TEdge> RemoveEdgesWith(EdgePredicate<TEdge> edgePredicate)
		{
			var edges = GetEdgesWith(edgePredicate);
			foreach (var edge in edges)
				RemoveEdge(edge);
			return this;
		}
		#endregion

		#region CreateAndClearMethods
		/// <inheritdoc/>
		public virtual OrientedGraph<TVertex, TEdge> Clear()
		{
			_vertices.Clear();
			_neighbors.Clear();
			_edgeCount = 0;
			return this;
		}

		/// <inheritdoc/>
		public static OrientedGraph<TVertex, TEdge> Create() => new OrientedGraph<TVertex, TEdge>();
		/// <inheritdoc/>
		/// <exception cref="VertexException">
		/// Exception thrown:
		/// - when one of the vertices already exists in the graph
		/// - when one of the vertices has empty name
		/// - when one of the vertices already belongs to a different graph
		/// </exception>
		/// <exception cref="EdgeException">
		/// Exception thrown:
		/// - when one of the edges already exists in the graph
		/// - when one of vertices of some edge does not exist in the graph
		/// - when one of the edges already belongs to a different graph
		/// </exception>
		public static OrientedGraph<TVertex, TEdge> Create(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges)
		{
			var graph = new OrientedGraph<TVertex, TEdge>();
			graph.AddVertices(vertices);
			graph.AddEdges(edges);
			return graph;
		}
		#endregion

		#region ApplyToVerticesMethods
		/// <inheritdoc/>
		/// <remarks>
		/// Time complexity: O(number of vertices)
		/// </remarks>
		public virtual OrientedGraph<TVertex, TEdge> ApplyToVertices(VertexAction<TVertex> vertexAction)
		{
			var vertices = GetVertices();
			foreach (var vertex in vertices)
				vertexAction(vertex);
			return this;
		}
		/// <inheritdoc/>
		/// <remarks>
		/// Time complexity: O(number of vertices)
		/// </remarks>
		public virtual OrientedGraph<TVertex, TEdge> ApplyToVerticesWith(VertexPredicate<TVertex> vertexPredicate, VertexAction<TVertex> vertexAction)
		{
			var vertices = GetVerticesWith(vertexPredicate);
			foreach (var vertex in vertices)
				vertexAction(vertex);
			return this;
		}
		#endregion

		#region ApplyToEdgesMethods
		/// <inheritdoc/>
		/// <remarks>
		/// Time complexity: O(number of edges)
		/// </remarks>
		public virtual OrientedGraph<TVertex, TEdge> ApplyToEdges(EdgeAction<TEdge> edgeAction)
		{
			var edges = GetEdges();
			foreach (var edge in edges)
				edgeAction(edge);
			return this;
		}
		/// <inheritdoc/>
		/// <remarks>
		/// Time complexity: O(number of edges)
		/// </remarks>
		public virtual OrientedGraph<TVertex, TEdge> ApplyToEdgesWith(EdgePredicate<TEdge> edgePredicate, EdgeAction<TEdge> edgeAction)
		{
			var edges = GetEdgesWith(edgePredicate);
			foreach (var edge in edges)
				edgeAction(edge);
			return this;
		}
		#endregion

		#region JsonSaveAndSerializeMethods
		/// <inheritdoc/>
		/// <exception cref="SerializationException">
		/// Exception thrown when serialization fails
		/// </exception>
		public virtual void SaveToJson(string Path) 
			=> File.WriteAllText(Path, SerializeToJson());
		/// <inheritdoc/>
		/// <exception cref="SerializationException">
		/// Exception thrown when serialization fails
		/// </exception>
		public virtual string SerializeToJson()
			=> SerializeToJson(new JsonSerializerOptions()
			{
				Converters =
				{
					new OrientedGraphConverter<TVertex, TEdge>(),
					new OrientedVertexConverter(),
					new OrientedEdgeConverter(),
					new VertexNameConverter()
				}
			});
		/// <inheritdoc/>
		/// <exception cref="SerializationException">
		/// Exception thrown when serialization fails
		/// </exception>
		public virtual string SerializeToJson(JsonSerializerOptions options)
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

		#region JsonLoadAndDeserializeMethods
		/// <inheritdoc/>
		/// <exception cref="DeserializationException">
		/// exception thrown when there is a problem with deserialization
		/// </exception>
		public static OrientedGraph<TVertex, TEdge> LoadFromJson(string Path) 
			=> DeserializeFromJson(File.ReadAllText(Path));
		/// <inheritdoc/>
		/// <exception cref="DeserializationException">
		/// exception thrown when there is a problem with deserialization
		/// </exception>
		public static OrientedGraph<TVertex, TEdge> DeserializeFromJson(string jsonString)
			=> DeserializeFromJson(jsonString, new JsonSerializerOptions()
			{
				Converters =
				{
					new OrientedGraphConverter<TVertex, TEdge>(),
					new OrientedVertexConverter(),
					new OrientedEdgeConverter(),
					new VertexNameConverter()
				}
			});
		/// <inheritdoc/>
		/// <exception cref="DeserializationException">
		/// exception thrown when there is a problem with deserialization
		/// </exception>
		public static OrientedGraph<TVertex, TEdge> DeserializeFromJson(string jsonString, JsonSerializerOptions options)
		{
			try{
				return JsonSerializer.Deserialize<OrientedGraph<TVertex, TEdge>>(jsonString, options)
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
		public static OrientedGraph<TVertex, TEdge> operator +(OrientedGraph<TVertex, TEdge> graph, TVertex vertex) => graph.AddVertex(vertex);
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
		public static OrientedGraph<TVertex, TEdge> operator +(OrientedGraph<TVertex, TEdge> graph, TEdge edge) => graph.AddEdge(edge);

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
		public static OrientedGraph<TVertex, TEdge> operator -(OrientedGraph<TVertex, TEdge> graph, TVertex vertex) => graph.RemoveVertex(vertex);
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
		public static OrientedGraph<TVertex, TEdge> operator -(OrientedGraph<TVertex, TEdge> graph, TEdge edge) => graph.RemoveEdge(edge);
		#endregion

		#region ProtectedMethods
		/// <summary>
		/// Checks if vertex exists in graph, if not throws exception
		/// </summary>
		/// <param name="vertex"></param>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex doesn't exist in graph
		/// </exception>
		protected virtual void ContainsVertex(VertexName vertex)
		{
			if (!IsVertex(vertex))
				throw new VertexException("Vertex doesn't exist in Graph");
		}
		/// <summary>
		/// Checks if edge exists in graph, if not throws exception
		/// </summary>
		/// <param name="vertexOut"></param>
		/// <param name="vertexIn"></param>
		/// <exception cref="EdgeException">
		/// Exception thrown when edge doesn't exist in graph
		/// </exception>
		protected virtual void ContainsEdge(VertexName vertexOut, VertexName vertexIn)
		{
			if (!IsEdge(vertexOut, vertexIn))
				throw new EdgeException("Edge doesn't exist in Graph");
		}
		/// <summary>
		/// Checks if vertex name is valid, if not throws exception (empty string)
		/// </summary>
		/// <param name="vertex"></param>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex name is invalid
		/// </exception>
		protected virtual void ValidVertexName(VertexName vertex)
		{
			if (vertex.Value == "")
				throw new VertexException("Vertex name can't be empty string");
		}
		#endregion

		#region InterfaceImplementations
		// IOrientedGraph implementations, because of fluent syntax
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.ApplyToVertices(VertexAction<TVertex> vertexAction) 
			=> ApplyToVertices(vertexAction);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.ApplyToVerticesWith(VertexPredicate<TVertex> vertexPredicate, VertexAction<TVertex> vertexAction) 
			=> ApplyToVerticesWith(vertexPredicate, vertexAction);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.ApplyToEdges(EdgeAction<TEdge> edgeAction) 
			=>	ApplyToEdges(edgeAction);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.ApplyToEdgesWith(EdgePredicate<TEdge> edgePredicate, EdgeAction<TEdge> edgeAction) 
			=>	ApplyToEdgesWith(edgePredicate, edgeAction);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.AddVertex(TVertex vertex) 
			=> AddVertex(vertex);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.AddVertices(IEnumerable<TVertex> vertices) 
			=> AddVertices(vertices);

		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.AddEdge(TEdge edge) 
			=> AddEdge(edge);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.AddEdges(IEnumerable<TEdge> edges) 
			=> AddEdges(edges);

		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveVertex(VertexName vertex) 
			=> RemoveVertex(vertex);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveVertex(TVertex vertex) 
			=> RemoveVertex(vertex);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveVertices(IEnumerable<VertexName> vertices)
			=> RemoveVertices(vertices);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveVertices(IEnumerable<TVertex> vertices)
			=> RemoveVertices(vertices);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveVerticesWith(VertexPredicate<TVertex> vertexPredicate)
			=> RemoveVerticesWith(vertexPredicate);

		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveEdge(VertexName vertexOut, VertexName vertexIn) 
			=> RemoveEdge(vertexOut, vertexIn);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveEdge(TEdge edge) 
			=> RemoveEdge(edge);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveEdges(IEnumerable<TEdge> edges)
			=> RemoveEdges(edges);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveEdges(IEnumerable<(VertexName vertexOut, VertexName vertexIn)> edges)
			=> RemoveEdges(edges);
		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.RemoveEdgesWith(EdgePredicate<TEdge> edgePredicate)
			=> RemoveEdgesWith(edgePredicate);

		IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.Clear()
			=> Clear();
		static IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.Create() 
			=> Create();
		static IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.Create(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges) 
			=> Create(vertices, edges);

		static IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.LoadFromJson(string path)
			=> DeserializeFromJson(path);
		static IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.DeserializeFromJson(string jsonString)
			=> DeserializeFromJson(jsonString);
		static IOrientedGraph<TVertex, TEdge> IOrientedGraph<TVertex, TEdge>.DeserializeFromJson(string jsonString, JsonSerializerOptions options)
			=> DeserializeFromJson(jsonString, options);
		#endregion
	}
}
