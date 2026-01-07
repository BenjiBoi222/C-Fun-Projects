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

    
    class Randoms
    {
        public static string[] foodTypes = {"fish", "chicken", "carrot", "beef", "apple"};


        ///<summary>Generates a random Dog if called</summary>
        ///<returns>A dog</returns>
        public static Animals GenerateAnimal()
        {
            Random rand = new();
            Animals animal = new Animals();

            string[] names = { "Buddy", "Bella", "Max", "Luna", "Charlie", "Daisy", "Cooper", "Milo" };
            string[] types = { "dog", "cat", "lama", "donkey", "horse"};
            animal.Name = names[rand.Next(0,names.Length)];
            animal.AmountOfDaysLeft = rand.Next(1, 8);
            animal.NeededFoodType = foodTypes[rand.Next(0, foodTypes.Length)];
            animal.AnimalType = types[rand.Next(0, types.Length)];
            switch (rand.Next(1, 4))
            {
                case 1: animal.AnimalSize = 1; animal.MoneyForAnimal = rand.Next(100, 251); break;
                case 2: animal.AnimalSize = 2; animal.MoneyForAnimal = rand.Next(251, 501); break;
                case 3: animal.AnimalSize = 4; animal.MoneyForAnimal = rand.Next(501, 1001); break;
            }
            return animal;
        }

        ///<summary>Generates a random amount of how much food an animal needs</summary>
        ///<returns>Food amount for SPECIFIC day</returns>
        public static int FoodForAnimal()
        {
            Random rand = new();
            int amount = 1;
            foreach(Animals animal in Hotel.AnimalsInHotel)
            {
                animal.AmountOfFoodPerDay = rand.Next(amount, amount * animal.AnimalSize);
            }
            return amount;
        }

        ///<summary>This function checks if any need is missing for an animal and if yes than the animal leaves</summary>
        public static void CheckAnimalSatisfaction()
        {
            for(int i = Hotel.AnimalsInHotel.Count - 1; i >= 0; i--)
            {
                if (Hotel.AnimalsInHotel[i].NeedsWalk == true ||
                    Hotel.AnimalsInHotel[i].NeedsWater == true ||
                    Hotel.AnimalsInHotel[i].NeedsFood == true)
                {
                    Console.WriteLine($"{Hotel.AnimalsInHotel[i].Name} got picked up after being unsatisfied!");
                    Hotel.UsedStack -= Hotel.AnimalsInHotel[i].AnimalSize;
                    Hotel.AnimalsInHotel.RemoveAt(i);
                }
            }
        }

        public static void GenerateBehavior()
        {
            Random rand = new();
            foreach(Animals animal in Hotel.AnimalsInHotel)
            {
                //Ensures a 33% random chance
                if(rand.Next(1, 4) == 3)
                {
                    animal.NeedsWalk = true;
                }
                animal.NeedsFood = true;
                int times = animal.AnimalSize;
                //Food depends on the animals size!
                animal.AmountOfFoodPerDay = rand.Next(1*times, 4*times);
                animal.NeedsWater = true;
            }
        }
    }
}

/// Version: 1.2.1
/// DevPlans: [✖️/✔️]
/// =========
/// 
///Game mechanic base:
///---------------
///[✔️]Random people with their dogs 
///[✔️]You can store the dogs based on how many you can take in 
///[✔️]Upgrades to the hotel can be made so there is more place for the dogs 
///[✔️]Dogs generate by random 
///[✔️]Each dog has a unique random generated name
///[✔️]User can pass a day by a button
///[✔️]Each dog has a unique staying amount that the user has to take care of them
///[✔️]Each dog has a size. (Small-1,Medium-2,Large-4) And they take up that much slot.
///[✔️]Each slot has a prize and that's how the dogs will be priced
///
///To-do's:
///----------
///[✔️]Add more than one type of animal like cats, lama, donkey, horse
///[✔️]Every day costs money to run the hotel, and the more slots you have the more you need to pay accordingly
///[✔️]Each animal has their unique food type that they need
///[✔️]Add a new food class 
///[✔️]Add the food logic to the store 
///[✔️]Change the Dog class to animals class and add the animal type field!
///[✖️]Animals need to be fed, drank and taken to a walk
///[✔️]If a pet is not taken care for, after a day they leave with no money!
///
/// 
/// 
///For the future:
///----------------
///1)Animals want to play, toys: ball, rope, freebee, quackToy <!--Easy to implement, same as food-->
///  Toys have durability and new needs to be purchased <!--Add a new toy class and implement it like food-->
///2)Cleaning needs to be done after animals leave the hotel <!--Easy implementation, has to add a simple logic-->
///3)Animals can get hurt and needs medical help: bangade, operation, death <!--Easy to implement, same as food-->
///4)Auto checkout by hiring new worker <!--Tricky, needs to add automation to checkouts if there is a worker-->
///!✖️!)Saving, but I'm unsure since i DON'T KNOW HOW TO WRITE INTO ROOT TXT <!--Impossible rn, have to ask teacher about file handlings-->
/// 
///Classes needed:
///----------------
///1]Dogs: stores all the dogs and their infos
///2]Hotel: stores all the hotel informations
///3]Upgrades: stores all the upgrades and their price
///4]Random: does all the random generations
///5]UI: stores all the funcions that the player can do
///6]Program: runs the game
///7]FoodTypes: stores all the food fields that can be added to the Hotel's storage
///


