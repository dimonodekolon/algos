using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_alg.Utilities
{
    public class MatrixGenerator
    {
        public static double[,] GenerateRandomMatrix(int rows, int cols)
        {
            double[,] matrix = new double[rows, cols];
            Random rand = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rand.NextDouble() * 10;
                }
            }
            return matrix;
        }
    }
}
