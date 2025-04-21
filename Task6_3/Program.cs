namespace Task6_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\"Отчет о продажах\"");
            Console.WriteLine();
            Console.WriteLine("Данные\n----------");
            Console.Write("Укажите год: ");
            string year = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Укажите месяц: ");
            string month = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Укажите количество проданных товаров: ");
            int count = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Укажите сумму продаж: ");
            double totalCost = Convert.ToDouble(Console.ReadLine());
            double averageCost = totalCost / count;
            Console.WriteLine("----------\n\n//////////");
            Console.WriteLine();
            Console.WriteLine($"Отчёт о продажах за {month} {year} года\n\n----------");
            Console.WriteLine();

            Console.WriteLine(string.Format("Общая сумма продаж: {0:0,0.00} руб.", totalCost));
            Console.WriteLine(string.Format("Количество проданных товаров: {0:0,0} шт.", count));
            Console.WriteLine(string.Format("Средняя стоимость товара: {0:0,0.00} руб.", averageCost));
            Console.WriteLine();
            Console.WriteLine("----------");

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
