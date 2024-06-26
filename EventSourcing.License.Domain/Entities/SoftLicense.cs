using EventSourcing.Core.Events;
using EventSourcing.AnLicense.Domain.Events;

namespace EventSourcing.AnLicense.Domain.Entities
{
	/// <summary>
	/// Represents a soft license entity.
	/// </summary>
	public class SoftLicense : IAgregateRoot
	{
		/// <summary>
		/// Gets or sets the license ID.
		/// </summary>
		public Guid LicenseId { get; private set; }

		/// <summary>
		/// Gets or sets the company name.
		/// </summary>
		public string CompanyName { get; private set; } = "";

		/// <summary>
		/// Gets or sets the project key.
		/// </summary>
		public string ProjectKey { get; private set; } = "";

		/// <summary>
		/// Gets or sets the project.
		/// </summary>
		public string Project { get; private set; } = "";

		/// <summary>
		/// Gets or sets the created by user.
		/// </summary>
		public string CreatedBy { get; private set; } = "";

		/// <summary>
		/// Gets or sets the created date and time.
		/// </summary>
		public DateTime CreatedAt { get; private set; }

		/// <summary>
		/// Gets or sets the last updated date and time.
		/// </summary>
		public DateTime LastUpdatedAt { get; private set; }

		/// <summary>
		/// Gets or sets the last updated by user.
		/// </summary>
		public string LastUpdatedBy { get; private set; } = "";

		/// <summary>
		/// Gets or sets the machine name.
		/// </summary>
		public string MachineName { get; private set; } = "";

		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		public string City { get; private set; } = "";

		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		public string State { get; private set; } = "";

		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		public string Country { get; private set; } = "";

		/// <summary>
		/// Gets or sets the product version.
		/// </summary>
		public string ProductVersion { get; private set; } = "";

		/// <summary>
		/// Gets or sets the license key.
		/// </summary>
		public string LicenseKey { get; private set; } = "";

		/// <summary>
		/// Gets or sets the license type.
		/// </summary>
		public string LicenseType { get; private set; } = "";

		/// <summary>
		/// Gets or sets the license family.
		/// </summary>
		public string LicenseFamily { get; private set; } = "";

		/// <summary>
		/// Gets or sets the reason.
		/// </summary>
		public string Reason { get; private set; } = "";

		/// <summary>
		/// Gets or sets the substitute license ID.
		/// </summary>
		public Guid? SubstituteLicenseId { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether the license is substituted.
		/// </summary>
		public bool IsSubstituted { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether the license is active.
		/// </summary>
		public bool IsActive { get; private set; }

		/// <summary>
		/// Applies the license created event to update the entity.
		/// </summary>
		/// <param name="event">The license created event.</param>
		public void Apply(LicenseCreatedEvent @event)
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

		/// <summary>
		/// Applies the license updated event to update the entity.
		/// </summary>
		/// <param name="event">The license updated event.</param>
		public void Apply(LicenseUpdatedEvent @event)
		{
			LastUpdatedBy = @event.UpdatedBy;
			LastUpdatedAt = new DateTime(@event.UpdatedAtUtcTicks).ToLocalTime();
			if (!string.IsNullOrWhiteSpace(@event.CompanyName))
			{
				CompanyName = @event.CompanyName;
			}
			if (!string.IsNullOrWhiteSpace(@event.ProjectKey))
			{
				ProjectKey = @event.ProjectKey;
			}
			if (!string.IsNullOrWhiteSpace(@event.Project))
			{
				Project = @event.Project;
			}
			MachineName = string.IsNullOrWhiteSpace(@event.MachineName) ? "" : @event.MachineName;
			City = string.IsNullOrWhiteSpace(@event.City) ? "" : @event.City;
			State = string.IsNullOrWhiteSpace(@event.State) ? "" : @event.State;
			Country = string.IsNullOrWhiteSpace(@event.Country) ? "" : @event.Country;
			ProductVersion = string.IsNullOrWhiteSpace(@event.ProductVersion) ? "" : @event.ProductVersion;
		}

		/// <summary>
		/// Applies the license activated event to update the entity.
		/// </summary>
		/// <param name="event">The license activated event.</param>
		public void Apply(LicenseActivatedEvent @event)
		{
			LastUpdatedAt = new DateTime(@event.ActivatedAtUtcTicks).ToLocalTime();
			LastUpdatedBy = @event.ActivatedBy;
			LicenseKey = @event.LicenseKey;
			LicenseType = @event.LicenseType;
			LicenseFamily = @event.LicenseFamily;
			IsActive = true;
		}

		/// <summary>
		/// Applies the license deactivated event to update the entity.
		/// </summary>
		/// <param name="event">The license deactivated event.</param>
		public void Apply(LicenseDeactivatedEvent @event)
		{
			LastUpdatedAt = new DateTime(@event.DeactivatedAtUtcTicks).ToLocalTime();
			LastUpdatedBy = @event.DeactivatedBy;
			Reason = @event.Reason;
			IsActive = false;
		}

		/// <summary>
		/// Applies the license substituted event to update the entity.
		/// </summary>
		/// <param name="event">The license substituted event.</param>
		public void Apply(LicenseSubstitutedEvent @event)
		{
			LastUpdatedAt = new DateTime(@event.SubstitutedAtUtcTicks).ToLocalTime();
			LastUpdatedBy = @event.SubstitutedBy;
			SubstituteLicenseId = @event.SubstituteLicenseId;
			IsActive = false;
			IsSubstituted = true;
		}

		/// <summary>
		/// Applies the domain event to update the entity.
		/// </summary>
		/// <param name="event">The domain event.</param>
		public void Apply(IDomainEvent @event)
		{
			switch (@event)
			{
				case LicenseCreatedEvent created:
					Apply(created);
					break;
				case LicenseUpdatedEvent updated:
					Apply(updated);
					break;
				case LicenseActivatedEvent activated:
					Apply(activated);
					break;
				case LicenseDeactivatedEvent deactivated:
					Apply(deactivated);
					break;
				case LicenseSubstitutedEvent substituted:
					Apply(substituted);
					break;
			}
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			string result = "--- AnLicense --- \n";
			result += $"LicenseId: {LicenseId}\n";
			result += $"CompanyName: {CompanyName}\n";
			result += $"ProjectKey: {ProjectKey}\n";
			result += $"Project: {Project}\n";
			result += $"CreatedBy: {CreatedBy}\n";
			result += $"CreatedAt: {CreatedAt}\n";
			result += $"LastUpdatedAt: {LastUpdatedAt}\n";
			result += $"LastUpdatedBy: {LastUpdatedBy}\n";
			result += $"MachineName: {MachineName}\n";
			result += $"City: {City}\n";
			result += $"State: {State}\n";
			result += $"Country: {Country}\n";
			result += $"ProductVersion: {ProductVersion}\n";
			result += $"LicenseKey: {LicenseKey}\n";
			result += $"LicenseType: {LicenseType}\n";
			result += $"LicenseFamily: {LicenseFamily}\n";
			result += $"Reason: {Reason}\n";
			result += $"SubstituteLicenseId: {SubstituteLicenseId}\n";
			result += $"IsSubstituted: {IsSubstituted}\n";
			result += $"IsActive: {IsActive}\n";
			return result;
		}
	}
}
