namespace BinarySearch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Найти число 33");
            Console.WriteLine();
            int s = 11; //искомое значение
            const int n = 22;
            int[] t = new int[n] { 16, 30, 33, 11, 12, 18, 46, 13, 45, 14, 34, 10, 46, 19, 47, 17, 30, 10, 46, 15, 47, 16 };
            Console.Write("Массив: ");
            for (int i = 0; i < 22; i++)
            {
                Console.Write("{0} ", t[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Отсортированный массив: ");
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

                Console.Write("{0} ", t[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
            string answer = "test";
            int cOfM = n / 2;
            int step = n / 2;
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
                    answer = "не найден";
                    break;
                }
                else
                {
                    answer = "найден";

                }
            }
            Console.WriteLine();
            Console.WriteLine(cOfM);
            Console.WriteLine(answer);
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
        static void test(string[] args)
        {
            Console.WriteLine("Найти число 33");
            Console.WriteLine();
            const int n = 22;
            int L = n;
            int[] t = new int[n] { 16, 30, 33, 11, 12, 18, 46, 13, 45, 14, 34, 10, 46, 19, 47, 17, 30, 10, 46, 15, 47, 16 };
            Console.Write("Массив: ");
            for (int i = 0; i < 22; i++)
            {
                Console.Write("{0} ", t[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Отсортированный массив: ");
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

                Console.Write("{0} ", t[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
            string answer;
            int cOfM = L / 2;
            //int lML = L - cOfM;
            //int lMR = L - lML;
            if (t[cOfM] == 33)
            {
                answer = "найден";
            }
            else
            {
                if (t[cOfM] > 33)
                {
                    int temp = cOfM;
                    cOfM = temp - (temp / 2);
                    //lML = temp - cOfM;
                    //lMR = temp - lML;
                }
                else
                {
                    int temp = cOfM;
                    cOfM = cOfM + (temp / 2);
                    //lML = lML + temp - cOfM;
                    //lMR = L - cOfM;
                }
            }
            if (t[cOfM] == 33)
            {
                answer = "найден";
            }
            else
            {
                if (t[cOfM] > 33)
                {
                    int temp = cOfM;
                    cOfM = temp - (temp / 2);
                    //lML = temp - cOfM;
                    //lMR = temp - lML;
                }
                else
                {
                    int temp = cOfM;
                    cOfM = cOfM + (temp / 2);
                    //lML = lML + temp - cOfM;
                    //lMR = L - cOfM;
                }
            }
            if (cOfM == 0)
            {
                answer = "не найден";
            }
            else
            {
                answer = "не понял";
            }
            Console.WriteLine();
            Console.WriteLine(cOfM);
            Console.WriteLine(answer);
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
