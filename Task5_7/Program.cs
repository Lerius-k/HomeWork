namespace Task5_7
{
    internal class Program
    {
        static void Main(string[] args)
        /*Сформировать двумерный массив из 10 строк и 5 столбцов. 
         *Заполнить его случайными числами в диапазоне [0, 10]. 
         *Определить максимальный элемент в каждой строке*/
        {
            Console.WriteLine("Определить максимальный элемент в каждой строке массива");
            Console.WriteLine();
            const int n = 10;
            const int m = 5;
            const int min = 0;
            int[,] t = new int[n, m];
            Random random = new Random();
            Console.Write("Массив: ");
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                int max = min;
                for (int j = 0; j < m; j++)
                {
                    t[i, j] = random.Next(min, 11);
                    Console.Write("{0,-3}", t[i, j]);
                    if (t[i, j] > max)
                    {
                        max = t[i, j];
                    }
                }
                Console.Write("Max:{0}", max);
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
