namespace Task5_3
{
    internal class Program
    {
        static void Main(string[] args)
        /*Сформировать одномерный массив из 10 случайных чисел из диапазона [0, 50]. 
         * Найти и вывести значение максимального, минимального элементов и их индексы*/
        {
            Console.WriteLine("Найти и вывести значение максимального, минимального элементов и их индексы");
            Console.WriteLine();
            const int n = 10;
            int[] t = new int[n];
            Random random = new Random();
            int iMax = 0;
            int iMin = 0;
            Console.Write("Массив: ");
            for (int i = 0; i < n; i++)
            {
                t[i] = random.Next(0, 51);
                Console.Write("{0} ", t[i]);
            }
            int max = t[0];
            int min = t[0];
            for (int i = 0; i < n; i++)
            {
                if (t[i] > max)
                {
                    max = t[i];
                    iMax = i;
                }
                if (t[i] < min)
                {
                    min = t[i];
                    iMin = i;
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Максимальное значение: {0}[{1}]", max, iMax);
            Console.WriteLine("Минимальное значение: {0}[{1}]", min, iMin);
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
