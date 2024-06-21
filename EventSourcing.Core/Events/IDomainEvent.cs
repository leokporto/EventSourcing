namespace EventSourcing.Core.Events
{
	public interface IDomainEvent
	{
		abstract Guid StreamId { get; }

		long CreatedAtUtcTicks { get; }
	}
}
