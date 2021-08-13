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

        public static void Withdraw()
        {
            int escape = EscapeOption();
            decimal amount = Input.AmountOfMoney("išimti");
            Console.Clear();

            if (amount > 0)
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
            else Menu.Show();
        }

        private static int EscapeOption()
        {
            int escape = 0;

            string text = $"Įveskite {escape}, jeigu norite sugrįžti.";
            Console.Write($"\n\n{text}");
            Console.SetCursorPosition(Console.CursorLeft - text.Length, Console.CursorTop - 2);

            return escape;
        }
    }
}
