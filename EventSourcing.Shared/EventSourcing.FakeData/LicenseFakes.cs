using EventSourcing.AnLicense.Domain.Events;
using EventSourcing.FakeData.Fakers;

namespace EventSourcing.FakeData
{
	public static class LicenseFakes
	{
		
		public static Dictionary<Guid, SortedList<long, DomainEvent>> GetFakeLicenseEvents(int totalStreams) 
		{
			if(totalStreams <=0) 
			{
				return new Dictionary<Guid, SortedList<long, DomainEvent>>();
			}

			Dictionary<Guid, SortedList<long, DomainEvent>> allStreams = new Dictionary<Guid, SortedList<long, DomainEvent>>();
			List<LicenseCreatedEvent> licenseCreatedEvents = new LicenseCreatedFaker().Generate(totalStreams);
			
			Random rdmAddDeactivated = new Random();

			foreach(var createdEvt in licenseCreatedEvents) 
			{ 
				SortedList<long, DomainEvent> events = new SortedList<long, DomainEvent>
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
			  

			  