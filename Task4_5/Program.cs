namespace Task4_5
{
    internal class Program
    {
        static void Main(string[] args) //Запрашивать у пользователя число до тех пор, пока он не введет число из диапазона [20; 60]
        {
            double n;
            Console.WriteLine("'Введите число из диапазона [20; 60]'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            do
            {
                Console.Write("Введите число из диапазона [20; 60]: ");
                n = Convert.ToDouble(Console.ReadLine());
            }
            while (n < 20 || n > 60);
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Спасибо");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
