using src.Algorithms;
using System;

namespace lab1_alg.src
{
    public class TimSort
    {
        private const int MIN_MERGE = 32;
        private static double[] tempArray; // Вспомогательный массив для слияния

        public static void Sort(double[] arr)
        {
            int n = arr.Length;
            tempArray = new double[n]; // Инициализируем один вспомогательный массив

            // Сначала сортируем подмассивы размером MIN_MERGE с помощью сортировки вставками
            for (int i = 0; i < n; i += MIN_MERGE)
            {
                InsertionSortAlgorithm.Sort(arr, i, Math.Min(i + MIN_MERGE - 1, n - 1));
            }

            // Затем объединяем эти подмассивы с помощью сортировки слиянием
            for (int size = MIN_MERGE; size < n; size = 2 * size)
            {
                for (int left = 0; left < n; left += 2 * size)
                {
                    int mid = left + size - 1;
                    int right = Math.Min(left + 2 * size - 1, n - 1);

                    if (mid < right)
                    {
                        MergeWithGalloping(arr, left, mid, right);
                    }
                }
            }
        }

        // Оптимизированное слияние с использованием общего вспомогательного массива
        private static void MergeWithGalloping(double[] arr, int left, int mid, int right)
        {
            int len1 = mid - left + 1;
            int len2 = right - mid;

            // Проверка на допустимость размеров
            if (len1 < 0 || len2 < 0 || left < 0 || right >= arr.Length)
            {
                Console.WriteLine($"Ошибка: некорректные индексы left={left}, mid={mid}, right={right}");
                return;
            }

            // Копируем данные во временный массив для всего диапазона
            Array.Copy(arr, left, tempArray, left, len1 + len2);

            int i1 = left;
            int i2 = mid + 1;
            int k = left;

            while (i1 <= mid && i2 <= right)
            {
                if (tempArray[i1] <= tempArray[i2])
                {
                    arr[k] = tempArray[i1];
                    i1++;
                }
                else
                {
                    arr[k] = tempArray[i2];
                    i2++;
                }
                k++;

                // Применяем галопирование, если элементы сильно различаются
                if (i1 <= mid && i2 <= right && tempArray[i1] > tempArray[i2])
                {
                    i1 = GallopRight(tempArray[i2], tempArray, i1, mid - left + 1, 0);
                }
                else if (i1 <= mid && i2 <= right && tempArray[i2] > tempArray[i1])
                {
                    i2 = GallopRight(tempArray[i1], tempArray, i2, right - mid, 0);
                }

                // Проверка на выход за пределы массива
                if (i1 > mid || i2 > right || k >= arr.Length)
                {
                    Console.WriteLine($"Ошибка: выход за пределы массива. i1={i1}, i2={i2}, k={k}, mid={mid}, right={right}");
                    return;
                }
            }

            // Копируем оставшиеся элементы из левой половины
            while (i1 <= mid)
            {
                arr[k] = tempArray[i1];
                i1++;
                k++;

                if (k >= arr.Length)
                {
                    Console.WriteLine($"Ошибка: выход за пределы массива при копировании левой половины. k={k}, i1={i1}, mid={mid}");
                    return;
                }
            }

            // Правая половина уже на месте, копирование не требуется
        }

        // Метод GallopRight для поиска с галопированием
        private static int GallopRight(double key, double[] arr, int baseIndex, int length, int hint)
        {
            int lastOffset = 0;
            int offset = 1;

            if (baseIndex + hint >= arr.Length || baseIndex < 0 || baseIndex + length >= arr.Length)
            {
                Console.WriteLine($"Ошибка: некорректные параметры в GallopRight. baseIndex={baseIndex}, length={length}, hint={hint}");
                return baseIndex;
            }

            // Проверка, является ли hint хорошим местом для поиска
            if (key > arr[baseIndex + hint])
            {
                // Галопируем вправо, чтобы найти ключ
                while (hint + offset < length && key > arr[baseIndex + hint + offset])
                {
                    lastOffset = offset;
                    offset = (offset << 1) + 1;  // Увеличиваем шаг экспоненциально
                }
                offset = Math.Min(offset, length - hint - 1);
            }
            else
            {
                // Галопируем влево, чтобы найти ключ
                while (hint - offset >= 0 && key <= arr[baseIndex + hint - offset])
                {
                    lastOffset = offset;
                    offset = (offset << 1) + 1;
                }
                offset = Math.Min(offset, hint);
            }

            // Внутри найденного диапазона выполняем бинарный поиск
            lastOffset++;
            int lo = hint - offset;
            int hi = hint - lastOffset;
            while (lo < hi)
            {
                int mid = lo + (hi - lo) / 2;
                if (key > arr[baseIndex + mid])
                    lo = mid + 1;
                else
                    hi = mid;
            }
            return lo;
        }
    }
}
