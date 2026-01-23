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
                "Files to ignore",
                "Delete ignore preferences",
                "Delete files history",
                "Main menu"
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
                    case 1:
                        DeleteExtensionPreferences(ignore);
                        break;
                    case 2:
                        DeleteFileHistory();
                        break;
                    case 3: return;
                }
            }
        }

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
        /// Shortens the System.Threading.Thread.Sleep to just Sleep
        /// </summary>
        /// <param name="amountInMiliSec">The amount of miliseconds the program sleeps before moving on</param>
        public static void Sleep(int amountInMiliSec) 
        {
            System.Threading.Thread.Sleep(amountInMiliSec);
        }
    }
}
