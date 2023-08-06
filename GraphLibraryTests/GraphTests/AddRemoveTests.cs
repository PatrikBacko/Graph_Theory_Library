using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Edges;
using GraphLibrary.Graphs.Exceptions;
using GraphLibrary.Vertices;

namespace GraphLibraryTests.GraphTests
{
	/// <summary>
	/// Class for tesing Addition and Removing methods of graph
	/// 
	/// methods and all its overloads:
	///	 - AddVertex
	///	 - AddVertices
	///	 - removeVertex
	///	 - removeVertices
	///	 
	///	 - AddEdge
	///	 - AddEdges
	///	 - RemoveEdge
	///	 - RemoveEdges
	/// </summary>
	[TestClass]
	public class AddRemoveTests
	{

		[TestMethod]
		public void AddVertexTest1() {
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertex = new OrientedVertex("0");

			graph.AddVertex(vertex);
			var expected = vertex;
			var actual = graph.GetVertex("0");

			Assert.IsTrue(graph.IsVertex(vertex));
			Assert.IsTrue(graph.IsVertex(vertex.Name));
			Assert.IsTrue(graph.IsVertex("0"));
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void AddVertexTest2() {
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertex = new OrientedVertex("1");

			graph.AddVertex(new OrientedVertex("0"));
			graph.AddVertex(vertex);
			var expected = vertex;
			var actual = graph.GetVertex("1");

			Assert.IsTrue(graph.IsVertex(vertex));
			Assert.IsTrue(graph.IsVertex(vertex.Name));
			Assert.IsTrue(graph.IsVertex("0"));
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void AddVertexTest3() {
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertex = new OrientedVertex("0");

			graph.AddVertex(new OrientedVertex("0"));

			Assert.ThrowsException<VertexException>(() => graph.AddVertex(vertex));
		}

		[TestMethod]
		public void AddVertexTest4() {
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertex = new OrientedVertex("0");

			graph.AddVertex(vertex);

			Assert.ThrowsException<VertexException>(() => graph.AddVertex(vertex));
		}


		[TestMethod]
		public void AddVerticesTest1() {
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertices = new List<OrientedVertex>() { new OrientedVertex("0"), new OrientedVertex("1"), new OrientedVertex("2") };

			graph.AddVertices(vertices);
			var expected = vertices;
			var actual = graph.GetVertices().ToList();
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void AddVerticesTest2() {
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertices = new List<OrientedVertex>() { new OrientedVertex("0"), new OrientedVertex("1"), new OrientedVertex("2") };
			var vertex = new OrientedVertex("0");

			graph.AddVertices(vertices);

			Assert.ThrowsException<VertexException>(() => graph.AddVertex(vertex));

		}

		[TestMethod]
		public void AddVerticesTest3() {
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertex = new OrientedVertex("0");
			var vertices = new List<OrientedVertex>() { vertex, new OrientedVertex("1"), new OrientedVertex("2") };

			graph.AddVertices(vertices);

			Assert.ThrowsException<VertexException>(() => graph.AddVertex(vertex));

		}

		[TestMethod]
		public void AddVerticesTest4() {
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();

			var vertices = new List<OrientedVertex>() { new OrientedVertex("0"), new OrientedVertex("1"), new OrientedVertex("2"), new OrientedVertex("0") };

			Assert.ThrowsException<VertexException>(() => graph.AddVertices(vertices));

		}

		[TestMethod]
		public void RemoveVertexTest1() {
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertex = new OrientedVertex("0");

			graph.AddVertex(vertex);
			graph.RemoveVertex(vertex);

			Assert.IsFalse(graph.IsVertex(vertex));
			Assert.IsFalse(graph.IsVertex(vertex.Name));
			Assert.IsFalse(graph.IsVertex("0"));
		}


		[TestMethod]
		public void RemoveVertexTest2()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertex = new OrientedVertex("0");

			graph.AddVertex(vertex);
			graph.RemoveVertex("0");

			Assert.IsFalse(graph.IsVertex(vertex));
			Assert.IsFalse(graph.IsVertex(vertex.Name));
			Assert.IsFalse(graph.IsVertex("0"));
		}

		[TestMethod]
		public void RemoveVertexTest3()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertex = new OrientedVertex("0");

			graph.AddVertex(new OrientedVertex("1"));
			graph.AddVertex(vertex);
			graph.AddVertex(new OrientedVertex("2"));
			graph.RemoveVertex(vertex);
			graph.RemoveVertex("1");

			Assert.IsFalse(graph.IsVertex(vertex));
			Assert.IsFalse(graph.IsVertex(vertex.Name));
			Assert.IsFalse(graph.IsVertex("0"));

			Assert.IsFalse(graph.IsVertex("1"));

			Assert.IsTrue(graph.IsVertex("2"));
		}

		[TestMethod]
		public void RemoveVertexTest4()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();

			Assert.ThrowsException<VertexException>(() => graph.RemoveVertex("0"));
		}

