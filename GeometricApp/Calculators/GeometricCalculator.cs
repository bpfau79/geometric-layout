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

        #region Constants

        private const int ASCII_START_CODE = 65;        // Start code of 'A'
        
        #endregion  

        #region Fields

        private int _length;                // Length of pixels
        private int _numColumns = 0;        // Number of top level columns that contains two-sub columns
        private int _numRows = 6;           // Max number of rows. IF this needs to be configured, then update constructor

        #endregion

        #region Constructor

        public GeometricCalculator(int length, int numColumns)
        {
            _length = length;
            _numColumns = numColumns;
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
                rowKey = RowIndexToAlpha(arr[0].Y / _length);

                // Given an x (col) value for the top left vertex, use the map to retrieve the two sub-column values. Divide by the length
                // since the map only stores 0-N values.
                var subColIndexes = FromColIndex(arr[0].X / _length);

                // If the first two vertices are the same, this is the left triangle...otherwise it is the right triangle
                colKey = arr[0].X == arr[1].X ? subColIndexes[0].ToString() : subColIndexes[1].ToString();
            }

            return $"{rowKey}{colKey}";
        }

        #endregion

        #region Converter Methods

        /// <summary>
        /// Given a character, get a 0-based row index value for that character. A=0, B=1, etc
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int AlphaToRowIndex(char c)
        {
            return (int)c - ASCII_START_CODE;
        }

        /// <summary>
        /// Given a 0-based row index, return the ASCII code equivalent where 0=A, 1=B, etc
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string RowIndexToAlpha(int index)
        {
            return Convert.ToChar(index + ASCII_START_CODE).ToString();
        }

        /// <summary>
        /// Given a 0-based sub column index, return the containing 0-based index. [1 or 2]=>0, [3 or 4]=>1, [5 or 6]=>2
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public int ToColIndex(int a)
        {
            if (a % 2 == 0)
            {
                return (a - 2) / 2;
            }
            else
            {
                return (a - 1) / 2;
            }
        }

        /// <summary>
        /// Given a 0-based outer column index, get the sub column values. 0=>[1,2]  1=>[3,4]  2=>[5,6] etc
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int[] FromColIndex(int i)
        {
            int[] arr = new int[2];
            arr[0] = (i * 2) + 1;
            arr[1] = (i * 2) + 2;
            return arr;
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

            var rowIndex = AlphaToRowIndex(Convert.ToChar(rowKey));

            var intVal = 0;
            int colVal = int.TryParse(colKey, out intVal) ? intVal : -1;

            var colIndex = ToColIndex(colVal);

            return new BoundingBoxModel(colIndex * _length, rowIndex * _length, colVal);
        }

        /// <summary>
        /// Generate list of keys that map to specific triangles. These are a sequence of strings that start with a letter than a number
        /// </summary>
        /// <returns></returns>
        public List<string> GenerateKeys()
        {
            var list = new List<string>();

            for (int i = 0; i < _numRows; i++)
            {
                for (int j = 1; j <= _numColumns; j++)
                {
                    list.Add($"{Convert.ToChar(i + ASCII_START_CODE).ToString()}{j}");
                }
            }
            return list;
        }

        #endregion

    }
}
