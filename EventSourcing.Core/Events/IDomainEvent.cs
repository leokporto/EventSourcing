namespace EventSourcing.Core.Events
{
	public interface IDomainEvent
	{
		abstract Guid StreamId { get; }

		string StreamIdText { get; }

		long CreatedAtUtcTicks { get; }

		void SetStreamIdText(string streamId);
	}
}
