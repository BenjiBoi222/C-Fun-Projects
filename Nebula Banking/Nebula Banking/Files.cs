using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula_Banking
{
    class File
    {
        /**
         * Because making a universal file reader would be way out of our league
         * I suggest making a reader and a writer to every txt file we make.
         */

        /// <summary>
        /// Reads the content of the User.txt, elements separated by ";" |
        /// Id;CardNumber;Password;Username
        /// </summary>
        public void ReadUserFile()
        {
            using (StreamReader sr = new StreamReader("Users.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] lines = line.Split(";");

                    // 1. Parse the values first
                    int id = int.Parse(lines[0]);
                    int cardNumber = int.Parse(lines[1]);
                    string password = lines[2];
                    string username = lines[3];

                    // 2. Create the object using those values
                    Users usr = new Users(cardNumber, password, username);
                    
                    // 3. Manually override the ID since User class auto-increments it
                    usr.Id = id;
                }
            }
        }

        /// <summary>
        /// Writes into the User.txt file, devided by ";" |
        /// Id;CardNumber;Password;Username
        /// </summary>
        public void WriteUserFile()
        {
            //If there is no user in the list, than save nothing
            if (Universal.Users.Count == 0) return;            

            using(StreamWriter sw = new StreamWriter("Users.txt"))
            {
                foreach(var userElement in Universal.Users)
                {
                    sw.WriteLine($"{userElement.Id};{userElement.CardNumber};{userElement.Password};{userElement.UserName}");
                }
            }
        }



    }
}
