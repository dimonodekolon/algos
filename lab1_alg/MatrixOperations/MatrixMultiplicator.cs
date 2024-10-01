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
            int bCols = B.GetLength(1);

            if (aCols != B.GetLength(0))
                throw new ArgumentException("Размеры матриц не совпадают для умножения.");

            double[,] result = new double[aRows, bCols];

            Parallel.For(0, aRows, i =>
            {
                for (int k = 0; k < aCols; k++)
                {
                    double temp = A[i, k];
                    for (int j = 0; j < bCols; j++)
                    {
                        result[i, j] += temp * B[k, j];
                    }
                }
            });

            return result;
        }

    }
}