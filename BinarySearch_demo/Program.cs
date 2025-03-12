namespace BinarySearch_demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Найти число s в массиве");
            Console.WriteLine();
            Console.Write("Введите искомое значение: ");
            int s = Convert.ToInt32(Console.ReadLine()); //искомое значение 33 11 7
            Console.WriteLine();
            const int n = 22;
            int[] t = new int[n] { 16, 30, 33, 11, 12, 18, 46, 13, 45, 14, 34, 10, 46, 19, 47, 17, 30, 10, 46, 15, 47, 16 };
            Console.WriteLine("Массив: ");
            for (int i = 0; i < 22; i++)
            {
                Console.Write("{0,3}", t[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Отсортированный массив: ");
            for (int i = 0; i < 21; i++)
            {
                for (int j = i + 1; j < 22; j++)
                {
                    int temp;
                    if (t[i] > t[j])
                    {
                        temp = t[i];
                        t[i] = t[j];
                        t[j] = temp;
                    }
                }
            }
            for (int i = 0; i < 22; i++) // выводит сортированный мвссив
            {
                Console.Write("{0,3}", t[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < 22; i++) // выводит индексы сортированного массива
            {
                Console.Write("{0,3}", i);
            }
            Console.WriteLine();
            Console.WriteLine();
            string answer = "test";
            int cOfM = n / 2;
            int step = n / 2 + 1; // накидываем единицу, чтобы нивелировать "округление" при целочисленном делении
            bool f = true;
            while (t[cOfM] != s)
            {
                step = step / 2;
                if (t[cOfM] > s)
                {
                    cOfM = cOfM - step;
                }
                else
                {
                    cOfM = cOfM + step;
                }
                if (step == 0)
                {
                    answer = "Не найден";
                    break;
                }
                else
                {
                    answer = "Найден";
                    f = false;
                }
            }
            Console.WriteLine(answer);
            Console.WriteLine("Индекс в сортированном массиве: {0}", cOfM);
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
