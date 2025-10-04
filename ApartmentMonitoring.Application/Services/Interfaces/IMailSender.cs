using System;
using System.Collections.Generic;
namespace ApartmentMonitoring.Application.Services.Interfaces
{
	public interface IMailSender
	{
		void Send(string to, string subject, string body);
	}
}
