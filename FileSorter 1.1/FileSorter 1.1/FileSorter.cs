using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace FileSorter_1._1
{
    class FileSorter
    {
        static List<FileMoveHistory> FilesList = new();
        const string HistoryFileName = "history.json";


        ///<summary>Shows the fluid menu and lets the user choose from the options</summary>
        public static void ShowMenu()
        {
            int option = 0;
            while (true)
            {
                Console.Clear();
                string[] menuOptions = { "Sort files", "UnSort files", "Delete empty folders", "Main menu" };
                Console.WriteLine("\n===Files Menu===");

                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == option)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"> {menuOptions[i]}");
                        Console.ResetColor();
                    }
                    else Console.WriteLine($"  {menuOptions[i]}");
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.DownArrow) option++;
                else if (keyInfo.Key == ConsoleKey.UpArrow) option--;

                if (option < 0) option = menuOptions.Length - 1;
                if (option >= menuOptions.Length) option = 0;

                //If enter is pressed
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Console.WriteLine("\n===Files Menu===");
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
                    System.Threading.Thread.Sleep(400);
                    switch (option)
                    {
                        case 0: FileSorter.SortFilesIntoSubFolders(); break;
                        case 1: FileSorter.UnDoSortFileIntoSubFolders(); break;
                        case 2: FileSorter.DeleteEmptyFolders(); break;
                        case 3: return ;
                    }
                    Console.WriteLine("\nDone! Press any key to return to menu...");
                    Console.ReadKey(true);
                }
            }
        }




        ///<summary>Loads the informations from the Json file if it exists and if not than creates it</summary>
        public static void LoadHistory()
        {
            if (!File.Exists(HistoryFileName)) return;
            try
            {
                string jsonString = File.ReadAllText(HistoryFileName);
                FilesList = JsonSerializer.Deserialize<List<FileMoveHistory>>(jsonString) ?? new();
                Console.WriteLine($"[System] Loaded {FilesList.Count} previous moves from history.");
                System.Threading.Thread.Sleep(800);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[System] Error loading history: {ex.Message}");
                FilesList = new List<FileMoveHistory>();
            }
        }
        ///<summary>Saves the statistics to the created/existing Json file</summary>
        private static void SaveHistory()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(FilesList, options);
                File.WriteAllText(HistoryFileName, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving history: {ex.Message}");
            }
        }


        ///<summary>Sorts the files inside a main folder into categorised sub-folders</summary>
        public static void SortFilesIntoSubFolders()
        {
            var extensionMap = new Dictionary<string, string>
            {
                // --- Microsoft Office ---
                { ".docx", "Word Documents" },
                { ".doc", "Word Documents" },
                { ".xlsx", "Excel Spreadsheets" },
                { ".xls", "Excel Spreadsheets" },
                { ".pptx", "PowerPoint Presentations" },
                { ".ppt", "PowerPoint Presentations" },

                // --- Images & Graphics ---
                { ".jpg", "Photos (JPG)" },
                { ".jpeg", "Photos (JPG)" },
                { ".png", "Graphics (PNG)" },
                { ".gif", "Animations (GIF)" },
                { ".webp", "Web Images" },
                { ".svg", "Vectors (SVG)" },
                { ".ico", "Icons" },
                { ".bmp", "Bitmaps" },
                { ".tiff", "High-Res Images" },

                // --- Documents & Text ---
                { ".pdf", "PDF Documents" },
                { ".txt", "Plain Text Files" },
                { ".rtf", "Rich Text" },
                { ".odt", "OpenOffice Docs" },
                { ".ods", "OpenOffice Sheets" },
                { ".odp", "OpenOffice Slides" },
                { ".csv", "CSV Data" },
                { ".xps", "XPS Documents" },

                // --- Archives & Compressed ---
                { ".zip", "Zip Archives" },
                { ".rar", "Rar Archives" },
                { ".7z", "7-Zip Archives" },
                { ".tar", "Tarballs" },
                { ".gz", "Gzip Files" },
                { ".iso", "Disc Images" },

                // --- Audio ---
                { ".mp3", "Music (MP3)" },
                { ".wav", "Wave Audio" },
                { ".flac", "Lossless Audio" },
                { ".m4a", "Apple Audio" },
                { ".aac", "AAC Audio" },
                { ".ogg", "Ogg Vorbis" },
                { ".wma", "Windows Audio" },

                // --- Video ---
                { ".mp4", "MP4 Videos" },
                { ".avi", "AVI Videos" },
                { ".mkv", "Matroska Videos" },
                { ".mov", "QuickTime Videos" },
                { ".wmv", "Windows Videos" },
                { ".webm", "Web Videos" },
                { ".flv", "Flash Videos" },
                { ".m4v", "Apple Videos" },

                // --- Programming & Code ---
                { ".cs", "Code" },
                { ".py", "Code" },
                { ".js", "Code" },
                { ".java", "Code" },
                { ".cpp", "Code" },
                { ".c", "Code" },
                { ".html", "Code" },
                { ".css", "Code" },
                { ".xml", "Code" },
                { ".json", "Code" },
                { ".sql", "Code" },
                { ".php", "Code" },
                { ".ts", "Code" },

                // --- Executables & Scripts ---
                { ".exe", "Executables" },
                { ".msi", "Installers" },
                { ".dll", "System Libraries" },
                { ".bat", "Windows Batch" },
                { ".sh", "Bash Scripts" },

                // --- Configuration & Logs ---
                { ".log", "Activity Logs" },
                { ".config", "App Configs" },
                { ".ini", "Initialization Files" }
            };

            string sourcePath = ShowAllInDirectory();
            if (sourcePath == "exit") return;
            else if (Directory.Exists(sourcePath))
            {
                string[] files = Directory.GetFiles(sourcePath);

                Dictionary<string, string> fileOrderd = new();
                foreach (string filePath in files)
                {
                    string extension = Path.GetExtension(filePath).ToLower();

                    string subFolderName = extensionMap.ContainsKey(extension)
                                           ? extensionMap[extension]
                                           : "Unrecognised";

                    string destinationFolder = Path.Combine(Path.GetDirectoryName(filePath) ?? sourcePath, subFolderName);

                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                    }

                    string fileName = Path.GetFileName(filePath);
                    string finalPath = Path.Combine(destinationFolder, fileName);

                    string _fileName = Path.GetFileName(filePath);
                    string _fileNameOnly = Path.GetFileNameWithoutExtension(filePath);
                    string _extension = Path.GetExtension(filePath);
                    int counter = 1;

                    while (File.Exists(finalPath))
                    {
                        string _newFileName = $"{_fileNameOnly} ({counter}){_extension}";
                        finalPath = Path.Combine(destinationFolder, _newFileName);
                        counter++;
                    }

                    string newFileName = Path.GetFileName(finalPath);

                    if (newFileName != fileName)
                    {
                        Console.WriteLine($"File name changed! {fileName} -> {Path.GetFileName(finalPath)}");
                    }

                    FileMoveHistory FileHistory = new FileMoveHistory();
                    FileHistory.FileName = newFileName;
                    FileHistory.NewPath = finalPath;
                    FileHistory.OriginalPath = Path.GetDirectoryName(filePath) ?? sourcePath;
                    FilesList.Add(FileHistory);

                    try
                    {
                        File.Move(filePath, finalPath);
                        fileOrderd.Add(newFileName, subFolderName);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"Error moving {fileName}: {ex.Message}");
                    }
                }

                var filesOrdered = fileOrderd.OrderBy(f => Path.GetExtension(f.Key)).ToList();
                string lastCategory = "";

                for (int i = 0; i < filesOrdered.Count; i++)
                {
                    string currentCategory = filesOrdered[i].Value;
                    string fileName = filesOrdered[i].Key;

                    if (currentCategory != lastCategory)
                    {
                        Console.WriteLine($"\n--- {currentCategory} ---");
                        lastCategory = currentCategory;
                    }

                    Console.WriteLine("Moved: " + fileName);
                }
                SaveHistory();
            }
        }

        ///<summary>Sorts the files back inside their original folder</summary>
        public static void UnDoSortFileIntoSubFolders()
        {
            string sourcePath = ShowAllInDirectory();
            if (sourcePath == "exit") return;
            else if (Directory.Exists(sourcePath))
            {
                for (int i = FilesList.Count - 1; i >= 0; i--)
                {
                    var history = FilesList[i];

                    if (history.OriginalPath == sourcePath)
                    {
                        try
                        {
                            if (File.Exists(history.NewPath))
                            {
                                string destinationPath = Path.Combine(sourcePath, history.FileName);

                                File.Move(history.NewPath, destinationPath, overwrite: true);
                                Console.WriteLine($"Restored: {history.FileName} -> {sourcePath}");

                                FilesList.RemoveAt(i);
                            }
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine($"Error restoring {history.FileName}: {ex.Message}");
                        }
                    }
                }
                DeleteEmptyFolders(sourcePath);
                SaveHistory();
            }
        }

        ///<summary>Deletes the empty folders inside a given/user chosen folder</summary>
        ///<param name="source">The main folder path to delete from. Base is nothing</param>
        public static void DeleteEmptyFolders(string source = "")
        {
            string sourcePath = string.Empty;
            if (source == "") sourcePath = ShowAllInDirectory();
            else sourcePath = source;
            if (sourcePath == "exit") return;

            string[] subDirs = Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories);
            int deletedCount = 0;

            foreach (string dir in subDirs.OrderByDescending(d => d.Length))
            {
                if (Directory.GetFileSystemEntries(dir).Length == 0)
                {
                    try
                    {
                        Directory.Delete(dir);
                        Console.WriteLine($"Deleted empty folder: {Path.GetFileName(dir)}");
                        deletedCount++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Could not delete {dir}: {ex.Message}");
                    }
                }
            }
            Console.WriteLine($"Cleanup finished. Removed {deletedCount} folders.");
        }

        ///<summary>Puts the 2year or older files into a archive subfolder for easier orgenisation</summary>
        public static void ArchiveOldFilesToDesktop()
        {
            // 1. Ask user which folder to scan
            string sourcePath = ShowAllInDirectory();
            if (sourcePath == "exit") return;

            // 2. Define the Desktop Archive path
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string archiveFolder = Path.Combine(desktopPath, "Archive_Old_Files");

            DateTime cutoffDate = DateTime.Now.AddYears(-2);
            int movedCount = 0;

            try
            {
                string[] files = Directory.GetFiles(sourcePath);

                foreach (string filePath in files)
                {
                    DateTime lastModified = File.GetLastWriteTime(filePath);

                    if (lastModified < cutoffDate)
                    {
                        // 3. Only create the folder if we actually find at least one old file
                        if (!Directory.Exists(archiveFolder))
                        {
                            Directory.CreateDirectory(archiveFolder);
                            Console.WriteLine("[System] Created Archive folder on Desktop.");
                        }

                        string fileName = Path.GetFileName(filePath);
                        string destPath = Path.Combine(archiveFolder, fileName);

                        // Handle collisions in the Archive folder too!
                        int counter = 1;
                        string finalDestPath = destPath;
                        while (File.Exists(finalDestPath))
                        {
                            string nameOnly = Path.GetFileNameWithoutExtension(fileName);
                            string ext = Path.GetExtension(fileName);
                            finalDestPath = Path.Combine(archiveFolder, $"{nameOnly} ({counter}){ext}");
                            counter++;
                        }

                        // 4. Record history for UNDO functionality
                        FileMoveHistory history = new FileMoveHistory
                        {
                            FileName = Path.GetFileName(finalDestPath),
                            NewPath = finalDestPath,
                            OriginalPath = sourcePath
                        };
                        FilesList.Add(history);

                        File.Move(filePath, finalDestPath);
                        Console.WriteLine($"Archived: {fileName}");
                        movedCount++;
                    }
                }

                if (movedCount > 0)
                {
                    SaveHistory();
                    Console.WriteLine($"\nSuccess! {movedCount} files moved to Desktop/Archive_Old_Files.");
                }
                else
                {
                    Console.WriteLine("\nNo files older than 2 years were found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        ///<summary>Shows the files inside a given path. The folders are the menu</summary>
        private static string ShowAllInDirectory()
        {
            string currentPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            while (true)
            {
                string[] files;
                string[] directories;
                try
                {
                    directories = Directory.GetDirectories(currentPath);
                    files = Directory.GetFiles(currentPath);
                }
                catch (Exception)
                {
                    Console.WriteLine("Access Denied to this folder!");
                    Console.WriteLine("Press any key to go back...");
                    Console.ReadKey();
                    currentPath = Directory.GetParent(currentPath)?.FullName ?? currentPath;
                    continue;
                }

                //We have all the directory names in the array
                string[] optionsMenu = { };

                for (int i = 0; i < directories.Length; i++)
                    optionsMenu = optionsMenu.Append(Path.GetFileName(directories[i])).ToArray();

                //I need to implement the function to be able to scroll through them
                bool refreshFolders = false;
                int place = 0;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"--- Directory Browser ---");
                    Console.WriteLine($"Current Path: {currentPath}");
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("Enter) Select folder");
                    Console.WriteLine("Space) Select current folder");
                    Console.WriteLine("Backspace) Go BACK to parent folder");
                    Console.WriteLine("E) Exit to menu");
                    Console.WriteLine("--- Folders ---");
                    for (int i = 0; i < optionsMenu.Length; i++)
                    {
                        if (i == place)
                            Console.WriteLine($"> {i + 1}: {optionsMenu[i]}");
                        else
                            Console.WriteLine($"  {i + 1}: {optionsMenu[i]}");
                    }

                    if (files.Length > 0)
                    {
                        Console.WriteLine("--- Files ---");
                        for (int i = 0; i < files.Length; i++)
                            Console.WriteLine($"{i + 1}:{Path.GetFileName(files[i])}");
                    }

                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.DownArrow) place++;
                    else if (keyInfo.Key == ConsoleKey.UpArrow) place--;

                    if (optionsMenu.Length > 0)
                    {
                        if (place == optionsMenu.Length) place = 0;
                        else if (place == -1) place = optionsMenu.Length - 1;
                    }
                    else place = -2;

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Enter:
                            if (place != -2)
                            {
                                currentPath = directories[place];
                                refreshFolders = true;
                            }
                            break;
                        case ConsoleKey.Backspace:
                            currentPath = Directory.GetParent(currentPath)?.FullName ?? currentPath;
                            refreshFolders = true;
                            break;
                        case ConsoleKey.E: return "exit";
                        case ConsoleKey.Spacebar:
                            return currentPath;
                    }
                    if (refreshFolders == true) break;

                }
            }
        }
    }

}
