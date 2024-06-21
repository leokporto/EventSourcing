using EventSourcing.Core.Data;
using EventSourcing.Core.Events;
using EventSourcing.Core.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Infra.Repositories
{
	public class LicenseEventRepository : IEventRepository
	{
		private IEventStore _eventStore = null;
		public LicenseEventRepository(IEventStore eventStore) 
		{
			_eventStore = eventStore;
		}

		public async Task<TEvent> AppendAsync<TEvent>(Guid streamId, TEvent @event) where TEvent : IDomainEvent
		{
			return await _eventStore.AppendAsync(@event);
		}

		public async Task<TEvent> CreateStreamAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
		{
			return await _eventStore.CreateAsync(@event);
		}

		public Task<IEnumerable<IDomainEvent>> ReadAsync(Guid streamId)
		{
			throw new NotImplementedException();
		}
	}
}
