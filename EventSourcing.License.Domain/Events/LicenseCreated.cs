using EventSourcing.Core.Events;

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
	}
}
