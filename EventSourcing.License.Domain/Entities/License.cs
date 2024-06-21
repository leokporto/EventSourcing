using EventSourcing.Core.Events;
using EventSourcing.License.Domain.Events;

namespace EventSourcing.License.Domain.Entities
{
	public class License : IAgregateRoot
	{
	
		public Guid LicenseId { get; private set; }

		public string CompanyName { get; private set; } = "";

		public string ProjectKey { get; private set; } = "";

		public string Project { get; private set; } = "";

		public string CreatedBy { get; private set; } = "";

		public DateTime CreatedAt { get; private set; }

		public DateTime LastUpdatedAt { get; private set; }

		public string LastUpdatedBy { get; private set; } = "";

		public string MachineName { get; private set; } = "";

		public string City { get; private set; } = "";

		public string State { get; private set; } = "";

		public string Country { get; private set; } = "";

		public string ProductVersion { get; private set; } = "";

		public string LicenseKey { get; private set; } = "";

		public string LicenseType { get; private set; } = "";

		public string LicenseFamily { get; private set; } = "";
		public string Reason { get; private set; } = "";

		public Guid? SubstituteLicenseId { get; private set; }

		public bool IsSubstituted { get; private set; }

		public bool IsActive { get; private set; }


		public void Apply(LicenseCreated @event)
		{
			LicenseId = @event.LicenseId;
			CompanyName = @event.CompanyName;
			ProjectKey = @event.ProjectKey;
			Project = @event.Project;
			CreatedBy = @event.CreatedBy;
			CreatedAt = new DateTime(@event.CreatedAtUtcTicks).ToLocalTime();
			LastUpdatedAt = CreatedAt;
			LastUpdatedBy = CreatedBy;
			
	}

		public void Apply(LicenseUpdated @event) 
		{ 
			LastUpdatedBy = @event.UpdatedBy;
			LastUpdatedAt = new DateTime(@event.UpdatedAtUtcTicks).ToLocalTime();
			if(!string.IsNullOrWhiteSpace(@event.CompanyName))
			{
				CompanyName = @event.CompanyName;
			}
			if(!string.IsNullOrWhiteSpace(@event.ProjectKey))
			{
				ProjectKey = @event.ProjectKey;
			}
			if(!string.IsNullOrWhiteSpace(@event.Project))
			{
				Project = @event.Project;
			}
			MachineName = string.IsNullOrWhiteSpace(@event.MachineName) ? "" : @event.MachineName;
			City = string.IsNullOrWhiteSpace(@event.City) ? "" : @event.City;
			State = string.IsNullOrWhiteSpace(@event.State) ? "" : @event.State;
			Country = string.IsNullOrWhiteSpace(@event.Country) ? "" : @event.Country;
			ProductVersion = string.IsNullOrWhiteSpace(@event.ProductVersion) ? "" : @event.ProductVersion;
			
		}

		public void Apply(LicenseActivated @event) 
		{
			LastUpdatedAt = new DateTime(@event.ActivatedAtUtcTicks).ToLocalTime();
			LastUpdatedBy = @event.ActivatedBy;
			LicenseKey = @event.LicenseKey;
			LicenseType = @event.LicenseType;
			LicenseFamily = @event.LicenseFamily;
			IsActive = true;
		}

		public void Apply(LicenseDeactivated @event) 
		{
			LastUpdatedAt = new DateTime(@event.DeactivatedAtUtcTicks).ToLocalTime();
			LastUpdatedBy = @event.DeactivatedBy;
			Reason = @event.Reason;
			IsActive = false;
		}

		public void Apply(LicenseSubstituted @event) 
		{
			LastUpdatedAt = new DateTime(@event.SubstitutedAtUtcTicks).ToLocalTime();
			LastUpdatedBy = @event.SubstitutedBy;
			SubstituteLicenseId = @event.SubstituteLicenseId;
			IsActive = false;
			IsSubstituted = true;
		}

		public void Apply(IDomainEvent @event)
		{
			switch(@event)
			{
				case LicenseCreated created:
					Apply(created);
					break;
				case LicenseUpdated updated:
					Apply(updated);
					break;
				case LicenseActivated activated:
					Apply(activated);
					break;
				case LicenseDeactivated deactivated:
					Apply(deactivated);
					break;
				case LicenseSubstituted substituted:
					Apply(substituted);
					break;
			}
		}
	}
}
