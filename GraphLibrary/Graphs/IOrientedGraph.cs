using GraphLibrary.Edges;
using GraphLibrary.Graphs.Delegates;
using GraphLibrary.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GraphLibrary.Graphs.Exceptions;

namespace GraphLibrary.Graphs
{
	/// <summary>
	/// Interface for oriented graph. <br />
	/// </summary>
	/// <typeparam name="TVertex"> 
	/// type of vertex in graph, must be <see cref="OrientedVertex"/>
	/// </typeparam>
	/// <typeparam name="TEdge"> 
	/// type of edge in graph, must be <see cref="OrientedEdge"/>
	/// </typeparam>
	public interface IOrientedGraph<TVertex, TEdge>
		where TVertex : OrientedVertex
		where TEdge : OrientedEdge
	{
		#region GetCountMethods
		/// <summary>
		/// Gets number of vertices in the graph. <br />
		/// Time complexity: O(1)
		/// </summary>
		/// <returns> 
		/// Count of Vertices in graph 
		/// </returns>
		int GetVertexCount();
		/// <summary>
		/// Gets number of edges in the graph.<br />
		/// Time complexity: O(1)
		/// </summary>
		/// <returns>
		/// Count of Edges in graph
		/// </returns>
		int GetEdgeCount();
		#endregion

		#region GetVerticesMethods
		/// <summary>
		/// Gets all vertices in the graph. <br />
		/// Time complexity: O(1)
		/// </summary>
		/// <returns> 
		/// <see cref="IEnumerable{TVertex}"/> of all (TVertex) vertices in graph 
		/// </returns>
		IEnumerable<TVertex> GetVertices();
		/// <summary>
		/// Gets all vertices in the graph that satisfy the given predicate. <br />
		/// Time complexity: O(number of vertices)
		/// </summary>
		/// <param name="vertexPredicate"> 
		/// <see cref="VertexPredicate{TVertex}"/> that decides which vertices will be included in result 
		/// </param>
		/// <returns> 
		/// <see cref="IEnumerable{TVertex}"/> of (TVertex) vertices that satisfy the predicate 
		/// </returns>
		IEnumerable<TVertex> GetVerticesWith(VertexPredicate<TVertex> vertexPredicate);
		#endregion

		#region GetEdgesMethods
		/// <summary>
		/// Gets all edges in the graph. <br />
		/// Time complexity: O(number of edges)
		/// </summary>
		/// <returns> 
		/// <see cref="IEnumerable{TEdge}"/> of all (TEdge) edges in graph 
		/// </returns>
		IEnumerable<TEdge> GetEdges();
		/// <summary>
		/// Gets all edges in the graph that satisfy the given predicate.<br />
		/// Time complexity: O(number of edges)
		/// </summary>
		/// <param name="edgePredicate"> 
		/// <see cref="EdgePredicate{TEdge}"/> that decides which edges will be included in result 
		/// </param>
		/// <returns> 
		/// <see cref="IEnumerable{TEdge}"/> of (TEdge) edges that satisfy the predicate
		/// </returns>
		IEnumerable<TEdge> GetEdgesWith(EdgePredicate<TEdge> edgePredicate);

		/// <summary>
		/// Applies the given action to all vertices in the graph. <br />
		/// Time complexity: O(number of vertices)
		/// </summary>
		/// <param name="vertexAction"> 
		/// <see cref="VertexAction{TVertex}"/> applied to all vertices 
		/// </param>
		/// <returns>
		/// itself after applying the action
		/// </returns>
		#endregion

		#region ApplyToVerticesMethods
		IOrientedGraph<TVertex, TEdge> ApplyToVertices(VertexAction<TVertex> vertexAction);
		/// <summary>
		/// Applies the given action to all vertices in the graph that satisfy the given predicate. <br />
		/// Time complexity: O(number of vertices)
		/// </summary>
		/// <param name="vertexPredicate">
		/// <see cref="VertexPredicate{TVertex}"/> that decides which vertices will be affected by vertexAction 
		/// </param>
		/// <param name="vertexAction"> 
		/// <see cref="VertexAction{TVertex}"/> that is applied to requested vertices
		/// </param>
		/// <returns>
		/// Itself after applying the action
		/// </returns>
		IOrientedGraph<TVertex, TEdge> ApplyToVerticesWith(VertexPredicate<TVertex> vertexPredicate, VertexAction<TVertex> vertexAction);

		/// <summary>
		/// Applies the given action to all <see cref="TEdge"/> in the graph. <br />
		/// Time complexity: O(number of edges)
		/// </summary>
		/// <param name="edgeAction"> 
		/// <see cref="EdgeAction{TEdge}"/> applied to all edges 
		/// </param>
		/// <returns> 
		/// Itself after applying the action	
		/// </returns>
		#endregion

		#region ApplyToEdgesMethods
		IOrientedGraph<TVertex, TEdge> ApplyToEdges(EdgeAction<TEdge> edgeAction);
		/// <summary>
		/// Applies the given action to all edges in the graph that satisfy the given predicate. <br />
		/// Time complexity: O(number of edges)
		/// </summary>
		/// <param name="edgePredicate">
		/// <see cref="EdgePredicate{TEdge}"/> that decides which edges will be affected by edgeAction
		/// </param>
		/// <param name="edgeAction">
		/// <see cref="EdgeAction{TEdge}"/> that is applied to requested edges"/>
		/// </param>
		/// <returns>
		/// Itself after applying the action
		/// </returns>
		IOrientedGraph<TVertex, TEdge> ApplyToEdgesWith(EdgePredicate<TEdge> edgePredicate, EdgeAction<TEdge> edgeAction);
		#endregion

		#region GetVertexMethods
		/// <summary>
		/// Gets the vertex with the given name. <br />
		/// </summary>
		/// <param name="vertex">
		/// <see cref="VertexName"/> of the vertex to be returned"/>
		/// </param>
		/// <returns>
		/// TVertex with the given name
		/// </returns>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception>
		TVertex GetVertex(VertexName vertex);
		#endregion

		#region GetEdgeMethods
		/// <summary>
		/// Gets the edge with the given name. <br />
		/// </summary>
		/// <param name="vertexOut">
		/// <see cref="VertexName"/> of the vertex that is the source of the edge to be returned"/>
		/// </param>
		/// <param name="vertexIn">
		/// <see cref="VertexName"/> of the vertex that is the destination of the edge to be returned"/>"/>
		/// </param>
		/// <returns>
		/// TEgde with the given vertices as source and destination
		/// </returns>
		/// <exception cref="EdgeException">
		/// Exception thrown when edge with the given vertices as source and destination does not exist in the graph, 
		/// or when vertices do not exist in the graph
		/// </exception>
		TEdge GetEdge(VertexName vertexOut, VertexName vertexIn);
		#endregion

		#region IsVertexMethods
		/// <summary>
		/// Returns true if the graph contains the given vertex and false otherwise <br />
		/// </summary>
		/// <param name="vertexName">
		/// <see cref="VertexName"/> of the vertex to be checked
		/// </param>
		/// <returns>
		/// bool indicating whether the graph contains vertex with the given name
		/// </returns>
		bool IsVertex(VertexName vertexName);
		/// <summary>
		/// Returns true if the graph contains the given vertex and false otherwise <br />
		/// </summary>
		/// <param name="vertex">
		/// <see cref="TVertex"/> to be checked"/>"/>
		/// </param>
		/// <returns>
		/// bool indicating whether the graph contains the given vertex
		/// </returns>
		bool IsVertex(TVertex vertex);
		#endregion

		#region IsEdgeMethods
		/// <summary>
		/// Returns true if the graph contains the given edge and false otherwise <br />
		/// </summary>
		/// <param name="vertexOut">
		/// <see cref="VertexName"/> of the vertex that is the source of the edge to be checked
		/// </param>
		/// <param name="vertexIn">
		/// <see cref="VertexName"/> of the vertex that is the destination of the edge to be checked
		/// </param>
		/// <returns>
		/// bool indicating whether the graph contains edge with the given vertices as source and destination
		/// </returns>
		bool IsEdge(VertexName vertexOut, VertexName vertexIn);
		/// <summary>
		/// Returns true if the graph contains the given edge and false otherwise <br />
		/// </summary>
		/// <param name="edge">
		/// <see cref="TEdge"/> to be checked
		/// </param>
		/// <returns>
		/// bool indicating whether the graph contains the given edge
		/// </returns>
		bool IsEdge(TEdge edge);
		#endregion

		#region GetAdjacentVerticesMethods
		/// <summary>
		/// Gets adjacent vertices connected by an edge leading into the given vertex <br />
		/// Time complexity: O(number of edges)
		/// </summary>
		/// <param name="vertex">
		/// <see cref="VertexName"/> of the vertex whose in adjacent vertices are to be returned
		/// </param>
		/// <returns>
		/// <see cref="IEnumerable{TVertex}"/> of vertices in adjacent to the given vertex
		/// </returns>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception>
		IEnumerable<TVertex> GetInAdjacentVertices(VertexName vertex);
		/// <summary>
		/// Gets adjacent vertices connected by an edge leading out of the given vertex <br />
		/// Time complexity: O(out Degree of given vertex)
		/// </summary>
		/// <param name="vertex">
		/// <see cref="VertexName"/> of the vertex whose out adjacent vertices are to be returned
		/// </param>
		/// <returns>
		/// <see cref="IEnumerable{TVertex}"/> of vertices out adjacent to the given vertex
		/// </returns>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception>
		IEnumerable<TVertex> GetOutAdjacentVertices(VertexName vertex);
		#endregion

		#region GetAdjacentEdgesMethods
		/// <summary>
		/// Gets edges leading into the given vertex <br />
		/// Time complexity: O(number of edges)
		/// </summary>
		/// <param name="vertex">
		/// <see cref="VertexName"/> of the vertex whose in edges are to be returned
		/// </param>
		/// <returns>
		/// <see cref="IEnumerable{TEdge}"/> of edges leading into the given vertex
		/// </returns>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception>
		IEnumerable<TEdge> GetInEdges(VertexName vertex);
		/// <summary>
		/// Gets edges leading out of the given vertex <br />
		/// Time complexity: O(out Degree of given vertex)
		/// </summary>
		/// <param name="vertex">
		/// <see cref="VertexName"/> of the vertex whose out edges are to be returned
		/// </param>
		/// <returns>
		/// <see cref="IEnumerable{TEdge}"/> of edges leading out of the given vertex
		/// </returns>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception>
		IEnumerable<TEdge> GetOutEdges(VertexName vertex);
		#endregion

		#region GetDegreeMethods
		/// <summary>
		/// Gets Count of edges leading into the given vertex <br />
		/// Time complexity: O(1)
		/// </summary>
		/// <param name="vertex">
		/// <see cref="VertexName"/> of the vertex whose In Degree is to be returned
		/// </param>
		/// <returns>
		/// In Degree of the given vertex
		/// </returns>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception>
		int GetInDegree(VertexName vertex);
		/// <summary>
		/// Gets Count of edges leading out of the given vertex <br />
		/// Time complexity: O(1)
		/// </summary>
		/// <param name="vertex">
		/// <see cref="VertexName"/> of the vertex whose Out Degree is to be returned
		/// </param>
		/// <returns>
		/// Out Degree of the given vertex
		/// </returns>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception>
		int GetOutDegree(VertexName vertex);
		#endregion

		#region AddVertexMethods
		/// <summary>
		/// Adds given vertex to the graph <br />
		/// If instance of the given vertex belongs to a different graph, <see cref="EdgeException"/> will be thrown <br />
		/// </summary>
		/// <param name="vertex">
		/// <see cref="TVertex"/> to be added to the graph
		/// </param>
		/// <returns>
		/// Itself after adding the given vertex
		/// </returns>
		/// <exception cref="VertexException">
		/// Exception thrown :
		/// - when vertex with the same name already exists in the graph,
		/// - when instance of the given vertex belongs to a different graph,
		/// - when name of the given vertex is empty string
		/// </exception>
		IOrientedGraph<TVertex, TEdge> AddVertex(TVertex vertex);
		/// <summary>
		/// Adds given vertices to the graph <br />
		/// </summary>
		/// <param name="vertices">
		/// <see cref="IEnumerable{TVertex}"/> of vertices to be added to the graph
		/// </param>
		/// <returns>
		/// Itself after adding the given vertices
		/// </returns>
		/// <exception cref="VertexException">
		/// Exception thrown in same cases as <see cref="AddVertex(TVertex)"/>
		/// </exception>
		IOrientedGraph<TVertex, TEdge> AddVertices(IEnumerable<TVertex> vertices);
		#endregion

		#region AddEdgeMethods
		/// <summary>
		/// Adds edge between given edge to the graph <br />
		/// If instance of the given edge belongs to a different graph, <see cref="EdgeException"/> will be thrown <br />
		/// </summary>
		/// <param name="edge">
		/// <see cref="TEdge"/> to be added to the graph
		/// </param>
		/// <returns>
		/// Itself after adding the given edge
		/// </returns>
		/// <exception cref="EdgeException">
		/// Exception thrown :
		/// - whenedge with same end vertices already exists in the graph,
		/// - when instance of the given edge belongs to a different graph,
		/// - when one of end vertices of the given edge does not exist in the graph
		/// </exception>
		IOrientedGraph<TVertex, TEdge> AddEdge(TEdge edge);
		/// <summary>
		/// Adds given edges to the graph <br />
		/// </summary>
		/// <param name="edges">
		/// <see cref="IEnumerable{TEdge}"/> of edges to be added to the graph
		/// </param>
		/// <returns>
		/// Itself after adding the given edges
		/// </returns>
		/// <exception cref="EdgeException">
		/// Thrown in same cases as <see cref="AddEdge(TEdge)"/>
		/// </exception>
		IOrientedGraph<TVertex, TEdge> AddEdges(IEnumerable<TEdge> edges);
		#endregion

		#region RemoveVertexMethods
		/// <summary>
		/// Removes vertex with the given name from the graph <br />
		/// </summary>
		/// <param name="vertex">
		/// <see cref="VertexName"/> of the vertex to be removed
		/// </param>
		/// <returns>
		/// Itself after removing the vertex
		/// </returns>
		/// <exception cref="VertexException">
		/// Exception thrown when vertex with the given name does not exist in the graph
		/// </exception>
		IOrientedGraph<TVertex, TEdge> RemoveVertex(VertexName vertex);
		/// <summary>
		/// Removes given vertex from the graph <br />
		/// </summary>
		/// <param name="vertex">
		/// <see cref="TVertex"/> to be removed
		/// </param>
		/// <returns>
		/// Itself after removing the vertex
		/// </returns>
		/// <exception cref="VertexException">
		/// exception thrown when vertex does not exist in the graph
		/// </exception>
		IOrientedGraph<TVertex, TEdge> RemoveVertex(TVertex vertex);
		/// <summary>
		/// Removes given vertices from the graph <br />
		/// </summary>
		/// <param name="vertices">
		/// <see cref="IEnumerable{TVertex}"/> of vertices to be removed
		/// </param>
		/// <returns>
		/// Itself after removing the vertices
		/// </returns>
		/// <exception cref="VertexException">
		/// Exception thrown when one of the vertices does not exist in the graph
		/// </exception>
		IOrientedGraph<TVertex, TEdge> RemoveVertices(IEnumerable<TVertex> vertices);
		/// <summary>
		/// Removes vertices with given names from the graph <br />
		/// </summary>
		/// <param name="vertices">
		/// <see cref="IEnumerable{VertexName}"/> of vertices to be removed
		/// </param>
		/// <returns>
		/// Itself after removing the vertices
		/// </returns>
		/// <exception cref="VertexException">
		/// Exception thrown when one of the vertex names does not exist in the graph
		/// </exception>
		IOrientedGraph<TVertex, TEdge> RemoveVertices(IEnumerable<VertexName> vertices);
		/// <summary>
		/// Removes vertices that satisfy given predicate from the graph <br />
		/// </summary>
		/// <param name="vertexPredicate">
		/// <see cref="VertexPredicate{TVertex}"/> predicate that vertices must satisfy to be removed
		/// </param>
		/// <returns>
		/// Itself after removing the vertices
		/// </returns>
		IOrientedGraph<TVertex, TEdge> RemoveVerticesWith(VertexPredicate<TVertex> vertexPredicate);
		#endregion

		#region RemoveEdgeMethods
		/// <summary>
		/// Removes edge between given vertices from the graph <br />
		/// </summary>
		/// <param name="vertexOut">
		/// <see cref="VertexName"/> of the source vertex of the edge
		/// </param>
		/// <param name="vertexIn">
		/// <see cref="VertexName"/> of the destination vertex of the edge
		/// </param>
		/// <returns>
		/// Itself after removing the edge
		/// </returns>
		/// <exception cref="EdgeException">
		/// Exception thrown when edge between given vertices does not exist in the graph
		/// or when one of the vertices does not exist in the graph
		/// </exception>
		IOrientedGraph<TVertex, TEdge> RemoveEdge(VertexName vertexOut, VertexName vertexIn);
		/// <summary>
		/// Removes given edge from the graph <br />
		/// </summary>
		/// <param name="edge">
		/// <see cref="TEdge"/> to be removed"/>
		/// </param>
		/// <returns>
		/// Itself after removing the edge
		/// </returns>
		/// <exception cref="EdgeException">
		/// Exception thrown when edge does not exist in the graph,
		/// or one of the vertices of the edge does not exist in the graph
		/// </exception>
		IOrientedGraph<TVertex, TEdge> RemoveEdge(TEdge edge);
		/// <summary>
		/// Removes given edges from the graph <br />
		/// </summary>
		/// <param name="edges">
		/// <see cref="IEnumerable{TEdge}"/> of edges to be removed
		/// </param>
		/// <returns>
		/// Itself after removing the edges
		/// </returns>
		/// <exception cref="EdgeException">
		/// Exception thrown same as in <see cref="RemoveEdge(TEdge)"/>
		/// </exception>
		IOrientedGraph<TVertex, TEdge> RemoveEdges(IEnumerable<TEdge> edges);
		/// <summary>
		/// Removes edges between given vertices from the graph <br />
		/// </summary>
		/// <param name="edges">
		/// <see cref="IEnumerable{Tuple}"/> of tuples containing <see cref="VertexName"/> of the source vertex and <see cref="VertexName"/> of the destination vertex
		/// </param>
		/// <returns>
		/// Itself after removing the edges
		/// </returns>
		/// <exception cref="EdgeException">
		/// Exception thrown same as in <see cref="RemoveEdge(VertexName, VertexName)"/>
		/// </exception>
		IOrientedGraph<TVertex, TEdge> RemoveEdges(IEnumerable<(VertexName vertexOut, VertexName vertexIn)> edges);
		/// <summary>
		/// Removes edges that satisfy given predicate from the graph <br />
		/// </summary>
		/// <param name="edgePredicate">
		/// <see cref="EdgePredicate{TEdge}"/> predicate that edges must satisfy to be removed
		/// </param>
		/// <returns>
		/// Itself after removing the edges
		/// </returns>
		IOrientedGraph<TVertex, TEdge> RemoveEdgesWith(EdgePredicate<TEdge> edgePredicate);
		#endregion

		#region CreateAndClearMethods
		/// <summary>
		/// Creates new empty graph <br />
		/// </summary>
		/// <returns>
		/// Empty graph
		/// </returns>
		static abstract IOrientedGraph<TVertex, TEdge> Create();
		/// <summary>
		/// Creates new graph with given vertices and edges <br />
		/// </summary>
		/// <param name="vertices">
		/// <see cref="IEnumerable{TVertex}"/> of vertices to be added to the graph
		/// </param>
		/// <param name="edges">
		/// <see cref="IEnumerable{TEdge}"/> of edges to be added to the graph
		/// </param>
		/// <returns>
		/// new graph with given vertices and edges
		/// </returns>
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
		static abstract IOrientedGraph<TVertex, TEdge> Create(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges);
		/// <summary>
		/// Removes all vertices and edges from the graph. <br />
		/// Time complexity: O(1)
		/// </summary>
		/// <returns>
		/// itself after clearing
		/// </returns>
		IOrientedGraph<TVertex, TEdge> Clear();
		#endregion

		#region JsonSaveAndSerializeMethods
		/// <summary>
		/// Saves graph to json file (with given path) <br />
		/// </summary>
		/// <param name="path">
		/// path to the file
		/// </param>
		/// <exception cref="SerializationException">
		/// Exception thrown when serialization fails
		/// </exception>
		void SaveToJson(string path);
		/// <summary>
		/// Serializes graph to json string <br />
		/// </summary>
		/// <returns> 
		/// Json string with serialized graph
		/// </returns>
		/// <exception cref="SerializationException">
		/// Exception thrown when serialization fails
		/// </exception>
		string SerializeToJson();
		/// <summary>
		/// Serializes graph to json string with custom made options <br />
		/// </summary>
		/// <param name="options">
		/// custom options for serialization
		/// </param>
		/// <returns>
		/// Json string with serialized graph
		/// </returns>
		/// <exception cref="SerializationException">
		/// Exception thrown when serialization fails
		/// </exception>
		string SerializeToJson(JsonSerializerOptions options);
		#endregion

		#region JsonLoadAndDeserializeMethods 
		/// <summary>
		/// Loads graph from json file (with given path) <br />
		/// </summary>
		/// <param name="path">
		/// path to the json file containing serialized graph
		/// </param>
		/// <returns>
		/// Deserialized graph from json file
		/// </returns>
		/// <exception cref="DeserializationException">
		/// exception thrown when there is a problem with deserialization
		/// </exception>
		static abstract IOrientedGraph<TVertex, TEdge> LoadFromJson(string path);
		/// <summary>
		/// Deserializes graph from json string <br />
		/// </summary>
		/// <param name="jsonString">
		/// string containing serialized graph in json format
		/// </param>
		/// <returns>
		/// graph deserialized from json string
		/// </returns>
		/// <exception cref="DeserializationException">
		/// exception thrown when there is a problem with deserialization
		/// </exception>
		static abstract IOrientedGraph<TVertex, TEdge> DeserializeFromJson(string jsonString);
		/// <summary>
		/// Deserializes graph from json string with custom made options <br />
		/// </summary>
		/// <param name="jsonString">
		/// string containing serialized graph in json format
		/// </param>
		/// <param name="options">
		/// custom options for deserialization
		/// </param>
		/// <returns>
		/// graph deserialized from json string
		/// </returns>
		/// /// <exception cref="DeserializationException">
		/// exception thrown when there is a problem with deserialization
		/// </exception>
		static abstract IOrientedGraph<TVertex, TEdge> DeserializeFromJson(string jsonString, JsonSerializerOptions options);
		#endregion

		#region OverridenOperators
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
		static IOrientedGraph<TVertex, TEdge> operator +(IOrientedGraph<TVertex, TEdge> graph, TVertex vertex) => graph.AddVertex(vertex);
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
		static IOrientedGraph<TVertex, TEdge> operator +(IOrientedGraph<TVertex, TEdge> graph, TEdge edge) => graph.AddEdge(edge);

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
		static IOrientedGraph<TVertex, TEdge> operator -(IOrientedGraph<TVertex, TEdge> graph, TVertex vertex) => graph.RemoveVertex(vertex);
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
		static IOrientedGraph<TVertex, TEdge> operator -(IOrientedGraph<TVertex, TEdge> graph, TEdge edge) => graph.RemoveEdge(edge);
		#endregion
	}
}
