namespace Task3_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double a, b, c, d, max;
            Console.WriteLine("'Вывод большего числа из четырёх'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Введите первое число 'a'");
            a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите второе число 'b'");
            b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите третье число 'c'");
            c = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите четвертое число 'd'");
            d = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой

            if (a >= b && a >= c && a >= d)
            {
                max = a;
            }
            else
            {
                if (b >= a && b >= c && b >= d)
                {
                    max = b;
                }
                else
                {
                    if (c >= a && c >= b && c >= d)
                    {
                        max = c;
                    }
                    else
                    {

                        max = d;
                    }
                }
            }

            Console.WriteLine("Решение: {0}", max);

            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
