using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricApp.Objects
{
    /// <summary>
    /// This class will maintain a mapping between rows and columns in the geometric grid of right triangles
    /// This allows for reverse lookup as well. 
    /// Rows A-F are mapped to integers 0-N and can be looked up in reverse
    /// Columns are mapped from sub-col values to the container. So [1,2]=>0 [3,4]=>1, [5,6]=>2 and so on and also allows
    /// for reverse lookup so that [0]=>[1,2], [1]=>[3,4] and so on
    /// </summary>
    public class GeometricMap
    {

        #region Fields

        public string RowValues;
        public int MaxColumnValue;

        private Dictionary<string, int> _rowHashForward;
        private Dictionary<int, string> _rowHashReverse;

        private Dictionary<int, int> _colHashForward;
        private Dictionary<int, int[]> _colHashReverse;

        #endregion

        #region Constructor

        public GeometricMap(string rowValues, int maxColumnValue)
        {
            this.RowValues = rowValues;
            this.MaxColumnValue = maxColumnValue;
            _rowHashForward = new Dictionary<string, int>();
            _rowHashReverse = new Dictionary<int, string>();
            _colHashForward = new Dictionary<int, int>();
            _colHashReverse = new Dictionary<int, int[]>();
        }

        #endregion

        #region Generate Methods
        public void Clear()
        {
            _rowHashForward.Clear();
            _rowHashReverse.Clear();
            _colHashForward.Clear();
            _colHashReverse.Clear();
        }

        public void Generate()
        {
            Clear();
            GenerateRowMappings();
            GenerateColMappings();
        }

        /// <summary>
        /// Generate row hash forward and reverse to map from A->0 B->1, and from 0->A, 1->B
        /// </summary>
        private void GenerateRowMappings()
        {
            for (int i = 0; i < RowValues.Length; i++)
            {
                var s = RowValues.Substring(i, 1);
                _rowHashForward.Add(s, i);
                _rowHashReverse.Add(i, s);
            }
        }

        /// <summary>
        /// Generate col hash so that we can determine the 0-based bounding index for a given value. Each 0-based
        /// column has a pair. Index 0 contains [1,2], index 1 contains [3,4] and so on
        /// </summary>
        private void GenerateColMappings()
        {
            /* Given a subset value [1,2], [3,4], [5,6]...for any one of those lookup the bounding column index
                _colHashForward[1] = 0
                _colHashForward[2] = 0
                _colHashForward[3] = 1
                _colHashForward[4] = 1

                We also create a reverse hash so that given a column index, we can get the column pair so that
                _colHashReverse[0] = {1,2}
                _colHashReverse[1] = {3,4}
             */
            var colIndex = 0;
            var list = new List<int>();

            for (int i = 1; i <= MaxColumnValue; i++)
            {
                list.Add(i);
                _colHashForward.Add(i, colIndex);
                if (i % 2 == 0)
                {
                    _colHashReverse.Add(colIndex, list.ToArray());
                    list.Clear();
                    colIndex++;
                }
            }
        }

        /// <summary>
        /// Generate list of keys that map to specific triangles. These are a sequence of strings that start with a letter than a number
        /// </summary>
        /// <returns></returns>
        public List<string> GenerateKeys()
        {
            var s = RowValues;
            var list = new List<string>();

            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 1; j <= MaxColumnValue; j++)
                {
                    list.Add($"{s.Substring(i, 1)}{j}");
                }
            }
            return list;
        }

        #endregion

        #region Converters

        public int ToRowIndex(string a)
        {
            if (_rowHashForward.ContainsKey(a))
            {
                return _rowHashForward[a];
            }
            else
            {
                return -1;
            }
        }

        public string FromRowIndex(int i)
        {
            if (_rowHashReverse.ContainsKey(i))
            {
                return _rowHashReverse[i];
            }
            else
            {
                return string.Empty;
            }
        }

        public int ToColIndex(int a)
        {
            if (_colHashForward.ContainsKey(a))
            {
                return _colHashForward[a];
            }
            else
            {
                return -1;
            }
        }

        public int[] FromColIndex(int i)
        {

            if (_colHashReverse.ContainsKey(i))
            {
                return _colHashReverse[i];
            }
            else
            {
                return new int[0];
            }
        }

        #endregion

    }
}
