namespace Task3_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double a, b;
            Console.WriteLine("'Вывод знака сравнения между двумя числами'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Введите первое число 'a'");
            a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите второе число 'b'");
            b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой

            string symbol;
            symbol = (a > b) ? ">" : (a < b) ? "<" : "=";

            Console.WriteLine("Решение: {0} {2} {1}", a, b, symbol);

            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
