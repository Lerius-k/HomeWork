namespace Task2_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const double d = 2.54; // дюйм в сантиметрах
            double sm, a, m, mm;
            Console.WriteLine("'Перевод дюймов в м см мм'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Ведите длину отрезка в дюймах");
            a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            mm = a * d * 10;
            m = Math.Floor(mm / 1000);
            sm = Math.Floor(mm % 1000 / 10);
            mm = Math.Round(mm - m * 1000 - sm * 10, 2);
            Console.WriteLine("Значение длины в м см мм: {0}м {1}см {2}мм", m, sm, mm);
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
