using EventSourcing.Core.Events;

namespace EventSourcing.License.Domain.Events
{
	public class LicenseDeactivated : DomainEvent
	{
		public LicenseDeactivated(Guid licenseId)
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
