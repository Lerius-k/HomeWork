namespace Task12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Book<string, int>[] books1 = new Book<string, int>[]
            {
                new Book<string, int>("F-1e34", "Храбрец", 1257, "Тритон"),
                new Book<string, int>("A-123r4", "Глупеец", 2007, "Барон"),
                new Book<string, int>("B-12y34", "Борец", 1997, "Быков"),
                new Book<string, int>("F-1234", "Телец", 1957, "Кирилленко"),
                new Book<string, int>("C-1t34", "Писец", 2022, "Тимон")
            };
            Book<int, string>[] books2 = new Book<int, string>[]
            {
                new Book<int, string>(1234, "Храбрец", "до нашей эры", "Тритон"),
                new Book<int, string>(3456, "Глупеец", "нашей эры", "Барон"),
                new Book<int, string>(3521, "Борец", "возраждение", "Быков"),
                new Book<int, string>(42, "Телец", "просвещение", "Кирилленко"),
                new Book<int, string>(987, "Писец", "новая история", "Тимон")
            };

            Book<string, int> findBook1 = FindBook(books1, "F-1234");
            Console.WriteLine(findBook1?.ToString() ?? "Не найдена" );
            Console.WriteLine();
            Book<int, string> findBook2 = FindBook(books2, 42);
            Console.WriteLine(findBook2?.ToString() ?? "Не найдена");
            Console.WriteLine();
            Book<int, string> findBook3 = FindBook(books2, 422);
            Console.WriteLine(findBook3?.ToString() ?? "Не найдена");

            Console.ReadKey();

        }
        public static Book<T, U> FindBook<T, U>(Book<T, U>[] books, T code)
        {
            foreach (Book<T, U> book in books)
            {
                if (book.Code.Equals(code))
                {
                    return book;
                }
            }
            return null;
        }
    }


    class Book<T, U>
    {
        public T Code { get; set; }
        public string Title { get; set; }
        public U PublicationYear { get; set; }
        public string Author { get; set; }

        public Book(T code, string title, U publicationYear, string author)
        {
            Code = code;
            Title = title;
            PublicationYear = publicationYear;
            Author = author;
        }

        public override string ToString()
        {
            return $"{Code} ({typeof(T).Name}), Название: {Title}, Автор: {Author}, Год: {PublicationYear} ({typeof(U).Name})";
        }
    }
}
