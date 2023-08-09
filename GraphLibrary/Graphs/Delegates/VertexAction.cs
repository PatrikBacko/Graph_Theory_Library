using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Vertices;

namespace GraphLibrary.Graphs.Delegates
{
    public delegate void VertexAction<TVertex>(TVertex vertex) where TVertex : Vertex;
}
