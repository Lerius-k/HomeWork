namespace Task2_1
{
    internal class Program
    {
        static void Main(string[] args) /*Вводятся три числа.
                                        Обменять их местами циклически.
                                        Например, вводятся a=1, b=2, c=3.
                                        После обмена в переменных должны оказаться значения a=3, b=1, c=2.*/
        {
            int a, b, c, t;
            Console.WriteLine("'Циклическое смещение последовательности чисел'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Ведите первое число 'a'");
            a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ведите второе число 'b'");
            b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ведите третье число 'c'");
            c = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Изначальное знечение abc: {0} {1} {2}", a, b, c);
            t = a;
            a = b;
            b = c;
            c = t;
            Console.WriteLine("После циклического смещения получено значение abc: {0} {1} {2}", a, b, c);
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
