using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace FileSorter_1._1
{
    class Server
    {
        public static List<ServerDevicesObjects> ServerDevices = new();
        public static List<ServerUsersObjects> ServerUsers = new();
        public static readonly string ServerDevicesFile = "devices.json";
        public static readonly string ServerUsersFile = "users.json";



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




        ///<!--The server's file handler functions-->

        ///<summary>Loads the existing server jsons into usable list's</summary>
        public static void LoadHistory()
        {
            if (File.Exists(ServerDevicesFile))
            {
                try
                {
                    string jsonString = File.ReadAllText(ServerDevicesFile);
                    ServerDevices = JsonSerializer.Deserialize<List<ServerDevicesObjects>>(jsonString) ?? new();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[System] Error loading history: {ex.Message}");
                    ServerDevices = new List<ServerDevicesObjects>();
                }
            }

            if (File.Exists(ServerUsersFile))
            {
                try
                {
                    string jsonIgnore = File.ReadAllText(ServerUsersFile);
                    ServerUsers = JsonSerializer.Deserialize<List<ServerUsersObjects>>(jsonIgnore) ?? new();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[System] Error loading history: {ex.Message}");
                    ServerUsers = new List<ServerUsersObjects>();
                }
            }
        }

        ///<summary>Saves the server data's to the created/existing Json file</summary>
        public static void SaveHistory()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonServerDevices = JsonSerializer.Serialize(ServerDevices, options);
                string jsonServerUsers = JsonSerializer.Serialize(ServerUsers, options);
                File.WriteAllText(ServerDevicesFile, jsonServerDevices);
                File.WriteAllText(ServerUsersFile, jsonServerUsers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving history: {ex.Message}");
            }
        }

        ///<!--The server functions-->


    }
}
