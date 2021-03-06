using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomatas
{
    class Transaction
    {
        public Transaction(DateTime date, decimal amount)
        {
            DateTime = date;
            Amount = amount;
            Type = amount > 0 ? "Pinigų įnešimas" : "Pinigų išėmimas";
            Currency = "Eur";
        }

        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime DateTime { get; set; }
        public string Currency { get; set; }
    }
}
