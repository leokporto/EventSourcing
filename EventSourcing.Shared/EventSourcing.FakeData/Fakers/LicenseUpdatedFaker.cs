using Bogus;
using EventSourcing.AnLicense.Domain.Events;

namespace EventSourcing.FakeData.Fakers
{
	public class LicenseUpdatedFaker : Faker<LicenseUpdatedEvent>
	{
		public LicenseUpdatedFaker(Guid streamId)
		{
			CustomInstantiator(f => new LicenseUpdatedEvent(streamId, $"{f.Name.FirstName()}.{f.Name.LastName()}"));
			RuleFor(lu => lu.UpdatedAtUtcTicks, x => x.Date.Recent(2, DateTime.UtcNow.Date.AddDays(-2)).Ticks);
			RuleFor(lu => lu.MachineName, x => x.Vehicle.Model());
			RuleFor(lu => lu.City, x => x.Address.City());
			RuleFor(lu => lu.State, x => x.Address.State());
			RuleFor(lu => lu.Country, x => x.Address.Country());
			RuleFor(lu => lu.ProductVersion, "an-9.2");
		}
	}
}
