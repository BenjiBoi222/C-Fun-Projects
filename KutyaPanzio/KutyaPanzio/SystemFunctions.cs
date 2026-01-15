using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutyaPanzio
{
    class SystemFunctions
    {
        ///<!--This is the main register menu before the game starts-->
        /// <summary>Registers the hotel name and starts the game</summary>
        public static void RegisterMenu()
        {
            Console.WriteLine("====Welcome to the hotel====");
            Console.Write("Please enter a name for the hotel: ");
            string hotelName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(hotelName)) { Hotel.HotelName = hotelName; Program.IsRegistered = true; Program.IsGameRunning = true; }
            else Console.WriteLine("Invalid naming!");
        }




        ///<!--Functions of the system that run independent of the user-->
        
        ///<summary>Functoin to check the hotel's debt and if there is a debt for 7 days the game ends</summary>
        public static void CheckForDebt()
        {
            if (Hotel.DaysInDebt == 7)
            {
                Console.WriteLine("The hotel closed due to unpaid debt");
                EndScreen();
            }

            if (Hotel.HotelMoney < 0)
            {
                Hotel.DaysInDebt++;
            }
        }


        ///<summary>Function to check the animals out if there is stuff</summary>
        public static void CheckOutIfStaff()
        {
            if (Hotel.StaffCount > 0)
            {
                UserFunctions.CheckAnimalOut();
            }
        }

        ///<summary>The screen that plays after the game has ended</summary>
        public static void EndScreen()
        {

            Console.Clear();

            string header = "==={Hotel.HotelName} Hotel===";
            string footer = "";
            for(int i = 0; i < header.Length; i++)
                footer += "=";
            

            Program.IsGameRunning = false;
            Console.WriteLine("The game has ended");
            Console.WriteLine(header);
            Console.WriteLine($"Days played: {Hotel.DayCount}");
            Console.WriteLine($"Amount of animals in the hotel: {Hotel.OverAllAnimalCount}");
            Console.WriteLine($"Money at the end: {Hotel.HotelMoney}");
            Console.WriteLine(footer);
        }
    }
}
