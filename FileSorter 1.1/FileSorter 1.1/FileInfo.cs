using System;
using System.Collections.Generic;
using System.Text;

namespace FileSorter_1._1
{
    class FileInfo
    {
        ///<summary>Displays the main help menu with navigation</summary>
        public static void ShowManual()
        {
            while (true)
            {
                Console.Clear();
                string[] menuOptions = 
                { 
                    "Getting Started", 
                    "Sort Files Feature", 
                    "UnSort Files Feature",
                    "Delete Empty Folders",
                    "Deep Sorter (Advanced)",
                    "Archive Old Files",
                    "File Categories Guide",
                    "Keyboard Shortcuts",
                    "Tips & Best Practices",
                    "Return to Main Menu"
                };
                string menuName = "Manual Option";
                Program.ShowMenuHelper(menuName, menuOptions, out int option, ">");
                Settings.MenuSelectUI(menuName, menuOptions, ">", option);

                switch (option)
                {
                    case 0: ShowGettingStarted(); break;
                    case 1: ShowSortFilesHelp(); break;
                    case 2: ShowUnSortFilesHelp(); break;
                    case 3: ShowDeleteEmptyFoldersHelp(); break;
                    case 4: ShowDeepSorterHelp(); break;
                    case 5: ShowArchiveOldFilesHelp(); break;
                    case 6: ShowFileCategoriesGuide(); break;
                    case 7: ShowKeyboardShortcuts(); break;
                    case 8: ShowTipsAndBestPractices(); break;
                    case 9: return;
                }

                Console.WriteLine("\nPress any key to return to help menu...");
                Console.ReadKey(true);
            }
        }

