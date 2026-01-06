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
                case 1: animal.DogSize = 1; animal.MoneyForDog = rand.Next(100, 251); break;
                case 2: animal.DogSize = 2; animal.MoneyForDog = rand.Next(251, 501); break;
                case 3: animal.DogSize = 4; animal.MoneyForDog = rand.Next(501, 1001); break;
            }
            return animal;
        }
    }
}

/// DevPlans: [✖️/✔️]
/// =========
/// 
///Game mechanic:
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
///[✖️]Every day costs money to run the hotel, and the more slots you have the more you need to pay accordingly
///[✖️]Each animal has their unique food type that they need
///[✖️]Add a new food class and add the logic to the store 
///[✖️]Change the Dog class to animals class and add the animal type field!
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
///[✖️]Dogs need to be fed, drank and taken to a walk
///[✖️]If a pet is not taken care for, after a day they leave with no money!


