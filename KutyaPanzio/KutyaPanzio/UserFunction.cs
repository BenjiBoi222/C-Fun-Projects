using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutyaPanzio
{
    class UserFunctions
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


        ///<!--Store menu functions-->
        private static void OpenStore()
        {
            bool isStoreActive = true;
            while (isStoreActive)
            {
                Console.WriteLine("\n===Store===");
                Console.WriteLine("1)Buy more slots");
                Console.WriteLine("0)Exit store");
                Console.Write("Enter selected option: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 0: isStoreActive = false; break;
                        case 1: BuyMoreStack(); break;
                    }
                }
            }
        }
        private static void BuyMoreStack()
        {
            Console.WriteLine("\n==Stack options==");
            Console.WriteLine("1)Buy 1 more stack");
            Console.WriteLine("2)Buy 2 more stack");
            Console.WriteLine("3)Buy 4 more stack");
            Console.WriteLine("4)Buy 8 more stack");
            Console.Write("Enter the pack you want(100$/stack): ");
            if (int.TryParse(Console.ReadLine(), out int pack))
            {
                switch (pack)
                {
                    case 1: BuyStack(1); break;
                    case 2: BuyStack(2); break;
                    case 3: BuyStack(4); break;
                    case 4: BuyStack(8); break;
                }
            }
        }
        private static void BuyStack(int pack)
        {
            int moneyNeeded = pack * 100;

            if (Hotel.HotelMoney >= moneyNeeded)
            {
                Hotel.HotelMoney -= moneyNeeded;
                Hotel.StackSize += pack;
            }
            else Console.WriteLine("Insuficent funds");
        }

        ///<!--Functions for the game's main menu-->
        public static void GameMenu()
        {
            Console.WriteLine($"\n==={Hotel.HotelName} Hotel Menu===");
            Console.WriteLine("1)Hotel info");
            Console.WriteLine("2)Open store");
            Console.WriteLine("3)Check for available dogs");
            Console.WriteLine("4)Check dog in");
            Console.WriteLine("5)Check dog out");
            Console.WriteLine("P)Pass a day");
            Console.WriteLine("M)User manual");
            Console.WriteLine("E)Exit the game without saving");
            Console.Write("Enter selected option: ");
            string choice = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(choice.ToLower()))
            {
                switch (choice)
                {
                    case "1": Hotel.HotelInfo(); break;
                    case "2": OpenStore(); break;
                    case "3": Hotel.CheckForDogs(); break;
                    case "4": CheckDogIn(); break;
                    case "5": CheckDogOut(); break;
                    case "p": PassDay(); break;
                    case "m": UserManual(); break;
                    case "e": Program.IsGameRunning = false; break;
                    default: Console.WriteLine("Invalid input!"); break;
                }
            }
            else Console.WriteLine("Invalid input type!");

            int amountOfDogsReadyToLeave = 0;
            foreach (Dogs dog in Hotel.DogsInHotel)
            {
                if (dog.AmountOfDaysLeft == 0) amountOfDogsReadyToLeave++;
            }
            if (amountOfDogsReadyToLeave > 0) Console.WriteLine($"{amountOfDogsReadyToLeave} dogs are ready to leave!");
        }
        ///<!--User manual-->
        private static void UserManual()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine($"   MANUAL: HOW TO RUN {Hotel.HotelName.ToUpper()}");
            Console.WriteLine("========================================");

            Console.WriteLine("\n1. THE OBJECTIVE");
            Console.WriteLine("   Manage your hotel capacity, care for dogs, and");
            Console.WriteLine("   earn money to expand your business.");

            Console.WriteLine("\n2. HOTEL CAPACITY (STACKS)");
            Console.WriteLine("   Every dog takes up 'Stack' space based on size:");
            Console.WriteLine("   * Small Dogs  : 1 Stack");
            Console.WriteLine("   * Medium Dogs : 2 Stacks");
            Console.WriteLine("   * Large Dogs  : 4 Stacks");
            Console.WriteLine("   You cannot check in a dog if you don't have enough");
            Console.WriteLine("   Remaining Capacity.");

            Console.WriteLine("\n3. THE DAILY CYCLE");
            Console.WriteLine("   * Use 'Check for available dogs' to see who is waiting.");
            Console.WriteLine("   * 'Check dog in' to start their stay.");
            Console.WriteLine("   * 'Pass a day' to progress time. Dogs stay for a ");
            Console.WriteLine("     set number of days.");

            Console.WriteLine("\n4. EARNING MONEY");
            Console.WriteLine("   Dogs ONLY pay when they leave. When a dog's 'Days'");
            Console.WriteLine("   hits 0, use 'Check dog out' (Option 5) to collect");
            Console.WriteLine("   your fee and free up hotel space.");

            Console.WriteLine("\n5. UPGRADING");
            Console.WriteLine("   Visit the Store to buy more Stacks. More stacks");
            Console.WriteLine("   mean you can board larger dogs or more dogs at once.");

            Console.WriteLine("\n========================================");
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        ///<!--Private Functions of the main menu-->

        /// <summary>Checks a dog in</summary>
        private static void CheckDogIn()
        {
            Hotel.CheckForDogs();

            Console.Write("\nEnter the dog's number to check in: ");
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                int index = number - 1;

                if (index >= 0 && index < Hotel.DogsInLine.Count)
                {
                    Dogs selectedDog = Hotel.DogsInLine[index];
                    Hotel.TakeDogIn(selectedDog);
                }
                else
                {
                    Console.WriteLine("Invalid dog number!");
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }

        }

        /// <summary>Checks a dog out</summary>
        private static void CheckDogOut()
        {
            Console.WriteLine("\n--- Processing Check-outs ---");

            if (Hotel.DogsInHotel.Count == 0)
            {
                Console.WriteLine("The hotel is currently empty.");
                return;
            }

            int moneyBefore = Hotel.HotelMoney;
            int dogsBefore = Hotel.DogsInHotel.Count;

            Hotel.TakeDogOut();

            int dogsDeparted = dogsBefore - Hotel.DogsInHotel.Count;
            int moneyEarned = Hotel.HotelMoney - moneyBefore;

            if (dogsDeparted > 0)
            {
                Console.WriteLine($"Success! {dogsDeparted} dog(s) went home.");
                Console.WriteLine($"You earned: ${moneyEarned}");
            }
            else
            {
                Console.WriteLine("No dogs are ready to go home yet. Check back tomorrow!");
            }
        }

        /// <summary>Passes a day</summary>
        private static void PassDay()
        {
            Hotel.DayCount++;
            foreach (Dogs dog in Hotel.DogsInHotel)
            {
                dog.AmountOfDaysLeft--;
            }

            //Ensures that after 3 days new dogs are available for the user to choose from, so the game is more fluent
            if (Hotel.DayCount % 3 == 0)
            {
                Hotel.DogsInLine.Clear();
            }
            Console.WriteLine("A day has passed..");
        }
    }
}
