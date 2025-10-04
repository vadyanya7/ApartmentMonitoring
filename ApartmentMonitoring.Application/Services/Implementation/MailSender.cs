using ApartmentMonitoring.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentMonitoring.Application.Services.Implementation
{
	public class MailSender : IMailSender
	{
		public void Send(string to, string subject, string body)
		{
			Console.WriteLine("Message sending to " + to+ ".");
		}
	}
}
