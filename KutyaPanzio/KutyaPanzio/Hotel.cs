using System;
using System.Collections.Generic;
using System.Linq;

namespace KutyaPanzio
{
    class Hotel
    {
        public static string HotelName { get; set; }
        public static int HotelMoney { get; set; } = 500;
        public static int HotelFeePerDay { get; set; } = 40;
        public static int DayCount { get; set; } = 1;
        public static int StackSize { get; set; } = 4;
        public static int UsedStack { get; set; }
        public static int RemainingStack => StackSize - (UsedStack + MessyStackAmount);
        public static int StaffCount { get; set; }
        public static int MessyStackAmount { get; set; }
        public static int DaysInDebt { get; set; }
        public static int RemainingInterestDay { get; set; }
        public static bool AlreadyTookLoan { get; set; }
        public static int OverAllAnimalCount { get; set; }

        public static List<Animals> AnimalInLine { get; set; } = new();
        public static List<Animals> AnimalsInHotel { get; set; } = new();
        public static List<FoodTypes> FoodTypes { get; set; } = new();

        /// <summary>Checks if there are 9 animals in line and if not, adds more</summary>
        public static void CheckDogsList()
        {
            while (AnimalInLine.Count < 9)
            {
                AnimalInLine.Add(Randoms.GenerateAnimal());
            }
        }

        /// <summary>Writes out all the information about the hotel</summary>
        public static void HotelInfo()
        {
            Console.WriteLine("\n======================");
            Console.WriteLine($"Hotel name: {HotelName}");
            Console.WriteLine($"Hotel money: ${HotelMoney}");
            Console.WriteLine($"Hotel staff: {StaffCount}");
            Console.WriteLine($"Current day: {DayCount}.");
            Console.WriteLine($"Hotel capacity: {StackSize}");
            Console.WriteLine($"Hotel capacity left: {RemainingStack}");
            Console.WriteLine($"Hotel dirty stack: {MessyStackAmount}");
            Console.WriteLine($"Hotel's daily fee: {HotelFeePerDay}");

            if (FoodTypes.Count > 0)
            {
                string headerFormat = "{0,-12} {1,-10}";
                Console.WriteLine("\n" + string.Format(headerFormat, "Food", "Stock"));
                Console.WriteLine(new string('-', 15));

                foreach (FoodTypes food in FoodTypes)
                {
                    Console.WriteLine($"{food.FoodType,-12} {food.FoodQuantity,-10}");
                }
            }

            if (AnimalsInHotel.Count > 0)
            {
                string headerFormat = "{0,-12} {1,-10} {2,-10} {3,-10} {4,-10} {5,-10}";
                Console.WriteLine("\n" + string.Format(headerFormat, "Name", "Type", "Size", "Fee", "Days", "Food"));
                Console.WriteLine(new string('-', 45));

                int stepper = 1;
                foreach (Animals animal in AnimalsInHotel)
                {
                    Console.WriteLine($"{stepper++}){animal.Name,-10} {animal.AnimalType,-10} {animal.AnimalSize,-10} ${animal.MoneyForAnimal,-9} {animal.AmountOfDaysLeft,-10} {animal.NeededFoodType}");
                }
            }

            Console.WriteLine("======================");
        }

        /// <summary>Shows all the available animals for check in</summary>
        public static void CheckForAnimal()
        {
            CheckDogsList();

            string headerFormat = "{0,-12} {1,-10} {2,-10} {3,-10} {4,-10} {5,-10}";
            Console.WriteLine("\n" + string.Format(headerFormat, "Name", "Type", "Size", "Fee", "Days", "Food"));
            Console.WriteLine(new string('-', 65));

            int stepper = 1;
            foreach (Animals animal in AnimalInLine)
            {
                Console.WriteLine($"{stepper++}){animal.Name,-10} {animal.AnimalType,-10} {animal.AnimalSize,-10} ${animal.MoneyForAnimal,-9} {animal.AmountOfDaysLeft,-10} {animal.NeededFoodType}");
            }
        }

        /// <summary>Checks if the animal exists and adds it to the hotel</summary>
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
            Console.WriteLine("The animal does not exist!");
        }

        /// <summary>Removes animals that are ready to leave</summary>
        public static void TakeAnimalOut()
        {
            for (int i = AnimalsInHotel.Count - 1; i >= 0; i--)
            {
                if (AnimalsInHotel[i].AmountOfDaysLeft <= 0)
                {
                    Console.WriteLine($" >> {AnimalsInHotel[i].Name} has been picked up!");

                    HotelMoney += AnimalsInHotel[i].MoneyForAnimal;
                    MessyStackAmount += AnimalsInHotel[i].AnimalSize;
                    UsedStack -= AnimalsInHotel[i].AnimalSize;
                    AnimalsInHotel.RemoveAt(i);
                }
            }
        }
    }
}