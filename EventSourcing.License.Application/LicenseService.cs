﻿using EventSourcing.AnLicense.Domain.Events;
using EventSourcing.Core.Data;
using EventSourcing.Core.Events;

namespace EventSourcing.License.Application
{
	public class LicenseService
	{
		private IEventRepository _eventRepository;

		public LicenseService(IEventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public async Task<NonQueryResponse> CreateStreamAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
		{
			if(@event == null)
			{
				return new NonQueryResponse { Success = false, Message = "Failed to create stream. Event is null." };
			}

			IDomainEvent resultEvt = await _eventRepository.CreateStreamAsync(@event);
			if(resultEvt == null || resultEvt.StreamId == Guid.Empty)
			{
				return new NonQueryResponse { Success = false, Message = "Failed to create stream" };
			}
			else
			{
				return new NonQueryResponse { Success = true, Message = "", Data = @event };
			}
		}

		
		public async Task<NonQueryResponse> AppendAsync(DomainEvent @event)
		{
			if (@event == null)
			{
				return new NonQueryResponse { Success = false, Message = "Failed to create stream. Event is null." };
			}

			IDomainEvent resultEvt = await _eventRepository.AppendAsync(@event);
			if (resultEvt == null || resultEvt.StreamId == Guid.Empty)
			{
				return new NonQueryResponse { Success = false, Message = "Failed to append new event to stream" };
			}
			else
			{
				return new NonQueryResponse { Success = true, Message = "", Data = @event };
			}
		}

		public async Task<QueryResponse> ReadAsync(Guid streamId)
		{
			if (streamId == Guid.Empty)
			{
				return new QueryResponse { Success = false, Message = "Failed to read events from stream. StreamId is empty." };
			}
			return await ReadAsync(streamId.ToString());
		}

		public async Task<QueryResponse> ReadAsync(string streamIdText)
		{
			if (string.IsNullOrWhiteSpace(streamIdText))
			{
				return new QueryResponse { Success = false, Message = "Failed to read events from stream. StreamId is empty." };
			}

			IEnumerable<IDomainEvent> result = await _eventRepository.ReadAsync(streamIdText);
			if (result == null || result.Count() == 0)
			{
				return new QueryResponse { Success = false, Message = "Failed to read events from stream" };
			}
			else
			{
				return new QueryResponse { Success = true, Message = "", Data = result };
			}
		}
	}
	
}
