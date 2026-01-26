using System;
using System.Collections.Generic;
using System.Text;

namespace FileSorter_1._1
{
    class Server
    {
        public static List<ServerDevicesObjects> ServerDevices = new();
        public static List<ServerUsersObjects> ServerUsers = new();




        ///<summary>Shows the menu and lets the user choose from the options</summary>
        public static void ShowMenu()
        {
            const string menuName = "Server Menu";
            while (true)
            {
                Console.Clear();
                string[] menuOptions = { "Connection Status", "Storage Monitor", "Live Metrics", "Main Menu"};

                Program.ShowMenuHelper(menuName, menuOptions, out int option, ">");

                Console.Clear();
                Console.WriteLine($"\n==={menuName}===");
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == option)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"> {menuOptions[i]}");
                        Console.ResetColor();
                    }
                    
                    else Console.WriteLine($"  {menuOptions[i]}");
                }

                //Adds 200 miliseconds waiting time
                Settings.Sleep(400);

                switch (option)
                {
                    
                    case 3: return;
                }

                Console.WriteLine("\nDone! Press any key to return to menu...");
                Console.ReadKey(true);
            }
        }
    }
}
