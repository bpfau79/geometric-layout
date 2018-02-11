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
    /// We use a bounding box as a model to determine the coordinates of a triangle
    /// </summary>
    public static class Program
    {
        public static void Main(string[] args)
        {

            var calc = new GeometricCalculator(length: 10, numColumns: 12);

            var keys = calc.GenerateKeys();

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
