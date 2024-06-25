using EventSourcing.Core.Data;
using EventSourcing.Core.ExternalServices;
using EventSourcing.EventStoreDb.Infra;
using EventSourcing.Infra.Repositories;
using EventSourcing.AnLicense.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EventSourcing.FakeData;
using EventSourcing.Core.Events;
using EventSourcing.License.Application;

internal class Program
{
	private static async Task Main(string[] args)
	{
		const string connectionString = "esdb://admin:changeit@localhost:2113?tls=false&tlsVerifyCert=false";
		await Host.CreateDefaultBuilder(args)
			.ConfigureServices(services =>
			{
				services.AddScoped<IEventStore, EventStoreDb>(x => new EventStoreDb(connectionString));				
				services.AddScoped<IEventRepository, LicenseEventRepository>();
				services.AddScoped<LicenseService>();
				services.AddHostedService<StartupHost>();
			})
			.Build().RunAsync();
	}

	private static IHostBuilder CreateHostBuilder(string[] args)
	{
		return Host.CreateDefaultBuilder(args)
			.ConfigureServices((hostContext, services) =>
			{

			});
	}
}

internal class StartupHost : IHostedService
{
	LicenseService _licenseService;
	public StartupHost(LicenseService licenseService) 
	{
		_licenseService = licenseService;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		Dictionary<Guid, SortedList<long,IDomainEvent>> allFakeEvents = LicenseFakes.GetLicenseEvents();

		foreach(var evt in allFakeEvents)
		{
			string streamIdText = "";
			foreach(var evtItem in evt.Value)
			{
				if (evtItem.Value is LicenseCreatedEvent created)
				{
					NonQueryResponse response = await _licenseService.CreateStreamAsync(created);
					Console.WriteLine($"New Stream creation status: {created.StreamId} - {response.Success} - {response.Message}");
					streamIdText = created.StreamIdText;
				}
				else 
				{
					evtItem.Value.SetStreamIdText(streamIdText);
					NonQueryResponse response = await _licenseService.AppendAsync(evtItem.Value.StreamId, evtItem.Value);
					Console.WriteLine($"New Event Appended to stream status: {evtItem.Value.StreamId} - {response.Success} - {response.Message}");
				}
				
			}
		}
				
		QueryResponse queryResponse = await _licenseService.ReadAsync(allFakeEvents.First().Key);
		Console.WriteLine($"Read Stream status: {queryResponse.Success} - {queryResponse.Message}");
		if(queryResponse.Success)
		{
			foreach(var evt in queryResponse.Data)
			{
				Console.WriteLine($"Event: {evt.StreamId} - {new DateTime(evt.CreatedAtUtcTicks)}");
			}
		}
		//TODO: Read Agregate
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}