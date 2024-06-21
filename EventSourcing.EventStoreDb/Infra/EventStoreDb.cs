using EventSourcing.Core.Events;
using EventSourcing.Core.ExternalServices;
using EventStore.Client;
using System.Text.Json;

namespace EventSourcing.EventStoreDb.Infra
{
	public class EventStoreDb : IEventStore
	{
		private EventStoreClientSettings _settings = null;
		public EventStoreDb(string connectionString)
		{
			_settings = EventStoreClientSettings.Create(connectionString);
		}

		public async Task<TEvent> AppendAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
		{
			if(string.IsNullOrWhiteSpace(@event.StreamIdText))
				@event.SetStreamIdText(Uuid.NewUuid().ToString());

			var eventData = new EventData(
				Uuid.Parse(@event.StreamIdText),
				"LicenseEvents",
				JsonSerializer.SerializeToUtf8Bytes(@event)
			);
			
			CancellationToken cancellationToken = default;
			using (var client = new EventStoreClient(_settings))
			{
				await client.AppendToStreamAsync(
					@event.StreamId.ToString(),
					StreamState.Any,
							new[] { eventData },
					cancellationToken: cancellationToken
				);
			}
			return @event;
		}

		

		public async Task<TEvent> CreateAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
		{			
			return await AppendAsync(@event);
		}

		public Task<IEnumerable<IDomainEvent>> ReadAsync(Guid streamId)
		{

			throw new NotImplementedException();
		}

		public Task<IEnumerable<IDomainEvent>> ReadAsync(string streamIdText)
		{
			throw new NotImplementedException();
		}
	}
}
