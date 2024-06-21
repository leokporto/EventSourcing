using EventSourcing.Core.Events;

namespace EventSourcing.License.Domain.Events
{
	public class LicenseSubstituted : DomainEvent
    {
		public LicenseSubstituted(Guid licenseId, Guid substituteLicenseId) 
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
