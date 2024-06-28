using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using EventSourcing.AnLicense.Domain.Events;
using EventSourcing.Core.Events;
using EventSourcing.Core.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventSourcing.DynamoDb.Infra
{
	public class DynamoDbEventStore : IEventStore
	{
		public readonly IAmazonDynamoDB _dynamoDbClient = new AmazonDynamoDBClient(Amazon.RegionEndpoint.SAEast1);
		private const string TABLE_NAME = "TstEvtSrc";

		public DynamoDbEventStore()
		{
		}

		public async Task<TEvent> AppendAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
		{
			var eventAsJson = JsonSerializer.Serialize<IDomainEvent>(@event);
			var itemAsDoc = Document.FromJson(eventAsJson);
			var itemAttributes= itemAsDoc.ToAttributeMap();

			var createItemRequest = new PutItemRequest
			{
				TableName = TABLE_NAME,
				Item = itemAttributes
			};

			
			await _dynamoDbClient.PutItemAsync(createItemRequest);

			return @event;
		}

		public async Task<TEvent> CreateAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
		{
			return await AppendAsync(@event);
		}

		public Task<bool> ExistsAsync(Guid streamId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> ExistsAsync(string streamIdText)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<IDomainEvent>> ReadAsync(Guid streamId)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<IDomainEvent>> ReadAsync(string streamIdText)
		{
			throw new NotImplementedException();
		}
	}
	
}
