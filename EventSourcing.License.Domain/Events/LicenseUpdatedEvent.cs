namespace EventSourcing.AnLicense.Domain.Events
{
	public class LicenseUpdatedEvent : DomainEvent
	{
		public LicenseUpdatedEvent() : base()
		{			
			UpdatedAtUtcTicks = DateTime.UtcNow.Ticks;			
		}

		public override Guid StreamId => LicenseId;

		public Guid LicenseId { get; init; }

		public long UpdatedAtUtcTicks { get; }

		public string UpdatedBy { get; init; }

		public string? CompanyName { get; init; }

		public string? ProjectKey { get; init; }

		public string? Project { get; init; }

		public string? MachineName { get; init; }

		public string? City { get; set; }

		public string? State { get; set; }

		public string? Country { get; set; }

		public string? ProductVersion { get; init; }

		
		public override string ToString()
		{
			string result = "--- LicenseUpdatedEvent --- \n";
			result += $"LicenseId: {LicenseId}\n";
			result += $"UpdatedBy: {UpdatedBy}\n";
			result += $"UpdatedAtUtcTicks: {UpdatedAtUtcTicks}\n";
			result += $"MachineName: {MachineName}\n";
			result += $"City: {City}\n";
			result += $"State: {State}\n";
			result += $"Country: {Country}\n";
			result += $"ProductVersion: {ProductVersion}\n";
			result += $"Project: {Project}\n";
			result += $"ProjectKey: {ProjectKey}\n";
			result += $"CompanyName: {CompanyName}\n";
			return result;
		}
	}
}
