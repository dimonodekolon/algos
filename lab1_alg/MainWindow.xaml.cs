using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot.Wpf;

namespace lab1_alg
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RunAlgorithmMeasurements();
        }

        private void RunAlgorithmMeasurements()
        {
            int[] ns = new int[10];
            double[] timesSum = new double[10];
            double[] timesProduct = new double[10];
            double[] timesPolyNaive = new double[10];
            double[] timesPolyHorner = new double[10];
            double[] timesBubbleSort = new double[10];
            double[] timesQuickSort = new double[10];
            double[] timesInsertionSort = new double[10];
            double[] timesMatrixMultiplication = new double[10];

            for (int j = 0; j < 10; j++)
            {
                int n = 100 * (j + 1); // Регулируем шаг
                ns[j] = n;
                double[] v = GenerateRandomVector(n);

                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                double sum = Sum(v);
                stopwatch.Stop();
                timesSum[j] = stopwatch.Elapsed.TotalMilliseconds;

                stopwatch.Restart();
                double product = Product(v);
                stopwatch.Stop();
                timesProduct[j] = stopwatch.Elapsed.TotalMilliseconds;

                stopwatch.Restart();
                double polyNaive = PolynomialNaive(v, 1.5);
                stopwatch.Stop();
                timesPolyNaive[j] = stopwatch.Elapsed.TotalMilliseconds;

                stopwatch.Restart();
                double polyHorner = PolynomialHorner(v, 1.5);
                stopwatch.Stop();
                timesPolyHorner[j] = stopwatch.Elapsed.TotalMilliseconds;

                double[] vToSort = (double[])v.Clone();
                stopwatch.Restart();
                BubbleSort(vToSort);
                stopwatch.Stop();
                timesBubbleSort[j] = stopwatch.Elapsed.TotalMilliseconds;

                vToSort = (double[])v.Clone();
                stopwatch.Restart();
                QuickSort(vToSort, 0, n - 1);
                stopwatch.Stop();
                timesQuickSort[j] = stopwatch.Elapsed.TotalMilliseconds;

                vToSort = (double[])v.Clone();
                stopwatch.Restart();
                InsertionSort(vToSort);
                stopwatch.Stop();
                timesInsertionSort[j] = stopwatch.Elapsed.TotalMilliseconds;

                double[,] A = GenerateRandomMatrix(n, n);
                double[,] B = GenerateRandomMatrix(n, n);

                stopwatch.Restart();
                double[,] result = MatrixMultiplication(A, B);
                stopwatch.Stop();
                timesMatrixMultiplication[j] = stopwatch.Elapsed.TotalMilliseconds;
            }

            PlotExecutionTime(plotViewTimesSum, ns, timesSum, "Сумма элементов", "Time (ms)");

            PlotExecutionTime(plotViewTimesProduct, ns, timesProduct, "Произведение элементов", "Time (ms)");

            PlotExecutionTime(plotViewTimesPolyNaive, ns, timesPolyNaive, "Наивное вычисление", "Time (ms)");

            PlotExecutionTime(plotViewTimesPolyHorner, ns, timesPolyHorner, "Метод Горнера", "Time (ms)");

            PlotExecutionTime(plotViewTimesBubbleSort, ns, timesBubbleSort, "Bubble Sort", "Time (ms)");

            PlotExecutionTime(plotViewTimesQuickSort, ns, timesQuickSort, "Quick Sort", "Time (ms)");

            PlotExecutionTime(plotViewTimesInsertionSort, ns, timesInsertionSort, "Сортировка вставками (Insertion sort)", "Time (ms)");

            PlotExecutionTime(plotViewTimesMatrixMultiplication, ns, timesMatrixMultiplication, "Matrix Multiplication", "Time (ms)");

        }

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

        public static double Sum(double[] vector)
        {
            double sum = 0;
            foreach (var value in vector)
            {
                sum += value;
            }
            return sum;
        }

        public static double Product(double[] vector)
        {
            double product = 1;
            foreach (var value in vector)
            {
                product *= value;
            }
            return product;
        }

        public static double PolynomialNaive(double[] coefficients, double x)
        {
            double result = 0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                result += coefficients[i] * Math.Pow(x, i);
            }
            return result;
        }

        public static double PolynomialHorner(double[] coefficients, double x)
        {
            double result = 0;
            for (int i = coefficients.Length - 1; i >= 0; i--)
            {
                result = result * x + coefficients[i];
            }
            return result;
        }

        public static void BubbleSort(double[] array)
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

        public static void QuickSort(double[] array, int left, int right)
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
            if (left < j) QuickSort(array, left, j);
            if (i < right) QuickSort(array, i, right);
        }

        public static void InsertionSort(double[] array)
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

        private void PlotExecutionTime(PlotView plotView, int[] ns, double[] executionTimes, string title, string yAxisTitle)
        {
            var plotModel = new PlotModel { Title = title };
            var lineSeries = new LineSeries
            {
                Title = "Время выполнения",
                MarkerType = MarkerType.Circle,
                MarkerSize = 4
            };

            for (int i = 0; i < ns.Length; i++)
            {
                lineSeries.Points.Add(new DataPoint(ns[i], executionTimes[i]));
            }

            plotModel.Series.Add(lineSeries);
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = yAxisTitle });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "n" });

            plotView.Model = plotModel;
        }
    }
}
