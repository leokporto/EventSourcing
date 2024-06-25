using Bogus;
using EventSourcing.AnLicense.Domain.Events;

namespace EventSourcing.FakeData.Fakers
{
	public class LicenseCreatedFaker : Faker<LicenseCreatedEvent>
	{
		public LicenseCreatedFaker()
		{
			RuleFor(lc => lc.CompanyName, x => x.Company.CompanyName());
			RuleFor(lc => lc.CreatedAtUtcTicks, x => x.Date.Recent(3, DateTime.UtcNow.AddDays(-10)).Ticks);
			RuleFor(lc => lc.CreatedBy, x => $"{x.Name.FirstName()}.{x.Name.LastName()}");
			RuleFor(lc => lc.Project, x => x.Commerce.ProductName());
			RuleFor(lc => lc.ProjectKey, x => $"{x.Date.Past(3, DateTime.UtcNow).ToString("yy")}.{x.Random.Int(1, 999).ToString("###")}");
		}
	}
}
