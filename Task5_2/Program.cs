namespace Task5_2
{
    internal class Program
    {
        static void Main(string[] args)
        //Сформировать одномерный массив из 10 случайных чисел из диапазона [-20, 20].
        //Определить количество положительных, отрицательных и равных нулю элементов
        {
            Console.WriteLine("Определить количество положительных, отрицательных и равных нулю элементов");
            Console.WriteLine();
            const int n = 10;
            int[] t = new int[n];
            Random random = new Random();
            int countPlus = 0; // количество положительных чисел
            int countMinus = 0; // количество отрицательных чисел
            int countZero = 0; // количество равных нулю чисел
            Console.Write("Массив: ");
            for (int i = 0; i < n; i++)
            {
                t[i] = random.Next(-20, 21);
                countPlus += t[i] > 0 ? 1 : 0;
                countMinus += t[i] < 0 ? 1 : 0;
                countZero += t[i] == 0 ? 1 : 0;
                Console.Write("{0} ", t[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Положительных: {0}", countPlus);
            Console.WriteLine("Отрицательных: {0}", countMinus);
            Console.WriteLine("Равных нулю: {0}", countZero);
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
