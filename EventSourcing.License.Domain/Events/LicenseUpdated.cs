using EventSourcing.Core.Events;

namespace EventSourcing.License.Domain.Events
{
	public class LicenseUpdated : DomainEvent
	{
		public LicenseUpdated(Guid licenseId, string user)
		{
			LicenseId = licenseId;
			UpdatedAtUtcTicks = DateTime.UtcNow.Ticks;
		}

		public override Guid StreamId => LicenseId;

		public Guid LicenseId { get; }

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
	}
}
