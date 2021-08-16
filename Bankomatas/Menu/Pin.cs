using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bankomatas
{
    static class Pin
    {
        private static string Path { get; } = @"pin.txt";

        private static string PIN
        {
            get { return GetPin(); }
            set { value.IsNewPin(); }
        }

        private static string GetPin()
        {
            return File.ReadAllLines(Path)[0];
        }

        private static void IsNewPin(this string pin)
        {
            File.WriteAllText(Path, pin);
        }

        // Hide PIN
        public static void Validate(string startOfSentence)
        {
            int triesAllowed = 3;

            for (int tries = 0; tries < triesAllowed; tries++)
            {
                string pinAttempt = EnterPin(startOfSentence);

                Console.Clear();
                if (pinAttempt == PIN)
                {
                    Program.LoggedIn = true;
                    return;
                }
                else
                {
                    int triesLeft = triesAllowed - tries - 1;
                    Console.WriteLine("Neteisingas PIN kodas. Bandykite dar kartą. Liko {0} bandyma{1}\n",
                        triesLeft,
                        triesLeft == 1 ? "s!" : "i.");
                }
            }

            Console.Clear();
            Console.WriteLine("Tris kartus įvestas neteisingas PIN kodas. Kortelė užbloguota!");
            Console.WriteLine("\nDėl tolimesnių veiksmų prašome kreiptis į artimiausią banko padalinį.\n");
            Program.LoggedIn = false;
            Environment.Exit(0);
        }

        public static void Change()
        {
            Validate("Įveskite dabartinį");

            Console.Clear();
            string newPin = EnterPin("Įveskite naują");
            Console.Clear();
            string newPinConfirmation = EnterPin("Pakartokite naują");
            Console.Clear();

            if (newPin == newPinConfirmation)
            {
                Console.WriteLine("Jūsų PIN yra pakeistas.");
                PIN = newPin;
            }
            else
            {
                Console.WriteLine("Įvesti PIN kodai nesutampa!");
            }

            Console.WriteLine();
            Menu.ShowOrExit();
        }

        private static string EnterPin(string startOfSentence)
        {
            int pinLength = 4;

            StringBuilder pin = new StringBuilder("", 4);
            string text = "";
            string pinShown;

            while (pin.Length < pinLength)
            {
                pinShown = Program.HidePin ?
                    string.Concat(Enumerable.Repeat("*", pin.Length)) :
                    pin.ToString();

                text = $"{startOfSentence} PIN (naudojant skaitmenis): {pinShown}";
                Console.Write(text);
                string input = Console.ReadKey().KeyChar.ToString();

                int num;
                if (int.TryParse(input, out num))
                {
                    pin.Append(num);
                }
                Input.BadKey(text);
            }

            return pin.ToString();
        }
    }
}
