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
        public static int HotelMoney { get; set; }
        public static int DayCount { get; set; } = 1;
        public static int StackSize { get; set; } = 4;
        public static int UsedStack { get; set; }
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
            if (DogsInHotel.Count > 0)
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

            Console.WriteLine("\n" + string.Format(headerFormat, "Name", "Size", "Fee", "Days"));
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
}
