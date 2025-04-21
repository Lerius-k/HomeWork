using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task6_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] phoneNumbers =
            {
                "+7(123)456-78-90",
                "+7(123)456-78-950",
                "+7(123)456-758-904",
                "+7(123)45678-90",
                "+7(123)456-78-90",
                "+7(1233)456-78-90",
                "+7(123)456-78-90",
                "+6(123)4567890",
                "8(123)456-78-90",
                "8(123) 456 -78-90",
                "8 (123) 456-78-90",
            };

            Regex regex = new Regex(@"^(\+7|8)\([0-9]{3}\)[0-9]{3}-[0-9]{2}-[0-9]{2}$");

            foreach (string str in phoneNumbers)
                if (regex.IsMatch(str))
                    Console.WriteLine("\"{0}\" - YES", str);
                else
                    Console.WriteLine("\"{0}\" -  no", str);
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
