using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutyaPanzio
{
    class Hotel
    {
        public static string HotelName { get; set; }
        public static int HotelMoney { get; set; } = 500;
        public static int DayCount { get; set; } = 1;
        public static int StackSize { get; set; } = 4;
        public static int UsedStack { get; set; }
        public static int RemainingStack => StackSize - UsedStack;


        ///<summary>A list of animals that are waiting in line to get into the hotel</summary>
        public static List<Animals> AnimalInLine { get; set; } = new();

        ///<summary>A list of animals that are inside the hotel already</summary>
        public static List<Animals> AnimalsInHotel { get; set; } = new();

        ///<summary>A list of all the foods the hotel has in stock</summary>
        public static List<FoodTypes> FoodTypes { get; set; } = new();

        ///<!--Hotel Functions-->
        ///<summary>Checks if there are 5 dogs in line and if not than it adds more</summary>
        public static void CheckDogsList()
        {
            if (AnimalInLine.Count < 9)
            {
                while (AnimalInLine.Count < 9)
                {
                    AnimalInLine.Add(Randoms.GenerateAnimal());
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
            Console.WriteLine($"Hotel's daily fee: {StackSize * 10}");
            if(FoodTypes.Count > 0)
            {
                string headerFormat = "{0,-12} {1,-10}";

                Console.WriteLine("\n" + string.Format(headerFormat, "Food", "Stock"));
                Console.WriteLine(new string('-', 15)); // Decorative separator line

                foreach (FoodTypes food in FoodTypes)
                { 
                    Console.WriteLine($"{food.FoodType,-12} {food.FoodQuantity,-10} ");
                }
            }

            if (AnimalsInHotel.Count > 0)
            {
                string headerFormat = "{0,-12} {1,-10} {2,-10} {3,-10}";

                Console.WriteLine("\n" + string.Format(headerFormat, "Name", "Size", "Fee", "Days"));
                Console.WriteLine(new string('-', 45)); // Decorative separator line

                foreach (Animals dog in AnimalsInHotel)
                {
                    // {animal.Name,-12} means left-align with a width of 12 characters
                    Console.WriteLine($"{dog.Name,-12} {dog.AnimalSize,-10} ${dog.MoneyForAnimal,-9} {dog.AmountOfDaysLeft,-10}");
                }
            }
            Console.WriteLine("======================");
        }

        ///<summary>Shows all the available dogs for check in</summary>
        public static void CheckForAnimal()
        {
            CheckDogsList();

            // Define a format string to keep headers and data aligned
            string headerFormat = "{0,-12} {1,-10} {2,-10} {3,-10} {4,-10} {5,-10}";

            Console.WriteLine("\n" + string.Format(headerFormat, "Name", "Type", "Size", "Fee", "Days", "Food"));
            Console.WriteLine(new string('-', 65)); // Decorative separator line

            int stepper = 1;
            foreach (Animals animal in AnimalInLine)
            {
                // {animal.Name,-12} means left-align with a width of 12 characters
                Console.WriteLine($"{stepper++}){animal.Name,-10} {animal.AnimalType, -10} {animal.AnimalSize,-10} ${animal.MoneyForAnimal,-9} {animal.AmountOfDaysLeft,-10} {animal.NeededFoodType}");
            }
        }

        /// <summary>Checks if the dog exists and than adds it into the list.</summary>
        public static void TakeAnimalIn(Animals dog)
        {
            for (int i = AnimalInLine.Count - 1; i >= 0; i--)
            {
                if (AnimalInLine[i] == dog)
                {
                    if (AnimalInLine[i].AnimalSize <= RemainingStack)
                    {
                        UsedStack += AnimalInLine[i].AnimalSize;
                        AnimalsInHotel.Add(AnimalInLine[i]);
                        AnimalInLine.RemoveAt(i);
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
        public static void TakeAnimalOut()
        {
            // We loop backwards (Count - 1 down to 0) 
            // because removing an item from a list shifts all subsequent items.
            // Looping forwards would cause you to skip dogs.
            for (int i = AnimalsInHotel.Count - 1; i >= 0; i--)
            {
                if (AnimalsInHotel[i].AmountOfDaysLeft <= 0)
                {
                    Console.WriteLine($" >> {AnimalsInHotel[i].Name} has been picked up!");

                    HotelMoney += AnimalsInHotel[i].MoneyForAnimal;
                    UsedStack -= AnimalsInHotel[i].AnimalSize;
                    AnimalsInHotel.RemoveAt(i);
                }
            }
        }
    }
}
