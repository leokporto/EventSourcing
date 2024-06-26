namespace EventSourcing.AnLicense.Domain.Events
{
	public class LicenseDeactivatedEvent : DomainEvent
	{
		public LicenseDeactivatedEvent() : base()
		{			
			DeactivatedAtUtcTicks = DateTime.UtcNow.Ticks;
		}

		public override Guid StreamId => LicenseId;

		public Guid LicenseId { get; init; }

		public long DeactivatedAtUtcTicks { get; }

		public string DeactivatedBy { get; init; }

		public string Reason { get; init; }

		public override string ToString()
		{
			string result = "--- LicenseDeactivatedEvent --- \n";
			result += $"LicenseId: {LicenseId}\n";
			result += $"DeactivatedBy: {DeactivatedBy}\n";
			result += $"DeactivatedAtUtcTicks: {DeactivatedAtUtcTicks}\n";
			result += $"Reason: {Reason}\n";
			return result;
		}
	}
}
