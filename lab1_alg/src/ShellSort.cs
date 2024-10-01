public class ShellSort
{
    public static void Sort(double[] arr)
    {
        int n = arr.Length;

        // Начинаем с большого шага, уменьшая его постепенно
        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            // Выполняем сортировку вставками для каждого подмассива с текущим шагом gap
            for (int i = gap; i < n; i++)
            {
                double temp = arr[i];
                int j;

                // Сдвигаем элементы подмассива arr[i - gap], arr[i - 2 * gap], ..., которые больше arr[i]
                for (j = i; j >= gap && arr[j - gap] > temp; j -= gap)
                {
                    arr[j] = arr[j - gap];
                }

                // Вставляем arr[i] на правильную позицию
                arr[j] = temp;
            }
        }
    }
}
