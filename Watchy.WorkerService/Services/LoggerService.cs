using System;
using System.IO;

namespace Watchy.WorkerService.Services
{
	public class LoggerService
	{
		private readonly string _directoryPath;
		public LoggerService()
		{
			_directoryPath = $"{AppDomain.CurrentDomain.BaseDirectory}Logs";
		}
		public void Log(string message)
		{
			string filePath = $"{_directoryPath}\\RateService_{DateTime.Now.ToShortDateString().Replace('/', '_')}.txt";
			if (!Directory.Exists(_directoryPath))
			{
				Directory.CreateDirectory(_directoryPath);
			}

			if (!File.Exists(filePath))
			{
				using StreamWriter sw = File.CreateText(filePath);
				sw.WriteLine(message);
			}
			else
			{
				using StreamWriter sw = File.AppendText(filePath);
				sw.WriteLine(message);
			}
			Console.WriteLine(message);
		}

		public void Error(Exception e, string message = "")
		{
			Log($"{message}\n\nException at {DateTime.Now.ToShortTimeString()}\n\n{e.Message}\n\n");
		}
	}
}
