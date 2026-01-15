using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutyaPanzio
{
    class Bank
    {
        public static int InvestedMoney { get; set; } = 0;
        public static int DepositedMoney { get; set; } = 0;
        public static void BankMenu()
        {
            Console.Clear();
            bool isBankActive = true;
            while (isBankActive)
            {
                Console.WriteLine("===Bank menu===");
                Console.WriteLine("1)Take out loan");
                Console.WriteLine("2)Deposit money");
                Console.WriteLine("3)Withdraw money");
                Console.WriteLine("4)Invest money");
                Console.WriteLine("0)Exit the bank menu");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 0: Console.Clear(); isBankActive = false; break;
                        case 1: TakeOutLoan(); break;
                    }
                }
                else Console.WriteLine("Invalid input'");
            }
        }

        ///<summary>Function to handle the loan handing out</summary>
        private static void TakeOutLoan()
        {
            if (Hotel.AlreadyTookLoan)
                Console.WriteLine("Loan unavailable, alrady took one out!");
            else
            {
                const int repayDayAmount = 30;
                Console.WriteLine("===Loan Option===");
                Console.WriteLine($"Each loan has a {repayDayAmount} day repay time!");
                Console.Write("Enter the amount of money you want to loan ($500 - 5000$): ");
                if (int.TryParse(Console.ReadLine(), out int amount))
                {
                    if (amount > 499 && amount < 5001)
                    {
                        Hotel.HotelFeePerDay += amount / repayDayAmount;
                        Hotel.HotelMoney += amount;
                        Hotel.RemainingInterestDay += repayDayAmount;
                        Hotel.AlreadyTookLoan = true;
                        Console.WriteLine($"${amount} successfully recieved!");
                    }
                }
            }
        }




    }
}
