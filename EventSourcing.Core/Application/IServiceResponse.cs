namespace EventSourcing.Core.Application
{
	public interface IServiceResponse
	{
		bool Success { get; }
		string Message { get; }
	}
}
