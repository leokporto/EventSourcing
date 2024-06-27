using EventSourcing.AnLicense.Domain.Entities;
using EventSourcing.AnLicense.Domain.Events;
using EventSourcing.Core.Data;
using EventSourcing.Core.ExternalServices;
using EventSourcing.FakeData;
using EventSourcing.Infra.Repositories;
using EventSourcing.License.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using EventSourcing.DynamoDb.Infra;

namespace EventSourcing.View.ConsoleApp
{
	internal class Program
	{
		private static async Task Main(string[] args)
		{
			
			IConfiguration config = new ConfigurationBuilder()
				.AddUserSecrets<AppConfiguration>()
				.Build();


			var builder = Host.CreateDefaultBuilder(args);			
			
			//string? connectionString = config.GetConnectionString("EventStoreDb");
			string? connectionString = config.GetConnectionString("DynamoDb");
			
			builder.ConfigureServices(services =>
				{

					//services.AddScoped<IEventStore, EventStoreDb.Infra.EventStoreDb>(x => new EventStoreDb.Infra.EventStoreDb(connectionString));
					services.AddScoped<IEventStore, DynamoDbEventStore>(x => new DynamoDbEventStore(connectionString));
					services.AddScoped<IEventRepository, LicenseEventRepository>();
					services.AddScoped<LicenseService>();
					services.AddHostedService<StartupHost>();
				});

			await builder!.Build().RunAsync();
			
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

			while (true)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("Choose option: ");
				Console.WriteLine("1 - Create Fake Streams");
				Console.WriteLine("2 - Read Streams");
				Console.WriteLine("3 - Read Agregate \n");
				Console.ForegroundColor = ConsoleColor.White;

				string? selectedOption = Console.ReadLine();
				switch (selectedOption)
				{
					case "1":
						Console.WriteLine("How many fake streams do you want to create?");
						string? totalStreamsInput = Console.ReadLine();
						if (string.IsNullOrWhiteSpace(totalStreamsInput))
						{
							LogError("Invalid number of streams");
							break;
						}

						int totalStreams = 0;
						if (!int.TryParse(totalStreamsInput, out totalStreams))
						{
							LogError("Invalid number of streams");
							break;
						}

						await CreateFakeStreams(totalStreams);

						break;

					case "2":
						string streamIdText = GetStreamId();

						if (!string.IsNullOrWhiteSpace(streamIdText))
						{
							await ReadStream(streamIdText);
						}

						break;

					case "3":
						string streamIdText2 = GetStreamId();
						if (!string.IsNullOrWhiteSpace(streamIdText2))
						{
							await ReadAgregate(streamIdText2);
						}

						break;

					default:
						LogError("Invalid option");
						break;
				}

				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("Do you want to continue? (Y/N)");
				Console.ForegroundColor = ConsoleColor.White;

				string? continueOption = Console.ReadLine();
				if (continueOption?.ToLower() != "y")
				{
					break;
				}
			}


		}

		private static string GetStreamId()
		{
			Console.WriteLine("Enter StreamId: ");
			string? streamIdText = Console.ReadLine();
			if (string.IsNullOrWhiteSpace(streamIdText))
			{
				LogError("Invalid StreamId");
			}

			return streamIdText;
		}

		private async Task ReadAgregate(string streamIdText)
		{
			QueryResponse queryResponse = await _licenseService.ReadAsync(streamIdText);
			LogResponse($"ReadAgregate: ", queryResponse);
			if (queryResponse.Success)
			{
				SoftLicense agregateLicense = new SoftLicense();
				foreach (var evt in queryResponse.Data)
				{
					agregateLicense.Apply(evt);
				}
				Console.WriteLine(agregateLicense);
			}
		}

		private async Task ReadStream(string streamIdText)
		{
			QueryResponse queryResponse = await _licenseService.ReadAsync(streamIdText);
			LogResponse($"Read Stream status: ", queryResponse);
			if (queryResponse.Success)
			{
				foreach (var evt in queryResponse.Data)
				{
					Console.WriteLine(evt);
				}
			}
		}

		private async Task CreateFakeStreams(int totalStreams)
		{
			string streamIdText;
			Dictionary<Guid, SortedList<long, DomainEvent>> allFakeEvents;
			allFakeEvents = LicenseFakes.GetFakeLicenseEvents(totalStreams);
			streamIdText = "";
			foreach (var evt in allFakeEvents)
			{
				streamIdText = "";
				foreach (var evtItem in evt.Value)
				{
					if (evtItem.Value is LicenseCreatedEvent)
					{
						NonQueryResponse response = await _licenseService.CreateStreamAsync(evtItem.Value);
						LogResponse($"New stream created: {evtItem.Value.StreamIdText}", response);
						streamIdText = evtItem.Value.StreamIdText;
					}
					else
					{
						evtItem.Value.SetStreamIdText(streamIdText);
						NonQueryResponse response = await _licenseService.AppendAsync(evtItem.Value);
						LogResponse($"New Event Appended: {evtItem.Value.StreamIdText}", response);
					}

				}
			}
		}

		private void LogResponse(string prefix, BaseServiceResponse response)
		{
			string message = response.Message;
			if (response.Success)
			{
				Console.WriteLine($"{prefix} - {response.Success}");
			}
			else
			{
				Console.WriteLine($"{prefix} - {response.Success} - {response.Message}");
			}
		}

		private static void LogError(string errorMessage)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(errorMessage);
			Console.ForegroundColor = ConsoleColor.White;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
