using EventSourcing.Core.Events;

namespace EventSourcing.Core.Data
{
	public interface IEventRepository
	{
		Task<TEvent> CreateStreamAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent;

		Task<TEvent> AppendAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent;

		Task<IEnumerable<IDomainEvent>> ReadAsync(Guid streamId);

		Task<IEnumerable<IDomainEvent>> ReadAsync(string streamIdText);

		Task<bool> ExistsAsync(Guid streamId);

		Task<bool> ExistsAsync(string streamIdText);
	}
}
