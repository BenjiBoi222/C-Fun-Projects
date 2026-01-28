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
                string[] menuOptions = { "Connection Status","Devices List","Server Metrics","Ping network devices","Main Menu"};

                Program.ShowMenuHelper(menuName, menuOptions, out int option, ">");

                Settings.MenuSelectUI(menuName, menuOptions, ">", option);
                switch (option)
                {
                    case 0: CheckConnection(); break;
                    case 1: ShowSavedDevices(); break;
                    case 2: ShowServerMetrics(); break;
                    case 3: GrabNetworkIPs(); break;
                    case 4: return;
                }
            }
        }


        ///<!--The server functions-->
        ///<summary>Sends out pings and if it gets a return it shows the device currently on the server</summary>
        static void CheckConnection()
        {
            //This list makes it so the servers are always on top

            List<ServerDevicesObjects> serverDevicesList = ServerDevices.OrderBy(x => x.DeviceType !=  "Router").ThenBy(x => x.DeviceType != "Server").ThenBy(x => x.DeviceName.Length).ToList();

            Console.Clear();
            Console.WriteLine("=== Connection Status ===");

            using (Ping pingSender = new())
            {
                foreach (var device in serverDevicesList)
                {

                    if (device.DeviceType == "Server") { Console.ForegroundColor = ConsoleColor.DarkBlue;  Console.Write("[Server] "); }
                    if (device.DeviceType == "Router") { Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("[Router] "); }
                    if (device.DeviceType == "Device") { Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("[Device] "); }

                    Console.ResetColor();

                    Console.Write($"Pinging: {device.DeviceName, -12} ");

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
                            Console.WriteLine($"[Offline]");
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
            Console.WriteLine("\nPress any key to return to the server menu...");
            Console.ReadKey(true);
        }


        private static void ShowSavedDevices()
        {
            Console.Clear();
            Console.WriteLine("\nThe server devices:");
            Console.WriteLine($"{"Name",-14} {"Ip",-15} {"Type",-15}");
            List<ServerDevicesObjects> serverDevicesList = Server.ServerDevices.OrderBy(x => x.DeviceType != "Server").ToList();
            foreach (ServerDevicesObjects devices in serverDevicesList)
            {
                
                Console.Write($"{devices.DeviceName,-15}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{devices.IpAddres,-16}");
                if (devices.DeviceType == "Server")
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"{devices.DeviceType,-15}");
                    Console.ResetColor();
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"{devices.DeviceType,-15}");
                    Console.ResetColor();
                }
            }


            Console.WriteLine("\nPress any key to return to the server list...");
            Console.ReadKey(true);
        }



        private static void ShowServerMetrics()
        {
            List<ServerDevicesObjects> serverTypeDevices = ServerDevices.Where(x => x.DeviceType == "Server").ToList();

            if (serverTypeDevices.Count == 0)
            {
                Console.WriteLine("No server-type devices found in your list.");
                return;
            }

            string[] servers = new string[serverTypeDevices.Count + 1];
            int lastItem = 0;
            for (int i = 0; i < servers.Length - 1; i++) { servers[i] = serverTypeDevices[i].DeviceName; lastItem++; }
            servers[lastItem] = "Back";

            Program.ShowMenuHelper("Servers", servers, out int option, ">");
            Settings.MenuSelectUI("Servers", servers, ">", option);

            if (option == lastItem) return;

            string sshUsername = serverTypeDevices[option].SshUsername;
            string sshPassword = serverTypeDevices[option].SshPassword;

            if(sshUsername == "none")
            {
                Console.Write($"Add the SSH Username for {servers[option]}: ");
                sshUsername = Console.ReadLine();
            }
            if(sshPassword == "none")
            {
                Console.Write($"Add the SSH password for {servers[option]}: ");
                sshPassword = Console.ReadLine();
            }

            ShowServerMetricsHandler(serverTypeDevices[option], sshUsername, sshPassword);
        }
        public static void ShowServerMetricsHandler(ServerDevicesObjects device, string username, string password)
        {
            try
            {
                // 1. Setup Connection Info with a specific Timeout
                var connectionInfo = new PasswordConnectionInfo(device.IpAddres, username, password)
                {
                    Timeout = TimeSpan.FromSeconds(5) // This is where the timeout lives!
                };

                // 2. Initialize the client using that info
                using (var client = new SshClient(connectionInfo))
                {
                    Console.WriteLine($"\n[System] Connecting to {device.IpAddres} (Timeout: 5s)...");
                    client.Connect();

                    Console.WriteLine("Connected! Press any key to stop monitoring.");
                    Settings.Sleep(1000);

                    while (!Console.KeyAvailable)
                    {
                        if (!client.IsConnected) break;

                        var cmd = client.CreateCommand(
                            "echo \"CPU: $(top -bn1 | grep 'Cpu(s)' | awk '{print $2 + $4}')%\" && " +
                            "free -m | awk 'NR==2{printf \"RAM: %s/%sMB (%.2f%%)\\n\", $3,$2,$3*100/$2 }' && " +
                            "df -h / | awk 'NR==2{printf \"Storage: %s/%s used (%s total)\\n\", $3,$2,$2}'"
                        );

                        var result = cmd.Execute();

                        Console.Clear();
                        Console.WriteLine($"=== Metrics: {device.DeviceName} ===");
                        Console.WriteLine($"Last Update: {DateTime.Now:HH:mm:ss}");
                        Console.WriteLine("----------------------------------------------------");
                        Console.WriteLine(result);
                        Console.WriteLine("----------------------------------------------------");
                        Console.WriteLine("Press any key to exit...");

                        Settings.Sleep(1000);
                    }

                    if (Console.KeyAvailable) Console.ReadKey(true);
                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("========================================");
                Console.WriteLine("        CONNECTION ERROR");
                Console.WriteLine("========================================");
                Console.WriteLine($"Target: {device.DeviceName} ({device.IpAddres})");
                Console.WriteLine($"Error:  {ex.Message}");
                Console.WriteLine("========================================");
                Console.ResetColor();

                Console.WriteLine("\nPress any key to return to the server list...");
                Console.ReadKey(true);
            }
        }



        private static void GrabNetworkIPs()
        {
            // 1. Menu Selection
            string[] savedDevices = new string[ServerDevices.Count + 1];
            for (int i = 0; i < ServerDevices.Count; i++)
            {
                savedDevices[i] = ServerDevices[i].DeviceName;
            }
            savedDevices[ServerDevices.Count] = "Back";

            Program.ShowMenuHelper("Select Anchor Device", savedDevices, out int option, ">");
            Settings.MenuSelectUI("Select Anchor Device", savedDevices, ">", option);

            if (option == ServerDevices.Count) return;

            // 2. Setup Scan Depth
            Console.Clear();
            Console.WriteLine("=== Scan Configuration ===");
            Console.WriteLine("1. Quick Scan (Current subnet: x.x.x.0-255)");
            Console.WriteLine("2. Deep Scan  (Everything except first IP: x.0.0.0-x.255.255.255)");
            Console.Write("\nChoose depth [1-2]: ");
            string choice = Console.ReadLine();

            string networkIp = ServerDevices[option].IpAddres;
            string[] parts = networkIp.Split('.');

            // Thread-safe collection and counters
            var onlineDevices = new System.Collections.Concurrent.ConcurrentBag<ServerDevicesObjects>();
            long completedIps = 0;
            // Calculation: Choice 2 is 256 * 256 * 256 (everything but the first octet)
            long totalIps = (choice == "2") ? 256L * 256L * 256L : 256L;

            Console.Clear();
            Console.CursorVisible = false;

            // 3. Progress Bar Task
            bool isDone = false;
            var progressTask = Task.Run(() => {
                while (!isDone)
                {
                    double percentage = (double)Interlocked.Read(ref completedIps) / totalIps * 100;
                    DrawProgressBar(percentage);
                    Thread.Sleep(100);
                }
            });

            // 4. Parallel Execution Logic
            if (choice == "2") // Deep Scan (Scans x.0.0.0 to x.255.255.255)
            {
                Parallel.For(0, 256, second => {
                    Parallel.For(0, 256, third => {
                        for (int fourth = 0; fourth < 256; fourth++)
                        {
                            string target = $"{parts[0]}.{second}.{third}.{fourth}";
                            PingAndStore(target, onlineDevices);
                            Interlocked.Increment(ref completedIps);
                        }
                    });
                });
            }
            else // Quick Scan (Scans x.x.x.0 to x.x.x.255)
            {
                Parallel.For(0, 256, fourth => {
                    string target = $"{parts[0]}.{parts[1]}.{parts[2]}.{fourth}";
                    PingAndStore(target, onlineDevices);
                    Interlocked.Increment(ref completedIps);
                });
            }

            isDone = true;
            progressTask.Wait();
            Console.CursorVisible = true;
            Console.WriteLine("\rScan Complete!                                     \n");

            // 5. Results Display
            Console.WriteLine("=== Network Online Devices ===");

            // Sorts IPs numerically (1.2 before 1.10)
            var sortedDevices = onlineDevices.OrderBy(d => Version.Parse(d.IpAddres)).ToList();
            int displayCounter = 1;

            foreach (var device in sortedDevices)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("[Found] ");
                Console.ResetColor();
                Console.Write($"Device-{displayCounter,-3} ({device.IpAddres,-15}) ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{"[Online]",20}");
                Console.ResetColor();
                displayCounter++;
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey(true);
        }

        // Helper method to handle the actual pinging
        private static void PingAndStore(string ip, System.Collections.Concurrent.ConcurrentBag<ServerDevicesObjects> list)
        {
            using (Ping p = new Ping())
            {
                try
                {
                    PingReply reply = p.Send(ip, 150);
                    if (reply.Status == IPStatus.Success)
                    {
                        list.Add(new ServerDevicesObjects
                        {
                            DeviceName = "Device",
                            IpAddres = ip,
                            DeviceType = "[LAN] "
                        });
                    }
                }
                catch { /* Ignore timeouts/errors */ }
            }
        }

        // Helper to draw the loading line
        private static void DrawProgressBar(double percentage)
        {
            int barLength = 40;
            int filledLength = (int)(percentage / 100 * barLength);

            // Use \r to return cursor to start of line without clearing screen
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(new string('█', filledLength));
            Console.Write(new string('-', barLength - filledLength));
            Console.ResetColor();
            Console.Write($"] {percentage:F2}% Complete   ");
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
