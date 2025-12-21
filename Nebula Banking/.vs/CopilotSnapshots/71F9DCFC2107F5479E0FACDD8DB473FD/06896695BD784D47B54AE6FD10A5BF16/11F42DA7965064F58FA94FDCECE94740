using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula_Banking
{
    class BankManager : Users
    {
        public bool CanViewAllAccounts { get; set; } = true;
        public bool CanCreateUsers { get; set; } = true;

        // Constructor for manager accounts
        public BankManager(int cardNumber, string password, string userName, bool userBalance) 
            : base(cardNumber, password, userName, userBalance)
        {
        }


        public void ViewAllAccounts(List<Users> allUsers)
        {
            Console.WriteLine("Manager viewing all accounts:");
            foreach (var user in allUsers)
            {
                user.DisplayUserInfo();
            }
        }

        // Override base method
        public override void DisplayUserInfo()
        {
            Console.WriteLine($"Manager ID: {Id}");
            Console.WriteLine($"Card Number: {CardNumber}");
        }
    }
}
