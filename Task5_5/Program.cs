namespace Task5_5
{
    internal class Program
    {
        static void Main(string[] args)
        /*Сформировать одномерный массив из 10 случайных чисел в диапазоне [-50, 50].
         *Первые 5 элементов упорядочить по возрастанию, вторые 5 – по убыванию.
         *Вывести отсортированный таким образом массив на экран*/
        {
            Console.WriteLine("Первые 5 элементов упорядочить по возрастанию, вторые 5 – по убыванию.");
            Console.WriteLine();
            const int n = 10;
            int[] t = new int[n];
            Random random = new Random();
            Console.Write("Массив 1: ");
            for (int i = 0; i < n; i++)
            {
                t[i] = random.Next(-50, 51);
                Console.Write("{0,4}", t[i]);
            }
            Console.WriteLine();

            int mid = n / 2;
            Console.Write("Массив 2: ");
            for (int i = 0; i < mid - 1; i++) //сортировка
            {
                for (int j = i + 1; j < mid; j++)
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
            for (int i = mid; i < n - 1; i++) //сортировка
            {
                for (int j = i + 1; j < n; j++)
                {
                    int temp;
                    if (t[i] < t[j])
                    {
                        temp = t[i];
                        t[i] = t[j];
                        t[j] = temp;
                    }
                }
            }

            for (int i = 0; i < n; i++) // выводит сортированный мвссив
            {
                Console.Write("{0,4}", t[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
