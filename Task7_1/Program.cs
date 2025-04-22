namespace Task7_1
{
    internal class Program
    {
        static void Main(string[] args) //создайте метод Square для вычисления площади треугольника по длинам его сторон
        {
            Console.WriteLine("\"Сравнить площиди треугольников\"");
            Console.WriteLine();
            double a, b, c, S1, S2;
            Console.WriteLine("Укажите стороны первого треугольника");
            Console.Write("a: ");
            a = Convert.ToDouble(Console.ReadLine());
            Console.Write("b: ");
            b = Convert.ToDouble(Console.ReadLine());
            Console.Write("c: ");
            c = Convert.ToDouble(Console.ReadLine());
            S1 = Square(a, b, c);
            Console.WriteLine($"Полощадь S1 = {S1:0.00}");
            Console.WriteLine();
            Console.WriteLine("Укажите стороны второго треугольника");
            Console.Write("a: ");
            a = Convert.ToDouble(Console.ReadLine());
            Console.Write("b: ");
            b = Convert.ToDouble(Console.ReadLine());
            Console.Write("c: ");
            c = Convert.ToDouble(Console.ReadLine());
            S2 = Square(a, b, c);
            Console.WriteLine($"Полощадь S2 = {S2:0.00}");
            Console.WriteLine();

            string answer = S1 > S2 ? $"Площидь первого треугольника больше.\nПлощадь = {S1:0.00}" : $"Площидь второго треугольника больше.\nПлощадь = {S2:0.00}";
            Console.WriteLine(answer);

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
        static double Square(double a, double b, double c)
        {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * p * (p - b) * p * (p - c));
        }
    }
}
