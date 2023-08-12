using GraphLibrary.Graphs.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests.GraphTests.OrientedGraphTests
{
    /// <summary>
    /// Tests for removing vertices from graph
    /// 
    /// methods (all overloads):
    /// - RemoveVertex
    /// - RemoveVertices
    /// </summary>
    [TestClass]
    public class RemoveVertexTests
    {
        [TestMethod]
        public void RemoveVertexTest1()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
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
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
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
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
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
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();

            Assert.ThrowsException<VertexException>(() => graph.RemoveVertex("0"));
        }

        [TestMethod]
        public void RemoveVertexTest5()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
            graph.AddVertex(new OrientedVertex("1"));
            graph.AddVertex(new OrientedVertex("2"));
            graph.RemoveVertex("1");


            Assert.ThrowsException<VertexException>(() => graph.RemoveVertex("0"));
        }

        [TestMethod]
        public void RemoveVertexTest6()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
            graph.AddVertex(new OrientedVertex("1"));
            graph.AddVertex(new OrientedVertex("0"));
            graph.RemoveVertex("1");
            graph.RemoveVertex("0");

            Assert.ThrowsException<VertexException>(() => graph.RemoveVertex("0"));
        }

        [TestMethod]
        public void RemoveVertexTest7()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
            graph.AddVertex(new OrientedVertex("1"));
            graph.AddVertex(new OrientedVertex("0"));
            graph.RemoveVertex("1");
            graph.RemoveVertex("0");

            Assert.ThrowsException<VertexException>(() => graph.GetVertex("0"));
        }


        [TestMethod]
        public void RemoveVerticesTest1()
        {
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
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
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
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
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
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
            IOrientedGraph<OrientedVertex, OrientedEdge> graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();


            var vertices = new List<OrientedVertex>() { new OrientedVertex("0"), new OrientedVertex("1"), new OrientedVertex("2") };
            graph.AddVertices(vertices);

            Assert.ThrowsException<VertexException>(() => graph.RemoveVertices(new List<VertexName> { "0", "1", "2", "0" }));
        }
    }
}
