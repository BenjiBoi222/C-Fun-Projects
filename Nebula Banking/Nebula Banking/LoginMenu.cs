using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula_Banking
{
    class LoginMenu
    {
        /// <summary>
        /// Shows the options inside the Login menu
        /// </summary>
        public void ShowMenu()
        {
            Console.WriteLine("\n=== Nebula Bank Login Menu ===");
            Console.WriteLine("0)Exit app");
            Console.WriteLine("1)Login");
            Console.WriteLine("2)Register");
        }

        /// <summary>
        /// Handles the selected menu option based on the specified choice.
        /// </summary>
        /// <param name="choice">The numeric value representing the user's menu selection. Each value corresponds to a specific menu action.</param>
        public void HandleMenu(int choice)
        {
            switch (choice)
            {
                case 0: Program.isRunning = false; break;
                case 1: LoginFunc(); break;
                case 2: RegisterFunc(); break;
                default: Console.WriteLine("Invalid input!"); break;
            }
        }
        /// <summary>
        /// A private function that checks for a given user inside the Userbase
        /// </summary>
        private static void LoginFunc()
        {

        }
        /// <summary>
        /// A private function that registers a new users IF it doesnt exist already!
        /// </summary>
        private static void RegisterFunc()
        {

        }
    }
}
