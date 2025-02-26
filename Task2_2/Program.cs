namespace Task2_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double a, b, c, r, angle;
            Console.WriteLine("'Перевод угла из градусов в радианы'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Ведите значение градусов");
            a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ведите значение минут");
            b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ведите значение секунд");
            c = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Значение угла в грудусах: {0} {1}' {2}''", a, b, c);
            angle = a + b / 60 + c / 60 / 60;
            r = angle * Math.PI / 180;
            Console.WriteLine("Значение угла в радианах: {0}", r);
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
