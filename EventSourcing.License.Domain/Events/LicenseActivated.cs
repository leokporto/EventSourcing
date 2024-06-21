using EventSourcing.Core.Events;

namespace EventSourcing.License.Domain.Events
{
	public class LicenseActivated : DomainEvent
	{
		public LicenseActivated(Guid licenseId)
		{
			LicenseId = licenseId;
			ActivatedAtUtcTicks = DateTime.UtcNow.Ticks;
		}

		public override Guid StreamId => LicenseId;

		public Guid LicenseId { get; }

		public long ActivatedAtUtcTicks { get; }

		public string ActivatedBy { get; init; }

		public string LicenseKey { get; init; }

		public string LicenseType { get; init; }

		public string LicenseFamily { get; init; }
	}
}
