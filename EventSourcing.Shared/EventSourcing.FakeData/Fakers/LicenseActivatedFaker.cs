using Bogus;
using EventSourcing.AnLicense.Domain.Events;

namespace EventSourcing.FakeData.Fakers
{
	public class LicenseActivatedFaker : Faker<LicenseActivatedEvent>
	{
		public LicenseActivatedFaker(Guid streamId)
		{
			CustomInstantiator(f => new LicenseActivatedEvent(streamId));
			RuleFor(lu => lu.ActivatedAtUtcTicks, x => x.Date.Recent(2, DateTime.UtcNow.Date).Ticks);
			RuleFor(la => la.ActivatedBy, x => $"{x.Name.FirstName()}.{x.Name.LastName()}");
			RuleFor(la => la.LicenseKey, x => $"SO{x.Random.Number(1000, 3000)}");
			RuleFor(la => la.LicenseType, x => x.PickRandom("Instrument", "Panel", "Machine", "Cell", "Line", "Area", "Plant", "ServerSmall", "ServerMedium", "ServerLarge", "Unlimited"));
			RuleFor(la => la.LicenseFamily, x => x.PickRandom("Express", "HMI", "Gateway", "Enterprise"));
		}
	}
}
