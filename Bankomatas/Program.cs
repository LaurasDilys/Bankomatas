using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bankomatas
{
    class Program
    {
        static bool LoggedIn { get; set; } = false;
        static string PIN { get; } = "1234";
        static Dictionary<int, string> Menu { get; } = new Dictionary<int, string>()
        {
            { 1, "Keisti kalbą" },
            { 2, "Keisti PIN kodą" },
            { 3, "Sąskaitos likutis" },
            { 4, "Sąskaitos išrašas" },
            { 5, "Įnešti pinigus" },
            { 6, "Išimti pinigus" },
            { 7, "Baigti darbą" }
        };

        static void Main(string[] args)
        {
            ChooseLanguage();
            LogIn();
            ShowMenu();
        }

        // Hide PIN
        static void LogIn()
        {
            EnterPin("Įveskite");
        }

        static void EnterPin(string start)
        {
            int triesAllowed = 3;
            int pinLength = 4;

            for (int tries = 0; tries < triesAllowed; tries++)
            {
                StringBuilder pinAttempt = new StringBuilder("", 4);
                string text = "";

                while (pinAttempt.Length < pinLength)
                {
                    text = $"{start} PIN (naudojant skaitmenis): {pinAttempt.ToString()}";
                    Console.Write(text);
                    string input = Console.ReadKey().KeyChar.ToString();

                    int num;
                    if (int.TryParse(input, out num))
                    {
                        pinAttempt.Append(num);
                    }
                    BadInputKey(text);
                }

                Console.Clear();
                if (pinAttempt.ToString() == PIN)
                {
                    LoggedIn = true;
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
            LoggedIn = false;
            Exit();
        }

        static void ShowMenu()
        {
            Console.WriteLine("Meniu:");
            foreach (var item in Menu)
                Console.WriteLine($"{item.Key} - {item.Value}");

            Console.WriteLine();
            int selection = Selection(1, Menu.Count);
            Console.Clear();

            switch (selection)
            {
                case (1):
                    ChooseLanguage();
                    break;
                case (2):
                    ChangePin();
                    break;
                case (3):
                    AccountBalance();
                    break;
                case (4):
                    ActivityHistory();
                    break;
                case (5):
                    Deposit();
                    break;
                case (6):
                    Withdraw();
                    break;
                case (7):
                    Exit();
                    break;
            }
        }

        static void MenuOrBack()
        {

        }

        // Add dictionary
        static void ChooseLanguage()
        {
            Console.WriteLine("Galimos kalbos:\n1 - LT\n2 - EN\n3 - RU\n");
            int selection = Selection(1, 3);
            string greeting = "";

            switch (selection)
            {
                case (1):
                    greeting = "Sveiki!";
                    break;
                case (2):
                    greeting = "Hello!";
                    break;
                case (3):
                    greeting = "Zdravstvuyte!";
                    break;
            }

            Console.Clear();
            Console.WriteLine(greeting);
            Console.WriteLine();

            if (LoggedIn) ShowMenu();
        }

        static void ChangePin()
        {
            EnterPin("Įveskite dabartinį");

            Console.WriteLine();
            
        }

        static void AccountBalance()
        {

        }

        static void ActivityHistory()
        {

        }

        static void Deposit()
        {

        }

        static void Withdraw()
        {

        }

        static void Exit()
        {
            Environment.Exit(0);
        }

        

        static int Selection(int min, int max)
        {
            int selection = 0;
            string text = "Jūsų pasirinkimas (įveskite sveikąjį skaičių): ";

            while (true)
            {
                Console.Write(text);
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out selection))
                {
                    if (selection >= min && selection <= max)
                    {
                        break;
                    }
                }
                BadInput(text, input);
            }
            return selection;
        }

        static void BadInput(string text, string input)
        {
            Console.SetCursorPosition(Console.CursorLeft + text.Length, Console.CursorTop - 1);
            Console.WriteLine(string.Concat(Enumerable.Repeat(" ", input.Length)));
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
        }

        // REIKIA TAISYTI
        static void BadInputKey(string text)
        {
            Console.SetCursorPosition(Console.CursorLeft + text.Length + 1, Console.CursorTop);
            Console.WriteLine(" ");
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
        }
    }


}
