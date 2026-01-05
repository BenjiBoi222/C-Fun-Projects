namespace KutyaPanzio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Classes in progress!");
        }
    }

    ///<!--This class is to handle all the Hotel functions!-->
    class Hotel
    {
        public string HotelName { get; set; }
        public int HotelMoney { get; set; }
        public int StackSize { get; set; }


        ///<summary>A list of dogs that are waiting in line to get into the hotel</summary>
        public static List<Dogs> DogsInLine { get; set; } = new();

        ///<summary>A list of dogs that are inside the hotel already</summary>
        public static List<Dogs> DogsInHotel { get; set; } = new();

        ///<summary>Checks if there are 5 dogs in line and if not than it adds more</summary>
        public void CheckDogsList()
        {
            if (DogsInLine.Count < 5)
            {
                while (DogsInLine.Count < 0)
                {
                    DogsInLine.Add(Randoms.GenerateDog());
                }
            }
        }

        /// <summary>Checks if the dog exists and than adds it into the list. !MORE CHECKS NEEDS ADDING LIKE SPACE REMAINING!</summary>
        /// <param name="dog">A dog parameter from the DogsInLine list</param>
        public static void TakeDogIn(Dogs dog)
        {
            foreach(Dogs dogs in DogsInLine)
            {
                if(dogs == dog)
                {
                    DogsInHotel.Add(dogs);
                    return;
                }
            }
            Console.WriteLine("The dog does not exist!");
        }
    }

    ///<!--This class is to handle all the dogs and to give them each a unnique elements-->
    class Dogs
    {
        ///<summary>Name of the dog</summary>
        public string Name { get; set; }

        ///<summary>The amount of days the dog needs to be in the hotel</summary>
        public int AmountOfDaysLeft { get; set; }

        ///<summary>1-small;2-medium;3-large</summary>
        public int DogSize { get; set; }
    }
    
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
            dogs.DogSize = rand.Next(1, 4);
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


