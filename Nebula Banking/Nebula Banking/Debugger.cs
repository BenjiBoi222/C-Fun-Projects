using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula_Banking
{
    class Debugger
    {
        public static void ShowUserListElements()
        {
            if (Universal.Users.Count > 0)
            {
                foreach (var user in Universal.Users)
                {
                    Console.WriteLine("=====");
                    Console.WriteLine($"{user.Id};{user.CardNumber};{user.Password};{user.UserName};{user.UserBalance}");
                    Console.WriteLine("=====");
                }
            }else Console.WriteLine("The list was empty");
        }



    }
}
