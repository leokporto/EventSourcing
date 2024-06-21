﻿namespace EventSourcing.Core.Events
{
	public abstract class DomainEvent : IDomainEvent
	{
		protected DomainEvent()
		{
			CreatedAtUtcTicks = DateTime.UtcNow.Ticks;
		}

		public abstract Guid StreamId { get; }

		public long CreatedAtUtcTicks { get; init; }

		public string StreamIdText { get; private set; }

		public void SetStreamIdText(string streamId)
		{
			if(!string.IsNullOrWhiteSpace(streamId))
			{
				StreamIdText = streamId;
			}
		}
	}
}
