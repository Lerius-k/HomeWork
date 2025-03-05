namespace Task3_11
{
    internal class Program
    {
        static void Main(string[] args)
        /*Можно ли на прямоугольном участке застройки размером a * b метров разместить два дома размером в плане p * q и r * s метров? 
         * Дома можно располагать только параллельно сторонам участка. Дома могу стоять «вплотную» друг к другу.*/
        {
            double a, b, p, q, r, s, pr, ps, qr, qs;
            string answer;
            Console.WriteLine("'Можно ли на прямоугольном участке застройки размером a * b метров разместить два дома размером в плане p * q и r * s метров?'");
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Укажите стороны участка застройки.");
            Console.Write("Сторона a: ");
            a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Сторона b: ");
            b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Укажите габариты первого дома.");
            Console.Write("Сторона a: ");
            p = Convert.ToDouble(Console.ReadLine());
            Console.Write("Сторона b: ");
            q = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Укажите габариты второго дома.");
            Console.Write("Сторона a: ");
            r = Convert.ToDouble(Console.ReadLine());
            Console.Write("Сторона b: ");
            s = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой

            pr = p + r;
            ps = p + s;
            qr = q + r;
            qs = q + s;

            if (pr <= a && q<=b && s<=b)
            {
                answer = "МОЖНО";
            }
            else
            {
                if (pr <= b && q <= a && s <= a)
                {
                    answer = "МОЖНО";
                }
                else
                {
                    if (ps <= a && q <= b && r <= b)
                    {
                        answer = "МОЖНО";
                    }
                    else
                    {
                        if (ps <= b && q <= a && r <= a)
                        {
                            answer = "МОЖНО";
                        }
                        else
                        {
                            if (qr <= a && p <= b && s <= b)
                            {
                                answer = "МОЖНО";
                            }
                            else
                            {
                                if (qr <= b && p <= a && s <= a)
                                {
                                    answer = "МОЖНО";
                                }
                                else
                                {
                                    if (qs <= a && p <= b && r <= b)
                                    {
                                        answer = "МОЖНО";
                                    }
                                    else
                                    {
                                        if (qs <= b && p <=a && r <= a)
                                        {
                                            answer = "МОЖНО";
                                        }
                                        else
                                        {
                                            answer = "НЕЛЬЗЯ";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("На данном участке {0} разместить такие дома.", answer);

            Console.WriteLine();//просто отделяю смысловые абзацы пустой строкой
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
