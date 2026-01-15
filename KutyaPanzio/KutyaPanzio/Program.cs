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
                SystemFunctions.RegisterMenu();
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
                    Hotel.MessyStackAmount += Hotel.AnimalsInHotel[i].AnimalSize;
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


        ///<!--The warning function-->
        ///<!--This function is large, mainly because it will have every element that needs to be done the current day-->

        public static void Reminders()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            //First reminder: How many animals are ready to leave
            int readyToLeave = AnimalLeavingReminder();
            if (readyToLeave > 0)
                Console.WriteLine($"{readyToLeave} animal(s) ready to leave! Proceed with checkout!");

            //Second reminder: How many animals needs to be fed
            int readyToEat = AnimalFoodReminder();
            if (readyToEat > 0)
                Console.WriteLine($"{readyToEat} animal(s) ready to eat! Proceed with feeding!");

            //Third reminder: How many animals needs to be given water
            int readyToDrink = AnimalWaterReminder();
            if (readyToDrink > 0)
                Console.WriteLine($"{readyToDrink} animal(s) ready to drink! Proceed with watering!");

            //Fourth reminder: How many animals need to go on a walk
            int readyToWalk = AnimalWalkReminder();
            if (readyToWalk > 0)
                Console.WriteLine($"{readyToWalk} animal(s) ready to go on a walk! Proceed with walkings!");

            //Fifth reminder: Is there any mess to clean inside the hotel
            if (Hotel.MessyStackAmount > 0)
                Console.WriteLine("There is mess that needs to be cleaned up!");


            Console.ResetColor();
        }
        /// <summary>Helper function. Shows how many animals are ready leaving</summary>
        /// <returns>Amount of animal thats ready to leave</returns>
        private static int AnimalLeavingReminder()
        {
            int animalsLeaving = 0;
            foreach (Animals animal in Hotel.AnimalsInHotel)
            {
                if (animal.AmountOfDaysLeft == 0)
                {
                    animalsLeaving++;
                }
            }
            return animalsLeaving;
        }
        /// <summary>Helper function. Shows how many animals are ready to eat</summary>
        /// <returns>Amount of animal thats ready to eat</returns>
        private static int AnimalFoodReminder()
        {
            int animalsLeftHungry = 0;
            foreach (Animals animal in Hotel.AnimalsInHotel)
            {
                if (animal.NeedsFood)
                {
                    animalsLeftHungry++;
                }
            }
            return animalsLeftHungry;
        }
        /// <summary>Helper function. Shows how many animals are ready to drink</summary>
        /// <returns>Amount of animal thats ready to drink</returns>
        private static int AnimalWaterReminder()
        {
            int animalsLeftThirsty = 0;
            foreach (Animals animal in Hotel.AnimalsInHotel)
            {
                if (animal.NeedsWater)
                {
                    animalsLeftThirsty++;
                }
            }
            return animalsLeftThirsty;
        }
        /// <summary>Helper function. Shows how many animals are ready to walk</summary>
        /// <returns>Amount of animal thats ready to walk</returns>
        private static int AnimalWalkReminder()
        {
            int animalsForWalk = 0;
            foreach (Animals animal in Hotel.AnimalsInHotel)
            {
                if (animal.NeedsWalk)
                {
                    animalsForWalk++;
                }
            }
            return animalsForWalk;
        }


    }
}

/// Version: 1.4.0
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
///Urgents:
///--------
///Before any more functionality, start using lambda for a cleaner code
/// 
///To-do's:
///----------
///[✔️]Add more than one type of animal like cats, lama, donkey, horse
///[✔️]Every day costs money to run the hotel, and the more slots you have the more you need to pay accordingly
///[✔️]Each animal has their unique food type that they need
///[✔️]Add a new food class 
///[✔️]Add the food logic to the store 
///[✔️]Change the Dog class to animals class and add the animal type field!
///[✔️]Function to show what each pet need every day
///[✔️]Animals need to be fed, drank and taken to a walk
///[✔️]If a pet is not taken care for, after a day they leave with no money!
///[✔️]When an animal leaves the hotel they leave their occupied stack dirty
///[✔️]Cleaning needs to be done after animals leave the hotel 
///[✔️]Cleaning reminder
///[✔️]Auto checkout by hiring new worker
///[✔️]Add a functionality that if the user's money is in "-" for 7 days than the hotel closes and the user fails
/// 
///Always expanding with new functions:
///-------------------------------------
///[ Currently[✔️] Overall:[✖️] ]Make a warning function that plays at each main menu display, that writes out all the things the user must do that day
///
/// 
/// 
///For the future:
///----------------
///1)Animals want to play, toys: ball, rope, freebee, quackToy <!--Easy to implement, same as food-->
///  Toys have durability and new needs to be purchased <!--Add a new toy class and implement it like food-->
///2)Add a banking system so users can take out loans if their money is going low
///3)Animals can get hurt and needs medical help: bangade, operation, death <!--Easy to implement, same as food-->
///5)Add an end screen that shows the amount of money, days statistic after the user is done with the game
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
///8]StoreFunctions: stores all the functions that handles the store element
///9]SystemFunctions: stores all the functions that run without users input for the game's system
///



