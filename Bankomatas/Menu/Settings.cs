using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bankomatas
{
    static class Settings
    {
        private static string Path { get; } = @"hidepin.txt";

        public static void Show()
        {
            Console.WriteLine("========= NUSTATYMAI =========\n");
            Console.WriteLine("Ar norite paslėpti (1), ar matyti (2) įvedamą PIN kodą?");
            Console.WriteLine("Įveskite 0, jeigu norite sugrįžti.\n");

            int selection = Input.EscapeOrSelectFrom(2);
            string choice = "";

            switch (selection)
            {
                case (0):
                    Menu.Show();
                    break;
                case (1):
                    Program.HidePin = true;
                    choice = "paslėpti";
                    break;
                case (2):
                    Program.HidePin = false;
                    choice = "matyti";
                    break;
            }

            Console.Clear();
            Console.WriteLine("========= NUSTATYMAI =========\n");
            Console.WriteLine($"Pasirinkta {choice} įvedamą PIN kodą.\n");
            Menu.ShowOrExit();
        }

        public static bool PinVisibilitySetting()
        {
            return bool.Parse(File.ReadAllLines(Path)[0]);
        }

        public static void PinVisibilitySetting(bool hidePin)
        {
            File.WriteAllText(Path, hidePin.ToString());
        }
    }
}
