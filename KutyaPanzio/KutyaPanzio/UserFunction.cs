using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                Console.WriteLine("2)Buy more food");
                Console.WriteLine("0)Exit store");
                Console.Write("Enter selected option: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 0: isStoreActive = false; break;
                        case 1: BuyMoreStack(); break;
                        case 2: BuyMoreFood(); break;
                        default: Console.WriteLine("Invalid input!"); break;
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
                    default: Console.WriteLine("Invalid input!"); break;
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


        ///<summary>This function lists all the food options, stores their value and adds it into the Hotels Foods storage if purchase was successful</summary>
        private static void BuyMoreFood()
        {
            var prices = new Dictionary<string, int>
            {
                { "fish", 10 }, { "chicken", 14 }, { "beef", 25 }, { "carrot", 5 }, { "apple", 3 }
            };

            Console.WriteLine("\n==Food options==");
            Console.WriteLine($"1)Fish({prices["fish"]}/piece)");
            Console.WriteLine($"2)Chicken({prices["chicken"]}/piece)");
            Console.WriteLine($"3)Beef({prices["beef"]}/piece)");
            Console.WriteLine($"4)Carrot({prices["carrot"]}/piece)");
            Console.WriteLine($"5)Apple({prices["apple"]}/piece)");
            Console.Write("Enter your choice: ");
            
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.Write("Enter the amount you want to buy: ");
                if (int.TryParse(Console.ReadLine(), out int amount))
                {
                    FoodTypes result = null;
                    switch (choice)
                    {
                        case 1: result = HadleFoodTypes(amount, prices["fish"], "fish"); break;
                        case 2: result = HadleFoodTypes(amount, prices["chicken"], "chicken"); break;
                        case 3: result = HadleFoodTypes(amount, prices["beef"], "beef"); break;
                        case 4: result = HadleFoodTypes(amount, prices["carrot"], "carrot"); break;
                        case 5: result = HadleFoodTypes(amount, prices["apple"], "apple"); break;
                        default: Console.WriteLine("Invalid input!"); break;
                    }

                    if(result != null) { Hotel.FoodTypes.Add(result); Console.WriteLine("Food succesfully bought"); }
                }
            }
        }
        /// <summary>Calculates the payable amount and handles the purchase/if unsuccessful than creates a null food element</summary>
        /// <param name="amount">The quantity of the food the user intends to but</param>
        /// <param name="price">The unique price of a SINGULAR quantity</param>
        /// <param name="foodType">The foods type/name</param>
        /// <returns>A FoodTypes object or null</returns>
        private static FoodTypes HadleFoodTypes(int amount, int price, string foodType)
        {
            int foodMoney = amount * price;

            if (foodMoney <= Hotel.HotelMoney)
            {
                Hotel.HotelMoney -= foodMoney;
                return new FoodTypes
                {
                    FoodType = foodType,
                    FoodQuantity = amount
                };
            }

            Console.WriteLine("Insufficient funds!");
            return null; 
        }


        ///<!--Functions for the game's main menu-->
        public static void GameMenu()
        {
            Console.WriteLine($"\n==={Hotel.HotelName} Hotel Menu===");
            Console.WriteLine("1)Hotel info");
            Console.WriteLine("2)Open store");
            Console.WriteLine("3)Check for available animals");
            Console.WriteLine("4)Check animal in");
            Console.WriteLine("5)Check animal out");
            Console.WriteLine("6)Check animal needs");
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
                    case "3": Hotel.CheckForAnimal(); break;
                    case "4": CheckAnimalIn(); break;
                    case "5": CheckAnimalOut(); break;
                    case "6": AnimalNeedsListed(); break;
                    case "p": PassDay(); break;
                    case "m": UserManual(); break;
                    case "e": Program.IsGameRunning = false; break;
                    default: Console.WriteLine("Invalid input!"); break;
                }
            }
            else Console.WriteLine("Invalid input type!");

            int amountOfDogsReadyToLeave = 0;
            foreach (Animals dog in Hotel.AnimalsInHotel)
            {
                if (dog.AmountOfDaysLeft == 0) amountOfDogsReadyToLeave++;
            }
            if (amountOfDogsReadyToLeave > 0) Console.WriteLine($"{amountOfDogsReadyToLeave} animal(s) are ready to leave!");
        }
        

        ///<!--Private Functions of the main menu-->

        /// <summary>Checks an animal in</summary>
        private static void CheckAnimalIn()
        {
            Hotel.CheckForAnimal();

            Console.Write("\nEnter the animal's number to check in: ");
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                int index = number - 1;

                if (index >= 0 && index < Hotel.AnimalInLine.Count)
                {
                    Animals selectedDog = Hotel.AnimalInLine[index];
                    Hotel.TakeAnimalIn(selectedDog);
                }
                else
                {
                    Console.WriteLine("Invalid animal number!");
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }

        }

        /// <summary>Checks an animal out</summary>
        private static void CheckAnimalOut()
        {
            Console.WriteLine("\n--- Processing Check-outs ---");

            if (Hotel.AnimalsInHotel.Count == 0)
            {
                Console.WriteLine("The hotel is currently empty.");
                return;
            }

            int moneyBefore = Hotel.HotelMoney;
            int animalsBefore = Hotel.AnimalsInHotel.Count;

            Hotel.TakeAnimalOut();

            int animalsDeparted = animalsBefore - Hotel.AnimalsInHotel.Count;
            int moneyEarned = Hotel.HotelMoney - moneyBefore;

            if (animalsDeparted > 0)
            {
                Console.WriteLine($"Success! {animalsDeparted} dog(s) went home.");
                Console.WriteLine($"You earned: ${moneyEarned}");
            }
            else
            {
                Console.WriteLine("No dogs are ready to go home yet. Check back tomorrow!");
            }
        }

        /// <summary>Checks all the staying animals needs and writes them up</summary>
        private static void AnimalNeedsListed()
        {
            if (Hotel.AnimalsInHotel.Count > 0)
            {
                // Use a consistent format for the header and the rows
                string rowFormat = "{0,-15} {1,-15} {2,-15} {3,-15} {4,-15}";

                Console.WriteLine("\n" + string.Format(rowFormat, "Name", "Food", "Food amount", "Water", "Walk"));
                Console.WriteLine(new string('-', 60));

                foreach (Animals animal in Hotel.AnimalsInHotel)
                {
                    // Only show the word "Needs" if the bool is true, otherwise leave it empty ""
                    string foodStatus = animal.NeedsFood ? "Needs" : "";
                    string waterStatus = animal.NeedsWater ? "Needs" : "";
                    string walkStatus = animal.NeedsWalk ? "Needs" : "";

                    Console.WriteLine(string.Format(rowFormat,
                        animal.Name,
                        foodStatus,
                        animal.AmountOfFoodPerDay,
                        waterStatus,
                        walkStatus));
                }
            }
            else Console.WriteLine("No animal is checked inside the hotel!");
        }


        /// <summary>Passes a day</summary>
        private static void PassDay()
        {
            Hotel.DayCount++;
            Hotel.HotelMoney -= Hotel.StackSize * 10;
            foreach (Animals dog in Hotel.AnimalsInHotel)
            {
                dog.AmountOfDaysLeft--;
            }

            //Ensures that after 3 days new dogs are available for the user to choose from, so the game is more fluent
            if (Hotel.DayCount % 3 == 0)
            {
                Hotel.AnimalInLine.Clear();
            }

            ///<!--THESE MUST STAY IN THIS ORDER-->
            ///<!--LOGIC ERROR APPEARS-->
            ///If the Generation runs before the check
            /// than some fields of the animal might get overwritten
            /// and if the only missing action was like for eg. a walk 
            /// and it gets overwritten by the generator, than the animal won't leave 
            /// by unsatisfaction.
            
            Randoms.CheckAnimalSatisfaction();
            Console.WriteLine("A day has passed..");
            Randoms.GenerateBehavior();
        }

        




        ///<!--User manual-->
        private static void UserManual()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine($"   MANUAL: HOW TO RUN {Hotel.HotelName.ToUpper()} HOTEL");
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
    }
}
