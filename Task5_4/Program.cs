namespace Task5_4
{
    internal class Program
    {
        static void Main(string[] args)
            /*Сформировать одномерный массив из 10 случайных чисел из диапазона [0, 10]. 
             * Перевернуть массив, т.е. переставить элементы массива в обратном порядке*/
        {
            Console.WriteLine("Перевернуть массив (переставить элементы массива в обратном порядке)");
            Console.WriteLine();
            const int n = 10;
            int[] t = new int[n];
            Random random = new Random();
            Console.Write("Массив: ");
            for (int i = 0; i < n; i++)
            {
                t[i] = random.Next(0, 11);
                Console.Write("{0,3}", t[i]);
            }
            Console.WriteLine();
            int start = 0;
            int end = n - 1;
            while (start < end)
            {
                int temp = t[start];
                t[start] = t[end];
                t[end] = temp;
                start += 1;
                end = n - 1 - start;
            }
            Console.Write("Виссам: ");
            foreach (int a in t)
            {
                Console.Write("{0,3}", a);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
