using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Vertices;

namespace GraphLibrary.Graphs.Delegates
{
    /// <summary>
    /// Delegate for vertex action, performs some action on TVertex.
    /// </summary>
    /// <typeparam name="TVertex">
    /// Type of vertex on which action is performed, vertex must be derived from <see cref="Vertex"/> class.
    /// </typeparam>
    /// <param name="vertex">
    /// TVertex on which action is performed.
    /// </param>
    public delegate void VertexAction<TVertex>(TVertex vertex) where TVertex : Vertex;
}
