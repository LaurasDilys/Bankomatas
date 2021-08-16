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
            get { return Settings.PinVisibilitySetting(); }
            set { Settings.PinVisibilitySetting(value); }
        }

        static void Main(string[] args)
        {
            // !LoggedIn, todėl negalima naudoti Language.Choose()
            //Language.Choose();
            //LogIn();
            Account.SetBalance();
            Menu.Show();
        }

        static void LogIn()
        {
            Pin.Validate("Įveskite");
        }

        // Dienos / vakaro / nakties – pagal DateTime.Now()
        public static void Exit()
        {
            Console.WriteLine("Buvo malonu Jums padėti. Geros dienos!\n");
            Environment.Exit(0);
        }
    }
}
