using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSorter_1._1
{
    class Settings
    {
        ///<summary>Shows the menu and lets the user choose from the options</summary>
        public static void ShowMenu()
        {
            
            string[] menuOptions =
            {
                "[File]Files to ignore",
                "[File]Delete ignore preferences",
                "[File]Delete files history",
                "[Server]Add IP address",
                "[Server]Change IP address",
                "[Program]Main menu"
            };
            while (true)
            {
                Program.ShowMenuHelper("Settings", menuOptions, out int option, ">");
                Console.Clear();
                Console.WriteLine("\n===Settings===");
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == option)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> {menuOptions[i]}");
                        Console.ResetColor();
                    }
                    else Console.WriteLine($"  {menuOptions[i]}");
                }
                Sleep(400);

                GetInfo(out string ignore);
                switch (option)
                {
                    case 0: ExtensionToIgnore(); break;
                    case 1: DeleteExtensionPreferences(ignore); break;
                    case 2: DeleteFileHistory();    break;
                    case 3: AddIpAddress(); break;
                    case 4: ChangeDeviceSettings(); break;
                    case 5: return;
                }
            }
        }

        ///<!--Files menu settings-->
        
        private static void DeleteFileHistory()
        {
            int amount = FileSorter.FilesList.Count; 
            FileSorter.FilesList.Clear();
            File.Delete(FileSorter.HistoryFileName);
            Console.WriteLine($"{amount} pcs of history were deleted");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void DeleteExtensionPreferences(string ignore)
        {
            
            while (true)
            {
                List<string> optionsList = new List<string> {"Main Menu" , "Delete all reference (WARNING: CANNOT BE UNDONE)" };
                int baseOptions = optionsList.Count;
                foreach (var ext in FileSorter.ExtensionToIgnore)
                {
                    optionsList.Add(ext);
                }

                string[] menuOptions = optionsList.ToArray();


                Program.ShowMenuHelper("Extensions", menuOptions, out int option, ">", 1);
                if (option == 1)
                {
                    FileSorter.ExtensionToIgnore.Clear();
                    Console.WriteLine("Deleted extension references: ");
                    Console.WriteLine(ignore);
                    Console.WriteLine("Press any key to continue...");

                    File.Delete(FileSorter.IgnoreFileName);
                }
                else if (option == 0) return;
                else
                {
                    Console.WriteLine($"Removed: {FileSorter.ExtensionToIgnore[option - baseOptions]}");
                    FileSorter.ExtensionToIgnore.RemoveAt(option - baseOptions);
                }

                Console.Clear();
                Console.WriteLine("\n===Extensions===");
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == option)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine($"> {menuOptions[i]}");
                        Console.ResetColor();
                    }
                    else Console.WriteLine($"  {menuOptions[i]}");
                }
                Sleep(400);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Lets the user choose what kind of extensions he wants to ignore inside the sortings
        /// </summary>
        private static void ExtensionToIgnore()
        {
            while (true)
            {
                Console.Clear();
                GetInfo(out string alreadyIgnored);

                Console.WriteLine("-----------");
                Console.WriteLine("Extensions already ignored: ");
                Console.WriteLine(alreadyIgnored);
                Console.WriteLine("-----------");
                Console.WriteLine("Extension format: 'cs, xlxs' ");
                Console.WriteLine("Enter the file extensions you want the program to ignore");

                Console.Write("Extension(Enter to save and exit): ");
                Console.CursorVisible = true;
                string extensionInput = Console.ReadLine();
                if (extensionInput == "")
                {
                    FileSorter.SaveHistory();
                    return;
                }

                if (extensionInput != null)
                {
                    string extension = "." + extensionInput.ToLower();
                    FileSorter.ExtensionToIgnore.Add(extension);
                }
            }
        }
        


        ///<!--Server menu sttings-->
        ///<summary>Adds an ip addres with a name to it</summary>
        private static void AddIpAddress()
        {
            while(true)
            {
                Console.Write("Enter device IP(x.x.x.x): ");
                string ipAddress = Console.ReadLine() ?? "x";

                Console.Write("Enter the device name: ");
                string ipDevice = Console.ReadLine() ?? string.Empty;

                Console.Write("Is this a server? (y/n): ");
                string ipIsServer = Console.ReadLine() ?? "n";

                if(ipAddress != "x" && ipDevice != string.Empty)
                {
                    ServerDevicesObjects devices = new();
                    devices.IpAddres = ipAddress;
                    devices.DeviceName = ipDevice;

                    if(ipIsServer.ToLower() == "y") devices.IsServer = true; 

                    Server.ServerDevices.Add(devices);
                    Server.SaveHistory();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Device saved successfully!");
                    Console.ResetColor();
                }
                Console.Write("Add more(y/n): ");
                string choice = Console.ReadLine() ?? string.Empty;

                if (choice.ToLower() == "n") return;
            }
        }


        /// <summary>
        /// Let's the user change or delete any device
        /// </summary>
        /// <summary>
        /// Lets the user change or delete any device
        /// </summary>
        private static void ChangeDeviceSettings()
        {
            while (true)
            {
                List<ServerDevicesObjects> devices = Server.ServerDevices;

                // Create menu options: List of devices + Return option
                string[] devicesOption = new string[devices.Count + 1];
                for (int i = 0; i < devices.Count; i++)
                {
                    devicesOption[i] = devices[i].DeviceName;
                }
                devicesOption[devices.Count] = "Back to Settings";

                Program.ShowMenuHelper("Select Device to Edit", devicesOption, out int selectedIndex, ">");

                // If user selects "Back to Settings"
                if (selectedIndex == devices.Count) return;

                // Enter the sub-menu for the specific device
                EditSpecificDevice(devices[selectedIndex]);
            }
        }

        private static void EditSpecificDevice(ServerDevicesObjects device)
        {
            string[] editOptions = { "Change Name", "Change IP", "Toggle Server Status", "Delete Device", "Back" };

            while (true)
            {
                Program.ShowMenuHelper($"Editing: {device.DeviceName}", editOptions, out int choice, ">");

                switch (choice)
                {
                    case 0: // Change Name
                        Console.Write($"Current name: {device.DeviceName}. Enter new name: ");
                        string newName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newName)) device.DeviceName = newName;
                        break;

                    case 1: // Change IP
                        Console.Write($"Current IP: {device.IpAddres}. Enter new IP: ");
                        string newIp = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newIp)) device.IpAddres = newIp;
                        break;

                    case 2: // Toggle Server
                        device.IsServer = !device.IsServer;
                        Console.WriteLine($"Server status is now: {device.IsServer}");
                        Sleep(1000);
                        break;

                    case 3: // Delete
                        Console.Write("Are you sure you want to delete this device? (y/n): ");
                        if (Console.ReadLine()?.ToLower() == "y")
                        {
                            Server.ServerDevices.Remove(device);
                            Server.SaveHistory();
                            return; // Exit back to device list
                        }
                        break;

                    case 4: // Back
                        Server.SaveHistory(); // Save changes before leaving
                        return;
                }
                Server.SaveHistory();
            }
        }



        ///<!--Helper functions for test and UI-->

        public static void MenuSelectUI(string menuName,string[] menuOptions, string indicator, int selected)
        {
            Console.Clear();
            Console.WriteLine($"\n==={menuName}===");
            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (selected == i)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine($"> {menuOptions[i]}");
                    Console.ResetColor();
                }
                else Console.WriteLine($"  {menuOptions[i]}");
            }
            Sleep(800);
        }

        /// <summary>
        /// A function that returns various informations 
        /// </summary>
        /// <param name="alreadyIgnored">A string of extensions that are already inside the ignore list</param>
        private static void GetInfo(out string alreadyIgnored)
        {
            alreadyIgnored = "";
            foreach (string extension in FileSorter.ExtensionToIgnore) alreadyIgnored += extension + ";";
        }


        /// <summary>
        /// Shows the system logs in every program startup
        /// </summary>
        public static void ShowSystemLogs()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[System][File] Loaded {FileSorter.FilesList.Count} previous moves from history.");
            Console.WriteLine($"[System][File] Loaded {FileSorter.ExtensionToIgnore.Count} previous reference.");
            Console.WriteLine($"[System][Server] Loaded {Server.ServerDevices.Count} server devices.");
            Console.ResetColor();
            Sleep(1500);
        }

        /// <summary>
        /// Shortens the System.Threading.Thread.Sleep to just Sleep
        /// </summary>
        /// <param name="amountInMiliSec">The amount of miliseconds the program sleeps before moving on</param>
        public static void Sleep(int amountInMiliSec) 
        {
            System.Threading.Thread.Sleep(amountInMiliSec);
        }
    }
}
