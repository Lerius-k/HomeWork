namespace Task2_7
{
    internal class Program
    {/* мне кажется, без обмана пользователя это никак не сделать, не используя
          * третью переменную */
        static void Main2(string[] args)
        { // подмена переменных при вводе
            int a, b;
            Console.WriteLine("'Замена последовательности двух чисел'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Ведите первое число 'a'");
            b = a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ведите второе число 'b'");
            a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("После замены получено значение ab: {0} {1}", a, b);
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
        static void Main(string[] args)
        { // подмена переменных при выводе
            int a, b;
            Console.WriteLine("'Замена последовательности двух чисел'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Ведите первое число 'a'");
            a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ведите второе число 'b'");
            b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Изначальное знечение ab: {0} {1}", a, b);
            Console.WriteLine("После замены получено значение ab: {1} {0}", a, b);
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
