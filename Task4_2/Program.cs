namespace Task4_2
{
    internal class Program //Вводится натуральное число n. Найти 1 + 1/2 + 1/3 + … + 1/n
    {
        static void Main(string[] args)
        {
            double n, vol;
            Console.WriteLine("'Найти 1 + 1/2 + 1/3 + … + 1/n'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.Write("Укажите натуральное число n: ");
            n = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            vol = 1;
            for (double i = 2; i <= n; i++)
            {
                vol += 1 / i;
            }
            Console.WriteLine("1 + 1/2 + 1/3 + … + 1/{0} = {1}", n, vol);
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
