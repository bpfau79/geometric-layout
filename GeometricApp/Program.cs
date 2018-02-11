using GeometricApp.Objects;
using GeometricApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricApp
{

    /// <summary>
    /// Program to determine the vertices for a given triangle as well as lookup a triangle based on a set of vertices
    /// </summary>
    public static class Program
    {
        public static void Main(string[] args)
        {
            var rowValues = "ABCDEF";
            var maxColValue = 12;

            var map = new GeometricMap(rowValues, maxColValue);
            map.Generate();

            var calc = new GeometricCalculator(map, 10);

            var keys = map.GenerateKeys();

            foreach (var key in keys)
            {
                var vertices = calc.CalculateVerticesFromKey(key);
                PrintResult(key, vertices);
                var keyFromVertices = calc.CalculateKeyFromVertices(vertices);
                Console.Write($" Reverse={keyFromVertices}");
                Console.WriteLine();
            }

            Console.WriteLine("Hit any key to exit...");
            Console.ReadKey();
        }


        private static void PrintResult(string key, List<Vertex> vertices)
        {
            var result = String.Join(" ", (from v in vertices select v.ToString()).ToArray());
            Console.Write($"Key={key} has vertices={result}");

        }

    }
}
