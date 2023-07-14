using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphThoeryLibrary.Vertices
{
    public struct Vertex : IVertex
    {
        public string Name { get; init; }

        //internal int Index { get; set; }

        public Vertex(string Name)
        {
            this.Name = Name;
            //Index = -1;
        }

        public override bool Equals(object other)
        {
            if (other is IVertex)
                return Name == ((IVertex)other).Name;
            return false;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
