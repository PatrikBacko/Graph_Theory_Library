using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests.SerializationTests.JsonSerializationTests
{
	/// <summary>
	/// <b> Tests for json serialization for WeightedOrientedGraphs </b>
	/// </summary>
	[TestClass]
	public class JsonSerializationWeightedOrientedGraphTests
	{
		[TestMethod]
		public void JsonSerializationTest1()
		{
			// Arrange
			int i = 0;
			int j = 1;
			var graph = TestGraphs.TestWeightedOrientedGraphs.GetPathTestWeightedOrientedGraph(5, () => i++, () => j = j*2);

			// Act
			var expectedVertices = graph.GetVertices().Select(v => (v.Name, v.DegreeIn, v.DegreeOut, v.Weight)).ToList();
			var expectedEdges = graph.GetEdges().Select(e => (e.VertexOut, e.VertexIn, e.Weight)).ToList();

			var jsonString = graph.SerializeToJson();
			var deserializedGraph = WeightedOrientedGraph<WeightedOrientedVertex<int>, WeightedOrientedEdge<int>, int>.DeserializeFromJson(jsonString);

			var actualVertices = deserializedGraph.GetVertices().Select(v => (v.Name, v.DegreeIn, v.DegreeOut, v.Weight)).ToList();
			var actualEdges = deserializedGraph.GetEdges().Select(e => (e.VertexOut, e.VertexIn, e.Weight)).ToList();

			// Assert
			Assert.AreEqual(graph.GetType(), deserializedGraph.GetType());
			Assert.AreEqual(expectedVertices.First().GetType(), actualVertices.First().GetType());
			Assert.AreEqual(expectedEdges.First().GetType(), actualEdges.First().GetType());

			CollectionAssert.AreEqual(expectedVertices, actualVertices);
			CollectionAssert.AreEqual(expectedEdges, actualEdges);
		}
	}
}
