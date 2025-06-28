using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task13_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Начало программы...");
            Console.WriteLine();

            //int[] numbers = new[] { 1, -2, 3, -4 };

            int a = -3;

            a = Module(a);
            Console.Write($"{a} ");
            Console.WriteLine();

            a = MultiplicationByTwo(a);
            Console.Write($"{a} ");
            Console.WriteLine();

            a = Squaring(a);
            Console.Write($"{a} ");
            Console.WriteLine();

            Console.WriteLine();
            Console.Write("Для завершиния нажмите любую клавишу");
            Console.ReadKey();
        }

        static int MultiplicationByTwo(int number)
        {
            return number * 2;
        }
        static int Squaring(int number)
        {
            return number * number;
        }
        static int Module(int number)
        {
            return Math.Abs(number);
        }

        /*
        static int[] MultiplicationByTwo(int[] ints)
        {
            for (int i = 0; i < ints.Length; i++)
            {
                int a = ints[i];
                ints[i] = a * 2;
            }
            return ints;
        }
        static int[] Squaring(int[] ints)
        {
            for (int i = 0; i < ints.Length; i++)
            {
                int a = ints[i];
                ints[i] = a * a;
            }
            return ints;
        }
        static int[] Module(int[] ints)
        {
            for (int i = 0; i < ints.Length; i++)
            {
                int a = ints[i];
                ints[i] = Math.Abs(a);
            }
            return ints;
        }
        */
    }
}
