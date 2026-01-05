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

        ///<!--Functions for the game's main menu-->
        public static void GameMenu()
        {
            Console.WriteLine($"\n==={Hotel.HotelName} Hotel Menu===");
            Console.WriteLine("1)Hotel info");
            Console.WriteLine("2)Open store");
            Console.WriteLine("3)Check for available dogs");
            Console.WriteLine("4)Check dog in");
            Console.WriteLine("5)Check dog out");
            Console.WriteLine("0)Exit the game without saving");
            Console.Write("Enter selected option: ");
        }
    }

    ///<!--This class is to handle all the Hotel functions-->
    class Hotel
    {
        public static string HotelName { get; set; }
        public static int HotelMoney { get; set; }
        public static int StackSize { get; set; } = 4;
        public static int UsedStack {  get; set; }
        public static int RemainingStack => StackSize - UsedStack;


        ///<summary>A list of dogs that are waiting in line to get into the hotel</summary>
        public static List<Dogs> DogsInLine { get; set; } = new();

        ///<summary>A list of dogs that are inside the hotel already</summary>
        public static List<Dogs> DogsInHotel { get; set; } = new();

        ///<!--Hotel Functions-->
        ///<summary>Checks if there are 5 dogs in line and if not than it adds more</summary>
        public void CheckDogsList()
        {
            if (DogsInLine.Count < 5)
            {
                while (DogsInLine.Count < 5)
                {
                    DogsInLine.Add(Randoms.GenerateDog());
                }
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

        ///<summary></summary>
        public static void TakeDogOut()
        {
            for (int i = DogsInHotel.Count - 1; i >= 0; i--)
            {
                if (DogsInHotel[i].AmountOfDaysLeft == 0)
                {
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
            
            dogs.Name = names[rand.Next(1,names.Length)];
            dogs.AmountOfDaysLeft = rand.Next(1, 8);
            switch (rand.Next(1, 4))
            {
                case 1: dogs.DogSize = 1;break;
                case 2: dogs.DogSize = 2;break;
                case 3: dogs.DogSize = 4;break;
            }
            dogs.MoneyForDog = rand.Next(1000, 10001);
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


