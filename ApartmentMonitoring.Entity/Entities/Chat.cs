using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentMonitoring.Entity.Entities
{
	public class Chat
	{
		public long Id { get; set; }
		public long ApartmentId { get; set; }
		public Apartment Apartment { get; set; }
		public long InitiatorId { get; set; }
		public User Initiator { get; set; }
		public long ReceiverId { get; set; }
		public User Receiver { get; set; }
		public List<ChatMessage> Messages { get; set; } = new();
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
