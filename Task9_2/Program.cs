namespace Task9_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount[] bankAccounts =
            [
            new BankAccount("Василий Петрович", 1000),
            new BankAccount("Павел Олегович", 187400),
            new BankAccount("Павел Александрович", 387400),
            new BankAccount("Павел Олегович", 2487400),
            new BankAccount("Светлана игоревна", 106844800),
            new BankAccount("Владимир Владимирович", 348435180)
            ];

            foreach (BankAccount a in bankAccounts)
            {
                Console.WriteLine(a);
            }
            Console.WriteLine();
            BankAccount.ShowTotalAccounts();
            Console.WriteLine(new string('|', 20));
            Console.WriteLine();
            // перевод приза на счет 
            Console.WriteLine();
            Console.WriteLine("перевод приза на счет");
            Console.WriteLine($"Текущий баланс: {bankAccounts[3].Balance:0,0.00} руб.");
            decimal prize = 10000;
            Console.WriteLine($"Приз: {prize:0,0.00} руб.");
            bankAccounts[3].Deposit(prize);
            Console.WriteLine(bankAccounts[3]);
            // покупка автомобиля
            Console.WriteLine();
            Console.WriteLine("покупка автомобиля");
            int person = 3;
            decimal car = 3000000;
            Console.WriteLine($"Текущий баланс: {bankAccounts[person].Balance:0,0.00} руб.");
            Console.WriteLine($"Покупка: {car:0,0.00} руб.");
            bankAccounts[3].Withdraw(car);
            Console.WriteLine(bankAccounts[person]);

            Console.WriteLine();
            int person2 = 4;
            Console.WriteLine($"Текущий баланс: {bankAccounts[person2].Balance:0,0.00} руб.");
            Console.WriteLine($"Покупка: {car:0,0.00} руб.");
            bankAccounts[4].Withdraw(car);
            Console.WriteLine(bankAccounts[person2]);

            Console.WriteLine("Нажмите любую клавишу для завершения...");

            Console.ReadKey();
        }
    }
    class BankAccount
    {
        private decimal _balance = 0;
        private static int _totalAccounts = 0;
        private int AccountNumber;
        public decimal Balance { get; private set; }
        private string _userName = "Ничейный";
        public BankAccount(string userName, ulong balance)
        {
            _userName = userName;
            _balance = Balance = balance;
            Random r = new Random();
            AccountNumber = r.Next(1000, 10000);
            _totalAccounts++;
        }

        public static void ShowTotalAccounts()
        {
            Console.WriteLine($"Общее количество созданных счетов: {_totalAccounts}");
        }

        public void Deposit(decimal replenishmentAmount)
        {
            _balance = Balance += replenishmentAmount;
        }

        public void Withdraw (decimal withdrawalAmount)
        {
            try
            {
                BalanceCheck(Balance, withdrawalAmount);
                _balance = Balance -= withdrawalAmount;
            }
            catch (ArgumentException a)
            {
                Console.WriteLine($"Ошибка операции по счету: {a.Message}");
            }

        }

        void BalanceCheck(decimal balance, decimal withdrawalAmount)
        {
            if (balance < withdrawalAmount)
                throw new ArgumentException("На счету недостаточно средств.");
        }


        public override string ToString()
        {
            string line = new string('-', 10);
            return $"{line}\nBank account:\n{AccountNumber}, {_userName}\nТекущий баланс: {_balance:0,0.00} руб.\nБаланс: {Balance:0,0.00} руб.\n{line}";
        }

    }
}
