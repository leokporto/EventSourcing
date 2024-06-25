﻿using Bogus;
using EventSourcing.AnLicense.Domain.Events;

namespace EventSourcing.FakeData.Fakers
{
	public class LicenseDeactivatedFaker : Faker<LicenseDeactivatedEvent>
	{
		public LicenseDeactivatedFaker(Guid streamId)
		{
			CustomInstantiator(f => new LicenseDeactivatedEvent(streamId));
			RuleFor(ld => ld.DeactivatedBy, x => $"{x.Name.FirstName()}.{x.Name.LastName()}");
			RuleFor(ld => ld.Reason, x => x.Lorem.Sentence());
		}
	}
}
