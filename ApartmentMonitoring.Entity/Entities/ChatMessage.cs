using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentMonitoring.Entity.Entities
{
	public class ChatMessage
	{
		public long Id { get; set; }
		public long ChatId { get; set; }
		public Chat Chat { get; set; }
		public long SenderId { get; set; }
		public User Sender { get; set; }
		public string Body { get; set; } = string.Empty;
		public DateTime SentAt { get; set; } = DateTime.UtcNow;
		public bool IsRead { get; set; } = false;
	}
}
