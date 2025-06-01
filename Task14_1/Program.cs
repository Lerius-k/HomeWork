namespace Task14_1
{
    internal class Program
    /*Требования:
Создайте список для хранения фамилий игроков (тип string).
Реализуйте следующие операции:
    Добавление игроков "Иванов", "Петров", "Сидоров" методом Add.
    Вставка игрока "Козлов" на позицию с индексом 1 методом Insert.
    Проверка наличия игрока "Петров" в команде методом Contains.
    Удаление игрока "Сидоров" методом Remove.
    Поиск индекса игрока "Козлов" методом IndexOf.
    Сортировка списка по алфавиту методом Sort.
    Выведите текущий состав команды в формате:
       1. Иванов  
       2. Козлов  
       3. Петров 
    Проверьте, пуст ли список (свойство Count), и очистите его методом Clear.*/
    {
        static void Main(string[] args)
        {
            List<string> players = new List<string>();

            players.Add("Иванов");
            players.Add("Петров");
            players.Add("Сидоров");

            players.Insert(1, "Козлов");

            bool getPlayer = players.Contains("Петров");
            Console.WriteLine($"Есть ли игрок \"Петров\" в команде? {getPlayer}");

            players.Remove("Сидоров");

            int sportPlayerIndex = players.IndexOf("Козлов");
            Console.WriteLine($"Индекс игрока \"Козлов\": {sportPlayerIndex}");

            players.Sort();

            Console.WriteLine("\nТекущий состав команды:");
            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {players[i]}");
            }

            bool emptyList = players.Count == 0 ? true : false;
            Console.WriteLine($"Команда пуста? {emptyList}");

            players.Clear();
            Console.WriteLine($"\nКоличество игроков после очистки: {players.Count}");
            Console.ReadKey();
        }
    }
}
