namespace Task7_4
{
    internal class Program
    {
        static void Main(string[] args)
        /*Напишите метод FindMax, который принимает переменное количество чисел и возвращает максимальное значение.
        Используйте ключевое слово params.*/
        {
            Console.WriteLine("\"Найти максимальное значение\"");
            Console.WriteLine();
            int[] numbers = { 3, 23, 1, 15, 5, 34, 345, 2435, 234, 234, 12, 123, 23, 6, 897, 43, 34, 34 };
            Console.Write($"Числа в массиве: ");
            foreach (int i in numbers)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();

            FindMax(numbers);
            Console.WriteLine();


            Console.WriteLine();
            Console.Write("Нажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
        static void FindMax(params int[] a)
        {
            int max = a[0];
            foreach (int i in a)
            {
                if (i > max)
                {
                    max = i;
                }
            }
            Console.Write($"Максимальное значение: {max}");
        }
    }
}
