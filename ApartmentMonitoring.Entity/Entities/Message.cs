
using ApartmentMonitoring.Entity.Enums;

namespace ApartmentMonitoring.Entity.Entities
{
	public class Message
	{
		public long Id { get; set; }
		public Source Source { get; set; }
		public string Group { get; set; }
		public string From { get; set; }
		public string Body {  get; set; }

		public List<string>? ImageLinks { get; set; }

	}
}
