using System.Reflection;

namespace Task3_10
{
    internal class Program
    {
        static void Main(string[] args)
        /*Застройщик построил n домов. 
         *Вывести фразу «Мы построили n домов», обеспечив правильное согласование числа со словом «дом», 
         *например: 20 — «Мы построили 20 домов», 32 — «Мы построили 32 дома», 41 — «Мы построили 41 дом».*/
        {
            double a;
            string b;
            Console.WriteLine("'Cколько доамов мы построли?'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Введите число:");
            a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой

            if ((a % 10 >= 5 && a % 10 <= 9) || a % 10 == 0)
            {
                b = "ов";
            }
            else
            {
                if (a % 10 == 1)
                {
                    b = "";
                }
                else
                {
                    if ((a % 10 >= 2 && a % 10 <= 4) && !(a % 100 >= 11 && a % 100 <= 14))
                    {
                        b = "а";
                    }
                    else
                    {
                        b = "ов";
                    }
                }
            }

            Console.WriteLine("Мы построили {0} дом{1}", a, b);

            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
