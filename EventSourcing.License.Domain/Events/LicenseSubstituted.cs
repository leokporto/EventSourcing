using EventSourcing.Core.Events;

namespace EventSourcing.AnLicense.Domain.Events
{
	public class LicenseSubstitutedEvent : DomainEvent
    {
		public LicenseSubstitutedEvent(Guid licenseId, Guid substituteLicenseId) : base()
		{ 
			LicenseId = licenseId;
			SubstituteLicenseId = substituteLicenseId;
			SubstitutedAtUtcTicks = DateTime.UtcNow.Ticks;
		}

		public override Guid StreamId => LicenseId;

		public Guid LicenseId { get; }

		public Guid SubstituteLicenseId { get; }

		public string SubstitutedBy { get; init; }

		public string Reason { get; init; }

		public long SubstitutedAtUtcTicks { get; }

	}
}
