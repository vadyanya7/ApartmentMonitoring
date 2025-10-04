namespace ApartmentMonitoring.Contracts.Apartments
{
	public class GetApartmentsRequest
	{
		public long LastId { get; set; }
		public int Count { get; set; } = 10;

		public ApartmentFilter? ApartmentFilter { get; set; }
	}
}
