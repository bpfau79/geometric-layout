using System;
using System.Collections.Generic;
using GeometricApp.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeometricApp.Tests
{
    [TestClass]
    public class GeometricAppTests
    {

        #region Geometric Calculator Tests

        [TestMethod]
        public void GeometricCalculator_When_Calculating_Vertices_From_Key_Then_Results_Match_Expected()
        {
            var expectedResults = CreateTestValues();

            // Given a calculator
            var calc = new GeometricApp.Services.GeometricCalculator(length: 10, numColumns: 12);

            foreach (var kvp in expectedResults)
            {
                // When calling calculate vertices from key
                var arr = calc.CalculateVerticesFromKey(kvp.Key).ToArray();

                // Then the results should match expected
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    var item = kvp.Value[i];

                    Assert.AreEqual(item[0], arr[i].X);
                    Assert.AreEqual(item[1], arr[i].Y);
                }
            }
        }

        [TestMethod]
        public void GeometricCalculator_When_Calculating_Key_From_Vertices_Then_Results_Match_Expected()
        {
            var expectedResults = CreateTestValues();

            // Given a calculator
            var calc = new GeometricApp.Services.GeometricCalculator(length: 10, numColumns: 12);

            foreach (var kvp in expectedResults)
            {
                // When calling calculate key from vertices
                var list = CreateVertexList(kvp.Value);
                var keyFromVertices = calc.CalculateKeyFromVertices(list);

                // Then the calculated key matches expected;
                Assert.AreEqual(kvp.Key, keyFromVertices);
            }
        }

        #endregion

        #region Helper Function

        private List<Vertex> CreateVertexList(List<int[]> list)
        {
            var result = new List<Vertex>();

            foreach (var item in list)
            {
                result.Add(new Vertex(item[0], item[1]));
            }
            return result;
        }

        private Dictionary<string, List<int[]>> CreateTestValues()
        {
            var dict = new Dictionary<string, List<int[]>>()
            {
                { "A1", new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 10 }, new int[] { 10, 10 } } },
                { "A2", new List<int[]> { new int[] { 0, 0 }, new int[] { 10, 0 }, new int[] { 10, 10 } } },
                { "A3", new List<int[]> { new int[] { 10, 0 }, new int[] { 10, 10 }, new int[] { 20, 10 } } },
                { "A4", new List<int[]> { new int[] { 10, 0 }, new int[] { 20, 0 }, new int[] { 20, 10 } } },
                { "A5", new List<int[]> { new int[] { 20, 0 }, new int[] { 20, 10 }, new int[] { 30, 10 } } },
                { "A6", new List<int[]> { new int[] { 20, 0 }, new int[] { 30, 0 }, new int[] { 30, 10 } } },
                { "A7", new List<int[]> { new int[] { 30, 0 }, new int[] { 30, 10 }, new int[] { 40, 10 } } },
                { "A8", new List<int[]> { new int[] { 30, 0 }, new int[] { 40, 0 }, new int[] { 40, 10 } } },
                { "A9", new List<int[]> { new int[] { 40, 0 }, new int[] { 40, 10 }, new int[] { 50, 10 } } },
                { "A10", new List<int[]> { new int[] { 40, 0 }, new int[] { 50, 0 }, new int[] { 50, 10 } } },
                { "A11", new List<int[]> { new int[] { 50, 0 }, new int[] { 50, 10 }, new int[] { 60, 10 } } },
                { "A12", new List<int[]> { new int[] { 50, 0 }, new int[] { 60, 0 }, new int[] { 60, 10 } } },
                { "B1", new List<int[]> { new int[] { 0, 10 }, new int[] { 0, 20 }, new int[] { 10, 20 } } },
                { "B2", new List<int[]> { new int[] { 0, 10 }, new int[] { 10, 10 }, new int[] { 10, 20 } } },
                { "B3", new List<int[]> { new int[] { 10, 10 }, new int[] { 10, 20 }, new int[] { 20, 20 } } },
                { "B4", new List<int[]> { new int[] { 10, 10 }, new int[] { 20, 10 }, new int[] { 20, 20 } } },
                { "B5", new List<int[]> { new int[] { 20, 10 }, new int[] { 20, 20 }, new int[] { 30, 20 } } },
                { "B6", new List<int[]> { new int[] { 20, 10 }, new int[] { 30, 10 }, new int[] { 30, 20 } } },
                { "B7", new List<int[]> { new int[] { 30, 10 }, new int[] { 30, 20 }, new int[] { 40, 20 } } },
                { "B8", new List<int[]> { new int[] { 30, 10 }, new int[] { 40, 10 }, new int[] { 40, 20 } } },
                { "B9", new List<int[]> { new int[] { 40, 10 }, new int[] { 40, 20 }, new int[] { 50, 20 } } },
                { "B10", new List<int[]> { new int[] { 40, 10 }, new int[] { 50, 10 }, new int[] { 50, 20 } } },
                { "B11", new List<int[]> { new int[] { 50, 10 }, new int[] { 50, 20 }, new int[] { 60, 20 } } },
                { "B12", new List<int[]> { new int[] { 50, 10 }, new int[] { 60, 10 }, new int[] { 60, 20 } } },
                { "C1", new List<int[]> { new int[] { 0, 20 }, new int[] { 0, 30 }, new int[] { 10, 30 } } },
                { "C2", new List<int[]> { new int[] { 0, 20 }, new int[] { 10, 20 }, new int[] { 10, 30 } } },
                { "C3", new List<int[]> { new int[] { 10, 20 }, new int[] { 10, 30 }, new int[] { 20, 30 } } },
                { "C4", new List<int[]> { new int[] { 10, 20 }, new int[] { 20, 20 }, new int[] { 20, 30 } } },
                { "C5", new List<int[]> { new int[] { 20, 20 }, new int[] { 20, 30 }, new int[] { 30, 30 } } },
                { "C6", new List<int[]> { new int[] { 20, 20 }, new int[] { 30, 20 }, new int[] { 30, 30 } } },
                { "C7", new List<int[]> { new int[] { 30, 20 }, new int[] { 30, 30 }, new int[] { 40, 30 } } },
                { "C8", new List<int[]> { new int[] { 30, 20 }, new int[] { 40, 20 }, new int[] { 40, 30 } } },
                { "C9", new List<int[]> { new int[] { 40, 20 }, new int[] { 40, 30 }, new int[] { 50, 30 } } },
                { "C10", new List<int[]> { new int[] { 40, 20 }, new int[] { 50, 20 }, new int[] { 50, 30 } } },
                { "C11", new List<int[]> { new int[] { 50, 20 }, new int[] { 50, 30 }, new int[] { 60, 30 } } },
                { "C12", new List<int[]> { new int[] { 50, 20 }, new int[] { 60, 20 }, new int[] { 60, 30 } } },
                { "D1", new List<int[]> { new int[] { 0, 30 }, new int[] { 0, 40 }, new int[] { 10, 40 } } },
                { "D2", new List<int[]> { new int[] { 0, 30 }, new int[] { 10, 30 }, new int[] { 10, 40 } } },
                { "D3", new List<int[]> { new int[] { 10, 30 }, new int[] { 10, 40 }, new int[] { 20, 40 } } },
                { "D4", new List<int[]> { new int[] { 10, 30 }, new int[] { 20, 30 }, new int[] { 20, 40 } } },
                { "D5", new List<int[]> { new int[] { 20, 30 }, new int[] { 20, 40 }, new int[] { 30, 40 } } },
                { "D6", new List<int[]> { new int[] { 20, 30 }, new int[] { 30, 30 }, new int[] { 30, 40 } } },
                { "D7", new List<int[]> { new int[] { 30, 30 }, new int[] { 30, 40 }, new int[] { 40, 40 } } },
                { "D8", new List<int[]> { new int[] { 30, 30 }, new int[] { 40, 30 }, new int[] { 40, 40 } } },
                { "D9", new List<int[]> { new int[] { 40, 30 }, new int[] { 40, 40 }, new int[] { 50, 40 } } },
                { "D10", new List<int[]> { new int[] { 40, 30 }, new int[] { 50, 30 }, new int[] { 50, 40 } } },
                { "D11", new List<int[]> { new int[] { 50, 30 }, new int[] { 50, 40 }, new int[] { 60, 40 } } },
                { "D12", new List<int[]> { new int[] { 50, 30 }, new int[] { 60, 30 }, new int[] { 60, 40 } } },
                { "E1", new List<int[]> { new int[] { 0, 40 }, new int[] { 0, 50 }, new int[] { 10, 50 } } },
                { "E2", new List<int[]> { new int[] { 0, 40 }, new int[] { 10, 40 }, new int[] { 10, 50 } } },
                { "E3", new List<int[]> { new int[] { 10, 40 }, new int[] { 10, 50 }, new int[] { 20, 50 } } },
                { "E4", new List<int[]> { new int[] { 10, 40 }, new int[] { 20, 40 }, new int[] { 20, 50 } } },
                { "E5", new List<int[]> { new int[] { 20, 40 }, new int[] { 20, 50 }, new int[] { 30, 50 } } },
                { "E6", new List<int[]> { new int[] { 20, 40 }, new int[] { 30, 40 }, new int[] { 30, 50 } } },
                { "E7", new List<int[]> { new int[] { 30, 40 }, new int[] { 30, 50 }, new int[] { 40, 50 } } },
                { "E8", new List<int[]> { new int[] { 30, 40 }, new int[] { 40, 40 }, new int[] { 40, 50 } } },
                { "E9", new List<int[]> { new int[] { 40, 40 }, new int[] { 40, 50 }, new int[] { 50, 50 } } },
                { "E10", new List<int[]> { new int[] { 40, 40 }, new int[] { 50, 40 }, new int[] { 50, 50 } } },
                { "E11", new List<int[]> { new int[] { 50, 40 }, new int[] { 50, 50 }, new int[] { 60, 50 } } },
                { "E12", new List<int[]> { new int[] { 50, 40 }, new int[] { 60, 40 }, new int[] { 60, 50 } } },
                { "F1", new List<int[]> { new int[] { 0, 50 }, new int[] { 0, 60 }, new int[] { 10, 60 } } },
                { "F2", new List<int[]> { new int[] { 0, 50 }, new int[] { 10, 50 }, new int[] { 10, 60 } } },
                { "F3", new List<int[]> { new int[] { 10, 50 }, new int[] { 10, 60 }, new int[] { 20, 60 } } },
                { "F4", new List<int[]> { new int[] { 10, 50 }, new int[] { 20, 50 }, new int[] { 20, 60 } } },
                { "F5", new List<int[]> { new int[] { 20, 50 }, new int[] { 20, 60 }, new int[] { 30, 60 } } },
                { "F6", new List<int[]> { new int[] { 20, 50 }, new int[] { 30, 50 }, new int[] { 30, 60 } } },
                { "F7", new List<int[]> { new int[] { 30, 50 }, new int[] { 30, 60 }, new int[] { 40, 60 } } },
                { "F8", new List<int[]> { new int[] { 30, 50 }, new int[] { 40, 50 }, new int[] { 40, 60 } } },
                { "F9", new List<int[]> { new int[] { 40, 50 }, new int[] { 40, 60 }, new int[] { 50, 60 } } },
                { "F10", new List<int[]> { new int[] { 40, 50 }, new int[] { 50, 50 }, new int[] { 50, 60 } } },
                { "F11", new List<int[]> { new int[] { 50, 50 }, new int[] { 50, 60 }, new int[] { 60, 60 } } },
                { "F12", new List<int[]> { new int[] { 50, 50 }, new int[] { 60, 50 }, new int[] { 60, 60 } } }

            };
            return dict;
        }
        #endregion

    }
}
