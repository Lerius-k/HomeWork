namespace Bisearch_grok
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Найти число s в массиве");
            Console.WriteLine();
            Console.Write("Введите искомое значение: ");
            int s = Convert.ToInt32(Console.ReadLine()); //искомое значение (тестовые значения 33 11 7 15 50)
            Console.WriteLine();
            const int n = 8;
            int[] t = new int[n] { 16, 30, 33, 11, 12, 18, 46, 13 };
            Console.WriteLine("Массив: ");
            for (int i = 0; i < n; i++)
            {
                Console.Write("{0,3}", t[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Отсортированный массив: ");
            for (int i = 0; i < n - 1; i++) //сортировка
            {
                for (int j = i + 1; j < n; j++)
                {
                    int temp;
                    if (t[i] > t[j])
                    {
                        temp = t[i];
                        t[i] = t[j];
                        t[j] = temp;
                    }
                }
            }
            for (int i = 0; i < n; i++) // выводит сортированный мвссив
            {
                Console.Write("{0,3}", t[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < n; i++) // выводит индексы сортированного массива
            {
                Console.Write("{0,3}", i);
            }
            Console.WriteLine();
            Console.WriteLine();
            //бинарный поиск
            string answer = "Тест";
            int low = 0;
            int high = n - 1;
            int index = 0;
            bool f = false;
            while (low <= high)
            {
                int mid = (low + high) / 2;
                if (t[mid] == s)
                {
                    answer = "Hайден";
                    index = mid;
                    f = true;
                    break;
                }
                else
                {
                    if (t[mid] > s)
                    {
                        high = mid - 1;
                    }
                    else
                    {
                        low = mid + 1;
                    }
                    answer = "Не найден";
                }
            }
            Console.WriteLine(answer);
            Console.WriteLine();
            if (f== true)
            {
                Console.WriteLine("Индекс: {0}", index);
            }
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
