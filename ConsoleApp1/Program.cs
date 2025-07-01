namespace ConsoleApp1
{
    internal class Program
    {
        static void Method1()
        {
            Console.WriteLine("Method1 запущен");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Method1 выводит счетчик = {i}");
                Thread.Sleep(1000);
            }
            Console.WriteLine("Method1 завершен");
        }

        static int Method3()
        {
            Console.WriteLine("Method3 запущен");
            int s = 0;
            for (int i = 0; i < 10; i++)
            {
                s += i;
                Thread.Sleep(500);
            }
            Console.WriteLine("Method3 завершен");
            return s;
        }

        static async Task Method1Async()
        {
            Console.WriteLine("Method1Async запущен");
            await Task.Run(() => Method1());
            Console.WriteLine("Method1Async завершен");
        }

        static async Task<int> Method3Async()
        {
            Console.WriteLine("Method3Async запущен");
            int result = await Task.Run(() => Method3());
            Console.WriteLine("Method3Async завершен");
            return result;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Main запущен");

            Method1Async();
            int r = Method3Async().Result;
            Console.WriteLine(r);

            Console.WriteLine("Main завершен");
            Console.ReadKey();
        }
    }
}
