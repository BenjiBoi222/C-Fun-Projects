using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutyaPanzio
{
    class StoreFunctions
    {
        ///<!--Store menu functions-->
        public static void OpenShop()
        {
            string[] shopMenuOptions =
            {
                "Buy more slots",
                "Buy more food",
                "Hire staff",
                "Back to menu"
            };
            bool isStoreActive = true;
            while (isStoreActive)
            {

                int choice = ShortFunctions.ShowMenu("Shop", shopMenuOptions, "-");
                {
                    switch (choice)
                    {
                        case 0: BuyMoreStack(); break;
                        case 1: BuyMoreFood(); break;
                        case 2: HireStaff(); break;
                        case 3: Console.Clear(); isStoreActive = false; break;
                    }
                }
            }
        }
        ///<!--Private functions to handle the stack shop-->
        /// <summary>Shows the buyable stack options and handles the users input</summary>
        private static void BuyMoreStack()
        {
            int stackPrice = 100 + Hotel.StackSize * 10;
            Console.WriteLine("\n==Stack options==");
            Console.WriteLine("1)Buy 1 more stack");
            Console.WriteLine("2)Buy 2 more stack");
            Console.WriteLine("3)Buy 4 more stack");
            Console.WriteLine("4)Buy 8 more stack");
            Console.Write($"Enter the pack you want({stackPrice}$/stack): ");
            if (int.TryParse(Console.ReadLine(), out int pack))
            {
                switch (pack)
                {
                    case 1: BuyStack(1, stackPrice); break;
                    case 2: BuyStack(2, stackPrice); break;
                    case 3: BuyStack(4, stackPrice); break;
                    case 4: BuyStack(8, stackPrice); break;
                    default: Console.WriteLine("Invalid input!"); break;
                }
            }
        }
        /// <summary>Handles the stack buying process</summary>
        /// <param name="pack">The pack's size the user wants</param
        /// <param name="stackPice">A price of one stack</param>
        private static void BuyStack(int pack, int stackPrice)
        {
            int moneyNeeded = pack * stackPrice;

            if (Hotel.HotelMoney >= moneyNeeded)
            {
                Hotel.HotelMoney -= moneyNeeded;
                Hotel.StackSize += pack;
                Hotel.HotelFeePerDay += pack * 10;
                Console.WriteLine($"{pack} amount of slot was bought for {moneyNeeded}");
            }
            else Console.WriteLine("Insuficent funds");
        }

        ///<!--Private functions to handle the food shop-->

        ///<summary>This function lists all the food options, stores their value and adds it into the Hotels Foods storage if purchase was successful</summary>
        private static void BuyMoreFood()
        {
            var prices = new Dictionary<string, int>
            {
                { "fish", 10 }, { "chicken", 14 }, { "beef", 25 }, { "carrot", 5 }, { "apple", 3 }
            };

            Console.WriteLine("\n==Food options==");
            Console.WriteLine($"1)Fish({prices["fish"]}/piece)");
            Console.WriteLine($"2)Chicken({prices["chicken"]}/piece)");
            Console.WriteLine($"3)Beef({prices["beef"]}/piece)");
            Console.WriteLine($"4)Carrot({prices["carrot"]}/piece)");
            Console.WriteLine($"5)Apple({prices["apple"]}/piece)");
            Console.Write("Enter your choice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.Write("Enter the amount you want to buy: ");
                if (int.TryParse(Console.ReadLine(), out int amount))
                {
                    FoodTypes result = null;
                    switch (choice)
                    {
                        case 1: result = HadleFoodTypes(amount, prices["fish"], "fish"); break;
                        case 2: result = HadleFoodTypes(amount, prices["chicken"], "chicken"); break;
                        case 3: result = HadleFoodTypes(amount, prices["beef"], "beef"); break;
                        case 4: result = HadleFoodTypes(amount, prices["carrot"], "carrot"); break;
                        case 5: result = HadleFoodTypes(amount, prices["apple"], "apple"); break;
                        default: Console.WriteLine("Invalid input!"); break;
                    }

                    if (result != null)
                    {
                        foreach (FoodTypes food in Hotel.FoodTypes)
                        {
                            if (food.FoodType == result.FoodType)
                            {
                                food.FoodQuantity += result.FoodQuantity;
                                Console.WriteLine("Food successfully bought");
                                return;
                            }
                        }
                        Hotel.FoodTypes.Add(result);
                        Console.WriteLine("Food successfully bought");
                    }
                }
            }
        }
        /// <summary>Calculates the payable amount and handles the purchase/if unsuccessful than creates a null food element</summary>
        /// <param name="amount">The quantity of the food the user intends to but</param>
        /// <param name="price">The unique price of a SINGULAR quantity</param>
        /// <param name="foodType">The foods type/name</param>
        /// <returns>A FoodTypes object or null</returns>
        private static FoodTypes HadleFoodTypes(int amount, int price, string foodType)
        {
            int foodMoney = amount * price;

            if (foodMoney <= Hotel.HotelMoney)
            {
                Hotel.HotelMoney -= foodMoney;
                return new FoodTypes
                {
                    FoodType = foodType,
                    FoodQuantity = amount
                };
            }

            Console.WriteLine("Insufficient funds!");
            return null;
        }

        ///<!--Private function to handle the staff hiring process-->
        ///<summary>Hires a staff member if the user decides he wants to hire one and if the user has enough money</summary>
        private static void HireStaff()
        {
            Console.WriteLine("==Staff options==");
            Console.WriteLine("Hiring stuff will cost 50$ and than 10$ each day!");
            Console.Write("Proceed with hiring? (y/n): ");
            string choice = Console.ReadLine();
            if (choice != null && choice.ToLower() == "y")
            {
                if (Hotel.HotelMoney >= 50)
                {
                    Hotel.HotelMoney -= 50;
                    Hotel.StaffCount++;
                    Hotel.HotelFeePerDay += 10;
                }
            }

        }


    }
}
