using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutyaPanzio
{
    class Bank
    {
        public static double InvestedMoney { get; set; } = 0;
        public static int InvestedDay { get; set; } = 0;
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
                Console.WriteLine("5)Cash out investment");
                Console.WriteLine("6)BankAccount infos");
                Console.WriteLine("0)Exit the bank menu");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 0: Console.Clear(); isBankActive = false; break;
                        case 1: TakeOutLoan(); break;
                        case 2: DepositMoney(); break;
                        case 3: WithdrawMoney(); break;
                        case 4: InvestMoney(); break;
                        case 5: CashOutInvested(); break;
                        case 6: BankInfo(); break;
                        default: Console.WriteLine("Invalid input'"); break;
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

        /// <summary>Function to handle the depositing of money</summary>
        private static void DepositMoney()
        {
            Console.WriteLine($"Hotel's money: {Hotel.HotelMoney}");
            Console.Write("Enter the amount of money you want to deposit ($100 - $10000): ");
            if (int.TryParse(Console.ReadLine(), out int amount))
            {
                if (amount >= 100 && amount <= 10000)
                {
                    if (amount <= Hotel.HotelMoney)
                    {
                        Hotel.HotelMoney -= amount;
                        DepositedMoney += amount;
                        Console.WriteLine($"Successfully deposited ${amount}!");
                    }
                    else Console.WriteLine("You don't have enough money to deposit that amount!");
                }
                else Console.WriteLine("Deposit amount must be between $100 and $10000!");
            }
            else Console.WriteLine("Invalid input!");
        }

        ///<summary>Function to handle the investing of money</summary>
        private static void WithdrawMoney()
        {
            Console.Write("Enter the amount of money you want to withdraw ($100 - $10000): ");
            if (int.TryParse(Console.ReadLine(), out int amount))
            {
                if (amount >= 100 && amount <= 10000)
                {
                    if (amount <= DepositedMoney)
                    {
                        Hotel.HotelMoney += amount;
                        DepositedMoney -= amount;
                        Console.WriteLine($"Successfully withdrew ${amount}!");
                    }
                    else Console.WriteLine("You don't have enough deposited money to withdraw that amount!");
                }
                else Console.WriteLine("Withdraw amount must be between $100 and $10000!");
            }
            else Console.WriteLine("Invalid input!");
        }

        ///<summary>Function to handle the investing of money</summary> 
        private static void InvestMoney()
        {
            Console.WriteLine($"Deposited Money: ${DepositedMoney}");
            Console.Write($"Enter how much you want to invest: $0 - ${DepositedMoney}: ");
            if (int.TryParse(Console.ReadLine(), out int amount))
            {
                if (amount >= 0 && amount <= DepositedMoney)
                {
                    DepositedMoney -= amount;
                    InvestedMoney += amount;
                    Console.WriteLine($"Successfully invested ${amount}!");
                }
                else Console.WriteLine("Invalid investment amount!");
            }
            else Console.WriteLine("Invalid input!");
        }

        ///<summary></summary>
        private static void BankInfo()
        {
            Console.WriteLine("===Bank Account Info===");
            Console.WriteLine($"Amount of day invested: {InvestedDay}");
            Console.WriteLine($"Invested money: ${InvestedMoney}");
            Console.WriteLine($"Deposited money: ${DepositedMoney}");
            Console.WriteLine("=======================");
        }


        private static void CashOutInvested()
        {
            if (InvestedMoney % 1 >= 0.5)
            {
                DepositedMoney += (int)Math.Ceiling(InvestedMoney);
                InvestedMoney = 0; 
            }
            else
            {
                Console.WriteLine("You can only cash out when the decimal is .5 or higher.");
            }
        }
        public static void BankActions()
        {
            if(InvestedMoney > 100)
            {
                InvestedMoney = ReturnNewInvestedMoney(InvestedMoney);
            }
        }

        private static double ReturnNewInvestedMoney(double investedMoney)
        {
            InvestedDay++;
            const double investerAmount = 1.01;
            investedMoney *= (investerAmount + investerAmount/5); 
            return investedMoney;
        }
    }
}
