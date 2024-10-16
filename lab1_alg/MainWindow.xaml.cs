﻿using OxyPlot.Series;
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
using lab1_alg.src;
using Accord.Math.Optimization;
using OxyPlot.Axes;

namespace lab1_alg
{
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
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

            // Запускаем выбранный алгоритм асинхронно
            var (sizes, times) = await Task.Run(() => RunSelectedAlgorithm(selectedAlgorithm, runs));

            // Строим график на UI-потоке
            PlotGraph(sizes, times, selectedAlgorithm);
        }

        // Метод для запуска выбранного алгоритма с вычислением среднего времени
        private (int[] sizes, double[] times) RunSelectedAlgorithm(string algorithm, int runs)
        {
            int desiredMaxN;
            int iterations;
            int step;

            switch (algorithm)
            {
                case "Функция константы":
                    desiredMaxN = 50000;
                    step = 50;
                    break;
                case "Сумма элементов":
                    desiredMaxN = 50000;
                    step = 50;
                    break;
                case "Произведение элементов":
                    desiredMaxN = 50000;
                    step = 50;
                    break;
                case "Полином (наивный)":
                    desiredMaxN = 50000;
                    step = 50;
                    break;
                case "Полином (Горнер)":
                    desiredMaxN = 50000;
                    step = 50;
                    break;
                case "Bubble Sort":
                    desiredMaxN = 5000;
                    step = 50;
                    break;
                case "Quick Sort":
                    desiredMaxN = 75000;
                    step = 100;
                    break;
                case "Insertion Sort":
                    desiredMaxN = 10000;
                    step = 25;
                    break;
                case "Coctail Sort":
                    desiredMaxN = 10000;
                    step = 50;
                    break;
                case "TimSort":
                    desiredMaxN = 50000;
                    step = 50;
                    break;
                case "ShellSort":
                    desiredMaxN = 20000;
                    step = 25;
                    break;
                case "Matrix Multiplication":
                    desiredMaxN = 1500;
                    step = 50;
                    break;
                case "Простое возведение в степень":
                    desiredMaxN = 50000;
                    step = 100;
                    break;
                case "Рекурсивное возведение в степень":
                    desiredMaxN = 50000;
                    step = 100;
                    break;
                case "Быстрое возведение в степень":
                    desiredMaxN = 50000;
                    step = 100;
                    break;
                case "Быстрое возведение в степень 2":
                    desiredMaxN = 50000;
                    step = 100;
                    break;
                default: throw new ArgumentException();
            }

            iterations = (desiredMaxN / step) + 1;
            double[] steps = null;
            if (algorithm.Contains("степень"))
            {
                steps = new double[iterations];
            }
            int[] sizes = new int[iterations];
            double[] times = new double[iterations];
            for (int i = 0; i < iterations; i++)
            {
                int n = i * step;
                sizes[i] = n == 0 ? 1 : n;  // Размерность вектора
                double totalTime = 0;
                int totalSteps = 0;
                int number = 2;
                int degree = i == 0 ? 1 : i * 10;

                for (int r = 0; r < runs; r++)
                {
                    try
                    {
                        double[] v = VectorGenerator.GenerateRandomVector(sizes[i]);

                        // Генерация данных вне измерения времени
                        double[,] A = null;
                        double[,] B = null;
                        if (algorithm == "Matrix Multiplication")
                        {
                            A = MatrixGenerator.GenerateRandomMatrix(sizes[i], sizes[i]);
                            B = MatrixGenerator.GenerateRandomMatrix(sizes[i], sizes[i]);
                        }

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
                            case "Insertion Sort":
                                InsertionSortAlgorithm.Sort(v, 0, v.Length - 1);
                                break;
                            case "Coctail Sort":
                                CoctailSort.ShakerSort(v);
                                break;
                            case "TimSort":
                                TimSort.Sort(v);
                                break;
                            case "ShellSort":
                                ShellSort.Sort(v);
                                break;
                            case "Matrix Multiplication":
                                MatrixMultiplicator.MatrixMultiplication(A, B);
                                break;
                            case "Простое возведение в степень":
                                MathPow.Pow(number, degree);
                                break;
                            case "Рекурсивное возведение в степень":
                                MathPow.RecPow(number, degree);
                                break;
                            case "Быстрое возведение в степень":
                                MathPow.QuickPow(number, degree);
                                break;
                            case "Быстрое возведение в степень 2":
                                MathPow.QuickPow2(number, degree);
                                break;
                        }
                        stopwatch.Stop();
                        totalSteps += MathPow.count;
                        MathPow.count = 0;
                        totalTime += stopwatch.Elapsed.TotalMilliseconds; // В миллисекундах
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при выполнении алгоритма: {ex.Message}");
                        times[i] = double.NaN;
                        break;
                    }
                }
                if (steps != null)
                {
                    steps[i] = totalSteps / runs;
                }
                times[i] = totalTime / runs;

                Debug.WriteLine($"Завершена итерация {i + 1}/{iterations} для n = {sizes[i]}");
            }
            if (algorithm.Contains("степень")) return (sizes, steps);
            return (sizes, times);
        }

        private void PlotGraph(int[] sizes, double[] times, string algorithmName)
        {
            var plotModel = new PlotModel { Title = $"Время выполнения: {algorithmName}" };

            var lineSeries = new LineSeries
            {
                Title = "Экспериментальные данные",
                MarkerType = MarkerType.Circle,
                MarkerSize = 1,
                Color = OxyColors.Blue
            };

            for (int i = 0; i < sizes.Length; i++)
            {
                lineSeries.Points.Add(new DataPoint(sizes[i], times[i]));
            }

            plotModel.Series.Add(lineSeries);

            // Находим наилучшую аппроксимирующую функцию
            var (bestFitName, theoreticalTimes) = FindBestFit(sizes, times);

            if (theoreticalTimes != null)
            {
                // Добавляем теоретическую кривую на график
                var theoreticalSeries = new LineSeries
                {
                    Title = $"Аппроксимация: {bestFitName}",
                    LineStyle = LineStyle.Dash,
                    Color = OxyColors.Red
                };

                for (int i = 0; i < sizes.Length; i++)
                {
                    theoreticalSeries.Points.Add(new DataPoint(sizes[i], theoreticalTimes[i]));
                }

                plotModel.Series.Add(theoreticalSeries);
            }
            else
            {
                MessageBox.Show("Не удалось найти подходящую аппроксимацию.");
            }

            // Настройка осей

            // Настройка оси X
            if (algorithmName.Contains("степень"))
            {
                plotModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    Title = "Шаги",
                    Minimum = 0,
                    Maximum = times.Max() * 1.1
                });
                plotModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    Title = "Значение степени",
                    Minimum = sizes.Min(),
                    Maximum = sizes.Max()
                });
            }
            else
            {
                plotModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    Title = "Время (миллисекунды)",
                    Minimum = 0,
                    Maximum = times.Max() * 1.1
                });
                plotModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    Title = "Размерность вектора (n)",
                    Minimum = sizes.Min(),
                    Maximum = sizes.Max()
                });
            }

            // Добавляем легенду
            plotModel.IsLegendVisible = true;
            // Устанавливаем модель для PlotView
            PlotView.Model = plotModel;
        }

        private (string bestFitName, double[] theoreticalTimes) FindBestFit(int[] sizes, double[] times)
        {
            var complexityFunctions = new List<(string Name, Func<double, double[], double> Function, double[] InitialGuess)>
    {
        ("O(1)", (n, p) => p[0], new double[] { times.Average() }),
        ("O(log n)", (n, p) => p[0] * Math.Log(n), new double[] { 1 }),
        ("O(n)", (n, p) => p[0] * n, new double[] { times.Average() / sizes.Average() }),
        ("O(n log n)", (n, p) => p[0] * n * Math.Log(n), new double[] { 1e-6 }),
        ("O(n^2)", (n, p) => p[0] * n * n, new double[] { 1e-6 }),
        ("O(n^3)", (n, p) => p[0] * n * n * n, new double[] { 1e-9 }),
        ("O(2^n)", (n, p) => p[0] * Math.Pow(2, n), new double[] { 1e-9 })
    };

            double bestError = double.PositiveInfinity;
            string bestFitName = null;
            double[] bestTheoreticalTimes = null;

            double[] nValues = sizes.Select(n => (double)n).ToArray();
            double[] timeValues = times.ToArray();

            foreach (var (Name, Function, InitialGuess) in complexityFunctions)
            {
                try
                {
                    // Создаем функцию ошибки
                    Func<double[], double> errorFunction = (parameters) =>
                    {
                        double error = 0;
                        for (int i = 0; i < nValues.Length; i++)
                        {
                            double modelValue = Function(nValues[i], parameters);
                            double diff = modelValue - timeValues[i];
                            error += diff * diff;
                        }
                        return error;
                    };

                    // Создаем оптимизатор
                    var optimizer = new NelderMead(numberOfVariables: InitialGuess.Length)
                    {
                        Function = errorFunction,
                        Solution = InitialGuess
                    };

                    bool success = optimizer.Minimize();

                    if (success)
                    {
                        double[] fittedParameters = optimizer.Solution;
                        double[] yFitted = nValues.Select(n => Function(n, fittedParameters)).ToArray();

                        // Вычисляем среднеквадратичную ошибку
                        double mse = yFitted.Zip(timeValues, (fitted, actual) => Math.Pow(fitted - actual, 2)).Average();

                        if (mse < bestError)
                        {
                            bestError = mse;
                            bestFitName = Name;
                            bestTheoreticalTimes = yFitted;
                        }
                    }
                }
                catch
                {
                    // Игнорируем ошибки для неподходящих функций
                    continue;
                }
            }

            return (bestFitName, bestTheoreticalTimes);
        }

    }
}