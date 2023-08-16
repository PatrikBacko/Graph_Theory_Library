using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Edges;

namespace GraphLibrary.Graphs.Delegates
{
    /// <summary>
    /// Delegate for edge action, performs some action on TEdge.
    /// </summary>
    /// <typeparam name="TEdge">
    /// Type of edge on which action is performed, edge must be derived from <see cref="Edge"/> class.
    /// </typeparam>
    /// <param name="edge">
    /// Edge on which action is performed.
    /// </param>
    public delegate void EdgeAction<TEdge>(TEdge edge)
        where TEdge : Edge;
}
