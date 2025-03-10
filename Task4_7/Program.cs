namespace Task4_7
{
    internal class Program
    {
        static void Main(string[] args) // Вводится n. Определить количество цифр в числе
        {
            int n, counter, Quotient;
            Console.WriteLine("'Определить количество цифр в числе'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.Write("Укажите целое число n: ");
            Quotient = n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            if (n == 0)
            {
                Console.WriteLine("Количество цифр в числе {0}: 1.", n);
                Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            }
            else
            {
                counter = 0;
                while (Quotient > 0)
                {
                    Quotient = Quotient / 10;
                    ++counter;
                }
                Console.WriteLine("Количество цифр в числе {0}: {1}.", n, counter);
                Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            }
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
