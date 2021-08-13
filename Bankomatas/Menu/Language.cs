using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomatas
{
    static class Language
    {
        // Add dictionary
        public static void Choose()
        {
            Console.WriteLine("Galimos kalbos:\n1 - LT\n2 - EN\n3 - RU\n");
            int selection = Input.SelectFrom(3);
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

            if (Program.LoggedIn) Menu.Show();
        }
    }
}
