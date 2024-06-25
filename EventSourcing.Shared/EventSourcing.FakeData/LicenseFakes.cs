using EventSourcing.AnLicense.Domain.Events;
using EventSourcing.Core.Events;
using EventSourcing.FakeData.Fakers;

namespace EventSourcing.FakeData
{
	public static class LicenseFakes
	{
		public const int TOTAL_STREAMS = 5;

		public static Dictionary<Guid, SortedList<long, IDomainEvent>> GetLicenseEvents() 
		{
			Dictionary<Guid, SortedList<long, IDomainEvent>> allStreams = new Dictionary<Guid, SortedList<long, IDomainEvent>>();
			List<LicenseCreatedEvent> licenseCreatedEvents = new LicenseCreatedFaker().Generate(TOTAL_STREAMS);
			
			Random rdmAddDeactivated = new Random();

			foreach(var createdEvt in licenseCreatedEvents) 
			{ 
				SortedList<long, IDomainEvent> events = new SortedList<long, IDomainEvent>
				{
					{ createdEvt.CreatedAtUtcTicks, createdEvt }
				};

				var altered = new LicenseUpdatedFaker(createdEvt.StreamId).Generate();
				events.Add(altered.UpdatedAtUtcTicks, altered);

				var activated = new LicenseActivatedFaker(createdEvt.StreamId).Generate();
				events.Add(activated.ActivatedAtUtcTicks, activated);

				int result = rdmAddDeactivated.Next(0, 100);
				if(result > 80) 
				{
					var deactivated = new LicenseDeactivatedFaker(createdEvt.StreamId).Generate();
					events.Add(deactivated.DeactivatedAtUtcTicks, deactivated);
				}

				allStreams.Add(createdEvt.StreamId, events);
			}

			return allStreams;
		}
	}

	

    
}
			  

			  