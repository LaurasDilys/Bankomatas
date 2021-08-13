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
            int selection = 0;
            string text = "Jūsų pasirinkimas (įveskite atitinkamą skaičių): ";

            while (true)
            {
                Console.Write(text);
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out selection))
                {
                    if (selection >= 1 && selection <= amountOfOptions)
                    {
                        break;
                    }
                }
                Bad(text, input);
            }
            return selection;
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
