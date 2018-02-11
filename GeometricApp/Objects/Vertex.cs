using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricApp.Objects
{
    /// <summary>
    /// Defines a vertex that has X and Y coordinates
    /// </summary>
    public class Vertex
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Vertex(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return $"[{X},{Y}]";
        }
    }
}
