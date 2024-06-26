using EventSourcing.Core.Data;
using EventSourcing.Core.Events;
using EventSourcing.Core.ExternalServices;

namespace EventSourcing.Infra.Repositories
{
	public class LicenseEventRepository : IEventRepository
	{
		private IEventStore _eventStore = null;
		public LicenseEventRepository(IEventStore eventStore) 
		{
			_eventStore = eventStore;
		}

		public async Task<TEvent> AppendAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
		{
			return await _eventStore.AppendAsync(@event);
		}		

		public async Task<TEvent> CreateStreamAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
		{
			return await _eventStore.CreateAsync(@event);
		}

		public async Task<bool> ExistsAsync(Guid streamId)
		{
			return await _eventStore.ExistsAsync(streamId.ToString());
		}

		public async Task<bool> ExistsAsync(string streamIdText)
		{
			if (string.IsNullOrEmpty(streamIdText))
			{
				return false;
			}
			return await _eventStore.ExistsAsync(streamIdText);
		}

		public async Task<IEnumerable<IDomainEvent>> ReadAsync(Guid streamId)
		{
			if(streamId == Guid.Empty)
			{
				return null;
			}

			return await _eventStore.ReadAsync(streamId);
		}

		public async Task<IEnumerable<IDomainEvent>> ReadAsync(string streamIdText)
		{
			if(string.IsNullOrWhiteSpace(streamIdText))
			{
				return null;
			}

			return await _eventStore.ReadAsync(streamIdText);
		}
	}
}
