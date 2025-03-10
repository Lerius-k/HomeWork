namespace Task4_6
{
    internal class Program
    {
        static void Main(string[] args) //Вводится n. Определить, является ли оно степенью 2-ки?
        {
            double n, result;
            Console.WriteLine("'Является ли число n степенью 2-ки?'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.Write("Введите число n: ");
            result = n = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            if (n == 1)
            {
                Console.WriteLine("Число {0} является степенью 2-ки. Степень 0", n);
                Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            }
            else
            {
                int i = 0;
                do
                {
                    result = result / 2;
                    i += 1;
                }
                while (result >= 2);
                if (result == 1)
                {
                    Console.WriteLine("Да. Число {0} является степенью 2-ки. Степень {1}", n, i);
                    Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
                }
                else
                {
                    Console.WriteLine("Нет. Число {0} не является степенью 2-ки", n);
                    Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
                }
            }
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
