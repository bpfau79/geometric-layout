using GeometricApp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricApp.Services
{

    /// <summary>
    /// Geometric calculator that will calculate vertices of a triangle for a given key (A1, A2, ....)
    /// This will also be able to accept a list of vertices and calculate the key for a triangle
    /// </summary>
    public class GeometricCalculator
    {

        #region Fields

        private GeometricMap _map;
        private int _length;

        #endregion

        #region Constructor

        public GeometricCalculator(GeometricMap map, int length)
        {
            _map = map;
            _length = length;
        }

        #endregion

        #region Calculate Methods

        public List<Vertex> CalculateVerticesFromKey(string key)
        {

            var list = new List<Vertex>();

            if (!string.IsNullOrWhiteSpace(key) && key.Length >= 2)
            {
                // Calculate the bounding box model and then based on the key, generate the left or right triangle
                // within the bounding box
                var boundingBox = CreateBoundingBoxModel(key);
                var vertices = CreateTriangleVertices(boundingBox);
                list.AddRange(vertices.ToList());
            }

            return list;
        }

        /// <summary>
        /// Given a list of vertices, use the map class to reverse lookup the triangle key
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        public string CalculateKeyFromVertices(List<Vertex> vertices)
        {

            var rowKey = "";
            var colKey = "";

            // We order so that the first element in the array is the top left
            var arr = vertices.OrderBy(v => v.X).ThenBy(v => v.Y).ToArray();

            if (vertices.Count > 0)
            {
                // Given a y (row) value for the top left vertex, map it back to the corresponding alpha character. Divide by the length
                // since the map current only stores 0-N values.
                rowKey = _map.FromRowIndex(arr[0].Y / _length);

                // Given an x (col) value for the top left vertex, use the map to retrieve the two sub-column values. Divide by the length
                // since the map only stores 0-N values.
                var subColIndexes = _map.FromColIndex(arr[0].X / _length);

                // If the first two vertices are the same, this is the left triangle...otherwise it is the right triangle
                colKey = arr[0].X == arr[1].X ? subColIndexes[0].ToString() : subColIndexes[1].ToString();
            }

            return $"{rowKey}{colKey}";
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Given a bounding box model, return the vertices for either the bottom left or top right triangle
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Vertex[] CreateTriangleVertices(BoundingBoxModel model)
        {
            var arr = new Vertex[3];
            var v = model.TopLeft;

            arr[0] = new Vertex(v.X, v.Y);                         // P1 Top Left

            if (model.IsTopRightTriangle)
            {
                arr[1] = new Vertex(v.X + _length, v.Y);           // P2 Top Right
            }
            else
            {
                arr[1] = new Vertex(v.X, v.Y + _length);           // P3 Bottom Left          
            }

            arr[2] = new Vertex(v.X + _length, v.Y + _length);     // P4 Bottom Right

            return arr;
        }

        /// <summary>
        /// Given a key (A1, A2, A3, etc), create a bounding box model that surrounds the triangle for the given key.
        /// The sub-column value determines if this will be for the bottom left or top right triangle
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private BoundingBoxModel CreateBoundingBoxModel(string key)
        {
            var rowKey = key.Substring(0, 1);
            var colKey = key.Substring(1);

            var rowIndex = _map.ToRowIndex(rowKey);

            var intVal = 0;
            int colVal = int.TryParse(colKey, out intVal) ? intVal : -1;

            var colIndex = _map.ToColIndex(colVal);

            return new BoundingBoxModel(colIndex * _length, rowIndex * _length, colVal);
        }

        #endregion  

    }
}
