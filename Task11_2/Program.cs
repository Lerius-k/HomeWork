namespace Task11_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IFlyable[] flyables = new IFlyable[]
            {
                new Bird(),
                new Bird(),
                new Airplane(),
                new Bird()
            };
            foreach (IFlyable i in flyables)
            {
                Console.WriteLine(i.Fly());
                Console.WriteLine();
            }


            Console.ReadKey();
        }
    }
    interface IFlyable
    {
        int MaxAltitude { get; }
        string Fly();


    }
    class Bird : IFlyable
    {
        public string Name => "Птица";
        public int MaxAltitude => 12000;
        public string Fly()
        {
            return $"{Name}. Лечу на высоте {MaxAltitude} метров.";
        }
    }
    class Airplane : IFlyable
    {
        public string Name => "Самолет";
        public int MaxAltitude => 11275;
        public int CountPassengers => 150;
        public string Fly()
        {
            return $"{Name}. Лечу на высоте {MaxAltitude} метров. Везу {CountPassengers} пассажиров.";
        }
    }
}
