using System;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using Watchy.WorkerService.Constants;
using System.IO;
using System.Linq;
using System.Management;

namespace Watchy.WorkerService.Services
{
	public class DeviceService
	{
		public const double MaxRamAllowed = 20;
		public const double MaxCpuAllowed = 20;
		public const double MaxDiskAllowed = 20;
		public static double GetRamUsage()
		{
			var wmiObject = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
			var memoryValues = wmiObject.Get().Cast<ManagementObject>().Select(managementObject => new
			{
				FreePhysicalMemory = double.Parse(managementObject["FreePhysicalMemory"].ToString()),
				TotalVisibleMemorySize = double.Parse(managementObject["TotalVisibleMemorySize"].ToString())
			}).FirstOrDefault();
			double memoryUsed = memoryValues.TotalVisibleMemorySize - memoryValues.FreePhysicalMemory;
			double ramUsage = memoryUsed * 100 / memoryValues.TotalVisibleMemorySize;
			return Round(ramUsage);
		}

		public static double GetCpuUsage()
		{
			var cpuCounter = new PerformanceCounter()
			{
				CategoryName = PerformanceCounterDriveInfo.Processor,
				CounterName = PerformanceCounterDriveInfo.ProcessorTime,
				InstanceName = PerformanceCounterDriveInfo.Total
			};
			cpuCounter.NextValue();
			Thread.Sleep(1000);
			return Round(cpuCounter.NextValue());
		}

		public static double GetDiskUsage()
		{
			string mainDisk = "C:\\";
			var driveInfo = DriveInfo.GetDrives().FirstOrDefault(drive => drive.Name == mainDisk);
			double diskUsed = driveInfo.TotalSize - driveInfo.TotalFreeSpace;
			double diskUsage = diskUsed * 100 / driveInfo.TotalSize;
			return Round(diskUsage);
		}

		private static double Round(double value)
			=> Math.Round(value, 2);
	}
}
