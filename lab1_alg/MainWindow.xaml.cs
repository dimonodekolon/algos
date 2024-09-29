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
using lab1_alg.Utilities;
using src.Algorithms;
using lab1_alg.MatrixOperations;

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

            Parallel.For(0, 10, j =>
            {
                int n = 100 * (j + 1);
                ns[j] = n;
                double[] v = VectorGenerator.GenerateRandomVector(n);

                timesSum[j] = Execution.MeasureAlgorithm(() => SumAlgorithm.Sum(v));
                timesProduct[j] = Execution.MeasureAlgorithm(() => ProductAlgorithm.Product(v));
                timesPolyNaive[j] = Execution.MeasureAlgorithm(() => PolynomialNaiveAlgorithm.CalculatePolynomialNaive(v, 1.5));
                timesPolyHorner[j] = Execution.MeasureAlgorithm(() => PolynomialHornerAlgorithm.CalculatePolynomialHorner(v, 1.5));

                double[] vToSort = (double[])v.Clone();
                timesBubbleSort[j] = Execution.MeasureAlgorithm(() => BubbleSortAlgorithm.Sort(vToSort));

                vToSort = (double[])v.Clone();
                timesQuickSort[j] = Execution.MeasureAlgorithm(() => QuickSortAlgorithm.Sort(vToSort, 0, n - 1));

                vToSort = (double[])v.Clone();
                timesInsertionSort[j] = Execution.MeasureAlgorithm(() => InsertionSortAlgorithm.Sort(vToSort));

                double[,] A = MatrixGenerator.GenerateRandomMatrix(n, n);
                double[,] B = MatrixGenerator.GenerateRandomMatrix(n, n);
                timesMatrixMultiplication[j] = Execution.MeasureAlgorithm(() => MatrixMultiplicator.MatrixMultiplication(A, B));
            });

            PlotExecutionTime(plotViewTimesSum, ns, timesSum, "Сумма элементов", "Time (ms)");

            PlotExecutionTime(plotViewTimesProduct, ns, timesProduct, "Произведение элементов", "Time (ms)");

            PlotExecutionTime(plotViewTimesPolyNaive, ns, timesPolyNaive, "Наивное вычисление", "Time (ms)");

            PlotExecutionTime(plotViewTimesPolyHorner, ns, timesPolyHorner, "Метод Горнера", "Time (ms)");

            PlotExecutionTime(plotViewTimesBubbleSort, ns, timesBubbleSort, "Bubble Sort", "Time (ms)");

            PlotExecutionTime(plotViewTimesQuickSort, ns, timesQuickSort, "Quick Sort", "Time (ms)");

            PlotExecutionTime(plotViewTimesInsertionSort, ns, timesInsertionSort, "Сортировка вставками (Insertion sort)", "Time (ms)");

            PlotExecutionTime(plotViewTimesMatrixMultiplication, ns, timesMatrixMultiplication, "Matrix Multiplication", "Time (ms)");

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
