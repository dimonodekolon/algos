using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Algorithms;

namespace lab1_alg.src
{
    public class Timsort
    {
        private const int MIN_MERGE = 64;

        public static void Sort(double[] arr)
        {
            int n = arr.Length;

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
                        Merge(arr, left, mid, right);
                    }
                }
            }
        }


        // Слияние двух отсортированных подмассивов arr[left..mid] и arr[mid+1..right]
        private static void Merge(double[] arr, int left, int mid, int right)
        {
            int len1 = mid - left + 1;
            int len2 = right - mid;
            double[] leftArr = new double[len1];
            double[] rightArr = new double[len2];

            // Копируем данные во временные массивы leftArr[] и rightArr[]
            for (int i = 0; i < len1; i++)
                leftArr[i] = arr[left + i];
            for (int i = 0; i < len2; i++)
                rightArr[i] = arr[mid + 1 + i];

            // Слияние временных массивов обратно в arr[left..right]
            int i1 = 0, i2 = 0;
            int k = left;
            while (i1 < len1 && i2 < len2)
            {
                if (leftArr[i1] <= rightArr[i2])
                {
                    arr[k] = leftArr[i1];
                    i1++;
                }
                else
                {
                    arr[k] = rightArr[i2];
                    i2++;
                }
                k++;
            }

            // Копируем оставшиеся элементы leftArr[], если есть
            while (i1 < len1)
            {
                arr[k] = leftArr[i1];
                i1++;
                k++;
            }

            // Копируем оставшиеся элементы rightArr[], если есть
            while (i2 < len2)
            {
                arr[k] = rightArr[i2];
                i2++;
                k++;
            }
        }
    }

}
