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
        private static void ChangeDeviceSettings()
        {
            while (true)
            {
                List<ServerDevicesObjects> devices = Server.ServerDevices;
                int baseOptions = 1;//Those options that are not devices but dev given
                string[] devicesOption = new string[devices.Count];

                int lastItem = 0;
                for (int i = 0; i < devices.Count - baseOptions; i++)
                {
                    devicesOption[i] = devices[i].DeviceName;
                    lastItem++;
                }
                devicesOption[lastItem] = "Main menu";

                Program.ShowMenuHelper("Devices", devicesOption, out int option, ">");
                MenuSelectUI("Devices", devicesOption, ">", option);
                
                if (option == lastItem) return;
                for (int i = 0; i < devices.Count - 1; i++)
                {
                    if (i == option)
                    {
                        while(true)
                        {
                            string[] deviceChangeOptions = { "Change name", "Change IP", "Change server status", "Back" };
                            Program.ShowMenuHelper(devices[i].DeviceName, deviceChangeOptions, out int deviceChangeOption, ">");

                            MenuSelectUI(devices[i].DeviceName, deviceChangeOptions, ">", deviceChangeOption);

                            if (deviceChangeOption == 3) return;

                            switch (deviceChangeOption)
                            {
                                case 0: 
                                    ; 
                                    break;
                                case 1: /*Change Ip*/; break;
                                case 2: /*Change Server*/; break;
                            }
                        }
                    }

                }
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
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"[System][File] Loaded {FileSorter.FilesList.Count} previous moves from history.");
            Console.WriteLine($"[System][File] Loaded {FileSorter.ExtensionToIgnore.Count} previous reference.");
            Console.WriteLine($"[System][Server] Loaded {Server.ServerDevices.Count} server devices.");
            Console.ResetColor();
            Sleep(3000);
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
