﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Alpaca.Markets.Extensions
{
    internal abstract class ClientWithSubscriptionReconnectBase<TClient> :
        ClientWithReconnectBase<TClient>
        where TClient : IStreamingDataClient
    {
        private readonly ConcurrentDictionary<String, IAlpacaDataSubscription> _subscriptions =
            new(StringComparer.Ordinal);

        protected ClientWithSubscriptionReconnectBase(
            TClient client,
            ReconnectionParameters reconnectionParameters)
            : base (client, reconnectionParameters)
        {
        }

        public ValueTask SubscribeAsync(
            IAlpacaDataSubscription subscription,
            CancellationToken cancellationToken = default)
        {
            foreach (var stream in subscription.Streams)
            {
                _subscriptions.TryAdd(stream, subscription);
            }

            return Client.SubscribeAsync(subscription, cancellationToken);
        }

        public ValueTask SubscribeAsync(
            IEnumerable<IAlpacaDataSubscription> subscriptions,
            CancellationToken cancellationToken = default)
        {
            var dataSubscriptions = new List<IAlpacaDataSubscription>(subscriptions);

            foreach (var subscription in dataSubscriptions)
            {
                foreach (var stream in subscription.Streams)
                {
                    _subscriptions.TryAdd(stream, subscription);
                }
            }

            return Client.SubscribeAsync(dataSubscriptions, cancellationToken);
        }

        public ValueTask UnsubscribeAsync(
            IAlpacaDataSubscription subscription,
            CancellationToken cancellationToken = default)
        {
            foreach (var stream in subscription.Streams)
            {
                _subscriptions.TryRemove(stream, out _);
            }

            return Client.UnsubscribeAsync(subscription, cancellationToken);
        }

        public ValueTask UnsubscribeAsync(
            IEnumerable<IAlpacaDataSubscription> subscriptions,
            CancellationToken cancellationToken = default)
        {
            var dataSubscriptions = new List<IAlpacaDataSubscription>(subscriptions);

            foreach (var stream in dataSubscriptions
                .SelectMany(subscription => subscription.Streams))
            {
                _subscriptions.TryRemove(stream, out _);
            }

            return Client.UnsubscribeAsync(dataSubscriptions, cancellationToken);
        }

        protected sealed override ValueTask OnReconnection(
            CancellationToken cancellationToken) =>
            Client.SubscribeAsync(_subscriptions.Values, cancellationToken);
    }
}
