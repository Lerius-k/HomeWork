namespace Task5_1
{
    internal class Program
    {
        static void Main(string[] args)
        //Сформировать одномерный массив из 10 случайных чисел из диапазона [0, 100].
        //Определить, каких чисел больше – четных или нечетных
        {
            Console.WriteLine("Каких чисел больше – четных или нечетных?");
            Console.WriteLine();
            const int n = 10;
            int[] t = new int[n];
            Random random = new Random();
            int ch = 0; // количество четных чисел
            int notCh = 0; // количество нечетных чисел
            Console.Write("Массив: ");
            for (int i = 0; i < n; i++)
            {
                t[i] = random.Next(0, 101);
                ch += t[i] % 2 == 0 ? 1 : 0;
                notCh += t[i] % 2 != 0 ? 1 : 0;
                Console.Write("{0} ", t[i]);
            }
            Console.WriteLine();
            Console.WriteLine("Четных: {0}", ch);
            Console.WriteLine("Нечетных: {0}", notCh);
            Console.WriteLine();
            string answer = ch > notCh ? "четных" : ch < notCh ? "нечетных" : "поровну";
            Console.WriteLine("Ответ: {0}", answer);
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
