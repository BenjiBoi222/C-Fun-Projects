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
            Console.WriteLine("=== Nebula Main Menu ===\n");
            
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

                case 3: MoneyFunctions.DepositMoney(); break;
                case 4: MoneyFunctions.WithdrawMoney(); break;
                case 5: MoneyFunctions.TransferMoney(); break;

                case 6: StockFunctions.ViewOwnedStocks(); break;
                case 7: StockFunctions.ViewStocksInMarket(); break;
                case 8: StockFunctions.BuyStocks(); break;
                case 9: StockFunctions.SellStocks(); break;

                case 10: SettingFunctions.ChangePassword(); break;
                case 11: SettingFunctions.ChangeUsername(); break;
            }
        }


    }
}
