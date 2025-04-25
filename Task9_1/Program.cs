using System.Threading.Channels;

namespace Task9_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //начало тестовой части с экспериментами и возможностями класса
            Book bookTest = new Book("Л. Толстой", "Война и мир", 1869, 1225);
            Book bookTest2 = new("Война и мир", 1869, 1225);
            Console.WriteLine(bookTest);

            bookTest.Year = 1578;
            bookTest.Pages = 325;

            Console.WriteLine(bookTest);
            Console.WriteLine(bookTest.Year);
            Console.WriteLine(bookTest.Pages);
            bookTest.Pages = 360;
            Console.WriteLine(bookTest.Pages);

            Console.WriteLine();
            Console.WriteLine(bookTest);
            Console.WriteLine(bookTest2);
            //конец тестовой части

            Console.WriteLine();

            Book book = new Book("Л. Толстой", "Война и мир", 1869, 1225);

            Console.WriteLine(book.GetInfo());

            string info = book.GetInfo();
            Console.WriteLine(info);

            Console.ReadKey();
        }
    }
    class Book
    {
        private string _title = "Без названия";
        private string _author = "Без автора";
        public short Year { get; set; }
        public short Pages { get; set; }

        public Book(string author, string title, short year, short pages)
        {
            _author = author;
            _title = title;
            Year = year;
            Pages = pages;
        }
        public Book(string title, short year, short pages)
        {
            _title = title;
            Year = year;
            Pages = pages;
        }

        public string GetInfo()
        {
            return $"{_author}, {_title}, {Year} г., {Pages} стр.";
        }


        public override string ToString()
        {
            return $"ToString: {_author}, {_title}, {Year} г., {Pages} стр.";
        }

    }
}
