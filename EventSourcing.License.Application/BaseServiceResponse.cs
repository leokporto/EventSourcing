using EventSourcing.Core.Application;

namespace EventSourcing.License.Application
{
	public class BaseServiceResponse : IServiceResponse
	{
		public bool Success { get; init; }

		public string Message { get; init; }
	}
}