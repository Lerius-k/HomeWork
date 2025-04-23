namespace Task8_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a;
            Console.WriteLine("\"Проверка возраста\"");
            Console.WriteLine();
            try
            {
                Console.Write("Введите возраст: ");
                a = Convert.ToInt32(Console.ReadLine());
                ValidateAge(a);
                Console.WriteLine();
                Console.WriteLine("Возраст корректный.");
            }
            catch (FormatException e)
            {
                Console.WriteLine();
                Console.WriteLine($"Некорректный ввод: {e.Message}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
        static void ValidateAge(int Age)
        {
            if (Age < 0)
                throw new ArgumentException("Возраст не может быть отрицательным");

            if (Age > 150)
                throw new ArgumentException("Слишком большой возраст");
        }
    }
}
