using EventSourcing.Core.Events;

namespace EventSourcing.AnLicense.Domain.Events
{
	public class LicenseDeactivatedEvent : DomainEvent
	{
		public LicenseDeactivatedEvent(Guid licenseId) : base()
		{
			LicenseId = licenseId;
			DeactivatedAtUtcTicks = DateTime.UtcNow.Ticks;
		}

		public override Guid StreamId => LicenseId;

		public Guid LicenseId { get; }

		public long DeactivatedAtUtcTicks { get; }

		public string DeactivatedBy { get; init; }

		public string Reason { get; init; }
	}
}
