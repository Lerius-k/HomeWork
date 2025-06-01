namespace Task14_2
{
    internal class Program
    {
        /*Требования:
    Создайте словарь и добавьте в него 3 товара:
        Ноутбуки ("A001") — 10 шт.
        Смартфоны ("B205") — 25 шт.
        Наушники ("C307") — 15 шт.
    Реализуйте следующие операции:
        Проверьте наличие товара с артикулом "B205" (ContainsKey).
        Обновите количество ноутбуков до 8 (продали 2 шт.) через индексатор.
        Получите количество наушников безопасным способом (TryGetValue).
        Увеличьте количество смартфонов на 5 (новый завоз).
        Удалите наушники из инвентаря (Remove).
    Выведите текущий инвентарь в формате:
       Артикул: A001, Количество: 8  
       Артикул: B205, Количество: 30  
    Проверьте, пуст ли словарь (Count), затем полностью очистите инвентарь (Clear).*/
        static void Main(string[] args)
        {
            Dictionary<string, int> storeHouse = new Dictionary<string, int>();

            storeHouse.Add("A001", 10);
            storeHouse.Add("B205", 25);
            storeHouse.Add("C307", 15);

            bool hasSmartphone = storeHouse.ContainsKey("B205");
            Console.WriteLine($"Есть ли товар с артикулом B205? {hasSmartphone}");
            storeHouse["A001"] -= 2;

            if (storeHouse.TryGetValue("C307", out int countHeadphones))
            {
                Console.WriteLine($"\nКоличество наушников: {countHeadphones}");
            }

            storeHouse["B205"] += 5;

            storeHouse.Remove("C307");

            Console.WriteLine("\nТекущий инвентарь:");
            foreach (KeyValuePair<string, int> products in storeHouse)
            {
                Console.WriteLine($"Артикул: {products.Key}, Количество: {products.Value}");
            }

            bool productsList = storeHouse.Count == 0;
            Console.WriteLine($"\nИнвентарь пуст? {productsList}");

            storeHouse.Clear();
            Console.WriteLine($"\nКоличество товаров после очистки: {storeHouse.Count}");
            Console.ReadKey();
        }
    }
}
