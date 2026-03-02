/*
 * PROJECT: TriDev System Vital Dashboard (Option A)
 * TARGET: tridevhungary.com Monitoring Agent
 * * CURRENT GOAL (Junior):
 * - Capture real-time CPU & RAM utilization using Microsoft.Extensions.Diagnostics.
 * - Render a live-updating dashboard in the console using Spectre.Console.
 * - Implement basic "Threshold Logic" to identify system stress.
 * * UPCOMING MILESTONES (Medior Learning):
 * 1. LOGGING: Add 'Serilog' to write CPU spikes (>90%) to a local "alerts.txt" file.
 * 2. ASYNC: Move the monitoring logic into a 'BackgroundService' (Worker Service).
 * 3. WEB INTEGRATION: Use SignalR to push these metrics to a Blazor dashboard 
 * on tridevhungary.com for remote monitoring.
 * * TECH STACK: .NET 9, Spectre.Console, ResourceMonitoring SDK.
 */

//============================-Dependencies-============================\\
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.ResourceMonitoring;
using Spectre.Console;
//======================================================================\\


namespace TriDev_System_Vital_Dashboard
{
    internal class SystemFunctions
    {
        public static void ShowMenu()
        {
            string[] menuOptions =
            {
                "Show Metrics",
                "Leave program"
            };

            int hovered = 0;
            while (true)
            {

                for (int i = 0;  i < menuOptions.Length; i++)
                {
                    if(i == hovered)
                    {
                        AnsiConsole.WriteLine($"[red] -{menuOptions[i]}");
                    }
                    else
                    {
                        AnsiConsole.WriteLine($" {menuOptions[i]}");
                    }
                }

                Console.ReadKey();
            }
        }


        /// <summary>
        /// Displays real-time CPU and memory usage metrics in a live console table with visual indicators.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task CheckSystemUsage()
        {
            Task runBackgroundSystemCheck = Task.Run(async () =>
            {
                PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                PerformanceCounter memCounter = new PerformanceCounter("Memory", "Available MBytes");

                var services = new ServiceCollection()
                    .AddLogging()
                    .AddResourceMonitoring()
                    .BuildServiceProvider();

                var monitor = services.GetRequiredService<IResourceMonitor>();

                var table = new Table().Border(TableBorder.Rounded);
                table.AddColumn("[yellow]Metric[/]");
                table.AddColumn("[yellow]Usage[/]");
                table.AddColumn("[yellow]Visual[/]");


                await AnsiConsole.Live(table).StartAsync(async ctx =>
                {
                    while (true)
                    {
                        float cpuUsage = cpuCounter.NextValue();
                        float availableMem = memCounter.NextValue();

                        float memUsagePercent = (1 - (availableMem / 16384)) * 100;

                        table.Rows.Clear();

                        string cpuColor = GetColor(cpuUsage);
                        string memoryColor = GetColor(memUsagePercent);

                        string cpuBar = new string('█', (int)(cpuUsage / 5)).PadRight(20, '░');
                        string memoryBar = new string('█', (int)(memUsagePercent / 5)).PadRight(20, '░');

                        table.AddRow("CPU Usage", $"{cpuUsage:F1}%", $"[{cpuColor}]{cpuBar}[/]");
                        table.AddEmptyRow();
                        table.AddRow("Memory Left to use", $"{memUsagePercent:F1}%", $"[{memoryColor}]{memoryBar}[/]");

                        ctx.Refresh();
                        await Task.Delay(1000);
                    }
                });
            });
            await runBackgroundSystemCheck;


            static string GetColor(double value)
            {
                if (value > 80) return "red";
                if (value > 50) return "yellow";
                if (value > 20) return "green";
                else return "blue";
            }
        
        }
    }
}
