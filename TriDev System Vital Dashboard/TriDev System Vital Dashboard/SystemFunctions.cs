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
        

    }
}
