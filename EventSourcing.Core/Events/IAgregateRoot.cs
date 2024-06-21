namespace EventSourcing.Core.Events
{
	public interface IAgregateRoot
	{
		void Apply(IDomainEvent @event);
	}
}
