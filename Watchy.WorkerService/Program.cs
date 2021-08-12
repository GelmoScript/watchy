using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watchy.WorkerService.Contexts;
using Watchy.WorkerService.Repositories;
using Watchy.WorkerService.Services;

namespace Watchy.WorkerService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseWindowsService(options =>
				{
					options.ServiceName = "WatchyService";
				})
				.ConfigureServices((hostContext, services) =>
				{
					services.AddHostedService<Worker>();
					services.AddTransient<ResourceService>();
					services.AddTransient<LoggerService>();
					services.AddTransient<EmailService>();
					services.AddTransient<ResourceDetailRepository>();
					services.AddTransient<UserRepository>();
					services.AddTransient<WatchyDbContext>();
				});
	}
}
