using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_alg.Utilities
{
    public class VectorGenerator
    {
        public static double[] GenerateRandomVector(int size)
        {
            double[] vector = new double[size];
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                vector[i] = rand.NextDouble() * 100;
            }
            return vector;
        }
    }
}
