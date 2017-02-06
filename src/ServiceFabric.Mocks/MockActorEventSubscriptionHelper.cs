﻿using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace ServiceFabric.Mocks
{
	/// <summary>
	/// Implementation of <see cref="IActorEventSubscriptionHelper"/> that doesn't subscribe to Actor events.
	/// </summary>
	public class MockActorEventSubscriptionHelper : IActorEventSubscriptionHelper
	{
		private readonly Task _completed = Task.FromResult(true);

		/// <inheritdoc />
		public Task SubscribeAsync<TEvent>(IActorEventPublisher actorProxy, TEvent subscriber) 
			where TEvent : IActorEvents
		{
			return _completed;
		}

		/// <inheritdoc />
		public Task SubscribeAsync<TEvent>(IActorEventPublisher actorProxy, TEvent subscriber, TimeSpan resubscriptionInterval)
			where TEvent : IActorEvents
		{
			return _completed;
		}

		/// <inheritdoc />
		public Task UnsubscribeAsync<TEvent>(IActorEventPublisher actorProxy, TEvent subscriber)
		{
			return _completed;
		}
	}

	/// <summary>
	/// Implementation of <see cref="IActorEventSubscriptionHelper"/> that actually subscribes to Actor events.
	/// </summary>
	public class ActorEventSubscriptionHelper : IActorEventSubscriptionHelper
	{
		/// <inheritdoc />
		public Task SubscribeAsync<TEvent>(IActorEventPublisher actorProxy, TEvent subscriber) where TEvent : IActorEvents
		{
			return actorProxy.SubscribeAsync(subscriber);
		}

		/// <inheritdoc />
		public Task SubscribeAsync<TEvent>(IActorEventPublisher actorProxy, TEvent subscriber, TimeSpan resubscriptionInterval) where TEvent : IActorEvents
		{
			return actorProxy.SubscribeAsync(subscriber, resubscriptionInterval);
		}

		/// <inheritdoc />
		public Task UnsubscribeAsync<TEvent>(IActorEventPublisher actorProxy, TEvent subscriber)
		{
			return actorProxy.UnsubscribeAsync(subscriber);
		}
	}

	/// <summary>
	/// Use this to mock Actor event subscriptions. Pass <see cref="ActorEventSubscriptionHelper"/> for actual subscriptions, 
	/// and <see cref="MockActorEventSubscriptionHelper"/> for test scenarios.
	/// </summary>
	public interface IActorEventSubscriptionHelper
	{
		/// <summary>Subscribe to a published actor event.</summary>
		Task SubscribeAsync<TEvent>(IActorEventPublisher actorProxy, TEvent subscriber) where TEvent : IActorEvents;
		
		/// <summary>Subscribe to a published actor event.</summary>
		Task SubscribeAsync<TEvent>(IActorEventPublisher actorProxy, TEvent subscriber, TimeSpan resubscriptionInterval) where TEvent : IActorEvents;
		
		/// <summary>Unsubscribe from a published actor event.</summary>
		Task UnsubscribeAsync<TEvent>(IActorEventPublisher actorProxy, TEvent subscriber);
	}
}
