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


        // Constructor for regular users
        public Users(int cardNumber, string password, string userName)
        {
            CardNumber = cardNumber;
            Password = password;
            UserName = userName;
        }

        // Virtual method - can be overridden by managers
        public virtual void DisplayUserInfo()
        {
            Console.WriteLine($"User ID: {Id}");
            Console.WriteLine($"Card Number: {CardNumber}");
        }
    }
}