        private static void ShowGettingStarted()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              GETTING STARTED GUIDE                     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Welcome to File Sorter! This utility helps you organize files");
            Console.WriteLine("and folders on your computer automatically.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📂 What does this app do?");
            Console.ResetColor();
            Console.WriteLine("  • Sorts files into categorized folders (Documents, Images, etc.)");
            Console.WriteLine("  • Undoes sorting operations using saved history");
            Console.WriteLine("  • Cleans up empty directories");
            Console.WriteLine("  • Archives old files you haven't used in years");
            Console.WriteLine("  • Deep sorts nested folder structures\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("🚀 How to use:");
            Console.ResetColor();
            Console.WriteLine("  1. Navigate menus using UP/DOWN arrow keys");
            Console.WriteLine("  2. Press ENTER to select an option");
            Console.WriteLine("  3. Browse folders with the Directory Browser");
            Console.WriteLine("  4. Press SPACE to select the current folder");
            Console.WriteLine("  5. Press E to exit/cancel operations\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Your files are tracked!");
            Console.ResetColor();
            Console.WriteLine("  The app saves a history.json file to undo sorting operations.\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("⚠ Warning:");
            Console.ResetColor();
            Console.WriteLine("  Always backup important files before using Deep Sorter!");
        }

        private static void ShowSortFilesHelp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              SORT FILES - USER GUIDE                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📋 What it does:");
            Console.ResetColor();
            Console.WriteLine("  Organizes files in a folder by moving them into category-based");
            Console.WriteLine("  subfolders (e.g., 'Word Documents', 'Photos (JPG)', 'Code').\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("🔧 How to use:");
            Console.ResetColor();
            Console.WriteLine("  1. Select 'Sort files' from the menu");
            Console.WriteLine("  2. Browse to the folder you want to organize");
            Console.WriteLine("  3. Press SPACE to confirm the folder");
            Console.WriteLine("  4. Watch as files are automatically sorted!\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📝 Example:");
            Console.ResetColor();
            Console.WriteLine("  Before:  C:\\Downloads\\report.pdf");
            Console.WriteLine("           C:\\Downloads\\photo.jpg");
            Console.WriteLine("           C:\\Downloads\\song.mp3\n");
            Console.WriteLine("  After:   C:\\Downloads\\PDF Documents\\report.pdf");
            Console.WriteLine("           C:\\Downloads\\Photos (JPG)\\photo.jpg");
            Console.WriteLine("           C:\\Downloads\\Music (MP3)\\song.mp3\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Features:");
            Console.ResetColor();
            Console.WriteLine("  • Handles duplicate filenames automatically");
            Console.WriteLine("  • Creates category folders only when needed");
            Console.WriteLine("  • Saves operation history for undo functionality");
            Console.WriteLine("  • Shows detailed progress during sorting\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("💡 Tip:");
            Console.ResetColor();
            Console.WriteLine("  Files with unknown extensions go to 'Unrecognised' folder.");
        }

        private static void ShowUnSortFilesHelp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             UNSORT FILES - USER GUIDE                  ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📋 What it does:");
            Console.ResetColor();
            Console.WriteLine("  Reverses a previous sort operation by moving files back to");
            Console.WriteLine("  their original locations using the saved history.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("🔧 How to use:");
            Console.ResetColor();
            Console.WriteLine("  1. Select 'UnSort files' from the menu");
            Console.WriteLine("  2. Browse to the SAME folder you sorted before");
            Console.WriteLine("  3. Press SPACE to confirm");
            Console.WriteLine("  4. Files will be restored to their original locations\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📝 Example:");
            Console.ResetColor();
            Console.WriteLine("  If you sorted C:\\Downloads, browse to C:\\Downloads");
            Console.WriteLine("  and select it. All files will return to C:\\Downloads.\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Features:");
            Console.ResetColor();
            Console.WriteLine("  • Restores original file structure");
            Console.WriteLine("  • Automatically removes empty category folders");
            Console.WriteLine("  • Updates history after successful undo\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("⚠ Important:");
            Console.ResetColor();
            Console.WriteLine("  • Only works if history.json exists");
            Console.WriteLine("  • Can only undo operations tracked in history");
            Console.WriteLine("  • Select the SAME folder where sorting was performed");
        }

        private static void ShowDeleteEmptyFoldersHelp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          DELETE EMPTY FOLDERS - USER GUIDE             ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📋 What it does:");
            Console.ResetColor();
            Console.WriteLine("  Scans a folder and all its subfolders, then removes any");
            Console.WriteLine("  empty directories to clean up your file structure.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("🔧 How to use:");
            Console.ResetColor();
            Console.WriteLine("  1. Select 'Delete empty folders' from the menu");
            Console.WriteLine("  2. Browse to the folder you want to clean");
            Console.WriteLine("  3. Press SPACE to confirm");
            Console.WriteLine("  4. All empty folders will be removed\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📝 Example:");
            Console.ResetColor();
            Console.WriteLine("  C:\\Projects\\\n" +
                              "    ├── OldProject\\ (empty)      ❌ DELETED\n" +
                              "    ├── CurrentProject\\\n" +
                              "    │   ├── bin\\ (empty)         ❌ DELETED\n" +
                              "    │   └── src\\ (has files)     ✓ KEPT\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Features:");
            Console.ResetColor();
            Console.WriteLine("  • Recursively scans all nested folders");
            Console.WriteLine("  • Processes deepest folders first");
            Console.WriteLine("  • Shows count of deleted folders");
            Console.WriteLine("  • Safe - only removes completely empty directories\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("💡 Tip:");
            Console.ResetColor();
            Console.WriteLine("  Great for cleanup after unsorting files or project builds!");
        }

        private static void ShowDeepSorterHelp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║         DEEP SORTER - ADVANCED FEATURE ⚠               ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📋 What it does:");
            Console.ResetColor();
            Console.WriteLine("  Collects ALL files from ALL subfolders (recursively) and");
            Console.WriteLine("  reorganizes them into categorized folders inside a new");
            Console.WriteLine("  'SortedFolder' directory.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("🔧 How it works:");
            Console.ResetColor();
            Console.WriteLine("  1. Gathers every file from all nested subfolders");
            Console.WriteLine("  2. Moves them all to the main folder (with renaming)");
            Console.WriteLine("  3. Creates a 'SortedFolder' directory");
            Console.WriteLine("  4. Sorts all files into categories inside 'SortedFolder'\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📝 Example:");
            Console.ResetColor();
            Console.WriteLine("  Before:  C:\\Messy\\");
            Console.WriteLine("             ├── SubA\\file1.pdf");
            Console.WriteLine("             ├── SubB\\photo.jpg");
            Console.WriteLine("             └── SubC\\Deep\\song.mp3\n");
            Console.WriteLine("  After:   C:\\Messy\\SortedFolder\\");
            Console.WriteLine("             ├── PDF Documents\\file1.pdf");
            Console.WriteLine("             ├── Photos (JPG)\\photo.jpg");
            Console.WriteLine("             └── Music (MP3)\\song.mp3\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("⚠ CRITICAL WARNINGS:");
            Console.ResetColor();
            Console.WriteLine("  • THIS CANNOT BE UNDONE!");
            Console.WriteLine("  • Destroys original folder structure");
            Console.WriteLine("  • Renames duplicate files automatically");
            Console.WriteLine("  • Use only for extremely disorganized folders");
            Console.WriteLine("  • BACKUP YOUR DATA FIRST!\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("💡 Best Use Case:");
            Console.ResetColor();
            Console.WriteLine("  Old backup drives with hundreds of nested folders where you");
            Console.WriteLine("  need to flatten and reorganize everything from scratch.");
        }

        private static void ShowArchiveOldFilesHelp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           ARCHIVE OLD FILES - USER GUIDE               ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📋 What it does:");
            Console.ResetColor();
            Console.WriteLine("  Finds files older than 2 years (based on last modified date)");
            Console.WriteLine("  and moves them to an 'Archive_Old_Files' folder on your Desktop.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("🔧 How to use:");
            Console.ResetColor();
            Console.WriteLine("  1. Select 'Archive old files' feature");
            Console.WriteLine("  2. Browse to the folder you want to scan");
            Console.WriteLine("  3. Press SPACE to start archiving");
            Console.WriteLine("  4. Old files move to Desktop\\Archive_Old_Files\\\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📝 Age Threshold:");
            Console.ResetColor();
            Console.WriteLine("  Files last modified before: " + DateTime.Now.AddYears(-2).ToString("MMMM dd, yyyy"));
            Console.WriteLine("  (2 years from today)\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Features:");
            Console.ResetColor();
            Console.WriteLine("  • Only creates archive folder if old files are found");
            Console.WriteLine("  • Handles duplicate filenames in archive");
            Console.WriteLine("  • Tracks moves in history for potential undo");
            Console.WriteLine("  • Shows count of archived files\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("💡 Tip:");
            Console.ResetColor();
            Console.WriteLine("  Perfect for cleaning Downloads folder or Documents directory!");
        }

        private static void ShowFileCategoriesGuide()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║            FILE CATEGORIES REFERENCE                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("Files are sorted into the following categories:\n");

            ShowCategory("📄 Documents", new[] { ".pdf → PDF Documents", ".docx, .doc → Word Documents", ".txt → Plain Text Files", ".xlsx, .xls → Excel Spreadsheets", ".pptx, .ppt → PowerPoint Presentations" });
            ShowCategory("🖼️  Images", new[] { ".jpg, .jpeg → Photos (JPG)", ".png → Graphics (PNG)", ".gif → Animations (GIF)", ".svg → Vectors (SVG)", ".bmp → Bitmaps" });
            ShowCategory("🎵 Audio", new[] { ".mp3 → Music (MP3)", ".wav → Wave Audio", ".flac → Lossless Audio", ".m4a → Apple Audio", ".ogg → Ogg Vorbis" });
            ShowCategory("🎬 Video", new[] { ".mp4 → MP4 Videos", ".avi → AVI Videos", ".mkv → Matroska Videos", ".mov → QuickTime Videos", ".webm → Web Videos" });
            ShowCategory("📦 Archives", new[] { ".zip → Zip Archives", ".rar → Rar Archives", ".7z → 7-Zip Archives", ".iso → Disc Images" });
            ShowCategory("💻 Code", new[] { ".cs, .py, .js, .java, .cpp → Code", ".html, .css, .xml, .json → Code", ".sql, .php, .ts → Code" });
            ShowCategory("⚙️  Executables", new[] { ".exe → Executables", ".msi → Installers", ".dll → System Libraries", ".bat → Windows Batch", ".sh → Bash Scripts" });

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n❓ Unknown Extensions:");
            Console.ResetColor();
            Console.WriteLine("  Files with unrecognized extensions → 'Unrecognised' folder");
        }

        private static void ShowCategory(string title, string[] extensions)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{title}:");
            Console.ResetColor();
            foreach (var ext in extensions)
            {
                Console.WriteLine($"  {ext}");
            }
            Console.WriteLine();
        }

        private static void ShowKeyboardShortcuts()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║            KEYBOARD SHORTCUTS REFERENCE                ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📂 Directory Browser:");
            Console.ResetColor();
            Console.WriteLine("  ↑↓ Arrows    Navigate through folders");
            Console.WriteLine("  ENTER        Open/select highlighted folder");
            Console.WriteLine("  SPACE        Confirm current folder selection");
            Console.WriteLine("  BACKSPACE    Go back to parent directory");
            Console.WriteLine("  E            Exit browser / Cancel operation\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📋 Menu Navigation:");
            Console.ResetColor();
            Console.WriteLine("  ↑↓ Arrows    Move through menu options");
            Console.WriteLine("  ENTER        Select current option");
            Console.WriteLine("  ANY KEY      Continue after operation completes\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("⚡ Quick Tips:");
            Console.ResetColor();
            Console.WriteLine("  • Hold arrow keys to scroll faster");
            Console.WriteLine("  • Menu wraps around (top ↔ bottom)");
            Console.WriteLine("  • Press E anytime to cancel and go back");
        }

        private static void ShowTipsAndBestPractices()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          TIPS & BEST PRACTICES                         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ DO:");
            Console.ResetColor();
            Console.WriteLine("  • Test on a small folder first");
            Console.WriteLine("  • Keep history.json file safe for undo capability");
            Console.WriteLine("  • Use regular Sort for everyday organization");
            Console.WriteLine("  • Archive old files to free up space");
            Console.WriteLine("  • Delete empty folders after unsorting\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("✗ DON'T:");
            Console.ResetColor();
            Console.WriteLine("  • Sort your entire C:\\ drive");
            Console.WriteLine("  • Use Deep Sorter on important project folders");
            Console.WriteLine("  • Delete history.json if you plan to undo");
            Console.WriteLine("  • Sort system folders (Windows, Program Files)");
            Console.WriteLine("  • Interrupt operations in progress\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("💡 Pro Tips:");
            Console.ResetColor();
            Console.WriteLine("  1. Common folders to organize:");
            Console.WriteLine("     • Downloads");
            Console.WriteLine("     • Desktop");
            Console.WriteLine("     • Documents");
            Console.WriteLine();
            Console.WriteLine("  2. Create a backup before Deep Sorter");
            Console.WriteLine();
            Console.WriteLine("  3. Run 'Delete empty folders' after unsorting");
            Console.WriteLine();
            Console.WriteLine("  4. Archive keeps files safe while decluttering\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("🎯 Recommended Workflow:");
            Console.ResetColor();
            Console.WriteLine("  Step 1: Sort files in Downloads folder");
            Console.WriteLine("  Step 2: Review the organization");
            Console.WriteLine("  Step 3: If satisfied, keep it; if not, UnSort");
            Console.WriteLine("  Step 4: Periodically archive old files");
            Console.WriteLine("  Step 5: Clean up empty folders");
        }
    }
}
