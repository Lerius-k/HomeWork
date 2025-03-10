namespace Task4_3
{
    internal class Program
    {
        static void Main(string[] args) //Вводятся целые числа a>0, b>0. Найти a^b. Не использовать класс Math
        {
            int a, b, pov;
            Console.WriteLine("'Найти a^b. числа a>0, b>0'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.Write("Укажите целое число a>0: ");
            a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Укажите целое число b>0: ");
            b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            pov = 1;
            for (int i = 1; i <= b; i++)
            {
                pov = pov * a;
            }
            Console.WriteLine("{0}^{1} = {2}", a, b, pov);
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
