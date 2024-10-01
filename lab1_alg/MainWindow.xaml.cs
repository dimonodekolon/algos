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

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный алгоритм
            string selectedAlgorithm = (AlgorithmComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedAlgorithm == null)
            {
                MessageBox.Show("Пожалуйста, выберите алгоритм.");
                return;
            }

            // Получаем количество запусков из текстового поля
            if (!int.TryParse(RunsTextBox.Text, out int runs) || runs <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректное количество запусков.");
                return;
            }

            // Запускаем выбранный алгоритм и собираем результаты
            var (sizes, times) = RunSelectedAlgorithm(selectedAlgorithm, runs);

            // Строим график
            PlotGraph(sizes, times, selectedAlgorithm);
        }

        // Метод для запуска выбранного алгоритма с вычислением среднего времени
        private (int[] sizes, double[] times) RunSelectedAlgorithm(string algorithm, int runs)
        {
            int iterations = 200;  // Для шагов по 10 (1, 11, 21, ... до 2000)
            int[] sizes = new int[iterations];
            double[] times = new double[iterations];

            for (int i = 0; i < iterations; i++)
            {
                int n = i == 0? 1: i * 10;
                // Размерность вектора, начиная с 1, с шагом 10
                sizes[i] = n;

                double totalTime = 0;
                for (int r = 0; r < runs; r++)
                {
                    double[] v = VectorGenerator.GenerateRandomVector(n);
                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    switch (algorithm)
                    {
                        case "Функция константы":
                            ConstAlgorithm.ConstantFunction(v);
                            break;
                        case "Сумма элементов":
                            SumAlgorithm.Sum(v);
                            break;
                        case "Произведение элементов":
                            ProductAlgorithm.Product(v);
                            break;
                        case "Полином (наивный)":
                            PolynomialNaiveAlgorithm.CalculatePolynomialNaive(v, 1.5);
                            break;
                        case "Полином (Горнер)":
                            PolynomialHornerAlgorithm.CalculatePolynomialHorner(v, 1.5);
                            break;
                        case "Bubble Sort":
                            BubbleSortAlgorithm.Sort(v);
                            break;
                        case "Quick Sort":
                            QuickSortAlgorithm.Sort(v, 0, v.Length - 1);
                            break;
                        case "Matrix Multiplication":
                            double[,] A = MatrixGenerator.GenerateRandomMatrix(n, n);
                            double[,] B = MatrixGenerator.GenerateRandomMatrix(n, n);
                            MatrixMultiplicator.MatrixMultiplication(A, B);
                            break;
                    }
                    stopwatch.Stop();

                    totalTime += stopwatch.Elapsed.TotalSeconds;
                }

                times[i] = totalTime / runs;
            }

            return (sizes, times);
        }

        private void PlotGraph(int[] sizes, double[] times, string algorithmName)
        {
            var plotModel = new PlotModel { Title = $"Время выполнения: {algorithmName}" };

            var lineSeries = new LineSeries
            {
                Title = algorithmName,
                MarkerType = MarkerType.Circle,
                MarkerSize = 1
            };

            // Преобразуем время в миллисекунды
            for (int i = 0; i < sizes.Length; i++)
            {
                lineSeries.Points.Add(new DataPoint(sizes[i], times[i] * 1000));  // Умножаем на 1000 для миллисекунд
            }

            plotModel.Series.Add(lineSeries);

            // Настройка оси Y для отображения в миллисекундах
            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Время (миллисекунды)",  // Изменяем заголовок оси
                StringFormat = "F2"  // Формат для вывода времени с двумя знаками после запятой
            });

            // Настройка оси X
            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Размерность вектора (n)",
                Minimum = 1,
                Maximum = 2000,
                MajorStep = 100,
                MinorStep = 10
            });

            // Устанавливаем модель для PlotView
            PlotView.Model = plotModel;
        }
    }
}
