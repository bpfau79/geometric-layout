using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricApp.Objects
{

    /// <summary>
    /// Around each triangle is a bounding box. This model will define the top left vertex of a bounding box
    /// as well as information on which column (lower left or upper right) for the triangle. This is done this way so that
    /// if any other 'types' of shapes need to be found we can start with the bounding box
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
