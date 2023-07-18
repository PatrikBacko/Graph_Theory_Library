using System.Runtime.CompilerServices;
using GraphLibrary.Edges;
using GraphLibrary.Vertices;

namespace GraphLibrary.Extensions.StringExtensions
{
    static public class StringExtensions
    {
        static public VertexName ToVertexName(this string srt)
        {
            return new VertexName(srt);
        }

        static public OrientedVertex ToOrientedVertex(this string srt) {
            return new OrientedVertex(new VertexName(srt));
        }

        static public OrientedEdge ToOrientedEdge(this string vertexOut, string vertexIn) { 
            return new OrientedEdge(vertexOut.ToVertexName(), vertexIn.ToVertexName());
        }
    }
}