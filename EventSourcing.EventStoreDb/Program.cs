using EventSourcing.Core.Data;
using EventSourcing.Core.ExternalServices;
using EventSourcing.EventStoreDb.Infra;
using EventSourcing.Infra.Repositories;
using EventSourcing.AnLicense.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
	IEventRepository _eventRepository;
	public StartupHost(IEventRepository eventRepo) 
	{
		_eventRepository = eventRepo;
	}
	public async Task StartAsync(CancellationToken cancellationToken)
	{
		LicenseCreatedEvent	licenceCreated = new LicenseCreatedEvent()
		{
			CompanyName = "CELPE",
			CreatedBy = "Leonardo",
			Project = "Suspensao Action.Net",
			ProjectKey = "24.023"
		};
		var createdResponse = await _eventRepository.CreateStreamAsync(licenceCreated);

		var streamIdText = createdResponse.StreamIdText;

		if (string.IsNullOrWhiteSpace(streamIdText))
			return;

		//TODO: Vreate other events
		//TODO: Read events
		//TODO: Read Agregate
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}