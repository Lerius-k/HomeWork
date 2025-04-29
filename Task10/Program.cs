using System.Runtime.InteropServices;

namespace Task10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Building buildingBase = new Building("пр-т Михаила Hагибина", 1584, 1956);
            MultiBuilding buildingHeir = new MultiBuilding("ул. Текучева", 25354, 2016, 22, true);

            Building buildingBase2 = buildingHeir; //апкастинг с потерей функционала
            if (buildingBase is MultiBuilding) //даункастинг с проверкой
            {
                MultiBuilding buildingHeir2 = (MultiBuilding)buildingBase;
            }
            Building buildingBase3 = buildingHeir as MultiBuilding;
            int T = buildingBase.BuildingAge;
            Console.WriteLine($"Зданию {buildingBase} {T} лет.");
            buildingBase.DisplayInfo();
            Console.WriteLine();
            int T2 = buildingHeir.BuildingAge;
            double k = buildingHeir.BuildiAreaPerFloor;
            Console.WriteLine($"Зданию {buildingHeir} {T2} лет.\nСредняя площадь этажа: {k}");
            buildingHeir.DisplayInfo();

            Console.WriteLine();
            Console.WriteLine(buildingBase);
            Console.WriteLine(buildingHeir);
            Console.WriteLine(buildingBase2);
            Console.WriteLine(buildingBase3);
            Console.ReadKey();
        }
    }
    public class Building
    {
        protected string _address = "Без адреса";
        protected double _area;
        protected int _yearBuilt;
        public int BuildingAge
        {
            get
            {
                DateTime t = new(_yearBuilt, 1, 1);
                return (DateTime.Now - t).Days / 365;
            }
        }
        public Building(string address, double area, int yearBuilt) // можно добавить валидацию на вводимый год, площадь.
        {
            _address = address;
            _area = area;
            _yearBuilt = yearBuilt;
        }
        public virtual double CalculateTax()
        {
            return _area * 1000;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Адрес:{_address}\nПлощадь:{_area}\nГод постройки:{_yearBuilt}\nНалог:{CalculateTax()}");
        }

    }
    sealed public class MultiBuilding : Building
    {
        internal int _floors;
        internal bool _hasElevator;
        public double BuildiAreaPerFloor
        {
            get
            {
                return _area / _floors;
            }
        }
        public MultiBuilding(string address, double area, int yearBuilt, int floors, bool hasElevator)// можно добавить валидацию на вводимую этажность.
            : base(address, area, yearBuilt)
        {
            _floors = floors;
            _hasElevator = hasElevator;
        }
        public override double CalculateTax()
        {
            if (_hasElevator == true)
            {
                return _area * 1000 * (1 + (_floors - 1) * 0.05) + 5000;
            }
            else
            {
                return _area * 1000 * (1 + (_floors - 1) * 0.05);
            }
        }
        public override void DisplayInfo()
        {
            Console.WriteLine($"Адрес:{_address}\nПлощадь:{_area}\nГод постройки:{_yearBuilt}\nНалог:{CalculateTax()}\nЭтажность:{_floors}\nЛифт:{_hasElevator}");
        }
    }
}
