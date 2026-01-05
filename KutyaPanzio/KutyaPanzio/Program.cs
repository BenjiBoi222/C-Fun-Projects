namespace KutyaPanzio
{
    internal class Program
    {
        public static bool IsRegistered = false;
        public static bool IsGameRunning = false;
        static void Main(string[] args)
        {
            while (!IsRegistered)
            {
                UserFunctions.RegisterMenu();
            }
            while (IsGameRunning)
            {
                UserFunctions.GameMenu();
            }
        }
    }

    class UserFunctions
    {
        ///<!--This is the main register menu before the game starts-->
        /// <summary>Registers the hotel name and starts the game</summary>
        public static void RegisterMenu()
        {
            Console.WriteLine("====Welcome to the hotel====");
            Console.Write("Please enter a name for the hotel: ");
            string hotelName = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(hotelName)) { Hotel.HotelName = hotelName; Program.IsRegistered = true; Program.IsGameRunning = true; }
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
                if(int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch(choice)
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
            if(int.TryParse(Console.ReadLine(), out int pack))
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

            if(Hotel.HotelMoney >=  moneyNeeded)
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
            if(!string.IsNullOrWhiteSpace(choice.ToLower()))
            {
                switch (choice)
                {
                    case "1": Hotel.HotelInfo(); break;
                    case "2": OpenStore(); break;
                    case "3": Hotel.CheckForDogs();break;
                    case "4": CheckDogIn(); break;
                    case "5": CheckDogOut(); break;
                    case "p": PassDay();break;
                    case "m": UserManual(); break;
                    case "e": Program.IsGameRunning = false; break;
                    default: Console.WriteLine("Invalid input!"); break;
                }
            }
            else Console.WriteLine("Invalid input type!");

            int amountOfDogsReadyToLeave = 0;
            foreach(Dogs dog in Hotel.DogsInHotel)
            {
                if(dog.AmountOfDaysLeft == 0) amountOfDogsReadyToLeave++;
            }
            if(amountOfDogsReadyToLeave > 0) Console.WriteLine($"{amountOfDogsReadyToLeave} dogs are ready to leave!");
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
            foreach(Dogs dog in Hotel.DogsInHotel)
            {
                dog.AmountOfDaysLeft--;
            }

            //Ensures that after 3 days new dogs are available for the user to choose from, so the game is more fluent
            if(Hotel.DayCount % 3 == 0)
            {
                Hotel.DogsInLine.Clear();
            }
            Console.WriteLine("A day has passed..");
        }
    }

    ///<!--This class is to handle all the Hotel functions-->
    class Hotel
    {
        public static string HotelName { get; set; }
        public static int HotelMoney { get; set; }
        public static int DayCount { get; set; } = 1;
        public static int StackSize { get; set; } = 4;
        public static int UsedStack {  get; set; }
        public static int RemainingStack => StackSize - UsedStack;


        ///<summary>A list of dogs that are waiting in line to get into the hotel</summary>
        public static List<Dogs> DogsInLine { get; set; } = new();

        ///<summary>A list of dogs that are inside the hotel already</summary>
        public static List<Dogs> DogsInHotel { get; set; } = new();

        ///<!--Hotel Functions-->
        ///<summary>Checks if there are 5 dogs in line and if not than it adds more</summary>
        public static void CheckDogsList()
        {
            if (DogsInLine.Count < 5)
            {
                while (DogsInLine.Count < 5)
                {
                    DogsInLine.Add(Randoms.GenerateDog());
                }
            }
        }

        ///<summary>Writes out all the information about the hotel</summary>
        public static void HotelInfo()
        {
            Console.WriteLine("\n======================");
            Console.WriteLine($"Hotel name: {HotelName}");
            Console.WriteLine($"Hotel money: ${HotelMoney}");
            Console.WriteLine($"Current day: {DayCount}.");
            Console.WriteLine($"Hotel capavity: {StackSize}");
            Console.WriteLine($"Hotel capavity left: {RemainingStack}");
            if(DogsInHotel.Count > 0)
            {
                string headerFormat = "{0,-12} {1,-10} {2,-10} {3,-10}";

                Console.WriteLine("\n" + string.Format(headerFormat, "Name", "Size", "Fee", "Days"));
                Console.WriteLine(new string('-', 45)); // Decorative separator line

                foreach (Dogs dog in DogsInHotel)
                {
                    // {dog.Name,-12} means left-align with a width of 12 characters
                    Console.WriteLine($"{dog.Name,-12} {dog.DogSize,-10} ${dog.MoneyForDog,-9} {dog.AmountOfDaysLeft,-10}");
                }
                Console.WriteLine("======================");
            }
            else Console.WriteLine("======================");
        }

        ///<summary>Shows all the available dogs for check in</summary>
        public static void CheckForDogs()
        {
            CheckDogsList();

            // Define a format string to keep headers and data aligned
            string headerFormat = "{0,-12} {1,-10} {2,-10} {3,-10}";

            Console.WriteLine("\n"+string.Format(headerFormat, "Name", "Size", "Fee", "Days"));
            Console.WriteLine(new string('-', 45)); // Decorative separator line

            int stepper = 1;
            foreach (Dogs dog in DogsInLine)
            {
                // {dog.Name,-12} means left-align with a width of 12 characters
                Console.WriteLine($"{stepper++}){dog.Name,-12} {dog.DogSize,-10} ${dog.MoneyForDog,-9} {dog.AmountOfDaysLeft,-10}");
            }
        }

        /// <summary>Checks if the dog exists and than adds it into the list.</summary>
        public static void TakeDogIn(Dogs dog)
        {
            for (int i = DogsInLine.Count - 1; i >= 0; i--)
            {
                if (DogsInLine[i] == dog)
                {
                    if (DogsInLine[i].DogSize <= RemainingStack)
                    {
                        UsedStack += DogsInLine[i].DogSize;
                        DogsInHotel.Add(DogsInLine[i]);
                        DogsInLine.RemoveAt(i);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("No space remaining!");
                        return;
                    }
                }
            }
            Console.WriteLine("The dog does not exist!");
        }

        ///<summary>Removes the dogs that are ready to leave</summary>
        public static void TakeDogOut()
        {
            // We loop backwards (Count - 1 down to 0) 
            // because removing an item from a list shifts all subsequent items.
            // Looping forwards would cause you to skip dogs.
            for (int i = DogsInHotel.Count - 1; i >= 0; i--)
            {
                if (DogsInHotel[i].AmountOfDaysLeft <= 0)
                {
                    Console.WriteLine($" >> {DogsInHotel[i].Name} has been picked up!");

                    HotelMoney += DogsInHotel[i].MoneyForDog;
                    UsedStack -= DogsInHotel[i].DogSize;
                    DogsInHotel.RemoveAt(i);
                }
            }
        }
    }

    ///<!--This class handles all the dogs and to give them each a unnique elements-->
    class Dogs
    {
        ///<summary>Name of the dog</summary>
        public string Name { get; set; }

        ///<summary>The amount of days the dog needs to be in the hotel</summary>
        public int AmountOfDaysLeft { get; set; }

        ///<summary>1-small;2-medium;3-large</summary>
        public int DogSize { get; set; }

        /// <summary>The amount of money the user will recieve after the dog leaves the hotel</summary>
        public int MoneyForDog { get; set; }
    }
    
    ///<!--This class handles all the random functions that needs to be done-->
    class Randoms
    {
        ///<summary>Generates a random Dog if called</summary>
        ///<returns>A dog</returns>
        public static Dogs GenerateDog()
        {
            Random rand = new();
            Dogs dogs = new Dogs();

            string[] names = { "Buddy", "Bella", "Max", "Luna", "Charlie", "Daisy", "Cooper", "Milo" };
            
            dogs.Name = names[rand.Next(0,names.Length)];
            dogs.AmountOfDaysLeft = rand.Next(1, 8);
            switch (rand.Next(1, 4))
            {
                case 1: dogs.DogSize = 1; dogs.MoneyForDog = rand.Next(100, 251); break;
                case 2: dogs.DogSize = 2; dogs.MoneyForDog = rand.Next(251, 501); break;
                case 3: dogs.DogSize = 4; dogs.MoneyForDog = rand.Next(501, 1001); break;
            }
            return dogs;
        }
    }
}

/// DevPlans:
/// =========
/// 
///Game mechanic:
///---------------
///Random people with their dogs
///You can store the dogs based on how many you can take in 
///Upgrades to the hotel can be made so there is more place for the dogs
///Dogs generate by random 
///Each dog has a unique random generated name
///User can pass a day by a button
///Each dog has a unique staying amount that the user has to take care of them
///Each dog has a size. (Small-1,Medium-2,Large-4) And they take up that much slot.
///Each slot has a prize and that's how the dogs will be priced
///
///Classes needed:
///----------------
///1]Dogs: stores all the dogs and their infos
///2]Hotel: stores all the hotel informations
///3]Upgrades: stores all the upgrades and their price
///4]Random: does all the random generations
///5]UI: stores all the funcions that the player can do
///6]Program: runs the game
///
///For the future:
///----------------
///Dogs need to be fed and drank
///


