using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula_Banking
{
    class LoginMenu
    {
        /// <summary>
        /// Shows the options inside the Login menu
        /// </summary>
        public void ShowMenu()
        {
            Console.WriteLine("\n=== Nebula Bank Login Menu ===");
            Console.WriteLine("0)Exit app");
            Console.WriteLine("1)Login");
            Console.WriteLine("2)Register");
        }

        /// <summary>
        /// Handles the selected menu option based on the specified choice.
        /// </summary>
        /// <param name="choice">The numeric value representing the user's menu selection. Each value corresponds to a specific menu action.</param>
        public void HandleMenu(int choice)
        {
            switch (choice)
            {
                case 0: Program.isRunning = false; break;
                case 1: LoginFunc(); break;
                case 2: RegisterFunc(); break;
                default: Console.WriteLine("Invalid input!"); break;
            }
        }


        /// <summary>
        /// A private function that checks for a given user inside the Userbase
        /// </summary>
        private static void LoginFunc()
        {
            Console.Write("Enter your card number: ");
            //Checks if the user given input is an {int}, if not than error
            if (int.TryParse(Console.ReadLine(), out var cardNumber))
            {
                    foreach(var userElement in Universal.Users)
                    {
                        if(userElement.CardNumber == cardNumber)
                        {
                            for(int i = 0; i < 3; i++)
                            {
                                Console.WriteLine($"Tries left: {3-i}");
                                Console.Write("Enter your password");
                                string password = Console.ReadLine();

                                if(password == userElement.Password)
                                {
                                    Universal._CurrentUserID_ = userElement.Id;
                                    Program.isLoggedIn = true;
                                    Program.switchFromMenu = true;
                                    Console.WriteLine("Login succesful");
                                }
                            }

                        }
                    }
                    //If the foreach loop can't find the card number inside, than the user doesn't exit
                    Console.WriteLine("Card not registered in database");
                    return;
            }
            else { Console.WriteLine("Invalid input, only numbers!"); }

        }
        /// <summary>
        /// A private function that registers a new users IF it doesnt exist already!
        /// </summary>
        private static void RegisterFunc()
        {
            string userPasword;
            int cardNumber;

            Console.Write("Enter your new username: ");
            string userName = Console.ReadLine();

            //User pasword with validation
            bool validPassword = false;
            while (!validPassword)
            {
                Console.Write("Enter new password: ");
                string password = Console.ReadLine();
                Console.Write("Enter new password again: ");
                string passwordTest = Console.ReadLine();

                if(password == passwordTest) {validPassword = true; userPasword = password;}
            }


            //Checks if the generated card number is unique and if not it generates a new one
            bool cardNumberIsUnique = false;
            while (!cardNumberIsUnique)
            {
                int number = GenerateCustomCardNumber();
                bool exists = false;
                foreach(var userElement in Universal.Users)
                {
                    if(userElement.CardNumber == number)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)    
                {
                    cardNumber = number;
                    cardNumberIsUnique = true;
                }
            }

            Users user = new Users(cardNumber, password, userName);
            Console.WriteLine("Registration succesful!");
            
            //========= After register userinfo =========//
            foreach(var userElement in Universal.Users)
            {
                if(userElement.cardNumber == cardNumber) userElement.DisplayUserInfo();
            }
            //===========================================//
        }


        //--Helper function--//

        /// <summary>
        /// Helper function for the register option, generates a random 12 card number 
        /// </summary>
        /// <returns>{int} - the card number</returns>
        private static int GenerateCustomCardNumber()
        {
            Random random = new Random();

            string cardNumber = string.empty;
            //THis while loop runs until the card number is less than 12 number long
            while(!cardNumber.Length == 12)
            {
                int number = random.Next(1, 10);
                cardNumber += number.ToString();
            }
             
            return int.Parse(cardNumber);
        }
    }
}
