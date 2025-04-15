namespace Task6_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\"Является ли предложение палиндромом без учёта пробелов и регистра?\"");
            Console.WriteLine();
            Console.Write("Введите предложение: ");
            Console.WriteLine();
            string str1 = Console.ReadLine();
            string str2 = str1.Replace(" ", "").ToLower();
            char[] charArray = new char[str2.Length];

            for (int i = 0; i < str2.Length; i++)
            {
                charArray[i] = str2[i];
            }

            int start = 0;
            int end = str2.Length - 1;
            while (start < end)
            {
                char t = charArray[start];
                charArray[start] = charArray[end];
                charArray[end] = t;
                start++;
                end--;
            }

            string newStr = new string(charArray);
            string answer = String.Compare(str2, newStr) == 0 ? "Да" : "Нет";

            Console.WriteLine();
            Console.WriteLine($"первоначальная строка: {str1}");
            Console.WriteLine($"упрощенная     строка: {str2}");
            Console.WriteLine($"новая          строка: {newStr}");
            Console.WriteLine($"является палиндромом? - {answer}");
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
