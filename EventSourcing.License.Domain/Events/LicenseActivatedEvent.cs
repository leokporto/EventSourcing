namespace EventSourcing.AnLicense.Domain.Events
{
	public class LicenseActivatedEvent : DomainEvent
	{
		public LicenseActivatedEvent() : base()
		{			
			ActivatedAtUtcTicks = DateTime.UtcNow.Ticks;
		}

		public override Guid StreamId => LicenseId;

		public Guid LicenseId { get; init; }

		public long ActivatedAtUtcTicks { get; }

		public string ActivatedBy { get; init; }

		public string LicenseKey { get; init; }

		public string LicenseType { get; init; }

		public string LicenseFamily { get; init; }
		
		public override string ToString()
		{
			string result = "--- LicenseActivatedEvent --- \n";
			result += $"LicenseId: {LicenseId}\n";
			result += $"ActivatedBy: {ActivatedBy}\n";
			result += $"ActivatedAtUtcTicks: {ActivatedAtUtcTicks}\n";
			result += $"LicenseKey: {LicenseKey}\n";
			result += $"LicenseType: {LicenseType}\n";
			result += $"LicenseFamily: {LicenseFamily}\n";
			return result;
		}
	}
}
