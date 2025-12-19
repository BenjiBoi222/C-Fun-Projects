using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula_Banking
{
    internal class AccountFunctions
    {
        public void ViewAccountBalance()
        {
            foreach(var userElement in Universal.Users)
            {
                if(userElement.Id == Universal._CurrentUserID_)
                {
                    Console.WriteLine("======================");
                    Console.WriteLine($"Cardnumber: {userElement.cardNumber}");
                    Console.WriteLine($"Balance: {userElement.cardNumber}");
                    Console.WriteLine("======================");
                }
            }
        }

        public void ViewAccountDetails()
        {
            
        }
    }
}