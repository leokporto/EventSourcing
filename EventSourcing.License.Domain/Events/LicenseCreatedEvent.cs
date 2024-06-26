namespace EventSourcing.AnLicense.Domain.Events
{
	public class LicenseCreatedEvent : DomainEvent
	{
		public LicenseCreatedEvent() : base()
		{
			LicenseId = Guid.NewGuid();
			CreatedAtUtcTicks = DateTime.UtcNow.Ticks;			
		}

		public override Guid StreamId => LicenseId;

		public Guid LicenseId { get; }

		public string CompanyName { get; init; }

		public string ProjectKey { get; init; }

		public string Project { get; init; }	

		public string CreatedBy { get; init; }

		public override string ToString()
		{
			string result = "--- LicenseCreatedEvent --- \n";
			result += $"LicenseId: {LicenseId}\n";
			result += $"CompanyName: {CompanyName}\n";
			result += $"ProjectKey: {ProjectKey}\n";
			result += $"Project: {Project}\n";
			result += $"CreatedBy: {CreatedBy}\n";
			result += $"CreatedAtUtcTicks: {CreatedAtUtcTicks}\n";
			return result;
		}
	}
}
