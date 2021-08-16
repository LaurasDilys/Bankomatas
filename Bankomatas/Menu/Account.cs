using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Bankomatas
{
    static class Account
    {
        private static string Path { get; set; } = @"history.txt";

        private static Dictionary<int, decimal> Banknotes { get; } = new Dictionary<int, decimal>()
        {
            { 1, 10 },
            { 2, 20 },
            { 3, 50 },
            { 4, 100 },
            { 5, 200 },
            { 6, 0 } // vietoj 0 bus pasirinkimas "Kita suma"
        };

        //

        private static decimal balance;
        private static decimal Balance
        {
            get { return balance; }
            set
            {
                decimal change = value - balance;
                change.AddToHistory();
                balance = value;
            }
        }

        public static void SetBalance()
        {
            balance = History.Values.Sum();
        }

        public static void ShowBalance()
        {
            Console.WriteLine($"Jūsų banko sąskaitoje yra {Balance} Eur\n");
            Menu.ShowOrExit();
        }

        //

        private static Dictionary<DateTime, decimal> History
        {
            get { return ReadHistory(); }
        }

        private static void AddToHistory(this decimal amount)
        {
            File.AppendAllText(Path, $"{DateTime.Now}{Environment.NewLine}");
            File.AppendAllText(Path, $"{amount}{Environment.NewLine}");
        }

        private static Dictionary<DateTime, decimal> ReadHistory()
        {
            Dictionary<DateTime, decimal> history = new Dictionary<DateTime, decimal>();
            string[] lines = File.ReadAllLines(Path).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

            for (int i = 0; i < lines.Length; i += 2)
            {
                history.Add(DateTime.Parse(lines[i]), decimal.Parse(lines[i + 1]));
            }

            return history;
        }

        public static void ShowHistory()
        {
            if (History.Count > 0)
            {
                DateTime currentDate = DateTime.Today.AddDays(1);
                DateTime operationDate;
                DateTime operationTime;
                decimal amount;

                PrintBalance();

                for (int i = History.Count - 1; i >= 0; i--)
                {
                    operationTime = History.ElementAt(i).Key;
                    operationDate = operationTime.Date;
                    amount = History.ElementAt(i).Value;

                    if (currentDate != operationDate)
                    {
                        currentDate = operationDate;
                        Print(currentDate);
                    }

                    Print(operationTime, amount);
                }

                Console.SetCursorPosition(Console.CursorLeft, 0);
                Console.WriteLine("========= SĄSKAITOS IŠRAŠAS =========\n");
                Menu.ShowOrExit();
            }
            else
            {
                Console.WriteLine("========= SĄSKAITOS IŠRAŠAS =========\n");
                Console.WriteLine("Operacijų nėra.\n");
                Menu.ShowOrExit();
            }

        }

        private static void PrintBalance()
        {
            int spacesCount = 8 - Balance.ToString().Length;
            spacesCount = spacesCount > 0 ? spacesCount : 1;

            Console.WriteLine("\n\n\n\n\n\n");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Jūsų banko sąskaitoje yra{0}{1} Eur",
                string.Concat(Enumerable.Repeat(" ", spacesCount)),
                Balance);
            Console.WriteLine("_____________________________________");
        }

        private static void Print(DateTime currentDate)
        {
            string currentDay;

            if (currentDate == DateTime.Today)
            {
                currentDay = "Šiandien";
            }
            else if (currentDate == DateTime.Today.AddDays(-1))
            {
                currentDay = "Vakar";
            }
            else
            {
                string day = currentDate.Date.ToString("dddd, MMMM dd",
                    CultureInfo.CreateSpecificCulture("lt-LT"));
                currentDay = day.First().ToString().ToUpper() +
                    day.Substring(1, day.Length - 1);
            }

            Console.WriteLine("\n");
            Console.WriteLine(currentDay);
            Console.WriteLine("-------------------------------------");
        }

        private static void Print(DateTime operationTime, decimal amount)
        {
            string time = operationTime.ToString("HH:mm");
            int spacesCount = 14 - amount.ToString().Length - time.Length;
            spacesCount = spacesCount > 0 ? spacesCount : 1;

            Console.Write("{0} ({1}) {2}",
                    amount > 0 ? "Pinigų įnešimas" : "Pinigų išėmimas",
                    time,
                    string.Concat(Enumerable.Repeat(" ", spacesCount)));

            if (amount > 0)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.Write(amount);
            Console.ResetColor();
            Console.WriteLine(" Eur");
        }

        //

        public static void Deposit()
        {
            int escape = EscapeOption();
            decimal amount = Input.AmountOfMoney("įnešti");
            Console.Clear();

            if (amount > 0)
            {
                Balance += amount;
                Console.WriteLine($"Į sąskaitą įnešėte {amount} Eur.");
                ShowBalance();
            }
            else Menu.Show();
        }

        // Visoms pinigų išėmimo operacijoms reikia tikrinti,
        // kiek turima pinigų – ir kokios tuomet yra išėmimo galimybės
        public static void Withdraw()
        {
            int escape = BanknoteOptions();
            Console.WriteLine("Kiek pinigų norite įnešti?");
            int selection = Input.EscapeOrSelectFrom(Banknotes.Count);

            Console.Clear();
            Switch(selection);
        }

        private static void Withdraw(decimal amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Iš sąskaitos išėmėte {amount} Eur.");
            }
            else
            {
                Console.WriteLine("Nepakanka lėšų.");
            }
            ShowBalance();
        }

        private static void WithdrawOther()
        {
            int escape = EscapeOption();
            decimal amount = Input.AmountOfMoney("išimti");
            Console.Clear();

            if (amount > 0)
            {
                // Pasirinkta suma turi būti dali iš 10, nes
                // 10 yra mažiausia išduodamų banknotų vertė
                if (amount % 10 == 0)
                {
                    Withdraw(amount);
                }
                else
                {
                    Console.WriteLine("Nėra galimybės išimti pasirinktos pinigų sumos.");
                    Console.WriteLine("Mažiausia bankomato išduodama kupiūra - 10 Eur.\n");
                    WithdrawOther();
                }
            }
            else Menu.Show();
        }

        private static void Switch(int selection)
        {
            if (selection == 0)
            {
                Menu.Show();
            }
            else if (Banknotes[selection] == 0)
            {
                WithdrawOther();
            }
            else
            {
                Withdraw(Banknotes[selection]);
            }
        }

        //

        private static int EscapeOption()
        {
            int escape = 0;

            string text = $"Įveskite {escape}, jeigu norite sugrįžti.";
            Console.Write($"\n\n{text}");
            Console.SetCursorPosition(Console.CursorLeft - text.Length, Console.CursorTop - 2);

            return escape;
        }

        private static int BanknoteOptions()
        {
            Console.WriteLine("\n\n");

            int lines = (Banknotes.Count / 2);
            for (int i = 1; i <= lines; i++)
            {
                int j = i + lines;
                Console.WriteLine("{0} - {1}              {2} - {3}\n",
                    i, Banknotes[i], j, Banknotes[j] == 0 ? "Kita suma" : $"{Banknotes[j]}");
            }

            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 2);
            int escape = EscapeOption();

            Console.SetCursorPosition(Console.CursorLeft, 0);
            return escape;
        }
    }
}
