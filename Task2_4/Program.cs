namespace Task2_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a, b, c, d, t,t2;
            Console.WriteLine("'Смена мест второй и четвертой цифры в четырехзначном числе'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Введите четырехзначное число");
            t = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            a = t / 1000;
            b = t % 1000 / 100;
            c = t % 100 / 10;
            d = t % 10;
            t2 = a * 1000 + d * 100 + c * 10 + b;
            Console.WriteLine("После замены мест получено число: {0}", t2);
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
