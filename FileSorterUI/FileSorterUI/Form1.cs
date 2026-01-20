using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using System.Drawing;

namespace FileSorterUI
{
    public partial class Form1 : Form
    {
        // --- YOUR ORIGINAL DATA ---
        private List<FileMoveHistory> FilesList = new();
        private const string HistoryFileName = "history.json";
        private string selectedPath = "";

        // --- UI ELEMENTS ---
        private ListBox lstLog;
        private Label lblSelectedPath;

        public Form1()
        {
            InitializeComponent(); // Required for WinForms
            SetupInterface();      // Creates the buttons and layout
            LoadHistory();         // Your original history loader
        }

        private void SetupInterface()
        {
            this.Text = "Advanced File Sorter";
            this.Size = new Size(650, 500);

            lblSelectedPath = new Label { Text = "No folder selected", Location = new Point(20, 20), Width = 400 };

            Button btnBrowse = new Button { Text = "Select Folder", Location = new Point(450, 15), Width = 150, Height = 30 };
            btnBrowse.Click += (s, e) => {
                using (var fbd = new FolderBrowserDialog())
                {
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        selectedPath = fbd.SelectedPath;
                        lblSelectedPath.Text = $"Path: {selectedPath}";
                    }
                }
            };

            Button btnSort = new Button { Text = "Sort Files", Location = new Point(20, 60), Width = 140 , Height = 30};
            btnSort.Click += (s, e) => SortFilesIntoSubFolders();

            Button btnUndo = new Button { Text = "Undo Sort", Location = new Point(170, 60), Width = 140, Height = 30 };
            btnUndo.Click += (s, e) => UnDoSortFileIntoSubFolders();

            Button btnDelete = new Button { Text = "Delete Empty", Location = new Point(320, 60), Width = 140, Height = 30 };
            btnDelete.Click += (s, e) => DeleteEmptyFolders();

            Button btnArchive = new Button { Text = "Archive Old", Location = new Point(470, 60), Width = 140, Height = 30 };
            btnArchive.Click += (s, e) => ArchiveOldFilesToDesktop();

            lstLog = new ListBox { Location = new Point(20, 100), Size = new Size(600, 340) };

            this.Controls.AddRange(new Control[] { lblSelectedPath, btnBrowse, btnSort, btnUndo, btnDelete, btnArchive, lstLog });
        }

        private void Log(string message) => lstLog.Items.Insert(0, $"[{DateTime.Now:HH:mm:ss}] {message}");

        // --- YOUR ORIGINAL LOGIC (PRESERVED) ---

        public void LoadHistory()
        {
            if (!File.Exists(HistoryFileName)) return;
            try
            {
                string jsonString = File.ReadAllText(HistoryFileName);
                FilesList = JsonSerializer.Deserialize<List<FileMoveHistory>>(jsonString) ?? new();
                Log($"Loaded {FilesList.Count} moves from history.");
            }
            catch (Exception ex) { Log($"Error loading history: {ex.Message}"); }
        }

