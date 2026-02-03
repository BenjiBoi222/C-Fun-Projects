using System;
using System.Collections.Generic;
using System.Text;

namespace FileSorter_1._1
{
    internal class ServerInfo
    {
        ///<summary>Displays the server help menu with navigation</summary>
        public static void ShowManual()
        {
            while (true)
            {
                Console.Clear();
                string[] serverMenuOptions = 
                { 
                    "Server Overview",
                    "Connection Status",
                    "Devices List",
                    "Server Metrics (SSH)",
                    "Network Scanner",
                    "Device Types & Setup",
                    "Troubleshooting",
                    "Back to Main Menu"
                };
                string menuName = "Server Manual";
                Program.ShowMenuHelper(menuName, serverMenuOptions, out int option, ">");
                Settings.MenuSelectUI(menuName, serverMenuOptions, ">", option);

                switch (option)
                {
                    case 0: ShowServerOverview(); break;
                    case 1: ShowConnectionStatusHelp(); break;
                    case 2: ShowDevicesListHelp(); break;
                    case 3: ShowServerMetricsHelp(); break;
                    case 4: ShowNetworkScannerHelp(); break;
                    case 5: ShowDeviceTypesSetup(); break;
                    case 6: ShowServerTroubleshooting(); break;
                    case 7: return;
                }

                Console.WriteLine("\nPress any key to return to server manual...");
                Console.ReadKey(true);
            }
        }

        private static void ShowServerOverview()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              SERVER FEATURES OVERVIEW                  ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("The Server module helps you monitor and manage network devices");
            Console.WriteLine("on your local network, including servers and computers.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("🌐 What can you do?");
            Console.ResetColor();
            Console.WriteLine("  • Check if devices are online (ping monitoring)");
            Console.WriteLine("  • View saved network devices in your list");
            Console.WriteLine("  • Monitor server CPU, RAM, and storage via SSH");
            Console.WriteLine("  • Scan your network to discover online devices");
            Console.WriteLine("  • Organize devices by type (Server, Router, Device)\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📋 Key Features:");
            Console.ResetColor();
            Console.WriteLine("  1. Connection Status  → Ping devices to check online status");
            Console.WriteLine("  2. Devices List       → View all saved network devices");
            Console.WriteLine("  3. Server Metrics     → Real-time SSH monitoring (CPU/RAM)");
            Console.WriteLine("  4. Network Scanner    → Discover devices on your network\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Data Storage:");
            Console.ResetColor();
            Console.WriteLine("  All device configurations are saved in 'devices.json'\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("⚠ Requirements:");
            Console.ResetColor();
            Console.WriteLine("  • Devices must be on the same network");
            Console.WriteLine("  • SSH access required for Server Metrics");
            Console.WriteLine("  • Valid credentials needed for Linux servers");
        }

        private static void ShowConnectionStatusHelp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          CONNECTION STATUS - USER GUIDE                ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📋 What it does:");
            Console.ResetColor();
            Console.WriteLine("  Pings all saved devices in your list to check if they're");
            Console.WriteLine("  currently online and displays response time in milliseconds.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("🔧 How to use:");
            Console.ResetColor();
            Console.WriteLine("  1. Select 'Connection Status' from Server Menu");
            Console.WriteLine("  2. Wait while each device is pinged (animated spinner)");
            Console.WriteLine("  3. View results showing [Online] or [Offline] status");
            Console.WriteLine("  4. Press any key to return to menu\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📝 Status Display:");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("  [Server] MyServer       [Online] 12ms");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  [Router] MainRouter     [Online] 3ms");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("  [Device] Laptop-01      [Offline]");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Features:");
            Console.ResetColor();
            Console.WriteLine("  • Color-coded device types (Server/Router/Device)");
            Console.WriteLine("  • Shows ping response time for online devices");
            Console.WriteLine("  • 2-second timeout per device");
            Console.WriteLine("  • Animated loading spinner during scan");
            Console.WriteLine("  • Prioritized display (Servers → Routers → Devices)\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("💡 Tip:");
            Console.ResetColor();
            Console.WriteLine("  Lower ping times (<50ms) indicate better network performance!");
        }

        private static void ShowDevicesListHelp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║            DEVICES LIST - USER GUIDE                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📋 What it does:");
            Console.ResetColor();
            Console.WriteLine("  Displays all network devices saved in your configuration");
            Console.WriteLine("  file with their names, IP addresses, and device types.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("🔧 How to use:");
            Console.ResetColor();
            Console.WriteLine("  1. Select 'Devices List' from Server Menu");
            Console.WriteLine("  2. View the formatted table of all devices");
            Console.WriteLine("  3. Note device names, IPs, and types");
            Console.WriteLine("  4. Press any key to return\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📝 Example Display:");
            Console.ResetColor();
            Console.WriteLine("  Name            Ip              Type");
            Console.WriteLine("  ────────────────────────────────────────");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  HomeServer      192.168.1.10    ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Server");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  MainRouter      192.168.1.1     ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Router");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  MyLaptop        192.168.1.45    ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Device");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Features:");
            Console.ResetColor();
            Console.WriteLine("  • Servers displayed first (prioritized)");
            Console.WriteLine("  • Color-coded device types");
            Console.WriteLine("  • Clean table formatting");
            Console.WriteLine("  • Shows data from 'devices.json'\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("💡 Device Types:");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("  Server  ");
            Console.ResetColor();
            Console.WriteLine("→ Linux/Windows servers with SSH access");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("  Router  ");
            Console.ResetColor();
            Console.WriteLine("→ Network routers and gateways");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("  Device  ");
            Console.ResetColor();
            Console.WriteLine("→ Computers, phones, IoT devices");
        }

