using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula_Banking
{
    class Users
    {
        static int UserId = 0;

        public int Id { get; set; } = UserId++;
        public int CardNumber { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public double UserBalance { get; set; }
        public string[,] UserStocks { get; set;}
        // [{string},{int},{double}]
        // [{name},{amount},{price/amount}]

        // Constructor for regular users
        public Users(int cardNumber, string password, string userName, double UserBalance)
        {
            CardNumber = cardNumber;
            Password = password;
            UserName = userName;
            UserBalance = UserBalance;
        }

        // Virtual method - can be overridden by managers
        public virtual void DisplayUserInfo()
        {
            Console.WriteLine($"User ID: {Id}");
            Console.WriteLine($"Card Number: {CardNumber}");
        }

        ///<summary>
        ///Lists out the basic infos of each stock the user owns 
        ///</summary>
        public void DisplayStocks()
        {
            Console.WriteLine("==============");
            for(int i = 0; i < UserStocks.Count(); i++)
            {
                Console.WriteLine($"Stock name: {UserStocks[i][0]}");
                Console.WriteLine($"Stock owned: {UserStocks[i][1]}");
                Console.WriteLine($"Stock price/amount: {UserStocks[i][2]}");
                Console.WriteLine("----");
            }
            Console.WriteLine("==============");
        }
    }
}
