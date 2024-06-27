using Amazon.DynamoDBv2;
using EventSourcing.Core.Events;
using EventSourcing.Core.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.DynamoDb.Infra
{
	public class DynamoDbEventStore : IEventStore
	{
		public readonly IAmazonDynamoDB _dynamoDbClient = new AmazonDynamoDBClient(Amazon.RegionEndpoint.SAEast1);
		private const string TABLE_NAME = "TstEvtSrc";

		public DynamoDbEventStore(string? connectionString)
		{
		}

		public Task<TEvent> AppendAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
		{
			throw new NotImplementedException();
		}

		public Task<TEvent> CreateAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
		{
			throw new NotImplementedException();
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
