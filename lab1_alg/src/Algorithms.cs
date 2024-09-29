using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Algorithms
{
    public class SumAlgorithm
    {
        public static double Sum(double[] vector)
        {
            double sum = 0;
            foreach (var value in vector)
            {
                sum += value;
            }
            return sum;
        }
    }

    public class ProductAlgorithm
    {
        public static double Product(double[] vector)
        {
            double product = 1;
            foreach (var value in vector)
            {
                product *= value;
            }
            return product;
        }

    }

    public class PolynomialNaiveAlgorithm
    {
        public static double CalculatePolynomialNaive(double[] coefficients, double x)
        {
            double result = 0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                result += coefficients[i] * Math.Pow(x, i);
            }
            return result;
        }
    }

    public class PolynomialHornerAlgorithm
    {
        public static double CalculatePolynomialHorner(double[] coefficients, double x)
        {
            double result = 0;
            for (int i = coefficients.Length - 1; i >= 0; i--)
            {
                result = result * x + coefficients[i];
            }
            return result;
        }
    }
    public class BubbleSortAlgorithm
    {
        public static void Sort(double[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        double temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
    }

    public class QuickSortAlgorithm
    {
        public static void Sort(double[] array, int left, int right)
        {
            int i = left, j = right;
            double pivot = array[(left + right) / 2];
            while (i <= j)
            {
                while (array[i] < pivot) i++;
                while (array[j] > pivot) j--;
                if (i <= j)
                {
                    double temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }
            if (left < j) Sort(array, left, j);
            if (i < right) Sort(array, i, right);
        }
    }

    public class InsertionSortAlgorithm
    {
        public static void Sort(double[] array)
        {
            int n = array.Length;
            for (int i = 1; i < n; i++)
            {
                double key = array[i];
                int j = i - 1;
                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }
    }

}
