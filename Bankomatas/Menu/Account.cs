using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomatas
{
    static class Account
    {
        private static decimal balance;
        private static decimal Balance
        {
            get { return balance; }
            set
            {
                decimal change = value - balance;
                History.Add(DateTime.Now, change);
                balance = value;

            }
        }

        private static Dictionary<DateTime, decimal> History { get; } = new Dictionary<DateTime, decimal>();

        private static Dictionary<int, decimal> Banknotes { get; } = new Dictionary<int, decimal>()
        {
            { 1, 10 },
            { 2, 20 },
            { 3, 50 },
            { 4, 100 },
            { 5, 200 },
            { 6, 0 } // vietoj 0 bus pasirinkimas "Kita suma"
        };

        public static void InitialBalanceOf(decimal amount)
        {
            // Dėl įvairumo įsivaizduojam, kad atitinkama
            // suma buvo įvesta vakar => AddDays(-1)

            if (amount > 0)
            {
                History.Add(DateTime.Now.AddDays(-1), amount);
                Balance += amount;
            }
        }

        public static void ShowBalance()
        {
            Console.WriteLine($"Jūsų banko sąskaitoje yra {Balance} Eur\n");
            Menu.ShowOrExit();
        }

        // Tik testinė versija – reikia pagražinti
        public static void ShowHistory()
        {
            foreach (var item in History)
            {
                Console.WriteLine(item.Key);
                Console.WriteLine(item.Value);
            }

            Console.WriteLine();
            Menu.ShowOrExit();
        }

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

            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1 - lines * 2);
            return escape;
        }
    }
}
