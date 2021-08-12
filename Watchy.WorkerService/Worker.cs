using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Watchy.WorkerService.Helpers;
using Watchy.WorkerService.Services;

namespace Watchy.WorkerService
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
		private readonly ResourceService _rateService;
		private readonly LoggerService _loggerService;

		public Worker(ILogger<Worker> logger, ResourceService rateService, LoggerService loggerService)
			=> (_logger, _rateService, _loggerService) = (logger, rateService, loggerService);

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_loggerService.Log($"Service started at {DateTimeOffset.Now}");
			while (!stoppingToken.IsCancellationRequested)
			{
				await _rateService.CheckResources();
				_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
				_loggerService.Log($"ExchangeChecked at {DateTimeOffset.Now}");
				await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
			}
			_loggerService.Log($"Service stopped at {DateTimeOffset.Now}");

		}
	}
}