        private static void ShowServerMetricsHelp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║         SERVER METRICS (SSH) - USER GUIDE              ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📋 What it does:");
            Console.ResetColor();
            Console.WriteLine("  Connects to a Linux server via SSH and displays real-time");
            Console.WriteLine("  system metrics including CPU usage, RAM, and disk storage.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("🔧 How to use:");
            Console.ResetColor();
            Console.WriteLine("  1. Select 'Server Metrics' from Server Menu");
            Console.WriteLine("  2. Choose a server from your devices list");
            Console.WriteLine("  3. Enter SSH username (if not saved)");
            Console.WriteLine("  4. Enter SSH password (if not saved)");
            Console.WriteLine("  5. View live metrics updating every second");
            Console.WriteLine("  6. Press any key to disconnect\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📝 Metrics Displayed:");
            Console.ResetColor();
            Console.WriteLine("  === Metrics: HomeServer ===");
            Console.WriteLine("  Last Update: 14:35:22");
            Console.WriteLine("  ────────────────────────────────────────");
            Console.WriteLine("  CPU: 23.5%");
            Console.WriteLine("  RAM: 3456/8192MB (42.19%)");
            Console.WriteLine("  Storage: 45G/120G used (120GB total)");
            Console.WriteLine("  ────────────────────────────────────────\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Features:");
            Console.ResetColor();
            Console.WriteLine("  • Real-time updates (refreshes every 1 second)");
            Console.WriteLine("  • Shows CPU percentage usage");
            Console.WriteLine("  • Displays RAM used/total with percentage");
            Console.WriteLine("  • Monitors root filesystem storage");
            Console.WriteLine("  • 5-second connection timeout");
            Console.WriteLine("  • Automatic credential prompting\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("⚠ Requirements:");
            Console.ResetColor();
            Console.WriteLine("  • Linux server with SSH enabled");
            Console.WriteLine("  • Valid username and password");
            Console.WriteLine("  • Server must be marked as 'Server' type in devices.json");
            Console.WriteLine("  • Commands: top, free, df must be available\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("💡 Tips:");
            Console.ResetColor();
            Console.WriteLine("  • Save SSH credentials in devices.json to avoid retyping");
            Console.WriteLine("  • High CPU (>80%) may indicate resource issues");
            Console.WriteLine("  • Keep RAM usage below 90% for optimal performance");
        }

        private static void ShowNetworkScannerHelp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          NETWORK SCANNER - USER GUIDE                  ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📋 What it does:");
            Console.ResetColor();
            Console.WriteLine("  Scans your local network to discover all online devices");
            Console.WriteLine("  by sending ping requests to IP address ranges.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("🔧 How to use:");
            Console.ResetColor();
            Console.WriteLine("  1. Select 'Ping network devices' from Server Menu");
            Console.WriteLine("  2. Choose an 'Anchor Device' from your saved list");
            Console.WriteLine("  3. Select scan depth:");
            Console.WriteLine("     [1] Quick Scan  → Same subnet (x.x.x.0-255)");
            Console.WriteLine("     [2] Deep Scan   → Entire network (x.0.0.0-x.255.255.255)");
            Console.WriteLine("  4. Wait for scan to complete (progress bar shown)");
            Console.WriteLine("  5. View list of discovered online devices\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📝 Scan Types:");
            Console.ResetColor();
            Console.WriteLine("  Quick Scan:");
            Console.WriteLine("    If anchor is 192.168.1.10 → Scans 192.168.1.0 to 192.168.1.255");
            Console.WriteLine("    Total: 256 addresses | Time: ~30 seconds\n");
            Console.WriteLine("  Deep Scan:");
            Console.WriteLine("    If anchor is 192.168.1.10 → Scans 192.0.0.0 to 192.255.255.255");
            Console.WriteLine("    Total: 16,777,216 addresses | Time: Several minutes\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📊 Progress Display:");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  [████████████████████████────────────────] 62.45% Complete");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📝 Results Example:");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("  [Found] ");
            Console.ResetColor();
            Console.Write("Device-1   (192.168.1.1     ) ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Online]");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("  [Found] ");
            Console.ResetColor();
            Console.Write("Device-2   (192.168.1.10    ) ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Online]");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Features:");
            Console.ResetColor();
            Console.WriteLine("  • Parallel scanning for maximum speed");
            Console.WriteLine("  • Real-time progress bar");
            Console.WriteLine("  • Results sorted by IP address");
            Console.WriteLine("  • 150ms timeout per IP (fast scanning)");
            Console.WriteLine("  • Thread-safe concurrent operations\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("⚠ Warning:");
            Console.ResetColor();
            Console.WriteLine("  • Deep Scan can take 10+ minutes!");
            Console.WriteLine("  • May generate high network traffic");
            Console.WriteLine("  • Some devices may block ICMP (ping) requests\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("💡 Best Practices:");
            Console.ResetColor();
            Console.WriteLine("  • Start with Quick Scan for home networks");
            Console.WriteLine("  • Use Deep Scan only for large enterprise networks");
            Console.WriteLine("  • Run during off-peak hours to avoid network disruption");
        }

        private static void ShowDeviceTypesSetup()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║         DEVICE TYPES & SETUP GUIDE                     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📋 Understanding Device Types:");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("🖥️  SERVER:");
            Console.ResetColor();
            Console.WriteLine("   • Linux/Unix servers with SSH access");
            Console.WriteLine("   • Can display CPU, RAM, and storage metrics");
            Console.WriteLine("   • Requires SSH username and password");
            Console.WriteLine("   • Example: Home NAS, Web server, Database server\n");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("📡 ROUTER:");
            Console.ResetColor();
            Console.WriteLine("   • Network gateway devices");
            Console.WriteLine("   • Typically your main internet router");
            Console.WriteLine("   • Usually has IP like 192.168.1.1 or 192.168.0.1");
            Console.WriteLine("   • Example: Home WiFi router, Modem\n");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("💻 DEVICE:");
            Console.ResetColor();
            Console.WriteLine("   • General computers and smart devices");
            Console.WriteLine("   • Windows PCs, Macs, smartphones, IoT devices");
            Console.WriteLine("   • No special requirements");
            Console.WriteLine("   • Example: Laptop, Desktop, Smart TV, Printer\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📝 devices.json Structure:");
            Console.ResetColor();
            Console.WriteLine("  [");
            Console.WriteLine("    {");
            Console.WriteLine("      \"IpAddres\": \"192.168.1.10\",");
            Console.WriteLine("      \"DeviceName\": \"HomeServer\",");
            Console.WriteLine("      \"DeviceType\": \"Server\",");
            Console.WriteLine("      \"SshUsername\": \"admin\",");
            Console.WriteLine("      \"SshPassword\": \"your_password\"");
            Console.WriteLine("   },");
            Console.WriteLine("    {");
            Console.WriteLine("      \"IpAddres\": \"192.168.1.1\",");
            Console.WriteLine("      \"DeviceName\": \"MainRouter\",");
            Console.WriteLine("      \"DeviceType\": \"Router\",");
            Console.WriteLine("      \"SshUsername\": \"none\",");
            Console.WriteLine("      \"SshPassword\": \"none\"");
            Console.WriteLine("    }");
            Console.WriteLine("  ]\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ How to Add Devices:");
            Console.ResetColor();
            Console.WriteLine("  1. Manually edit 'devices.json' with a text editor");
            Console.WriteLine("  2. Use Network Scanner to find devices");
            Console.WriteLine("  3. Manually add entry with correct format");
            Console.WriteLine("  4. Set SshUsername/SshPassword to 'none' if not a server\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("💡 Tips:");
            Console.ResetColor();
            Console.WriteLine("  • Store SSH passwords safely (consider encryption)");
            Console.WriteLine("  • Use meaningful device names");
            Console.WriteLine("  • Keep IP addresses static for servers (configure in router)");
        }

        private static void ShowServerTroubleshooting()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              TROUBLESHOOTING GUIDE                     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Device Shows [Offline] but is Online:");
            Console.ResetColor();
            Console.WriteLine("   • Firewall may be blocking ICMP (ping) requests");
            Console.WriteLine("   • Check Windows Firewall / Linux iptables settings");
            Console.WriteLine("   • Try pinging manually: ping 192.168.1.10");
            Console.WriteLine("   • Verify IP address is correct in devices.json\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ SSH Connection Timeout:");
            Console.ResetColor();
            Console.WriteLine("   • Ensure SSH is enabled on the server");
            Console.WriteLine("     Linux: sudo systemctl start ssh");
            Console.WriteLine("   • Check if SSH port 22 is open");
            Console.WriteLine("   • Verify username and password are correct");
            Console.WriteLine("   • Try connecting manually: ssh user@192.168.1.10\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Network Scanner Finds No Devices:");
            Console.ResetColor();
            Console.WriteLine("   • Ensure you're on the same network");
            Console.WriteLine("   • Check if 'Anchor Device' IP is correct");
            Console.WriteLine("   • Some networks block ping between devices (AP Isolation)");
            Console.WriteLine("   • Try Quick Scan first before Deep Scan\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Server Metrics Show Errors:");
            Console.ResetColor();
            Console.WriteLine("   • Commands missing? Install: sudo apt install procps");
            Console.WriteLine("   • Permission denied? Check SSH user permissions");
            Console.WriteLine("   • Connection drops? Check server's sshd_config timeout\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ devices.json Not Loading:");
            Console.ResetColor();
            Console.WriteLine("   • Check JSON syntax (missing commas, brackets)");
            Console.WriteLine("   • Use online JSON validator");
            Console.WriteLine("   • Ensure file is in the same folder as the program");
            Console.WriteLine("   • Check for extra/missing quotes\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Best Debugging Steps:");
            Console.ResetColor();
            Console.WriteLine("  1. Verify network connectivity (ping device manually)");
            Console.WriteLine("  2. Check firewall rules on both client and server");
            Console.WriteLine("  3. Test SSH access manually before using app");
            Console.WriteLine("  4. Review error messages carefully");
            Console.WriteLine("  5. Check devices.json formatting\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("💡 Common Solutions:");
            Console.ResetColor();
            Console.WriteLine("  • Restart router if devices won't respond");
            Console.WriteLine("  • Use static IPs for critical devices");
            Console.WriteLine("  • Keep SSH credentials up to date");
            Console.WriteLine("  • Test with Connection Status before Server Metrics");
        }
    }
}
