﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomatas
{
    static class Menu
    {
        static Dictionary<int, string> Options { get; } = new Dictionary<int, string>()
        {
            { 1, "Keisti kalbą" },
            { 2, "Keisti PIN kodą" },
            { 3, "Sąskaitos likutis" },
            { 4, "Sąskaitos išrašas" },
            { 5, "Įnešti pinigus" },
            { 6, "Išimti pinigus" },
            { 7, "Baigti darbą" }
        };

        public static void ShowOrExit()
        {
            Console.WriteLine("Jūs galite grįžti į meniu (1) arba baigti darbą (2).");
            int selection = Input.SelectFrom(2);

            Console.Clear();
            switch (selection)
            {
                case (1):
                    Menu.Show();
                    break;
                case (2):
                    Program.Exit();
                    break;
            }
        }

        public static void Show()
        {
            Console.WriteLine("Meniu:");
            foreach (var item in Options)
                Console.WriteLine($"{item.Key} - {item.Value}");

            Console.WriteLine();
            int selection = Input.SelectFrom(Options.Count);
            Console.Clear();

            Switch(selection);
        }

        private static void Switch(int selection)
        {
            switch (selection)
            {
                case (1):
                    Language.Choose();
                    break;
                case (2):
                    Pin.Change();
                    break;
                case (3):
                    Account.ShowBalance();
                    break;
                case (4):
                    Account.ShowHistory();
                    break;
                case (5):
                    Account.Deposit();
                    break;
                case (6):
                    Account.Withdraw();
                    break;
                case (7):
                    Program.Exit();
                    break;
            }
        }
    }
}