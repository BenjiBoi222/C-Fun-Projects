using System;
using System.IO;
using System.Linq;
using System.Text.Json;

class Program
{
    static void Main(string[] args  )
    {
        FileSorter.LoadHistory();
        while (true) ShowMenu();
    }

    static void ShowMenu()
    {
        Console.WriteLine("\n===Functionalities===");
        Console.WriteLine("0)Close Program");
        Console.WriteLine("1)Sort Files");
        Console.WriteLine("2)UnDo sorted files");
        Console.WriteLine("3)Delete empty folders");
        Console.WriteLine("4)Collect old files(2 years or more)");
        Console.Write("Enter input: ");
        if(int.TryParse(Console.ReadLine(), out int choice))
        {
            switch(choice)
            {
                case 0: Console.WriteLine("Closing program..."); Environment.Exit(0); break;
                case 1: FileSorter.SortFilesIntoSubFolders(); break;
                case 2: FileSorter.UnDoSortFileIntoSubFolders(); break;
                case 3: FileSorter.DeleteEmptyFolders(); break;
                case 4: FileSorter.ArchiveOldFilesToDesktop(); break;
                default: Console.WriteLine("Invalid option"); break;
            }
        }
        else Console.WriteLine("Wrong input type");
    }
}


class FileSorter
{
    static List<FileMoveHistory> FilesList = new();
    const string HistoryFileName = "history.json"; 
    
    public static void LoadHistory()
    {
        if (!File.Exists(HistoryFileName)) return;
        try
        {
            string jsonString = File.ReadAllText(HistoryFileName);
            FilesList = JsonSerializer.Deserialize<List<FileMoveHistory>>(jsonString) ?? new();
            Console.WriteLine($"[System] Loaded {FilesList.Count} previous moves from history.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[System] Error loading history: {ex.Message}");
            FilesList = new List<FileMoveHistory>(); // Ensure it's not null
        }
    }
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

            foreach (string filePath in files)
            {
                string extension = Path.GetExtension(filePath).ToLower();

                string subFolderName = extensionMap.ContainsKey(extension)
                                       ? extensionMap[extension]
                                       : "Unrecognised";

                string destinationFolder = Path.Combine(Path.GetDirectoryName(filePath), subFolderName);

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
                FileHistory.OriginalPath = Path.GetDirectoryName(filePath); 
                FilesList.Add(FileHistory);

                try
                {
                    File.Move(filePath, finalPath);
                    Console.WriteLine($"Moved: {newFileName} -> {subFolderName}");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error moving {fileName}: {ex.Message}");
                }
            }
            SaveHistory();
        }
    }
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
            SaveHistory();
        }
    }
    public static void DeleteEmptyFolders()
    {
        string sourcePath = ShowAllInDirectory();
        if(sourcePath == "exit") return;

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

    private static string ShowAllInDirectory()
    {
        string currentPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"--- Directory Browser ---");
            Console.WriteLine($"Current Path: {currentPath}");
            Console.WriteLine("-------------------------");
            Console.WriteLine("0) SELECT this folder now");
            Console.WriteLine("B) Go BACK to parent folder");

            string[] directories;
            try
            {
                directories = Directory.GetDirectories(currentPath);
            }
            catch (Exception)
            {
                Console.WriteLine("Access Denied to this folder!");
                Console.WriteLine("Press any key to go back...");
                Console.ReadKey();
                currentPath = Directory.GetParent(currentPath)?.FullName ?? currentPath;
                continue;
            }

            for (int i = 0; i < directories.Length; i++)
            {
                Console.WriteLine($"{i + 1}) {Path.GetFileName(directories[i])}");
            }

            Console.Write("\nChoose a folder number, '0' to Sort, 'B' to go Back, or 'E' to exit: ");
            string input = Console.ReadLine()?.ToUpper();

            if (input == "0") return currentPath;
            if (input == "E") return "exit";
            if (input == "B")
            {
                currentPath = Directory.GetParent(currentPath)?.FullName ?? currentPath;
                continue;
            }

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= directories.Length)
            {
                currentPath = directories[choice - 1];
            }
            else
            {
                Console.WriteLine("Invalid choice. Press any key...");
                Console.ReadKey();
            }
        }
    }
}

class FileMoveHistory
{
    public string FileName { get; set; } = string.Empty;
    public string OriginalPath { get; set; } = string.Empty;
    public string NewPath { get; set; } = string.Empty;
}
