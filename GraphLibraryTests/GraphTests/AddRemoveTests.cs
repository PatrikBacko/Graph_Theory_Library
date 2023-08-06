using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Edges;
using GraphLibrary.Graphs.Exceptions;

namespace GraphLibraryTests.GraphTests
{
	[TestClass]
	public class AddTests
	{

		//TODO : maybe prerobiť testy podľa prvého testu ??
		[TestMethod]
		public void AddVertexTest1(){
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex,OrientedEdge>.CreateGraph();
			var vertex = new OrientedVertex("0");

			graph.AddVertex(vertex);
			var actual = graph.GetVertex("0");

			Assert.IsTrue(graph.IsVertex(actual));
			Assert.IsTrue(graph.IsVertex(vertex.Name));
		}

		[TestMethod]
		public void AddVertexTest2(){
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertex = new OrientedVertex("1");

			graph.AddVertex(new OrientedVertex("0"));
			graph.AddVertex(vertex);
			var expected = vertex;
			var actual = graph.GetVertex("1");

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void AddVertexTest3(){
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertex = new OrientedVertex("0");

			graph.AddVertex(new OrientedVertex("0"));

			Assert.ThrowsException<VertexException>(() => graph.AddVertex(vertex));
		}

		[TestMethod]
		public void AddVertexTest4(){
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertex = new OrientedVertex("0");

			graph.AddVertex(vertex);

			Assert.ThrowsException<VertexException>(() => graph.AddVertex(vertex));
		}


		[TestMethod]
		public void AddVerticesTest1(){
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertices = new List<OrientedVertex>() { new OrientedVertex("0"), new OrientedVertex("1"), new OrientedVertex("2") };

			graph.AddVertices(vertices);
			var expected = vertices;
			var actual = graph.GetVertices().ToList();
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void AddVerticesTest2(){
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertices = new List<OrientedVertex>() { new OrientedVertex("0"), new OrientedVertex("1"), new OrientedVertex("2") };
			var vertex = new OrientedVertex("0");

			graph.AddVertices(vertices);

			Assert.ThrowsException<VertexException>(() => graph.AddVertex(vertex));
			
		}

		[TestMethod]
		public void AddVerticesTest3()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			var vertex = new OrientedVertex("0");
			var vertices = new List<OrientedVertex>() { vertex, new OrientedVertex("1"), new OrientedVertex("2") };

			graph.AddVertices(vertices);

			Assert.ThrowsException<VertexException>(() => graph.AddVertex(vertex));

		}

		[TestMethod]
		public void AddVerticesTest4()
		{
			IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.CreateGraph();
			
			var vertices = new List<OrientedVertex>() { new OrientedVertex("0"), new OrientedVertex("1"), new OrientedVertex("2"), new OrientedVertex("0") };

			Assert.ThrowsException<VertexException>(() => graph.AddVertices(vertices));

		}

	//TODO : AddEdgeTests
	}
}
