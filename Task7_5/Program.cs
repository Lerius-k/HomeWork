namespace Task7_5
{
    internal class Program
    {
        static void Main(string[] args)
        /*Напишите перегруженные методы Multiply, 
        которые могут умножать два числа (целые или дробные) 
        и возвращать результат.*/
        {
            Console.WriteLine("\"Найти произведение\"");
            Console.WriteLine();

            Multiply(2, 3);
            Console.WriteLine();
            Multiply(2.5, 3.5);
            Console.WriteLine();
            Multiply(2, 3.5);
            Console.WriteLine();
            Multiply(2.5, 3);
            Console.WriteLine();


            Console.WriteLine();
            Console.Write("Нажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
        static void Multiply(int a, int b)
        {
            Console.WriteLine(a * b);
        }
        static void Multiply(double a, double b)
        {
            Console.WriteLine(a * b);
        }
        static void Multiply(int a, double b)
        {
            Console.WriteLine(a * b);
        }
        static void Multiply(double a, int b)
        {
            Console.WriteLine(a * b);
        }
    }
}

