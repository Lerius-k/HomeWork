namespace Task1_Edited
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ShoppingCartService cart = new ShoppingCartService();
            var items = new Dictionary<decimal, int>
                {
                    { 100, 2 }, // 2 товара по 100
                    { 300, 1 }, // 1 товар по 300
                    { 250, 4 }, // 4 товара по 250
                    { 200, 1 }  // 1 товар по 200
                };
            cart.CalculateTotalPriceWithQuantities("Premium", items);
            Console.ReadKey();
        }
    }
    public class ShoppingCartService // упрощенная версия класса
    {
        public decimal CalculateTotalPriceWithQuantities(string customerType, Dictionary<decimal, int> itemsWithQuantities)
        {
            decimal baseTotal = 0;
            List<decimal> allPrices = new List<decimal>();
            foreach (var item in itemsWithQuantities) //перебираем все цены в корзине товаров
            {
                for (int i = 0; i < item.Value; i++)
                {
                    baseTotal += item.Key; // срузу суммируем цены
                }
            }
            decimal discount = customerType == "Regular" ? baseTotal * 0.05m : 0; // определяем нужна скидка в 5% или нет

            decimal finalPrice = baseTotal - discount; // считаем финальную стоимость

            Console.WriteLine($"Base: {baseTotal}, Discount: {discount}, Final: {finalPrice}");
            return finalPrice;
        }
    }
}
