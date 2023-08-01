using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests.GraphTests
{
	[TestClass]
	public class AddRemoveTests
	{
		[TestMethod]
		public void AddOneVertexTest1(){
			var graph = new OrientedGraph<OrientedVertex, OrientedEdge>();
			graph.AddVertex(new OrientedVertex("0"));


		}
	}
}
