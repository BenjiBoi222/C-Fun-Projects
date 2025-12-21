using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula_Banking
{
    class MainMenu
    {
        public static void ShowMenu()
        {
            Console.WriteLine("\n=== Nebula Main Menu ===\n");
            
            Console.WriteLine("--- Account Management ---");
            Console.WriteLine("1) View Account Balance");
            Console.WriteLine("2) View Account Details");
            
            Console.WriteLine("--- Money Operations ---");
            Console.WriteLine("3) Deposit Money");
            Console.WriteLine("4) Withdraw Money");
            Console.WriteLine("5) Transfer Money");

            Console.WriteLine("--- Stock Operations ---");
            Console.WriteLine("6) View owned stocks");
            Console.WriteLine("7) View all available stocks");
            Console.WriteLine("8) Buy stocks");
            Console.WriteLine("9) Sell stocks");
            
            Console.WriteLine("--- Settings ---");
            Console.WriteLine("10) Change password");
            Console.WriteLine("11) Change username");
            
            
            Console.WriteLine("0) Logout");
        }

        public static void HandleMenu(int choice)
        {
            switch (choice)
            {
                case 0: Console.WriteLine("Logging out..."); Program.isLoggedIn = false; Program.switchFromMenu = true; break;
                case 1: AccountFunctions.ViewAccountBalance(); break;
                case 2: AccountFunctions.ViewAccountDetails(); break;

                // Money functions not implemented yet
                case 3: Console.WriteLine("Deposit not implemented yet."); break;
                case 4: Console.WriteLine("Withdraw not implemented yet."); break;
                case 5: Console.WriteLine("Transfer not implemented yet."); break;

                case 6: StockFunctions.ViewOwnedStocks(); break;
                case 7: StockFunctions.ViewStocksInMarket(); break;
                case 8: StockFunctions.BuyStocks(); break;
                case 9: Console.WriteLine("Sell stocks not implemented yet."); break;

                // Settings not implemented yet
                case 10: Console.WriteLine("Change password not implemented yet."); break;
                case 11: Console.WriteLine("Change username not implemented yet."); break;
            }
        }


    }
}
