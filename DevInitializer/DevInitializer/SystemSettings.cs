using System;
using System.Collections.Generic;
using System.Text;

namespace DevInitializer
{
    internal class SystemSettings
    {
        /// <summary>
        /// A basic menu selector with interactive highligting
        /// </summary>
        /// <param name="menuOptions">The options for the menu items</param>
        public static int ShowMenu(string[] menuOptions, string title)
        {
            int chosenIndex = 0;
            while (true)
            {
                Console.CursorVisible = false;
                Console.Clear();
                Console.WriteLine($"===--{title}--===\n");
                for (int i = 0;  i < menuOptions.Length; i++)
                {
                    if(i == chosenIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"> {menuOptions[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {menuOptions[i]}");
                    }
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    chosenIndex--;
                }
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    chosenIndex++;
                }

                if (chosenIndex == menuOptions.Length) chosenIndex = 0;
                if (chosenIndex < 0) chosenIndex = menuOptions.Length - 1;
                
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Console.WriteLine($"You selected: {menuOptions[chosenIndex]}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    return chosenIndex;
                }
            }
        }
        //----
        public static async void ToolInstallerMenu()
        {
            // 1. Setup the data as objects
            List<DevTool> tools = new List<DevTool>
            {
                // --- Runtimes ---
                new DevTool {
                    Name = "Node.js (LTS)",
                    Category = "Runtimes",
                    DownloadLink = "https://nodejs.org/dist/v20.11.1/node-v20.11.1-x64.msi"
                },
                new DevTool {
                    Name = ".NET 10 SDK",
                    Category = "Runtimes",
                    DownloadLink = "https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.ps1"
                },
                new DevTool {
                    Name = "Python 3",
                    Category = "Runtimes",
                    DownloadLink = "https://www.python.org/ftp/python/3.12.2/python-3.12.2-amd64.exe"
                },

                // --- Editors & IDEs ---
                new DevTool {
                    Name = "VS Code",
                    Category = "Editors",
                    DownloadLink = "https://update.code.visualstudio.com/latest/win32-x64-user/stable"
                },
                new DevTool {
                    Name = "Visual Studio Community",
                    Category = "Editors",
                    DownloadLink = "https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&rel=17"
                },
                new DevTool {
                    Name = "JetBrains Rider (Trial)",
                    Category = "Editors",
                    DownloadLink = "https://download.jetbrains.com/rider/JetBrains.Rider-2024.1.exe"
                },

                // --- API & Data ---
                new DevTool {
                    Name = "Postman",
                    Category = "API & Data",
                    DownloadLink = "https://dl.pstmn.io/download/latest/win64"
                },
                new DevTool {
                    Name = "Docker Desktop",
                    Category = "API & Data",
                    DownloadLink = "https://desktop.docker.com/win/main/amd64/Docker%20Desktop%20Installer.exe"
                },
                new DevTool {
                    Name = "DBeaver Community",
                    Category = "API & Data",
                    DownloadLink = "https://dbeaver.io/files/dbeaver-ce-latest-x86_64-setup.exe"
                },
                new DevTool {
                    Name = "MongoDB Compass",
                    Category = "API & Data",
                    DownloadLink = "https://downloads.mongodb.com/compass/mongodb-compass-1.42.2-win32-x64.exe"
                },

                // --- DevOps & Source Control ---
                new DevTool {
                    Name = "Git for Windows",
                    Category = "DevOps",
                    DownloadLink = "https://github.com/git-for-windows/git/releases/download/v2.44.0.windows.1/Git-2.44.0-64-bit.exe"
                },
                new DevTool {
                    Name = "GitHub Desktop",
                    Category = "DevOps",
                    DownloadLink = "https://central.github.com/deployments/desktop/desktop/latest/win32"
                },
                new DevTool {
                    Name = "Azure Data Studio",
                    Category = "DevOps",
                    DownloadLink = "https://go.microsoft.com/fwlink/?linkid=2261358"
                },

                // --- Utilities ---
                new DevTool {
                    Name = "PowerToys",
                    Category = "Utilities",
                    DownloadLink = "https://github.com/microsoft/PowerToys/releases/download/v0.79.0/PowerToysSetup-0.79.0-x64.exe"
                },
                new DevTool {
                    Name = "Fiddler Everywhere",
                    Category = "Utilities",
                    DownloadLink = "https://api.getfiddler.com/win/latest"
                }
            };

            int hoverIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== DEV TOOL INSTALLER ===");
                Console.WriteLine("(Arrows to move, Space to toggle, Enter to Finish)\n");

                string lastCategory = "";

                for (int i = 0; i < tools.Count; i++)
                {
                    // Print Category Header if it changes
                    if (tools[i].Category != lastCategory)
                    {
                        lastCategory = tools[i].Category;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\n--- {lastCategory} ---");
                        Console.ResetColor();
                    }

                    // Selection indicator [ ] or [X]
                    string checkbox = tools[i].IsChecked ? "[X]" : "[ ]";

                    // Highlight the hovered line
                    if (i == hoverIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"{checkbox} {tools[i].Name} <--");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"{checkbox} {tools[i].Name}");
                    }
                }

                // Input Handling
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow) hoverIndex = Math.Max(0, hoverIndex - 1);
                if (key == ConsoleKey.DownArrow) hoverIndex = Math.Min(tools.Count - 1, hoverIndex + 1);

                if (key == ConsoleKey.Spacebar)
                {
                    // Toggle the checkmark
                    tools[hoverIndex].IsChecked = !tools[hoverIndex].IsChecked;
                }

                if (key == ConsoleKey.Enter) break; // Exit loop to start "download"
            }

            // Process only the selected items
            var selectedTools = tools.Where(t => t.IsChecked).ToList();
            await ProcessDownloads(selectedTools);

            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }

        public static async Task ProcessDownloads(List<DevTool> selectedTools)
        {
            using HttpClient client = new HttpClient();

            foreach (var tool in selectedTools)
            {
                if (string.IsNullOrEmpty(tool.DownloadLink)) continue;

                Console.WriteLine($"\n[SYSTEM] Downloading: {tool.Name}");

                try
                {
                    using var response = await client.GetAsync(tool.DownloadLink, HttpCompletionOption.ResponseHeadersRead);
                    var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                    var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Path.GetFileName(tool.DownloadLink));

                    using var contentStream = await response.Content.ReadAsStreamAsync();
                    using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

                    var buffer = new byte[8192];
                    long totalRead = 0;
                    int read;

                    while ((read = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, read);
                        totalRead += read;

                        if (totalBytes != -1)
                        {
                            int percentage = (int)((totalRead * 100) / totalBytes);
                            Console.Write($"\rProgress: [{new string('=', percentage / 5)}{new string(' ', 20 - (percentage / 5))}] {percentage}%");
                        }
                    }
                    Console.WriteLine($"\n[SUCCESS] {tool.Name} ready on Desktop.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] {tool.Name} failed: {ex.Message}");
                }
            }
        }

    }
    public class DevTool
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsChecked { get; set; }
        public string DownloadLink { get; set; }
    }
}
