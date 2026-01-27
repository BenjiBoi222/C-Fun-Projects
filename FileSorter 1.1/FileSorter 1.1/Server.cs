using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using Renci.SshNet;

namespace FileSorter_1._1
{
    class Server
    {
        public static List<ServerDevicesObjects> ServerDevices = new();
        public static readonly string ServerDevicesFile = "devices.json";


        ///<summary>Shows the menu and lets the user choose from the options</summary>
        public static void ShowMenu()
        {
            const string menuName = "Server Menu";
            while (true)
            {
                Console.Clear();
                string[] menuOptions = { "Connection Status", "Main Menu"};

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
                    case 0: CheckConnection(); break;
                    case 1: return;
                }

                Console.WriteLine("\nDone! Press any key to return to menu...");
                Console.ReadKey(true);
            }
        }


        ///<!--The server functions-->
        ///<summary>Sends out pings and if it gets a return it shows the device currently on the server</summary>
        static void CheckConnection()
        {
            Console.Clear();
            Console.WriteLine("=== Connection Status ===");

            using (Ping pingSender = new())
            {
                foreach (var device in ServerDevices)
                {

                    if (device.IsServer) { Console.ForegroundColor = ConsoleColor.DarkBlue; Console.Write("[Server] "); }
                    else { Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("[Device] "); }
                    Console.ResetColor();

                    Console.Write($"Pinging {device.DeviceName} ");

                    // Animáció indítása egy külön szálon, hogy ne blokkolja a Pinget
                    bool isDone = false;
                    var loaderTask = Task.Run(() => {
                        string[] spinner = { "⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏" };
                        int counter = 0;
                        while (!isDone)
                        {
                            Console.Write(spinner[counter % 10]);
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            counter++;
                            Thread.Sleep(100);
                        }
                    });

                    try
                    {
                        PingReply reply = pingSender.Send(device.IpAddres, 2000);
                        isDone = true; // Megállítjuk az animációt
                        loaderTask.Wait(); // Megvárjuk, amíg a szál befejezi az utolsó kört

                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);


                        if (reply.Status == IPStatus.Success)
                        {
                            
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"[Online] {reply.RoundtripTime}ms");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("[Offline]");
                        }
                    }
                    catch (Exception ex)
                    {
                        isDone = true;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"\n[Error] {ex.Message}");
                    }
                    finally
                    {
                        Console.ResetColor();
                    }
                }
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

            
        }

        ///<summary>Saves the server data's to the created/existing Json file</summary>
        public static void SaveHistory()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonServerDevices = JsonSerializer.Serialize(ServerDevices, options);
                File.WriteAllText(ServerDevicesFile, jsonServerDevices);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving history: {ex.Message}");
            }
        }



    }
}
