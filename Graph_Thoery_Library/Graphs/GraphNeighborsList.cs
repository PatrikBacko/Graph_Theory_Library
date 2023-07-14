using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphThoeryLibrary.Edges;
using GraphThoeryLibrary.Vertices;


namespace GraphThoeryLibrary.Graphs
{
	
    public class GraphNeighborsList<TVertexType> : IOrientedGraph where TVertexType : IVertex
    {
        protected Dictionary<TVertexType, HashSet<TVertexType>> NeighboursList { get; set; }

        public GraphNeighborsList()
		{
			NeighboursList = new Dictionary<TVertexType, HashSet<TVertexType>>();
		}
        public IEnumerable<IVertex> GetVertices()
		{
			foreach (var vertex in NeighboursList.Keys)
				yield return vertex;
		}
		public IEnumerable<IOrientedEdge> GetEdges(){
			foreach (var vertex in NeighboursList.Keys){
				foreach (var neighbour in NeighboursList[vertex]){
					yield return new OrientedEdge(vertex, neighbour);
				}
			}	
		}
		public IEnumerable<IVertex> GetInAdjacentVertices(IVertex vertex){
			foreach (var v in NeighboursList.Keys){ 
				if (NeighboursList[v].Contains((TVertexType)vertex)) 
					yield return v; 
			}
		}
		public IEnumerable<IVertex> GetOutAdjacentVertices(IVertex vertex)
		{
			foreach (var v in NeighboursList[(TVertexType)vertex])
				yield return v;
		}
		//public IEnumerable<Vertex> GetOutAdjacentVertices(Vertex vertex){
		//	return NeighboursList[vertex];
		//}
		public IEnumerable<IOrientedEdge> GetInEdges(IVertex vertex){
			foreach (var v in NeighboursList.Keys){
				if (NeighboursList[v].Contains((TVertexType)vertex)) 
					yield return new OrientedEdge(v, vertex); 
			}
		}
		public IEnumerable<IOrientedEdge> GetOutEdges(IVertex vertex){
			foreach (var v in NeighboursList[(TVertexType)vertex]) 
				yield return new OrientedEdge(vertex, v);
		}

		public int GetInDegree(IVertex vertex) => GetInAdjacentVertices(vertex).Count();
		public int GetOutDegree(IVertex vertex) => GetOutAdjacentVertices(vertex).Count();
		public int GetVertexCount() => NeighboursList.Count;
		public int GetEdgeCount() => GetEdges().Count();
		public bool IsEdge(IVertex vertex1, IVertex vertex2) => NeighboursList[(TVertexType)vertex1].Contains((TVertexType)vertex2);

		public IOrientedGraph AddVertex(IVertex vertex){
			NeighboursList[(TVertexType)vertex] = new HashSet<TVertexType>();
			return this;
		}
		public IOrientedGraph AddEdge(IOrientedEdge edge){
			NeighboursList[(TVertexType)edge.InVertex].Add((TVertexType)edge.OutVertex);
			return this;
		}

		public IOrientedGraph AddEdge(IVertex vertex1, IVertex vertex2){
			NeighboursList[(TVertexType)vertex1].Add((TVertexType)vertex2);
			return this;
		}

		public IOrientedGraph RemoveVertex(IVertex vertex){
			NeighboursList.Remove((TVertexType)vertex);
			foreach (var v in NeighboursList.Keys){
				NeighboursList[v].Remove((TVertexType)vertex);
			}
			return this;
		}
		public IOrientedGraph RemoveEdge(IOrientedEdge edge){
			NeighboursList[(TVertexType)edge.InVertex].Remove((TVertexType)edge.OutVertex);
			return this;
		}

		public IOrientedGraph RemoveEdge(IVertex vertex1, IVertex vertex2){
			NeighboursList[(TVertexType)vertex1].Remove((TVertexType)vertex2);
			return this;
		}
    }
}
