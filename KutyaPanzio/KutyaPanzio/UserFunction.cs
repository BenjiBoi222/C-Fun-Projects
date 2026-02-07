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
        ///<!--Functions for the game's main menu-->
        public static void GameMenu()
        {
            string[] mainMenuOptions =
            {
                "Hotel info",
                "Open shop",
                "Check for available animals",
                "Check animal in",
                "Check animal out",
                "Check animal needs",
                "Animal Care & Maintenance",
                "Clean up mess",
                "Bank menu",
                "Pass a day",
                "User manual",
                "Exit the game without saving"
            };


            int option = ShortFunctions.ShowMenu(Hotel.HotelName, mainMenuOptions, ">");

            switch (option)
            {
                case 0: Hotel.HotelInfo(); break;
                case 1: StoreFunctions.OpenShop(); break;
                case 2: Hotel.CheckForAnimal(); break;
                case 3: CheckAnimalIn(); break;
                case 4: CheckAnimalOut(); break;
                case 5: AnimalNeedsListed(); break;
                case 6: AnimalCareList(); break;
                case 7: CleanAnimalMess(); break;
                case 8: Bank.BankMenu(); break;
                case 9: PassDay(); break;
                case 10: UserManual(); break;
                case 11: SystemFunctions.EndScreen(); break;
                case -1: Randoms.ClownFace(); break;
                default: Console.WriteLine("Invalid input!"); break;
            }
            SystemFunctions.Reminders();
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
                    Hotel.OverAllAnimalCount++;
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
        public static void CheckAnimalOut()
        {
            Console.WriteLine("\n--- Processing Check-outs ---");

            if (Hotel.AnimalsInHotel.Count == 0)
            {
                Console.WriteLine("The hotel is currently empty.");
            }
            else
            {

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
                    Console.WriteLine("No animal is ready to go home yet. Check back tomorrow!");
                }
            }

            Console.WriteLine("Press any button to continue...");
            Console.ReadKey();
        }

        /// <summary>Checks all the staying animals needs and writes them up</summary>
        private static void AnimalNeedsListed()
        {
            if (Hotel.AnimalsInHotel.Count > 0)
            {
                // Use a consistent format for the header and the rows
                string rowFormat = "{0,-15} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}";

                Console.WriteLine("\n" + string.Format(rowFormat, "Name", "Food","Food type","Food amount", "Water", "Walk"));
                Console.WriteLine(new string('-', 100));

                foreach (Animals animal in Hotel.AnimalsInHotel)
                {
                    // Only show the word "Needs" if the bool is true, otherwise leave it empty ""
                    string foodStatus = animal.NeedsFood ? "Needs" : "Fed";
                    string waterStatus = animal.NeedsWater ? "Needs" : "Watered";
                    string walkStatus = animal.NeedsWalk ? "Needs" : "Walked";

                    Console.WriteLine(string.Format(rowFormat,
                        animal.Name,
                        foodStatus,
                        animal.NeededFoodType,
                        animal.AmountOfFoodPerDay,
                        waterStatus,
                        walkStatus));
                }
            }
            else Console.WriteLine("No animal is checked inside the hotel!");

            Console.WriteLine("Press any button to continue...");
            Console.ReadKey();
        }

        ///<!--Private functions and menu of the care taking actions-->
        /// <summary>Shows all the interactions the user can do to take care of the animals</summary>
        private static void AnimalCareList()
        {
            int staffCount = Hotel.StaffCount;
            int capacity = 1 + staffCount;
            bool menuIsRunning = true;

            while(menuIsRunning)
            {
                Console.WriteLine("\n=== Animal Care & Maintenance ===");
                Console.WriteLine("1) Feed the animal");
                Console.WriteLine("2) Give drink to animal");
                Console.WriteLine("3) Take an animal out on a walk");
                Console.WriteLine("0) Exit Care & Maintenance menu");
                Console.WriteLine($"\nWarning: Currently you can take care of {capacity} animals at a time.");
                Console.WriteLine($"(Base: 1 + Staff: {staffCount})");
                Console.Write("Enter chosen option: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 0: menuIsRunning = false; break;
                        case 1: FeedAnimal(capacity); break;
                        case 2: GiveDrinkToAnimal(capacity); break;
                        case 3: TakeAnimalOnWalk(capacity); break;
                    }
                }
            }
        }

        /// <summary>Gives food to as much animal per function run as much stuff the hotel has (base:1 stuff)</summary>
        /// <param name="maxAmount">The amount of staff inside the hotel</param>
        private static void FeedAnimal(int maxAmount)
        {
            int fedCount = 0;

            foreach (var animal in Hotel.AnimalsInHotel)
            {
                if (fedCount >= maxAmount)
                {
                    break;
                }

                if (animal.NeedsFood)
                {
                    foreach (var food in Hotel.FoodTypes)
                    {
                        if (food.FoodType == animal.NeededFoodType)
                        {
                            if (food.FoodQuantity >= animal.AmountOfFoodPerDay)
                            {
                                food.FoodQuantity -= animal.AmountOfFoodPerDay;
                                animal.NeedsFood = false;
                                fedCount++;
                                Console.WriteLine($"{animal.Name} got fed.");
                            }
                            else
                            {
                                Console.WriteLine($"{animal.Name} couldn't be fed: isn't enough {food.FoodType} in storage.");
                            }
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>Gives water to as much animal per function run as much stuff the hotel has (base:1 stuff)</summary>
        /// <param name="maxAmount">The amount of staff inside the hotel</param>
        private static void GiveDrinkToAnimal(int maxAmount)
        {
            int givenDrinkCount = 0;

            foreach (var animal in Hotel.AnimalsInHotel)
            {
                if (givenDrinkCount >= maxAmount)
                {
                    break;
                }

                if (animal.NeedsWater)
                {
                    animal.NeedsWater = false;
                    givenDrinkCount++;
                    Console.WriteLine($"{animal.Name} was given water.");
                }
            }
        }

        /// <summary>Takes as much animal for a walk per function run as much stuff the hotel has (base:1 stuff)</summary>
        /// <param name="maxAmount">The amount of staff inside the hotel</param>
        private static void TakeAnimalOnWalk(int maxAmount)
        {
            int takenOnWalk = 0;
            foreach (var animal in Hotel.AnimalsInHotel)
            {
                if (takenOnWalk >= maxAmount)
                {
                    break;
                }

                if (animal.NeedsWalk)
                {
                    if(animal.NeedsWater || animal.NeedsFood)
                        Console.WriteLine($"{animal.Name} cannot be walked until fed and given water!");
                    else
                    {
                        animal.NeedsWalk = false;
                        takenOnWalk++;
                        Console.WriteLine($"{animal.Name} was taken to a walk.");
                    }
                }
            }
        }


        private static void CleanAnimalMess()
        {
            if(Hotel.MessyStackAmount > 0)
            {
                Hotel.MessyStackAmount = 0;
                Console.WriteLine("The stacks were cleaned!");
            }
            Console.WriteLine("Press any button to continue...");
            Console.ReadKey();
        }



        /// <summary>Passes a day</summary>
        private static void PassDay()
        {
            Hotel.DayCount++;
            Hotel.HotelMoney -= Hotel.HotelFeePerDay;
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
            
            SystemFunctions.CheckAnimalSatisfaction();
            Console.WriteLine("A day has passed..");
            SystemFunctions.CheckForDebt();
            Randoms.GenerateBehavior();
            SystemFunctions.CheckOutIfStaff();
            Bank.BankActions();

            ShortFunctions.Sleep(1);
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
