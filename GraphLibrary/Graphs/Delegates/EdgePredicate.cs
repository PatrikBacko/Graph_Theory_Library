using GraphLibrary.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Graphs.Delegates
{
    /// <summary>
    /// Delegate for edge predicate, which returns true if TEdge satisfies some condition.
    /// </summary>
    /// <typeparam name="TEdge">
    /// Type of edge, must be derived from <see cref="Edge"/> class.
    /// </typeparam>
    /// <param name="edge">
    /// TEgde on which predicate is performed.
    /// </param>
    /// <returns>
    /// bool value, which is true if TEdge satisfies some condition, false otherwise.
    /// </returns>
    public delegate bool EdgePredicate<TEdge>(TEdge edge) 
        where TEdge : Edge;
}
