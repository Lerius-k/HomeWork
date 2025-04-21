using System.Text;

namespace Task6_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Сформируйте строку в формате: \"Имя: [имя], Возраст: [возраст], Город: [город]\"");
            Console.WriteLine();
            StringBuilder sb = new StringBuilder();
            Console.Write("Укажите имя: ");
            sb.Append("Имя: " + Console.ReadLine());
            Console.Write("Укажите возраст: ");
            sb.Append("; Возраст: " + Console.ReadLine());
            Console.Write("Укажите город: ");
            sb.Append("; Город: " + Console.ReadLine() + ".");
            Console.WriteLine();
            Console.WriteLine(sb);
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
