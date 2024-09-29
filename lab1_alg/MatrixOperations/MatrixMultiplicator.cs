using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_alg.MatrixOperations
{
    internal class MatrixMultiplicator
    {
        public static double[,] MatrixMultiplication(double[,] A, double[,] B)
        {
            int aRows = A.GetLength(0);
            int aCols = A.GetLength(1);
            int bRows = B.GetLength(0);
            int bCols = B.GetLength(1);
            if (aCols != bRows) throw new Exception("Размеры матрицы не совпадают.");
            double[,] result = new double[aRows, bCols];
            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < bCols; j++)
                {
                    for (int k = 0; k < aCols; k++)
                    {
                        result[i, j] += A[i, k] * B[k, j];
                    }
                }
            }
            return result;
        }
    }
}
