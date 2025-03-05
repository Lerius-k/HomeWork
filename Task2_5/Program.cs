namespace Task2_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double a, b;
            Console.WriteLine("'Округление длины до целых в большую сторону'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Введите значение длины в метрах");
            a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            b = Math.Ceiling(a);
            Console.WriteLine("Округленное значение: {0}", b);
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
