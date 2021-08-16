using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bankomatas
{
    class Program
    {
        public static bool LoggedIn { get; set; } = false;
        public static bool HidePin
        {
            get { return Settings.PinVisibility(); }
            set { Settings.PinVisibility(value); }
        }

        static void Main(string[] args)
        {
            Language.Choose();
            LogIn();
            Account.SetBalance();
            Menu.Show();
        }

        static void LogIn()
        {
            Pin.Validate("Įveskite");
        }

        public static void Exit()
        {
            Console.WriteLine("Buvo malonu Jums padėti. Geros dienos!\n");
            Environment.Exit(0);
        }
    }
}
