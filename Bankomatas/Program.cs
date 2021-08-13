using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bankomatas
{
    class Program
    {
        public static bool LoggedIn { get; set; } = false;

        static void Main(string[] args)
        {
            Account.InitialBalanceOf(1000);
            //Language.Choose();
            //LogIn();
            Menu.Show();
        }

        static void LogIn()
        {
            Pin.Validate("Įveskite");
        }

        

        

        

        

        

        

        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
