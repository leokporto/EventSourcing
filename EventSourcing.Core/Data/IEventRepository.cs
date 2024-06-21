using EventSourcing.Core.Entities;
using EventSourcing.Core.Events;

namespace EventSourcing.Core.Data
{
	public interface IEventRepository
	{
		Task<TEvent> CreateStreamAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent;

		Task<TEvent> AppendAsync<TEvent>(Guid streamId, TEvent @event) where TEvent : IDomainEvent;

		Task<IEnumerable<IDomainEvent>> ReadAsync(Guid streamId);
	}
}
