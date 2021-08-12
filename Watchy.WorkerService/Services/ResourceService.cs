using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchy.WorkerService.Models;
using Watchy.WorkerService.Repositories;

namespace Watchy.WorkerService.Services
{
	public class ResourceService
	{
		private readonly ResourceDetailRepository _resourceDetailRepository;
		private readonly EmailService _emailService;
		private readonly LoggerService _loggerService;

		public ResourceService(ResourceDetailRepository resourceDetailRepository, EmailService emailService, LoggerService loggerService)
			=> (_resourceDetailRepository, _emailService, _loggerService) 
			= (resourceDetailRepository, emailService, loggerService);

		public async Task CheckResources()
		{
			double ramUsage = 0, diskUsage = 0, cpuUsage = 0;
			ResourceDetail resourceDetails = null;

			(ramUsage, diskUsage, cpuUsage) =
				(DeviceService.GetRamUsage(), DeviceService.GetDiskUsage(), DeviceService.GetCpuUsage());
			try
			{
				resourceDetails = await SaveResourceDetails(ramUsage, diskUsage, cpuUsage);
			}
			catch(Exception err)
			{
				_loggerService.Error(err, "Error saving a resource detail");
				return;
			}


			string message = "";
			bool sendEmail = false;

			if (ramUsage > DeviceService.MaxRamAllowed)
			{
				message += $"\n\nLa memoria RAM está al {ramUsage}% de su capacidad, " +
					$"sobrepasando el límite establecido de {DeviceService.MaxRamAllowed}%.\n" +
					$"Fecha: {resourceDetails.VerificationDate.Value.ToShortDateString()}\n" +
					$"Hora: {resourceDetails.VerificationDate.Value.ToShortTimeString()}";
				sendEmail = true;
			}

			if (diskUsage > DeviceService.MaxDiskAllowed)
			{
				message += $"\n\nEl disco está al {diskUsage}% de su capacidad, " +
					$"sobrepasando el límite establecido de {DeviceService.MaxDiskAllowed}%.\n" +
					$"Fecha: {resourceDetails.VerificationDate.Value.ToShortDateString()}\n" +
					$"Hora: {resourceDetails.VerificationDate.Value.ToShortTimeString()}";
				sendEmail = true;
			}

			if (cpuUsage > DeviceService.MaxCpuAllowed)
			{
				message += $"\n\nEl disco está al {cpuUsage}% de su capacidad, " +
					$"sobrepasando el límite establecido de {DeviceService.MaxCpuAllowed}%.\n" +
					$"Fecha: {resourceDetails.VerificationDate.Value.ToShortDateString()}\n" +
					$"Hora: {resourceDetails.VerificationDate.Value.ToShortTimeString()}";
				sendEmail = true;
			}

			if (sendEmail)
			{
				//string subject = "Alerta de uso de dispositivo";
				//await _emailService.SendEmailToAllUsers(subject, message);
			}
		}

		public async Task<ResourceDetail> SaveResourceDetails(double ramUsage, double diskUsage, double cpuUsage)
		{
			var resourceDetail = new ResourceDetail()
			{
				VerificationDate = DateTime.Now,
				RamUse = ramUsage,
				CpuUse = cpuUsage,
				DiskUse = diskUsage
			};

			return await _resourceDetailRepository.Create(resourceDetail);
		}

	}
}
