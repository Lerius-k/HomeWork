namespace Task14_3
{
    internal class Program
    {
        /*Разработайте систему управления подписчиками, используя HashSet<string> для хранения email-адресов. Система должна обеспечивать уникальность подписчиков и поддерживать основные операции с множествами.
         Требования:
    Создайте HashSet для хранения email-подписчиков с регистронезависимым сравнением
    Реализуйте следующий функционал:
        Добавление новых подписчиков:
            alice@example.com
            bob@example.com
            charlie@example.com
        Попытку добавления дубликата с выводом результата операции
        Проверку наличия подписчиков в системе
    Создайте второе множество newSubscribers с подписчиками:
        bob@example.com (существующий)
        dave@example.com (новый)
        eve@example.com (новый)
    Выполните операции с множествами:
        Объединение множеств (добавление новых подписчиков)
        Поиск пересечения (общих подписчиков)
    Реализуйте:
        Удаление подписчика
        Проверку на подмножество
        Очистку всей коллекции*/
        static void Main(string[] args)
        {
            HashSet<string> userEmail = new HashSet<string>();

            userEmail.Add("alice@example.com");
            userEmail.Add("bob@example.com");
            userEmail.Add("charlie@example.com");

            bool addedDuplicate = userEmail.Add("alice@example.com");
            Console.WriteLine($"Дубликат alice@example.com добавлен? {addedDuplicate}");

            Console.WriteLine($"Есть ли bob@example.com в подписчиках? {userEmail.Contains("bob@example.com")}");
            Console.WriteLine($"Есть ли dave@example.com в подписчиках? {userEmail.Contains("dave@example.com")}");

            HashSet<string> newUsers = new HashSet<string> { "bob@example.com", "dave@example.com", "eve@example.com" };

            HashSet<string> allUsers = new HashSet<string>(userEmail);
            allUsers.UnionWith(newUsers);
            Console.WriteLine("\nПодписчики после объединения:");
            foreach (string Email in allUsers)
            {
                Console.WriteLine(Email);
            }

            HashSet<string> commonUsers = new HashSet<string>(userEmail);
            commonUsers.IntersectWith(newUsers);
            Console.WriteLine("\nОбщие подписчики:");
            foreach (string Email in commonUsers)
            {
                Console.WriteLine(Email);
            }

            Console.WriteLine($"\nУдалили charlie@example.com? {allUsers.Remove("charlie@example.com")}\nВсего подписчиков: {allUsers.Count}");

            HashSet<string> testGroup = new HashSet<string> { "alice@example.com", "bob@example.com" };
            Console.WriteLine($"\ntestGroup является подмножеством? {testGroup.IsSubsetOf(allUsers)}");

            allUsers.Clear();
            Console.WriteLine($"\nПодписчиков после очистки: {allUsers.Count}");

            Console.ReadKey();
        }
    }
}
