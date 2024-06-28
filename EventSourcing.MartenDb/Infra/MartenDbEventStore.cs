using EventSourcing.Core.Events;
using Marten;
using Marten.Events;
using Microsoft.Extensions.Logging;
using Npgsql.Replication.PgOutput.Messages;
using static System.Formats.Asn1.AsnWriter;
using Core_IEventStore = EventSourcing.Core.ExternalServices.IEventStore;

namespace EventSourcing.MartenDb.Infra
{
	public class MartenDbEventStore : Core_IEventStore, IDisposable
	{
		private DocumentStore _docStore = null;
		private bool disposedValue;

		public MartenDbEventStore(string connectionString)
		{
			_docStore = DocumentStore.For(ds =>
			{
				ds.Connection(connectionString);
			});

			

		}

		public async Task<TEvent> AppendAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
		{
			await using var session = _docStore.LightweightSession();

			session.Events.Append(@event.StreamId, @event);

			await session.SaveChangesAsync();

			return @event;
		}

		public async Task<TEvent> CreateAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
		{
			return await AppendAsync(@event);
		}

		public async Task<bool> ExistsAsync(Guid streamId)
		{
			await using var session = _docStore.LightweightSession();

			var item = session.Events.Load(streamId);
			return item != null;
		}

		public async Task<bool> ExistsAsync(string streamIdText)
		{
			if (string.IsNullOrWhiteSpace(streamIdText))
			{
				return false;
			}

			return await ExistsAsync(Guid.Parse(streamIdText));
		}

		public async Task<IEnumerable<IDomainEvent>> ReadAsync(Guid streamId)
		{
			await using var session = _docStore.LightweightSession();

			var streamEvents = await session.Events.FetchStreamAsync(streamId);

			List<IDomainEvent> result = new List<IDomainEvent>();
			
			if (streamEvents != null && streamEvents.Any())
			{
				foreach(var evt in streamEvents)
				{
					object evtData = evt.Data;
					if (evtData != null)
					{
						var castEvt = evtData as IDomainEvent;

						if(castEvt != null)
							result.Add(castEvt);
					}
				}
			}

			return result;
		}

		public async Task<IEnumerable<IDomainEvent>> ReadAsync(string streamIdText)
		{
			if(string.IsNullOrWhiteSpace(streamIdText))
			{
				return null;
			}

			return await ReadAsync(Guid.Parse(streamIdText));
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
				_docStore.Dispose();
				disposedValue = true;
			}
		}

		
		~MartenDbEventStore()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: false);
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
