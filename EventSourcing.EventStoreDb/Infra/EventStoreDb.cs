using EventSourcing.AnLicense.Domain.Events;
using EventSourcing.Core.Events;
using EventSourcing.Core.ExternalServices;
using EventStore.Client;
using System.Text.Json;


namespace EventSourcing.EventStoreDb.Infra
{
	public class EventStoreDb : IEventStore, IDisposable
	{
		
		private EventStoreClientSettings _settings = null;
		private bool disposedValue;
		EventStoreClient _client = null;

		public EventStoreDb(string connectionString)
		{
			_settings = EventStoreClientSettings.Create(connectionString);
			_client = new EventStoreClient(_settings);
		}

		// override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		~EventStoreDb()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: false);
		}

		public async Task<TEvent> AppendAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
		{
			if(string.IsNullOrWhiteSpace(@event.StreamIdText))
				@event.SetStreamIdText(Uuid.NewUuid().ToString());

			try
			{
				var eventData = new EventData(
					Uuid.NewUuid(),
					$"LicenseEvent",
					JsonSerializer.SerializeToUtf8Bytes(@event)
				);

				CancellationToken cancellationToken = default;

				await _client.AppendToStreamAsync(
					@event.StreamIdText,
					StreamState.Any,
							new[] { eventData },
					cancellationToken: cancellationToken
				);

			}
			catch(Exception ex)
			{
				return default(TEvent);
			}
			return @event;
		}

		

		public async Task<TEvent> CreateAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
		{			
			return await AppendAsync(@event);
		}

		public async Task<bool> ExistsAsync(Guid streamId)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> ExistsAsync(string streamIdText)
		{
			CancellationToken cancellationToken = default;

			var result = _client.ReadStreamAsync(Direction.Forwards, streamIdText, StreamPosition.Start, 1, cancellationToken: cancellationToken);				

			if (await result.ReadState == ReadState.Ok) 
				return true;				
			

			return false;
		}

		public async Task<IEnumerable<IDomainEvent>> ReadAsync(Guid streamId)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<IDomainEvent>> ReadAsync(string streamIdText)
		{
			CancellationToken cancellationToken = default;
			

			IList<IDomainEvent> domainEvents = new List<IDomainEvent>();

			var result = _client.ReadStreamAsync(Direction.Forwards, streamIdText, StreamPosition.Start, cancellationToken: cancellationToken);

			if (await result.ReadState == ReadState.Ok)
			{

				var events = await result.ToListAsync();

				foreach (var resolvedEvent in events)
				{
					var dataArray = resolvedEvent.Event.Data.ToArray();
					var deserializedEvent = JsonSerializer.Deserialize<DomainEvent>(dataArray);

					if(deserializedEvent != null)
						domainEvents.Add(deserializedEvent);					
				}
			}
		

			return domainEvents;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				_client?.Dispose();
				disposedValue = true;
			}
		}

		

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
