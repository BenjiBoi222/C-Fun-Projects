using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutyaPanzio
{
    class ShortFunctions
    {

        /// <summary>
        /// Simplifies the menu with style
        /// </summary>
        /// <param name="name">The name of the menu</param>
        /// <param name="menuOptions">The menu which the user can choose the options from</param>
        /// <param name="indicator">The symbol infront of the hovered option</param>
        /// <returns></returns>
        public static int ShowMenu(string name, string[] menuOptions, string indicator)
        {
            //This list should also do the selection background
            //Gonna need:
            //List, Name, Indicator
            int lastItem = -1;
            int choosenIndex = 0;
            bool isMenuShown = true;
            while (isMenuShown)
            {
                if(clearConsole) Console.Clear();
                Console.WriteLine($"=========-{name} Hotel-=========");
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if(i == choosenIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"{indicator} {menuOptions[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {menuOptions[i]}");
                    }
                    lastItem = i;
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                
                if(keyInfo.Key == ConsoleKey.UpArrow) choosenIndex--;
                if(keyInfo.Key == ConsoleKey.DownArrow) choosenIndex++;

                if (choosenIndex == -1) choosenIndex = lastItem;
                if (choosenIndex == lastItem + 1) choosenIndex = 0;

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Console.WriteLine($"=========-{name} Hotel-=========");
                    for (int i = 0; i < menuOptions.Length; i++)
                    {
                        if (i == choosenIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine($"{indicator} {menuOptions[i]}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($"  {menuOptions[i]}");
                        }
                        lastItem = i;
                    }
                    Sleep(1);
                    return choosenIndex;
                }
            }




            return choosenIndex;
        }


        /// <summary>
        /// Forces the system to stop for a given amount of time
        /// </summary>
        /// <param name="sleepTime">The time preiod to stop in seconds</param>
        public static void Sleep(int sleepTime) 
        {
            sleepTime = sleepTime * 1000;
            System.Threading.Thread.Sleep(sleepTime);
        }


    }
}
