using EventSourcing.Core.Events;

namespace EventSourcing.AnLicense.Domain.Events
{
	public class LicenseUpdatedEvent : DomainEvent
	{
		public LicenseUpdatedEvent(Guid licenseId, string user) : base()
		{
			LicenseId = licenseId;
			UpdatedAtUtcTicks = DateTime.UtcNow.Ticks;
			UpdatedBy = user;
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
