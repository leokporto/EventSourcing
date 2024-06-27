using EventSourcing.Core.Events;
using System.Text.Json.Serialization;

namespace EventSourcing.AnLicense.Domain.Events
{
	[JsonDerivedType(typeof(LicenseCreatedEvent), nameof(LicenseCreatedEvent))]
	[JsonDerivedType(typeof(LicenseUpdatedEvent), nameof(LicenseUpdatedEvent))]
	[JsonDerivedType(typeof(LicenseActivatedEvent), nameof(LicenseActivatedEvent))]
	[JsonDerivedType(typeof(LicenseDeactivatedEvent), nameof(LicenseDeactivatedEvent))]
	[JsonDerivedType(typeof(LicenseSubstitutedEvent), nameof(LicenseSubstitutedEvent))]
	[JsonPolymorphic]
	public abstract class DomainEvent : IDomainEvent
	{
		protected DomainEvent()
		{
			CreatedAtUtcTicks = DateTime.UtcNow.Ticks;
		}

		public abstract Guid StreamId { get; }

		public long CreatedAtUtcTicks { get; init; }

		public string StreamIdText { get; private set; }

		[JsonPropertyName("pk")]
		public string Pk => StreamId.ToString();

		[JsonPropertyName("sk")]
		public long  Sk => CreatedAtUtcTicks;

		public void SetStreamIdText(string streamId)
		{
			if (!string.IsNullOrWhiteSpace(streamId))
			{
				StreamIdText = streamId;
			}
		}
	}
}
