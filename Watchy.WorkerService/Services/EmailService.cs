using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Watchy.WorkerService.Repositories;

namespace Watchy.WorkerService.Services
{
	public class EmailService
	{
		private readonly string _smtpProvider = "smtp.gmail.com";
		private readonly string _hostEmailAddress = "gerrmosen@gmail.com";
		private readonly string _hostEmailPassword = "Personal@Gmail71";
		private readonly UserRepository _userRepository;
		private readonly LoggerService _loggerService;


		public EmailService(UserRepository userRepository, LoggerService loggerService)
			=> (_userRepository, _loggerService)
			= (userRepository, loggerService);

		public async Task SendEmail(string subject, string body, params string[] to)
		{
			var mail = new MailMessage();
			var smtpClient = new SmtpClient(_smtpProvider);

			mail.From = new MailAddress(_hostEmailAddress);
			mail.To.Add(string.Join(",", to));
			mail.Subject = subject;
			mail.Body = body;

			smtpClient.Port = 587;
			smtpClient.Credentials = new NetworkCredential(_hostEmailAddress, _hostEmailPassword);
			smtpClient.EnableSsl = true;

			try
			{
				_loggerService.Log($"Send email at {DateTime.Now.ToShortTimeString()}");
				await smtpClient.SendMailAsync(mail);
			}
			catch
			{
				_loggerService.Log($"FAILED!! Send email at {DateTime.Now.ToShortTimeString()}");
			}
		}

		public async Task SendEmailToAllUsers(string subject, string body)
		{
			var users = await _userRepository.GetAll();
			if (!users.Any()) return;
			var emails = users.Select(subscriptor => subscriptor.Email);
			await SendEmail(subject, body, emails.ToArray());
		}
	}
}
