namespace Task3_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double a, b, c, middle;
            Console.WriteLine("'Вывод среднего числа (медианы) из трёх'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Введите первое число 'a'");
            a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите второе число 'b'");
            b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите третье число 'c'");
            c = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой

            if ((a <= b && a >= c) || (a >= b && a <= c))
            {
                middle = a;
            }
            else
            {
                if ((b <= a && b >= c) || (b >= a && b <= c))
                {
                    middle = b;
                }
                else
                {
                    middle = c;
                }
            }

            Console.WriteLine("Решение: {0}", middle);

            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
