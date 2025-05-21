namespace Task11_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Animal[] animal = new Animal[]
            {
                new Cat(),
                new Cat(),
                new Dog(),
                new Cat()
            };
            foreach (Animal i in animal)
            {
                i.ShowInfo();
                Console.WriteLine();
            }


            Console.ReadKey();
        }
    }

    abstract class Animal
    {
        public abstract string Name { get; }
        public abstract string Say();

        public void ShowInfo()
        {
            Console.WriteLine($"{Name} говорит {Say()}");
        }
    }

    class Cat : Animal
    {
        public override string Name => "Кот";
        public override string Say()
        {
            return "Мяу!";
        }
    }
    class Dog : Animal
    {
        public override string Name => "Собака";
        public override string Say()
        {
            return "Гав!";
        }
    }
}
