using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibraryTests.TestGraphs;

namespace GraphLibraryTests.OrientedGraphTests
{
    /// <summary>
    /// Tests for the GetVertexCount and GetEdgeCount methods
    /// </summary>
    [TestClass]
    public class VertexEdgeCountTests
    {
        [TestMethod]
        public void VertexCountTest1()
        {
            for (int i = 0; i < 20; i++)
            {
                var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(i);
                Assert.AreEqual(i, graph.GetVertexCount());
            }
        }

        [TestMethod]
        public void VertexCountTest2()
        {
            var graph = TestOrientedGraphs.ClearTestOrientedGraph;
            Assert.AreEqual(0, graph.GetVertexCount());
        }

        [TestMethod]
        public void VertexCountTest3()
        {
            var graph = TestOrientedGraphs.GetVertexTestOrientedGraph(10);
            graph.RemoveVertex("0");
            graph.RemoveVertex("4");
            Assert.AreEqual(8, graph.GetVertexCount());
        }

        [TestMethod]
        public void EdgeCountTest1()
        {
            var graph = TestOrientedGraphs.ClearTestOrientedGraph;
            Assert.AreEqual(0, graph.GetEdgeCount());
        }

        [TestMethod]
        public void EdgeCountTest2()
        {
            for (int i = 1; i < 20; i++)
            {
                var graph = TestOrientedGraphs.GetPathTestOrientedGraph(i);
                Assert.AreEqual(i - 1, graph.GetEdgeCount());
            }
        }

        [TestMethod]
        public void EdgeCountTest3()
        {
            for (int i = 1; i < 20; i++)
            {
                var graph = TestOrientedGraphs.GetCompleteTestOrientedGraph(i);
                Assert.AreEqual(i * (i - 1), graph.GetEdgeCount());
            }
        }

        [TestMethod]
        public void EdgeCountTest4()
        {
            var graph = TestOrientedGraphs.GetCompleteTestOrientedGraph(10);
            graph.RemoveEdge("0", "1");
            graph.RemoveEdge("8", "6");
            graph.RemoveEdge("4", "3");
            Assert.AreEqual(87, graph.GetEdgeCount());
        }
    }
}
