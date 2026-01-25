namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.ReadKey();
        }
    }
    public class ShoppingCartService
    {
        public decimal CalculateTotalPrice(string customerType, List<decimal> itemPrices)
        {
            decimal baseTotal = 0;
            for (int i = 0; i < itemPrices.Count; i++)
            {
                baseTotal += itemPrices[i];
            }

            decimal discount = 0;

            if (customerType == "Regular")
            {
                discount = baseTotal * 0.05m; // 5%
            }
            else if (customerType == "Premium")
            {
                discount = baseTotal * 0.15m; // 15%
                if (discount > 1000)
                {
                    discount = 1000 + (discount - 1000) * 0.1m;
                }
            }
            else if (customerType == "VIP")
            {
                discount = baseTotal * 0.20m; // 20%
            }

            decimal finalPrice = baseTotal - discount;

            Console.WriteLine($"Base: {baseTotal}, Discount: {discount}, Final: {finalPrice}");
            return finalPrice;
        }

        public decimal CalculateTotalPriceWithQuantities(string customerType, Dictionary<decimal, int> itemsWithQuantities)
        {
            List<decimal> allPrices = new List<decimal>();
            foreach (var item in itemsWithQuantities)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    allPrices.Add(item.Key);
                }
            }
            return CalculateTotalPrice(customerType, allPrices);
        }
    }
}
