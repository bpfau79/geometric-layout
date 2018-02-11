using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricApp.Objects
{

    /// <summary>
    /// Around each triangle is a bounding box. This model will define the top left vertex of a bounding box
    /// as well as information on which column (lower left or upper right) for the triangle
    /// </summary>
    public class BoundingBoxModel
    {

        public Vertex TopLeft { get; private set; }
        public int ColumnSubVal { get; private set; }

        public BoundingBoxModel(int x, int y, int colSubVal) {
            TopLeft = new Vertex(x, y);
            this.ColumnSubVal = colSubVal;
        }

        public bool IsTopRightTriangle
        {
            get { return ColumnSubVal % 2 == 0; }
        }

    }
}
