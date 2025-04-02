namespace Task5_6
{
    internal class Program
    {
        static void Main(string[] args)
        //Сформировать двумерный массив 
        {
            Console.WriteLine("Сформировать двумерный массив");
            Console.WriteLine();
            const int n = 5;
            int[,] t = new int[n, n];
            Console.WriteLine("Массив: ");
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    t[i, j] = (i + j) % 2 == 0 ? 1 : 0;
                    Console.Write("{0} ", t[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
