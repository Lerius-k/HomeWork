namespace Task3_8
{
    internal class Program
    {
        static void Main(string[] args) //Вводится число. Вывести «Да», если оно попадает в диапазон [-10,10], и «Нет» в противном случае.
        {
            double a;
            string b;
            Console.WriteLine("'Число попадает в диапазон [-10,10] ?'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Введите число:");
            a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой

            b = (a >= -10 && a <= 10) ? "Да" : "Нет";

            Console.WriteLine("Решение: {0}", b);

            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
