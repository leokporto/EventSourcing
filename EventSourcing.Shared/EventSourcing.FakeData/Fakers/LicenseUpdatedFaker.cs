using Bogus;
using EventSourcing.AnLicense.Domain.Events;

namespace EventSourcing.FakeData.Fakers
{
	public class LicenseUpdatedFaker : Faker<LicenseUpdatedEvent>
	{
		public LicenseUpdatedFaker(Guid streamId)
		{
			RuleFor(lu => lu.LicenseId, streamId);
			RuleFor(lu => lu.UpdatedBy, f => $"{f.Name.FirstName()}.{f.Name.LastName()}");
			RuleFor(lu => lu.UpdatedAtUtcTicks, x => x.Date.Recent(2, DateTime.UtcNow.Date.AddDays(-2)).Ticks);
			RuleFor(lu => lu.MachineName, x => x.Vehicle.Model());
			RuleFor(lu => lu.City, x => x.Address.City());
			RuleFor(lu => lu.State, x => x.Address.State());
			RuleFor(lu => lu.Country, x => x.Address.Country());
			RuleFor(lu => lu.ProductVersion, "an-9.2");
			//RuleFor(lu => lu.Project, "");
			//RuleFor(lu => lu.ProjectKey, "");
			//RuleFor(lu => lu.CompanyName, "");
		}
	}
}
