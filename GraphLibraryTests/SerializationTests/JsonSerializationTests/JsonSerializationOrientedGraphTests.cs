using GraphLibrary.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests.SerializationTests.JsonSerializationTests
{
    /// <summary>
    /// <b> Tests for Json Serialization on IOrientedGraph </b>
    /// </summary>
    [TestClass]
    public class JsonSerializationOrientedGraphTests
    {
        [TestMethod]
        public void JsonSerializationTest1()
        {
            // Arrange
            var graph = TestGraphs.TestOrientedGraphs.GetPathTestOrientedGraph(5);

			// Act
			var expectedVertices = graph.GetVertices().Select(v => (v.Name, v.DegreeIn, v.DegreeOut)).ToList();
            var expectedEdges = graph.GetEdges().Select(e => (e.VertexOut, e.VertexIn)).ToList();

			var jsonString = graph.SerializeToJson();
            var deserializedGraph = OrientedGraph<OrientedVertex, OrientedEdge>.DeserializeFromJson(jsonString);
            
            var actualVertices = deserializedGraph.GetVertices().Select(v => (v.Name, v.DegreeIn, v.DegreeOut)).ToList();
            var actualEdges = deserializedGraph.GetEdges().Select(e => (e.VertexOut, e.VertexIn)).ToList();

			// Assert
            Assert.AreEqual(graph.GetType(), deserializedGraph.GetType());
            Assert.AreEqual(expectedVertices.First().GetType(), actualVertices.First().GetType());
            Assert.AreEqual(expectedEdges.First().GetType(), actualEdges.First().GetType());

            CollectionAssert.AreEqual(expectedVertices, actualVertices);
            CollectionAssert.AreEqual(expectedEdges, actualEdges);
		}
	}
}
