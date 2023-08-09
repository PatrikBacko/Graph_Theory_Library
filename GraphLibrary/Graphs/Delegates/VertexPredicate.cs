using GraphLibrary.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Graphs.Delegates
{
    public delegate bool VertexPredicate<TVertex>(TVertex vertex) where TVertex : Vertex;
}
