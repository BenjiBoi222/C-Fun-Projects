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
            if (!string.IsNullOrWhiteSpace(hotelName)) 
            {
                string finalHotelName = "";
                for(int i = 0; i < hotelName.Length; i++)
                {
                    if (i == 0) finalHotelName = hotelName[i].ToString().ToUpper();
                    else finalHotelName += hotelName[i].ToString().ToLower();
                }

                Hotel.HotelName = finalHotelName;
                Program.IsRegistered = true;
                Program.IsGameRunning = true; 
            }
            else Console.WriteLine("Invalid naming!");
        }

        ///<summary>This function checks if any need is missing for an animal and if yes than the animal leaves</summary>
        public static void CheckAnimalSatisfaction()
        {
            for (int i = Hotel.AnimalsInHotel.Count - 1; i >= 0; i--)
            {
                if (Hotel.AnimalsInHotel[i].NeedsWalk == true ||
                    Hotel.AnimalsInHotel[i].NeedsWater == true ||
                    Hotel.AnimalsInHotel[i].NeedsFood == true)
                {
                    Console.WriteLine($"{Hotel.AnimalsInHotel[i].Name} got picked up after being unsatisfied!");
                    Hotel.UsedStack -= Hotel.AnimalsInHotel[i].AnimalSize;
                    Hotel.MessyStackAmount += Hotel.AnimalsInHotel[i].AnimalSize;
                    Hotel.AnimalsInHotel.RemoveAt(i);
                }
            }
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
            if (Hotel.HotelMoney < 0) Hotel.DaysInDebt++;
            if (Hotel.HotelMoney >= 0) Hotel.DaysInDebt = 0;
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


        ///<!--The warning function-->
        ///<!--This function is large, mainly because it will have every element that needs to be done the current day-->
        public static void Reminders()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            //First reminder: How many animals are ready to leave
            int readyToLeave = Hotel.AnimalsInHotel.Count(a => a.AmountOfDaysLeft == 0);
            if (readyToLeave > 0)
                Console.WriteLine($"{readyToLeave} animal(s) ready to leave! Proceed with checkout!");

            //Second reminder: How many animals needs to be fed
            int readyToEat = Hotel.AnimalsInHotel.Count(a => a.NeedsFood); ;
            if (readyToEat > 0)
                Console.WriteLine($"{readyToEat} animal(s) ready to eat! Proceed with feeding!");

            //Third reminder: How many animals needs to be given water
            int readyToDrink = Hotel.AnimalsInHotel.Count(a => a.NeedsWater); ;
            if (readyToDrink > 0)
                Console.WriteLine($"{readyToDrink} animal(s) ready to drink! Proceed with watering!");

            //Fourth reminder: How many animals need to go on a walk
            int readyToWalk = Hotel.AnimalsInHotel.Count(a => a.NeedsWalk);
            if (readyToWalk > 0)
                Console.WriteLine($"{readyToWalk} animal(s) ready to go on a walk! Proceed with walkings!");

            int debt = Hotel.DaysInDebt;
            if (debt > 0)
            {
                Console.WriteLine($"Your hotel has been in debt for {debt}!");
                Console.WriteLine($"{7 - debt} days remaining before your hotel closes!");
            }


            //Fifth reminder: Is there any mess to clean inside the hotel
            if (Hotel.MessyStackAmount > 0)
                Console.WriteLine("There is mess that needs to be cleaned up!");


            Console.ResetColor();
        }
    }
}
