using EventSourcing.Core.Events;

namespace EventSourcing.Core.ExternalServices
{
	public interface IEventStore
	{
		Task AppendAsync(IDomainEvent @event);

		Task<IEnumerable<IDomainEvent>> ReadAsync(Guid streamId);
	}
}
