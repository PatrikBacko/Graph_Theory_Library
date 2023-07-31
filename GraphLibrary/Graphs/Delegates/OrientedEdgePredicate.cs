using GraphLibrary.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Graphs.Delegates
{
    public delegate bool OrientedEdgePredicate<TEdge>(TEdge edge) where TEdge : OrientedEdge;
}
