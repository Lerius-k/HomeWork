namespace BinarySearch_demo_v2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Найти число s в массиве");
            Console.WriteLine();
            Console.Write("Введите искомое значение: ");
            int s = Convert.ToInt32(Console.ReadLine()); //искомое значение (тестовые значения 33 11 7 15 50)
            Console.WriteLine();
            const int n = 8;
            int[] t = new int[n] { 16, 30, 33, 11, 12, 18, 46, 13 };
            Console.WriteLine("Массив: ");
            for (int i = 0; i < n; i++)
            {
                Console.Write("{0,3}", t[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Отсортированный массив: ");
            for (int i = 0; i < n-1; i++) //сортировка
            {
                for (int j = i + 1; j < n; j++)
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
            for (int i = 0; i < n; i++) // выводит сортированный мвссив
            {
                Console.Write("{0,3}", t[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < n; i++) // выводит индексы сортированного массива
            {
                Console.Write("{0,3}", i);
            }
            Console.WriteLine();
            Console.WriteLine();
            //бинарный поиск
            string answer = "Найден";
            int cOfM = n / 2;
            int step = n % 2 == 0 ? n / 2 : n / 2 + 1; // проверяем на четность длину массива. накидываем единицу, чтобы нивелировать "округление" при целочисленном делении
            bool f = true;
            while (t[cOfM] != s && step >= 0)
            {
                step = step % 2 == 0 ? step / 2 : step / 2 + 1; //проверяем на четность длину оставшейся части массива. накидываем единицу, чтобы нивелировать "округление" при целочисленном делении
                if (t[cOfM] > s)
                {
                    cOfM = cOfM - step;
                }
                else
                {
                    cOfM = cOfM + step;
                }
                if (cOfM<0||cOfM>=n)
                {
                    answer = "Не найден";
                    f = false;
                    break;
                }
            }
            Console.WriteLine(answer);
            Console.WriteLine("Индекс в сортированном массиве: {0}", cOfM);
            Console.WriteLine();
            Console.WriteLine("Флаг: {0}", f);
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
