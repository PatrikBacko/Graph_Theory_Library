using GraphLibrary.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests.TestGraphs
{
    public static class TestOrientedGraphs
    {
        public static IOrientedGraph<OrientedVertex, OrientedEdge> ClearTestOrientedGraph
            => OrientedGraph<OrientedVertex, OrientedEdge>.Create();
        public static IOrientedGraph<OrientedVertex, OrientedEdge> VertexTestOrientedGraph1
            => OrientedGraph<OrientedVertex, OrientedEdge>.Create()
                    .AddVertex(new OrientedVertex("0"))
                    .AddVertex(new OrientedVertex("1"))
                    .AddVertex(new OrientedVertex("2"));

        public static IOrientedGraph<OrientedVertex, OrientedEdge> GetVertexTestOrientedGraph(int vertexCount)
        {
            if (vertexCount < 0)
                throw new ArgumentException("Vertex count must be positive");

            var graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
            for (int i = 0; i < vertexCount; i++)
                graph.AddVertex(new OrientedVertex(i.ToString()));
            return graph;
        }
        public static IOrientedGraph<OrientedVertex, OrientedEdge> GetCompleteTestOrientedGraph(int vertexCount)
        {
            if (vertexCount < 0)
                throw new ArgumentException("Vertex count must be positive");

            var graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
            for (int i = 0; i < vertexCount; i++)
                graph.AddVertex(new OrientedVertex(i.ToString()));

            for (int i = 0; i < vertexCount; i++)
                for (int j = 0; j < vertexCount; j++)
                    if (i != j)
                        graph.AddEdge(new OrientedEdge(i.ToString(), j.ToString()));

            return graph;
        }
        public static IOrientedGraph<OrientedVertex, OrientedEdge> GetRandomTestOrientedGraph(int vertexCount, int edgeCount)
        {
            if (vertexCount < 0)
                throw new ArgumentException("Vertex count must be positive");
            if (edgeCount > vertexCount * (vertexCount - 1))
                throw new ArgumentException("Too many edges for this graph");
            if (edgeCount < 0)
                throw new ArgumentException("Edge count must be positive");

            var graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
            for (int i = 0; i < vertexCount; i++)
                graph.AddVertex(new OrientedVertex(i.ToString()));

            var random = new Random();
            for (int i = 0; i < edgeCount; i++)
            {
                var from = random.Next(0, vertexCount).ToString();
                var to = random.Next(0, vertexCount).ToString();
                if (from == to || graph.IsEdge(from, to))
                    i--;
                else
                    graph.AddEdge(new OrientedEdge(from, to));
            }

            return graph;
        }

        public static IOrientedGraph<OrientedVertex, OrientedEdge> GetRandomTestOrientedGraph(int vertexCount)
        {
            if (vertexCount < 0)
                throw new ArgumentException("Vertex count must be positive");

            int maxEdgeCount = vertexCount * (vertexCount - 1);

            var graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
            for (int i = 0; i < vertexCount; i++)
                graph.AddVertex(new OrientedVertex(i.ToString()));

            var random = new Random();
            var edgeCount = random.Next(0, maxEdgeCount);
            for (int i = 0; i < edgeCount; i++)
            {
                var from = random.Next(0, vertexCount).ToString();
                var to = random.Next(0, vertexCount).ToString();
                if (from == to || graph.IsEdge(from, to))
                    i--;
                else
                    graph.AddEdge(new OrientedEdge(from, to));
            }

            return graph;
        }

        public static IOrientedGraph<OrientedVertex, OrientedEdge> GetCycleTestOrientedGraph(int vertexCount)
        {
            if (vertexCount < 0)
                throw new ArgumentException("Vertex count must be positive");

            var graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
            for (int i = 0; i < vertexCount; i++)
                graph.AddVertex(new OrientedVertex(i.ToString()));

            for (int i = 0; i < vertexCount; i++)
                graph.AddEdge(new OrientedEdge(i.ToString(), ((i + 1) % vertexCount).ToString()));

            return graph;
        }

        public static IOrientedGraph<OrientedVertex, OrientedEdge> GetPathTestOrientedGraph(int vertexCount)
        {
            if (vertexCount < 0)
                throw new ArgumentException("Vertex count must be positive");

            var graph = OrientedGraph<OrientedVertex, OrientedEdge>.Create();
            for (int i = 0; i < vertexCount; i++)
                graph.AddVertex(new OrientedVertex(i.ToString()));

            for (int i = 0; i < vertexCount - 1; i++)
                graph.AddEdge(new OrientedEdge(i.ToString(), (i + 1).ToString()));

            return graph;
        }

        public static IOrientedGraph<OrientedVertex, OrientedEdge> DagTestOrientedGraph1
            => OrientedGraph<OrientedVertex, OrientedEdge>.Create()
                .AddVertex(new OrientedVertex("0"))
                .AddVertex(new OrientedVertex("1"))
                .AddVertex(new OrientedVertex("2"))
                .AddVertex(new OrientedVertex("3"))
                .AddVertex(new OrientedVertex("4"))
                .AddVertex(new OrientedVertex("5"))
                .AddEdge(new OrientedEdge("2", "3"))
                .AddEdge(new OrientedEdge("3", "1"))
                .AddEdge(new OrientedEdge("4", "0"))
                .AddEdge(new OrientedEdge("4", "1"))
                .AddEdge(new OrientedEdge("5", "0"))
                .AddEdge(new OrientedEdge("5", "2"));
        public static IOrientedGraph<OrientedVertex, OrientedEdge> DagTestOrientedGraph2
            => OrientedGraph<OrientedVertex, OrientedEdge>.Create()
                .AddVertex(new OrientedVertex("0"))
                .AddVertex(new OrientedVertex("1"))
                .AddVertex(new OrientedVertex("2"))
                .AddVertex(new OrientedVertex("3"))
                .AddEdge(new OrientedEdge("0", "1"))
                .AddEdge(new OrientedEdge("0", "2"))
                .AddEdge(new OrientedEdge("1", "3"))
                .AddEdge(new OrientedEdge("2", "3"));

        public static IOrientedGraph<OrientedVertex, OrientedEdge> NotDagTestOrientedGraph1
            => OrientedGraph<OrientedVertex, OrientedEdge>.Create()
                .AddVertex(new OrientedVertex("0"))
                .AddVertex(new OrientedVertex("1"))
                .AddVertex(new OrientedVertex("2"))
                .AddVertex(new OrientedVertex("3"))
                .AddVertex(new OrientedVertex("4"))
                .AddVertex(new OrientedVertex("5"))
                .AddEdge(new OrientedEdge("2", "3"))
                .AddEdge(new OrientedEdge("3", "1"))
                .AddEdge(new OrientedEdge("4", "0"))
                .AddEdge(new OrientedEdge("4", "1"))
                .AddEdge(new OrientedEdge("5", "0"))
                .AddEdge(new OrientedEdge("5", "2"))
                .AddEdge(new OrientedEdge("3", "5"));
    }
}
