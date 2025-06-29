namespace Task13_2
{
    public delegate void DeviceStateChanged(string deviceType, string newState, DateTime time);
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Начало программы...");
            Console.WriteLine();
            SmartHomeSystem smartHome = new SmartHomeSystem();

            smartHome.MyHomeDeviceStateChanged += (string device, string state, DateTime time) =>
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {device}: {state}");
            };

            smartHome.TurnOnLight();
            smartHome.TurnOffLight();
            smartHome.LockDoor();
            smartHome.UnlockDoor();
            smartHome.SetTemperature(35);

            Console.WriteLine();
            Console.Write("Для завершиния нажмите любую клавишу");
            Console.ReadKey();
        }
    }
    public class SmartHomeSystem
    {
        private bool _lightState;
        private bool _doorLocked;
        private int _temperature;
        public event DeviceStateChanged MyHomeDeviceStateChanged;


        public void TurnOnLight()
        {
            _lightState = true;
            MyHomeDeviceStateChanged?.Invoke("Light", "Включен", DateTime.Now);
        }
        public void TurnOffLight()
        {
            _lightState = false;
            MyHomeDeviceStateChanged?.Invoke("Light", "Выключен", DateTime.Now);
        }
        public void LockDoor()
        {
            _doorLocked = true;
            MyHomeDeviceStateChanged?.Invoke("Door", "Заблокирована", DateTime.Now);
        }
        public void UnlockDoor()
        {
            _doorLocked = false;
            MyHomeDeviceStateChanged?.Invoke("Door", "Разблокирована", DateTime.Now);
        }
        public void SetTemperature(int newTemp)
        {
            _temperature = newTemp;
            MyHomeDeviceStateChanged?.Invoke("Thermostat", $"Температура изменена на {newTemp}°C", DateTime.Now);
        }

    }

}
