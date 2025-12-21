using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula_Banking
{
    class Universal
    {
        //========== User List ==========//
        //This is where all the Users are stored inside a List
        public static List<Users> Users = new List<Users>();
        //This is where all the Stocks are stored inside a List
        public static List<Stocks> Stocks = new List<Stocks>();
        //===============================//

        //========== Current user ==========//
        //This is where the ID of the logged in user is stored 
        public static string _CurrentUserID_ = string.Empty;
        //==================================//



    }
}
