using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomatas
{
    static class Account
    {
        public static decimal Balance { get; private set; }

        private static Dictionary<DateTime, decimal> History { get; set; } = new Dictionary<DateTime, decimal>();

        public static void InitialBalanceOf(decimal amount)
        {
            // Dėl įvairumo įsivaizduojam, kad atitinkama suma
            // buvo įvesta vakar => AddDays(-1)

            History.Add(DateTime.Now.AddDays(-1), amount);
        }

        public static void ShowBalance()
        {
            Console.Clear();
            Console.WriteLine(Balance);
        }

        public static void ShowHistory()
        {
            throw new NotImplementedException();
        }

        public static void Deposit(decimal amount)
        {
            throw new NotImplementedException();
        }

        public static void Withdraw(decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
