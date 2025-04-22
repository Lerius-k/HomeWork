namespace Task7_3
{
    internal class Program
    {
        static void Main(string[] args)
        /*Напишите метод PrintNumbers, который выводит на экран числа из массива. 
         У метода должен быть необязательный параметр reverse, который по умолчанию равен false. 
         Если reverse равен true, числа выводятся в обратном порядке.*/
        {
            Console.WriteLine("\"Вывод чисел из массива\"");
            Console.WriteLine();
            int[] numbers = { 1, 2, 3, 4, 5 };

            PrintNumbers(numbers);
            Console.WriteLine();
            PrintNumbers(numbers, false);
            Console.WriteLine();
            PrintNumbers(numbers, true);
            Console.WriteLine();
            PrintNumbers(numbers, true);
            Console.WriteLine();


            Console.WriteLine();
            Console.Write("Нажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
        static void PrintNumbers(int[] a, bool reverse = false)
        {
            if (reverse)
            {
                Console.Write("В обратном порядке: ");
                for (int i = 4; i >= 0; i--)
                {
                    Console.Write($"{a[i]} ");
                }

            }
            else
            {
                Console.Write("В прямом порядке: ");
                foreach (var i in a)
                {
                    Console.Write($"{i} ");
                }
            }
        }
    }
}
