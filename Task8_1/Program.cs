using System;

namespace Task8_1
{
    internal class Program
    {
        static void Main(string[] args)
        /*Смоделируйте работу простого калькулятора. 
        Программа должна запрашивать 2 целых числа, а затем – код операции (например, 1 – сложение, 2 – вычитание, 3 – произведение, 4 – частное). 
        После этого на консоль выводится ответ. 
        Используйте обработку деления на ноль (DivideByZeroException), нечислового ввода (FormatException).*/
        {
            double a, b, result; //не захотел делать целые числа, так как не нравится целочисленное деление. создал свое исключение при делении на ноль.
            byte option;
            Console.WriteLine("\"Простой калькулятор\"");
            Console.WriteLine();
            try
            {
                Console.Write("Введите первый аргумент: ");
                a = Convert.ToDouble(Console.ReadLine());
                Console.Write("Введите второй аргумент: ");
                b = Convert.ToDouble(Console.ReadLine());
                Console.Write("Коды операций:\n1 – сложение, 2 – вычитание, 3 – произведение, 4 – частное\nУкажите код команды: ");
                option = Convert.ToByte(Console.ReadLine());
                result = SimpleCalc(a, b, option);
                Console.WriteLine("Результат: {0:0.00}", result);
            }
            catch (FormatException e)
            {
                Console.WriteLine();
                Console.WriteLine($"Некорректный ввод: {e.Message}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine();
                Console.WriteLine($"Деление на ноль: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
        static double SimpleCalc(double x, double y, byte comand)
        {
            double result = 0;
            if (comand > 0 && comand < 5)
            {
                {
                    switch (comand)
                    {
                        case 1:
                            result = x + y;
                            break;
                        case 2:
                            result = x - y;
                            break;
                        case 3:
                            result = x * y;
                            break;
                        case 4:
                            if (y == 0)
                            {
                                throw new ArgumentException("Деление на ноль!");
                            }
                            else
                            {
                                result = x / y;
                                break;
                            }
                    }
                    return result;
                }
            }
            throw new ArgumentException("Неверный код операции.");
        }
    }
}
