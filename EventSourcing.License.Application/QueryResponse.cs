using EventSourcing.Core.Events;

namespace EventSourcing.License.Application
{
	public class QueryResponse : BaseServiceResponse
	{		

		public IEnumerable<IDomainEvent> Data { get; init; }
	}
	
}
