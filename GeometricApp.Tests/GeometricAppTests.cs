using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeometricApp.Tests
{
    [TestClass]
    public class GeometricAppTests
    {

        #region Geometric Map Tests
        [TestMethod]
        public void GeometricMapTest_When_Initializing_With_Empty_String_And_0_Columns_Then_No_Exception_Is_Thrown_When_Calling_Generate()
        {
            // Given a geometric test map with empty row values and 0 column then no
            var map = new GeometricApp.Objects.GeometricMap("", 0);

            // When calling generate
            map.Generate();

            // Then no exception should be thrown
        }

        [TestMethod]
        public void GeometricMapTest_When_Initializing_With_3_Rows_and_3_Columns_Then_Resulting_Map_Matches_Expected()
        {
            var expectedRowIndexes = new int[] { 0, 1, 2 };
            var expectedColSubIndexes = new List<int[]>()
            {
                new int[] { 1, 2 },
                new int[] { 3, 4 }
            };

            var str = "ABC";
            var colCount = 4;

            // Given a geometric test map with 3 rows and columns
            var map = new GeometricApp.Objects.GeometricMap(str, colCount);

            // When calling generate
            map.Generate();

            // Then the resulting map matches the expected row
            for(int i = 0; i < str.Length - 1; i++)
            {
                Assert.AreEqual(expectedRowIndexes[i], map.ToRowIndex(str.Substring(i, 1)));   
            }       

            // And resulting map matches the expected column sub indexes
            for(int i = 0; i < expectedColSubIndexes.Count; i++)
            {
                var mapSubCols = map.FromColIndex(i);
                var arr = expectedColSubIndexes[i];
                Assert.AreEqual(mapSubCols[0], arr[0]);
                Assert.AreEqual(mapSubCols[1], arr[1]);
            }

        }

        #endregion

        #region Geometric Calculator Tests

        [TestMethod]
        public void GeometricCalculator_When_Calculating_Vertices_From_Key_Then_Results_Match_Expected()
        {
            var expected = new List<int[]>
            {
                new int[] { 0, 10},
                new int[] { 10, 10},
                new int[] { 10, 20},
            };

            var str = "ABC";
            var colCount = 4;

            var map = new GeometricApp.Objects.GeometricMap(str, colCount);
            map.Generate();

            // Given a calculator
            var calc = new GeometricApp.Services.GeometricCalculator(map, 10);

            // When calling calculate vertices from key B2
            var arr = calc.CalculateVerticesFromKey("B2").ToArray();

            // Then the results should match expected
            for(int i = 0; i < expected.Count; i++)
            {
                var item = expected[i];

                Assert.AreEqual(item[0], arr[i].X);
                Assert.AreEqual(item[1], arr[i].Y);
            }
        }

        #endregion

    }
}
