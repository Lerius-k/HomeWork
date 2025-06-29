using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task13_1
{
    internal class Program
    {
        delegate int Transformer(int a);
        static void Main(string[] args)
        {
            Console.WriteLine("Начало программы...");
            Console.WriteLine();
            int[] a = [-3, 4, 1, 0, 15];

            /* 
            Вариант без скрытых методов (на null не проверяю, так как контролирую)
            
            Transformer transformer = MultiplicationByTwo;

            int [] res1 = Transform(a, transformer);
            Console.WriteLine(string.Join(", ", res1));

            transformer -= MultiplicationByTwo;
            transformer += Module;
            int[] res2 = Transform(res1, transformer);
            Console.WriteLine(string.Join(", ", res2));
            
            transformer -= Module;
            transformer += Squaring;
            int[] res3 = Transform(res2, transformer);
            Console.WriteLine(string.Join(", ", res3));
            */

            //вариант со скрытыми методами через лямбда выражение на месте делегата

            int[] res1 = Transform(a, n => n * 2);
            Console.WriteLine(string.Join(", ", res1));
            int[] res2 = Transform(res1, n => Math.Abs(n));
            Console.WriteLine(string.Join(", ", res2));
            int[] res3 = Transform(res2, n => n * n);
            Console.WriteLine(string.Join(", ", res3));


            Console.WriteLine();
            Console.Write("Для завершиния нажмите любую клавишу");
            Console.ReadKey();
        }
        static int[] Transform(int[] array, Transformer transformer)
        {
            int[] result = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = transformer(array[i]);
            }
            return result;
        }
        // методы для делегата при указании метода в делегате
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

        /* методы, работающие с массивами, которые планировал передать делегату. 
         * задача звучала по другому, поэтому закоментил.
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
