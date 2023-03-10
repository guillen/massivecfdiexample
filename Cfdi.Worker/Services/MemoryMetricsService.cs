using Cfdi.Domain.Services;
using Cfdi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Worker.Services
{
    internal class MemoryMetricsService : IMemoryMetricsService
    {
        public Metric GetDiskWindowsMetrics(string diskName)
        {
            DriveInfo drive = new DriveInfo(diskName);

            var totalBytes = drive.TotalSize;
            var freeBytes = drive.AvailableFreeSpace;

            var metrics = new Metric();
            metrics.Free = freeBytes / (1024 * 1024);
            metrics.Total = totalBytes / (1024 * 1024);
            metrics.Used = totalBytes - freeBytes;

            return metrics;
        }

        public Metric GetRAMWindowsMetrics()
        {
            var output = "";

            var info = new ProcessStartInfo();
            info.FileName = "wmic";
            info.Arguments = "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value";
            info.RedirectStandardOutput = true;

            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
            }

            var lines = output.Trim().Split("\n");
            var freeMemoryParts = lines[0].Split("=", StringSplitOptions.RemoveEmptyEntries);
            var totalMemoryParts = lines[1].Split("=", StringSplitOptions.RemoveEmptyEntries);

            var metrics = new Metric();
            metrics.Total = Math.Round(double.Parse(totalMemoryParts[1]) / 1024, 0);
            metrics.Free = Math.Round(double.Parse(freeMemoryParts[1]) / 1024, 0);
            metrics.Used = metrics.Total - metrics.Free;

            return metrics;
        }
    }
}
