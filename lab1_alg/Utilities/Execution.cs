using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_alg.Utilities
{
    internal class Execution
    {
        public static double MeasureAlgorithm<T>(Func<T> algorithm, int runs = 5)
        {
            Stopwatch stopwatch = new Stopwatch();
            double totalTime = 0;
            for (int i = 0; i < runs; i++)
            {
                stopwatch.Restart();
                algorithm.Invoke(); // Вызов алгоритма, возвращающего значение типа T
                stopwatch.Stop();
                totalTime += stopwatch.Elapsed.TotalMilliseconds;
            }
            return totalTime / runs;
        }

        public static double MeasureAlgorithm(Action algorithm, int runs = 5)
        {
            Stopwatch stopwatch = new Stopwatch();  
            double totalTime = 0;
            for (int i = 0; i < runs; i++)
            {
                stopwatch.Restart();
                algorithm.Invoke();
                stopwatch.Stop();
                totalTime += stopwatch.Elapsed.TotalMilliseconds;
            }
            return totalTime / runs;
        }

    }
}
