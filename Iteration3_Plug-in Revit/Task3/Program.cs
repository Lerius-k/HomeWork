using Microsoft.Extensions.DependencyInjection;

namespace Task2
{   // не стал классы и интерфейсы в отдлеьный файлы программы выносить, чтобы было удобно и быстро настроить и проверить код.
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                //_______решение без контейнера зависимостей_______
                //var messageSender = new SmsSender();
                //var messageSender2 = new EmailSender();
                //var fileLogger = new FileLogger();

                //var service = new NotificationService(messageSender2, fileLogger);
                //______________
                Console.WriteLine("Укажите тип рассылки:\n1 - Email\n2 - SMS\nИначе, по-умалчанияю, Email");
                int a = 1;
                try
                {
                    a = Convert.ToInt32(Console.ReadLine());
                }
                catch { }

                ServiceCollection services = new ServiceCollection(); // "открываем корзинку" для указания зависимостей

                services.AddSingleton<ILogger, FileLogger>(); //Регистрируем логгер
                if (a == 2) //определяем, на какой рассыльщик регистрируемся

                    services.AddSingleton<INotificationSender, SmsSender>();
                else
                    services.AddSingleton<INotificationSender, EmailSender>();

                services.AddSingleton<NotificationService>(); //регистрируем сервис уведомлений

                ServiceProvider provider = services.BuildServiceProvider(); //"закрываем корзинку"

                var service = provider.GetRequiredService<NotificationService>();
                service.SendNotification("Ваш заказ готов", "user@example.com");
                Console.ReadKey();
            }
        }


    }
    public class NotificationService
    {
        private readonly INotificationSender _messageSender; //убираем жесткую привязку и ждем класс, реализующий интерфейс 
        private readonly ILogger _fileLogger;//убираем жесткую привязку и ждем класс, реализующий интерфейс 

        public NotificationService(INotificationSender messageSender, ILogger fileLogger)
        {
            _messageSender = messageSender; //внедрили зависимость от класса-рассыльщика
            _fileLogger = fileLogger; //внедрили зависимость от класса-логгера
        }

        public void SendNotification(string message, string recipient)
        {
            // Логика подготовки уведомления
            string formattedMessage = $"Уведомление: {message}"; //можно вынести в отдельный класс "форматировщик", чтобы доконца соблюсти SRP принцип (Single Responsibility Principle)

            // Отправка сообщения через класс-рассыльщик
            _messageSender.SendMessage(recipient, formattedMessage);

            // Запись лога через класс-логировщик
            _fileLogger.Log(recipient);
        }
    }

    // ________Интерфейс рассыльщика__________

    public interface INotificationSender
    {
        public void SendMessage(string to, string message);
    }

    // ________Класс рассыльщика SMS__________

    public class SmsSender : INotificationSender
    {
        public void SendMessage(string to, string message)
        {
            // Симуляция отправки SMS
            Console.WriteLine($"SMS для {to}: {message}");
        }
    }

    // ________Класс рассыльщика имейлов__________
    public class EmailSender : INotificationSender
    {
        public void SendMessage(string to, string message)
        {
            // Симуляция отправки email
            Console.WriteLine($"Email для {to}: {message}");
        }
    }

    // ________интерфейс логера__________
    public interface ILogger
    {
        public void Log(string message);
    }
    // ________класс логера__________
    public class FileLogger : ILogger
    {
        private const string FileName = "SenderLog.log";

        public void Log(string recipient)
        {
            File.AppendAllText(FileName, $"Отправлено уведомление для {recipient}{Environment.NewLine}");
        }
    }
}