		[TestMethod]
		public void RemoveVertexTest5()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			graph.AddVertex(new OrientedVertex("1"));
			graph.AddVertex(new OrientedVertex("2"));
			graph.RemoveVertex("1");


			Assert.ThrowsException<VertexException>(() => graph.RemoveVertex("0"));
		}

		[TestMethod]
		public void RemoveVertexTest6()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			graph.AddVertex(new OrientedVertex("1"));
			graph.AddVertex(new OrientedVertex("0"));
			graph.RemoveVertex("1");
			graph.RemoveVertex("0");

			Assert.ThrowsException<VertexException>(() => graph.RemoveVertex("0"));
		}

		[TestMethod]
		public void RemoveVertexTest7()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			graph.AddVertex(new OrientedVertex("1"));
			graph.AddVertex(new OrientedVertex("0"));
			graph.RemoveVertex("1");
			graph.RemoveVertex("0");

			Assert.ThrowsException<VertexException>(() => graph.GetVertex("0"));
		}


		[TestMethod]
		public void RemoveVerticesTest1()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertex0 = new OrientedVertex("0");
			var vertex1 = new OrientedVertex("1");
			var vertex2 = new OrientedVertex("2");

			var vertices = new List<OrientedVertex>() { vertex0, vertex2 };


			graph.AddVertex(vertex0);
			graph.AddVertex(vertex1);
			graph.AddVertex(vertex2);
			graph.RemoveVertices(vertices);
			var expected = new List<OrientedVertex> { vertex1 };
			var actual = graph.GetVertices().ToList();
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void RemoveVerticesTest2()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertices = new List<OrientedVertex>() { new OrientedVertex("0"), new OrientedVertex("1"), new OrientedVertex("2") };

			graph.AddVertices(vertices);
			graph.RemoveVertices(vertices);
			var expected = new List<OrientedVertex>();
			var actual = graph.GetVertices().ToList();

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void RemoveVerticesTest3()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertex = new OrientedVertex("1");

			var vertices = new List<OrientedVertex>() { new OrientedVertex("0"), vertex, new OrientedVertex("2") };

			graph.AddVertices(vertices);
			graph.RemoveVertices(new List<VertexName> { "0", "2" });
			var expected = new List<OrientedVertex> { vertex };
			var actual = graph.GetVertices().ToList();

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void RemoveVerticesTest4()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();


			var vertices = new List<OrientedVertex>() { new OrientedVertex("0"), new OrientedVertex("1"), new OrientedVertex("2") };
			graph.AddVertices(vertices);

			Assert.ThrowsException<VertexException>(() => graph.RemoveVertices(new List<VertexName> { "0", "1", "2", "0" }));
		}



		[TestMethod]
		public void AddEdgeTest1() {
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.VertexTestGraph1;

			var edge = new OrientedEdge("0", "1");
			graph.AddEdge(edge);

			Assert.IsTrue(graph.IsEdge(edge));
			Assert.IsTrue(graph.IsEdge(edge.VertexOut, edge.VertexIn));
			Assert.IsTrue(graph.IsEdge("0", "1"));

			Assert.AreEqual(graph.GetEdge(edge.VertexOut, edge.VertexIn), edge);
			Assert.AreEqual(graph.GetEdge("0", "1"), edge);
		}

		[TestMethod]
		public void AddEdgeTest2() {
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.VertexTestGraph1;

			var edge = new OrientedEdge("0", "1");
			graph.AddEdge(edge);
			Assert.ThrowsException<EdgeException>(() => graph.AddEdge(edge));
		}

		[TestMethod]
		public void AddEdgeTest3()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.VertexTestGraph1;

			var edge = new OrientedEdge("0", "1");
			graph.AddEdge(edge);

			Assert.ThrowsException<EdgeException>(() => graph.AddEdge(new OrientedEdge("0", "1")));
		}

		[TestMethod]
		public void AddEdgeTest4()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.VertexTestGraph1;

			var edge = new OrientedEdge("0", "4");

			Assert.ThrowsException<EdgeException>(() => graph.AddEdge(edge));
		}
		[TestMethod]
		public void AddEdgeTest5()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.VertexTestGraph1;

			var edge = new OrientedEdge("0", "0");

			Assert.ThrowsException<EdgeException>(() => graph.AddEdge(edge));
		}

		[TestMethod]
		public void AddEdgesTest1(){
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.GetVertexTestGraph(10);
			var edges = new List<OrientedEdge>();
			for(int i = 0; i < 10; i++) {
				for(int j = 0; j < 10; j++) {
					if(i != j)
						edges.Add(new OrientedEdge(i.ToString(), j.ToString()));
				}
			}
			graph.AddEdges(edges);
			var expected = edges;
			var actual = graph.GetEdges().ToList();
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void AddEdgesTest2(){
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.GetVertexTestGraph(5);
			var edges = new List<OrientedEdge>();
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					if (i != j)
						edges.Add(new OrientedEdge(i.ToString(), j.ToString()));
				}
			}
			graph.AddEdges(edges);
			
			Assert.IsTrue(graph.IsEdge("0", "1"));
			Assert.IsTrue(graph.IsEdge("2", "4"));
			Assert.IsTrue(graph.IsEdge("4", "3"));
		}

		[TestMethod]
		public void AddEdgesTest3()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.GetVertexTestGraph(10);
			var edges = new List<OrientedEdge>();
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					edges.Add(new OrientedEdge(i.ToString(), j.ToString()));
				}
			}
			Assert.ThrowsException<EdgeException>(() => graph.AddEdges(edges));
		}

		[TestMethod]
		public void RemoveEdgeTest1(){
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.GetVertexTestGraph(5);
			var edge = new OrientedEdge("0", "1");
			graph.AddEdge(edge);
			graph.RemoveEdge(edge);

			Assert.IsFalse(graph.IsEdge(edge));
			Assert.IsFalse(graph.IsEdge(edge.VertexOut, edge.VertexIn));
			Assert.IsFalse(graph.IsEdge("0", "1"));
		}

		[TestMethod]
		public void RemoveEdgeTest2()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.GetVertexTestGraph(5);
			var edge = new OrientedEdge("0", "1");
			graph.AddEdge(edge);
			graph.RemoveEdge(edge);

			Assert.ThrowsException<EdgeException>(() => graph.RemoveEdge(edge));
		}

		[TestMethod]
		public void RemoveEdgeTest3()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.GetVertexTestGraph(5);
			var edge = new OrientedEdge("0", "1");
			graph.AddEdge(edge);
			graph.RemoveEdge(edge);

			Assert.ThrowsException<EdgeException>(() => graph.GetEdge("0", "1"));
		}

		[TestMethod]
		public void RemoveEdgeTest4()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.GetVertexTestGraph(5);
			var edge = new OrientedEdge("0", "1");

			Assert.ThrowsException<EdgeException>(() => graph.RemoveEdge("0", "1"));
		}

		[TestMethod]
		public void RemoveEdgeTest5()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.GetVertexTestGraph(5);
			var edge = new OrientedEdge("0", "1");

			Assert.ThrowsException<EdgeException>(() => graph.RemoveEdge("0", "10"));
		}

		[TestMethod]
		public void RemoveEdgeTest6()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.GetVertexTestGraph(5);
			var edge1 = new OrientedEdge("0", "1");
			var edge2 = new OrientedEdge("1", "4");
			var edge3 = new OrientedEdge("3", "2");
			var edge4 = new OrientedEdge("2", "3");
			
			graph.AddEdge(edge1).AddEdge(edge2).AddEdge(edge3).AddEdge(edge4);

			var expected = new List<OrientedEdge>() {edge1, edge4};
			var actual = graph.RemoveEdge(edge2).RemoveEdge("3", "2").GetEdges().ToList();

			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void RemoveEdgesTest1(){
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.GetVertexTestGraph(10);
			var edges = new List<OrientedEdge>();
			for(int i = 0; i < 10; i++) {
				for(int j = 0; j < 10; j++) {
					if(i != j)
						edges.Add(new OrientedEdge(i.ToString(), j.ToString()));
				}
			}
			graph.AddEdges(edges);
			graph.RemoveEdges(edges);
			var expected = new List<OrientedEdge>();
			var actual = graph.GetEdges().ToList();
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void RemoveEdgesTest2()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.GetVertexTestGraph(10);
			var edges = new List<(VertexName, VertexName)>();
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					if (i != j){
						graph.AddEdge(new OrientedEdge(i.ToString(), j.ToString()));
						edges.Add((i.ToString(), j.ToString()));
					}
				}
			}
			graph.RemoveEdges(edges);
			var expected = new List<OrientedEdge>();
			var actual = graph.GetEdges().ToList();
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void RemoveEdgesTest3()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = TestGraphs.GetVertexTestGraph(5);
			var edge1 = new OrientedEdge("0", "1");
			var edge2 = new OrientedEdge("1", "4");
			var edge3 = new OrientedEdge("3", "2");
			var edge4 = new OrientedEdge("2", "3");

			graph.AddEdge(edge1).AddEdge(edge2).AddEdge(edge3).AddEdge(edge4);
			var edges = new List<OrientedEdge>() { edge2, edge3};

			var expected = new List<OrientedEdge>() { edge1, edge4 };
			var actual = graph.RemoveEdges(edges).GetEdges().ToList();

			CollectionAssert.AreEqual(expected, actual);
		}
	}
}
