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

		public async Task<bool> ExistsAsync(Guid streamId)
		{
			return await ExistsAsync(streamId.ToString());
		}

		public async Task<bool> ExistsAsync(string streamIdText)
		{
			CancellationToken cancellationToken = default;
			


			using (var client = new EventStoreClient(_settings))
			{
				var result = client.ReadStreamAsync(Direction.Forwards, streamIdText, StreamPosition.Start, 1, cancellationToken: cancellationToken);				

				if (await result.ReadState == ReadState.Ok) 
					return true;				
			}

			return false;
		}

		public async Task<IEnumerable<IDomainEvent>> ReadAsync(Guid streamId)
		{
			return await ReadAsync(streamId.ToString());
		}

		public async Task<IEnumerable<IDomainEvent>> ReadAsync(string streamIdText)
		{
			CancellationToken cancellationToken = default;

			IEnumerable<IDomainEvent> domainEvents = new List<IDomainEvent>();

			using (var client = new EventStoreClient(_settings))
			{
				var result = client.ReadStreamAsync(Direction.Forwards, streamIdText, StreamPosition.Start, cancellationToken: cancellationToken);

				if (await result.ReadState == ReadState.Ok)
				{

					var events = await result.ToListAsync();

					foreach (var resolvedEvent in events)
					{
						var deserializedEvent = JsonSerializer.Deserialize<IDomainEvent>(
							resolvedEvent.Event.Data.ToArray()
						);

						domainEvents.Append(deserializedEvent);
					}
				}
			}

			return domainEvents;
		}
	}
}
