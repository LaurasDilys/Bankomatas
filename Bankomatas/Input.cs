using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bankomatas
{
    static class Input
    {
        public static int SelectFrom(int amountOfOptions)
        {
            return SelectFromVariant(amountOfOptions, false);
        }

        public static int EscapeOrSelectFrom(int amountOfOptions)
        {
            return SelectFromVariant(amountOfOptions, true);
        }

        private static int SelectFromVariant(int amountOfOptions, bool withTheAdditionOfZero)
        {
            int selection = -1;
            int firstSelection = withTheAdditionOfZero ? 0 : 1;
            string text = "Jūsų pasirinkimas (įveskite atitinkamą skaičių): ";

            while (true)
            {
                Console.Write(text);
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out selection)
                    && selection >= firstSelection
                    && selection <= amountOfOptions)
                {
                    break;
                }
                Bad(text, input);
            }
            return selection;
        }

        public static decimal AmountOfMoney(string depositOrWithdraw)
        {
            decimal amount = 0;
            string text = $"Kiek pinigų norite {depositOrWithdraw} (Eur)? ";

            while (true)
            {
                Console.Write(text);
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out amount))
                {
                    if (amount >= 0)
                    {
                        break;
                    }
                }
                Bad(text, input);
            }
            return amount;
        }

        public static void Bad(string text, string input)
        {
            Console.SetCursorPosition(Console.CursorLeft + text.Length, Console.CursorTop - 1);
            Console.WriteLine(string.Concat(Enumerable.Repeat(" ", input.Length)));
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
        }

        // REIKIA TAISYTI
        public static void BadKey(string text)
        {
            Console.SetCursorPosition(Console.CursorLeft + text.Length + 1, Console.CursorTop);
            Console.WriteLine(" ");
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
        }
    }
}
