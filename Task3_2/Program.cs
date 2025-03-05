namespace Task3_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double a, b, c, max;
            Console.WriteLine("'Вывод большего числа из трёх'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Введите первое число 'a'");
            a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите второе число 'b'");
            b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите третье число 'c'");
            c = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой

            max = (a > b) ? ((a > c) ? a : c) : ((b > c) ? b : c);

            Console.WriteLine("Решение: {0}", max);

            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
