using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_alg.src
{
    public class MathPow
    {
        private static int f = 0;
        public static int count = 0;
        public static int Pow(int x, int n)
        {
            f = 1;
            int k = 0;
            while (k < n)
            {
                f *= x;
                k++;
                count += 3;
            }
            count += 3;
            return f;      // Возвращаем результат
        }

        public static int RecPow(int x, int n)
        {
            if (n == 0)
            {
                f = 1;
                count += 3;
                return f;
            }
            count++;
            f = RecPow(x, n / 2); // вычисляем половину степени
            if (n % 2 == 1)
            {
                f = f * f * x; // если степень четная
            }
            else
            {
                f *= f; // если степень нечетная
            }
            count += 5;
            return f;
        }
        // Быстрое возведение в степень
        public static int QuickPow(int x, int n)
        {
            int c = x;
            int k = n;
            if (k % 2 == 1) { f = c; }
            else { f = 1; }
            count += 5;
            do
            {
                k /= 2;
                c *= c;
                if (k % 2 == 1)
                {
                    f *= c;
                    count++;
                }
                count += 4;
            } while (k != 0);
            return f;
        }

        // Классический быстрый алгоритм
        public static int QuickPow2(int x, int n)
        {
            f = 1;
            int c = x;
            int k = n;
            count += 4;
            while (k != 0)
            {
                if (k % 2 == 0)
                {
                    f *= c;
                    k--;
                }
                else
                {
                    c *= c;
                    k /= 2;
                }
                count += 4;
            }
            count += 2;
            return f;
        }
    }
}
 