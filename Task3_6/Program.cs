namespace Task3_6
{
    internal class Program
    {
        static void Main(string[] args) //Вводится число. Вывести «Да», если оно четное, и «Нет» в противном случае.
        {
            double a;
            string b;
            Console.WriteLine("'Число чётное?'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Введите число:");
            a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой

            b = (a % 2 == 0) ? "Да" : "Нет";

            Console.WriteLine("Решение: {0}", b);

            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
