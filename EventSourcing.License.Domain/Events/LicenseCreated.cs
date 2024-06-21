using EventSourcing.Core.Events;

namespace EventSourcing.License.Domain.Events
{
	public class LicenseCreated : DomainEvent
	{
		public LicenseCreated()
		{ 
			LicenseId = new Guid();
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
