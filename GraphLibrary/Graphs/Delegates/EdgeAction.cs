using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary.Edges;

namespace GraphLibrary.Graphs.Delegates
{
    public delegate void EdgeAction<TEdge>(TEdge edge)
        where TEdge : Edge;
}
