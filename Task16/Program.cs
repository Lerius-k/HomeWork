namespace Task16
{
    internal class Program
    {
        static int[] Method1(int size)
        {
            Console.WriteLine("Генерация массива...");
            var random = new Random();
            int[] array = new int[size];

            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(1, 10);
                Console.Write($"{array[i]} ");
                Thread.Sleep(1000);
            }

            Console.WriteLine("\nМассив сгенерирован!");
            return array;
        }

        static int Method2(int[] array)
        {
            Console.WriteLine("\nВычисление среднего арифметического...");
            int sum = 0;

            foreach (int num in array)
            {
                sum += num;
                Thread.Sleep(800);
            }
            Console.WriteLine("Среднее арифметическое вычислено!");
            return sum/array.Length;
        }
        static async Task<int[]> Method1Async(int size)
        {
            Console.WriteLine("\nMethod1Async запущен");
            int[] array = await Task.Run(() => Method1(size));
            Console.WriteLine("Method1Async завершен");
            return array;
        }

        static async Task<int> Method2Async(int[] array)
        {
            Console.WriteLine("\nMethod2Async запущен");
            int result = await Task.Run(() => Method2(array));
            Console.WriteLine("Method2Async завершен");
            return result;
        }
       
        static async Task Main(string[] args)
        {
            Console.WriteLine("Main запущен");

            //Задачи продолжения
            Console.WriteLine("\nЗадачи продолжения");

            Task<int[]> task1 = Task.Run(() => Method1(10));
            Task<int> task2 = task1.ContinueWith(t =>
            {
                int[] array = t.Result;
                return Method2(array);
            });
            Console.WriteLine(task2.Result);
            Console.WriteLine("Задачи продолжения завершены");


            //async / await
            Console.WriteLine("\nЗадачи async / await");

            int[] array = await Method1Async(10);
            int r = await Method2Async(array);
            Console.WriteLine(r);
            Console.WriteLine("Задачи async / await завершены");


            Console.WriteLine("Main завершен");
            Console.ReadKey();
        }

    }

}
