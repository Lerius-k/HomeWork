namespace Task6_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\"Найти самое длинное слово в предложении\"");
            Console.WriteLine();
            Console.Write("Введите предложение: ");
            Console.WriteLine();
            string str;
            string[] t;
            str = Console.ReadLine();
            t = str.Split(' ');
            int max = t[0].Trim().Length;
            string longWord = t[0].Trim();
            foreach (string a in t)
            {
                int temp;
                temp = a.Trim().Length;
                if (temp > max)
                {
                    max = temp;
                    longWord = a;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Самое длинное слово: \"{0}\"", longWord);
            Console.WriteLine("Количество символов: {0}", max);
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
        static void test(string[] args) //оптимизация, которую предложила нейронка
        {
            Console.WriteLine("\"Найти самое длинное слово в предложении\"\n");
            Console.Write("Введите предложение: ");
            string[] words = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
            if (words.Length == 0) { Console.WriteLine("Слов нет"); return; }
            var (longWord, max) = words.Aggregate((w: words[0], len: words[0].Length), (acc, w) => w.Length > acc.len ? (w, w.Length) : acc);
            Console.WriteLine($"\nСамое длинное слово: \"{longWord}\"\nКоличество символов: {max}\n\nНажмите любую клавишу...");
            Console.ReadKey();
            /*Оптимизации:
             * 1. Убраны лишние Trim() — Split() с RemoveEmptyEntries сам убирает пустые элементы.
             * 2. Использован Aggregate вместо foreach — меньше кода, функциональный подход.
             * 3. Сокращён вывод с помощью интерполяции строк ($).
             * 4. Добавлена проверка на пустой ввод.
             * 5. Убраны лишние переменные (str, t, temp).
             * 6. Оптимизирована работа с памятью за счёт null-условного оператора (?.).
             * 
             * Работает так же, но компактнее и чуть быстрее.*/

        }
    }
}
