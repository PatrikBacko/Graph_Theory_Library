using GraphLibrary.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Graphs.Delegates
{
    /// <summary>
    /// Delegate for vertex predicate, which returns true if TVertex satisfies some condition.
    /// </summary>
    /// <typeparam name="TVertex">
    /// Type of vertex, which must be derived from <see cref="Vertex"/> class.
    /// </typeparam>
    /// <param name="vertex">
    /// TVertex on which predicate is performed.
    /// </param>
    /// <returns>
    /// bool value, which is true if TVertex satisfies some condition, false otherwise.
    /// </returns>
    public delegate bool VertexPredicate<TVertex>(TVertex vertex) 
        where TVertex : Vertex;
}
