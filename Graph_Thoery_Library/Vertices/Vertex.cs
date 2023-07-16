using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphThoeryLibrary.Vertices
{

    public interface IOrientedVertex : IVertex
    {
        //public int DegreeIn { get; }
        //public int DegreeOut { get; }
    }

    public abstract class Vertex : IVertex
    {
        public string Name { get; init; }

        public Vertex(string Name)
        {
            this.Name = Name;
        }

        public override bool Equals(object? other)
        {
            if (other is Vertex)
                return Name == ((Vertex)other).Name;
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
    //public class UnorientedVertex : Vertex {
    //	public int Degree { get; internal set; }

    //	public UnorientedVertex(string Name, int Degree) : base(Name)
    //	{
    //		this.Degree = Degree;
    //	}
    //}
    public class OrientedVertex : Vertex, IOrientedVertex
	{
        //public int DegreeIn { get; internal set; }
        //public int DegreeOut { get; internal set; }


        public OrientedVertex(string Name) : base(Name)
        {
            //DegreeIn = -1;
            //DegreeOut = -1;
        }
    }
}
