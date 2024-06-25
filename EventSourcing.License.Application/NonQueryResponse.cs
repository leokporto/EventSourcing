using EventSourcing.Core.Events;

namespace EventSourcing.License.Application
{
	public class NonQueryResponse : BaseServiceResponse
	{		
		public IDomainEvent Data { get; init; }
	}
	
}