        private void SaveHistory()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(FilesList, options);
                File.WriteAllText(HistoryFileName, jsonString);
            }
            catch (Exception ex) { Log($"Error saving history: {ex.Message}"); }
        }

        public void SortFilesIntoSubFolders()
        {
            if (string.IsNullOrEmpty(selectedPath)) { MessageBox.Show("Select a folder first!"); return; }

            // Your exact extension map
            var extensionMap = new Dictionary<string, string>
            {
                { ".docx", "Word Documents" }, { ".doc", "Word Documents" }, { ".xlsx", "Excel Spreadsheets" },
                { ".xls", "Excel Spreadsheets" }, { ".pptx", "PowerPoint Presentations" }, { ".ppt", "PowerPoint Presentations" },
                { ".jpg", "Photos (JPG)" }, { ".jpeg", "Photos (JPG)" }, { ".png", "Graphics (PNG)" },
                { ".gif", "Animations (GIF)" }, { ".webp", "Web Images" }, { ".svg", "Vectors (SVG)" },
                { ".ico", "Icons" }, { ".bmp", "Bitmaps" }, { ".tiff", "High-Res Images" },
                { ".pdf", "PDF Documents" }, { ".txt", "Plain Text Files" }, { ".rtf", "Rich Text" },
                { ".odt", "OpenOffice Docs" }, { ".ods", "OpenOffice Sheets" }, { ".odp", "OpenOffice Slides" },
                { ".csv", "CSV Data" }, { ".xps", "XPS Documents" }, { ".zip", "Zip Archives" },
                { ".rar", "Rar Archives" }, { ".7z", "7-Zip Archives" }, { ".tar", "Tarballs" },
                { ".gz", "Gzip Files" }, { ".iso", "Disc Images" }, { ".mp3", "Music (MP3)" },
                { ".wav", "Wave Audio" }, { ".flac", "Lossless Audio" }, { ".m4a", "Apple Audio" },
                { ".aac", "AAC Audio" }, { ".ogg", "Ogg Vorbis" }, { ".wma", "Windows Audio" },
                { ".mp4", "MP4 Videos" }, { ".avi", "AVI Videos" }, { ".mkv", "Matroska Videos" },
                { ".mov", "QuickTime Videos" }, { ".wmv", "Windows Videos" }, { ".webm", "Web Videos" },
                { ".flv", "Flash Videos" }, { ".m4v", "Apple Videos" }, { ".cs", "Code" },
                { ".py", "Code" }, { ".js", "Code" }, { ".java", "Code" }, { ".cpp", "Code" },
                { ".c", "Code" }, { ".html", "Code" }, { ".css", "Code" }, { ".xml", "Code" },
                { ".json", "Code" }, { ".sql", "Code" }, { ".php", "Code" }, { ".ts", "Code" },
                { ".exe", "Executables" }, { ".msi", "Installers" }, { ".dll", "System Libraries" },
                { ".bat", "Windows Batch" }, { ".sh", "Bash Scripts" }, { ".log", "Activity Logs" },
                { ".config", "App Configs" }, { ".ini", "Initialization Files" }
            };

            string[] files = Directory.GetFiles(selectedPath);
            foreach (string filePath in files)
            {
                string extension = Path.GetExtension(filePath).ToLower();
                string subFolderName = extensionMap.ContainsKey(extension) ? extensionMap[extension] : "Unrecognised";
                string destinationFolder = Path.Combine(Path.GetDirectoryName(filePath), subFolderName);

                if (!Directory.Exists(destinationFolder)) Directory.CreateDirectory(destinationFolder);

                string fileName = Path.GetFileName(filePath);
                string finalPath = Path.Combine(destinationFolder, fileName);
                string _fileNameOnly = Path.GetFileNameWithoutExtension(filePath);
                string _extension = Path.GetExtension(filePath);
                int counter = 1;

                while (File.Exists(finalPath))
                {
                    string _newFileName = $"{_fileNameOnly} ({counter}){_extension}";
                    finalPath = Path.Combine(destinationFolder, _newFileName);
                    counter++;
                }

                FileMoveHistory FileHistory = new FileMoveHistory
                {
                    FileName = Path.GetFileName(finalPath),
                    NewPath = finalPath,
                    OriginalPath = Path.GetDirectoryName(filePath)
                };
                FilesList.Add(FileHistory);

                try
                {
                    File.Move(filePath, finalPath);
                    Log($"Moved: {Path.GetFileName(finalPath)} -> {subFolderName}");
                }
                catch (IOException ex) { Log($"Error moving {fileName}: {ex.Message}"); }
            }
            SaveHistory();
            MessageBox.Show("Sorting finished!");
        }

        public void UnDoSortFileIntoSubFolders()
        {
            if (string.IsNullOrEmpty(selectedPath)) return;
            for (int i = FilesList.Count - 1; i >= 0; i--)
            {
                var history = FilesList[i];
                if (history.OriginalPath == selectedPath)
                {
                    try
                    {
                        if (File.Exists(history.NewPath))
                        {
                            string destinationPath = Path.Combine(selectedPath, history.FileName);
                            File.Move(history.NewPath, destinationPath, true);
                            Log($"Restored: {history.FileName}");
                            FilesList.RemoveAt(i);
                        }
                    }
                    catch (IOException ex) { Log($"Error restoring {history.FileName}: {ex.Message}"); }
                }
            }
            SaveHistory();
        }

        public void DeleteEmptyFolders()
        {
            if (string.IsNullOrEmpty(selectedPath)) return;
            string[] subDirs = Directory.GetDirectories(selectedPath, "*", SearchOption.AllDirectories);
            int deletedCount = 0;
            foreach (string dir in subDirs.OrderByDescending(d => d.Length))
            {
                if (Directory.GetFileSystemEntries(dir).Length == 0)
                {
                    try
                    {
                        Directory.Delete(dir);
                        Log($"Deleted: {Path.GetFileName(dir)}");
                        deletedCount++;
                    }
                    catch (Exception ex) { Log($"Error: {ex.Message}"); }
                }
            }
            Log($"Cleanup finished. Removed {deletedCount} folders.");
        }

        public void ArchiveOldFilesToDesktop()
        {
            if (string.IsNullOrEmpty(selectedPath)) return;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string archiveFolder = Path.Combine(desktopPath, "Archive_Old_Files");
            DateTime cutoffDate = DateTime.Now.AddYears(-2);
            int movedCount = 0;

            string[] files = Directory.GetFiles(selectedPath);
            foreach (string filePath in files)
            {
                if (File.GetLastWriteTime(filePath) < cutoffDate)
                {
                    if (!Directory.Exists(archiveFolder)) Directory.CreateDirectory(archiveFolder);

                    string fileName = Path.GetFileName(filePath);
                    string destPath = Path.Combine(archiveFolder, fileName);
                    int counter = 1;
                    string finalDestPath = destPath;

                    while (File.Exists(finalDestPath))
                    {
                        string nameOnly = Path.GetFileNameWithoutExtension(fileName);
                        string ext = Path.GetExtension(fileName);
                        finalDestPath = Path.Combine(archiveFolder, $"{nameOnly} ({counter}){ext}");
                        counter++;
                    }

                    FileMoveHistory history = new FileMoveHistory
                    {
                        FileName = Path.GetFileName(finalDestPath),
                        NewPath = finalDestPath,
                        OriginalPath = selectedPath
                    };
                    FilesList.Add(history);
                    File.Move(filePath, finalDestPath);
                    Log($"Archived: {fileName}");
                    movedCount++;
                }
            }
            if (movedCount > 0) SaveHistory();
            Log($"Archived {movedCount} files.");
        }
    }

    public class FileMoveHistory
    {
        public string FileName { get; set; } = string.Empty;
        public string OriginalPath { get; set; } = string.Empty;
        public string NewPath { get; set; } = string.Empty;
    }
}