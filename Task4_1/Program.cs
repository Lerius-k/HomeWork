namespace Task4_1
{
    internal class Program
    //Вводится натуральное число n. Найти n! Например, 6! = 1 * 2 * 3 * 4 * 5 * 6.
    {
        static void Main(string[] args)
        {
            int n, vol;
            Console.WriteLine("'Найти n!'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.Write("Укажите натуральное число n: ");
            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            vol = 1;
            for (int i = 1; i <= n; i++)
            {
                vol = vol * i;
            }
            Console.WriteLine("{0}! = {1}", n, vol);
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
