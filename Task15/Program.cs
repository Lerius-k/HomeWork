using System.Collections.Generic;

namespace Task15
{
    /*
    Модель  компьютера  характеризуется  кодом  и  названием  марки компьютера,  
    типом  процессора,  частотой  работы  процессора,  объемом оперативной памяти, 
    объемом жесткого диска, объемом памяти видеокарты, стоимостью компьютера в условных 
    единицах и количеством экземпляров, имеющихся в наличии. 
    Создать список, содержащий 6-10 записей с различным набором значений характеристик.

Определить:
- все компьютеры с указанным процессором. Название процессора запросить у пользователя;
- все компьютеры с объемом ОЗУ не ниже, чем указано. Объем ОЗУ запросить у пользователя;
- вывести весь список, отсортированный по увеличению стоимости;
- вывести весь список, сгруппированный по типу процессора;
- найти самый дорогой и самый бюджетный компьютер;
- есть ли хотя бы один компьютер в количестве не менее 30 штук?
    */
    public class Computer
    {
        public string Brand { get; set; }
        public string Processor { get; set; }
        public double GHz { get; set; }
        public int RAM { get; set; }
        public int SDD { get; set; }
        public int VideoMemory { get; set; }
        public decimal Cost { get; set; }
        public int InStock { get; set; }

        public override string ToString()
        {
            return $"Производитель: {Brand}, Процессор: {Processor}, Частота процессора: {GHz} ГГц, ОЗУ: {RAM} ГБ, Накопитель: {SDD} ГБ, Видеопамять: {VideoMemory} ГБ, Стоимость: {Cost} ус.ед., В наличии {InStock} шт.;";
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Начало программы...");
            Console.WriteLine();

            List<Computer> Products = new List<Computer>()
            {
                new Computer() {Brand = "Lenovo", Processor = "Intel", GHz = 2.4, RAM = 16, SDD = 1024, VideoMemory = 4, Cost = 30000, InStock = 24},
                new Computer() {Brand = "Lenovo", Processor = "AMD", GHz = 3.4, RAM = 8, SDD = 512, VideoMemory = 2, Cost = 60000, InStock = 2},
                new Computer() {Brand = "Lenovo", Processor = "Intel", GHz = 4.4, RAM = 32, SDD = 512, VideoMemory = 1, Cost = 40000, InStock = 14},
                new Computer() {Brand = "HUAWEI", Processor = "AMD", GHz = 2.0, RAM = 16, SDD = 256, VideoMemory = 8, Cost = 80000, InStock = 65},
                new Computer() {Brand = "HUAWEI", Processor = "Intel", GHz = 3.0, RAM = 16, SDD = 512, VideoMemory = 4, Cost = 100000, InStock = 8},
                new Computer() {Brand = "HUAWEI", Processor = "Intel", GHz = 4.0, RAM = 8, SDD = 512, VideoMemory = 6, Cost = 20000, InStock = 64},
                new Computer() {Brand = "ASUS", Processor = "AMD", GHz = 1.2, RAM = 8, SDD = 1024, VideoMemory = 6, Cost = 50000, InStock = 8},
                new Computer() {Brand = "ASUS", Processor = "Intel", GHz = 1.7, RAM = 16, SDD = 512, VideoMemory = 4, Cost = 70000, InStock = 12},
                new Computer() {Brand = "Acer", Processor = "AMD", GHz = 2.7, RAM = 64, SDD = 512, VideoMemory = 2, Cost = 120000, InStock = 4},
                new Computer() {Brand = "Acer", Processor = "Intel", GHz = 3.5, RAM = 16, SDD = 512, VideoMemory = 4, Cost = 35000, InStock = 6},
            };

            //вывод списка товаров
            Console.WriteLine("Список всех товаров:");
            foreach (Computer pC in Products)
            {
                Console.WriteLine(pC.ToString());
            }
            Console.WriteLine();

            //поиск по процессору
            Console.WriteLine("Поиск...\nУкажите процессор: ");
            string searchProcessor = Console.ReadLine(); //нужно делать проверку ввода пользователем

            var searchComputersProcessor = Products
                .Where(computer => computer.Processor == searchProcessor)
                .ToList();

            Console.WriteLine($"\nТовары по бренду \"{searchProcessor}\":");
            foreach (Computer pC in searchComputersProcessor)
            {
                Console.WriteLine(pC.ToString());
            }
            Console.WriteLine();

            //поиск по ОЗУ
            Console.WriteLine("Поиск...\nУкажите нижний порог ОЗУ, ГБ: ");
            int searchRAM = Convert.ToInt32(Console.ReadLine());

            var searchComputersRAM = Products
                .Where(computer => computer.RAM >= searchRAM)
                .ToList();

            Console.WriteLine($"\nТовары с ОЗУ не ниже {searchRAM} ГБ:");
            foreach (Computer pC in searchComputersRAM)
            {
                Console.WriteLine(pC.ToString());
            }
            Console.WriteLine();

            //сортировка по цене
            var sortedByCost = Products
                .OrderBy(computer => computer.Cost);

            Console.WriteLine($"\nТовары с сортировкой по возростанию цены:");
            foreach (Computer pC in sortedByCost)
            {
                Console.WriteLine(pC.ToString());
            }
            Console.WriteLine();

            //группировка по процессору
            var groupingByProcessor = Products
                .GroupBy(computer => computer.Processor);

            Console.WriteLine($"\nТовары с группировкой по процессору:");
            foreach (var group in groupingByProcessor)
            {
                Console.WriteLine($"Процессор: {group.Key}");
                foreach (var pC in group)
                {
                    Console.WriteLine(pC.ToString());
                }
            }
            Console.WriteLine();


            Console.WriteLine($"Самый дорогой компютер: {sortedByCost.LastOrDefault()}");
            Console.WriteLine($"Самый бюджетный компютер: {sortedByCost.FirstOrDefault()}");
            Console.WriteLine($"\nEсть ли хотя бы один компьютер в количестве не менее 30 штук? {Products.Any(pC => pC.InStock >= 30)}");

            Console.WriteLine();
            Console.Write("Для завершиния нажмите любую клавишу");
            Console.ReadKey();
        }
    }
}
