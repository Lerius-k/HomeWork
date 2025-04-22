namespace Task7_2
{
    internal class Program
    {
        static void Main(string[] args) //Создайте метод CalcCube для вычисления объема и площади поверхности куба по длине его ребра.
        {
            Console.WriteLine("\"Площадь поверхности куба и объем по длине его ребра\"");
            Console.WriteLine();
            double edgeLength, V, S;
            Console.WriteLine("Укажите длину ребра куба");
            Console.Write("a: ");
            edgeLength = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();

            CalcCube(edgeLength, out V, out S);
            Console.WriteLine($"Объем: {V}\nПлощадь поверхности: {S}");

            Console.WriteLine();
            Console.Write("Нажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
        static void CalcCube(double a, out double volume, out double surfaceArea)
        {
            volume = a * a * a;
            surfaceArea = a * a * 6;
        }
    }
}
