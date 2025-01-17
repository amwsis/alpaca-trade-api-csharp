﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpaca.Markets.Extensions
{
    internal sealed class DisposableAlpacaDataSubscription<TItem> :
        IDisposableAlpacaDataSubscription<TItem>
    {
        private readonly IAlpacaDataSubscription<TItem> _subscription;

        private IStreamingDataClient? _client;

        private DisposableAlpacaDataSubscription(
            IAlpacaDataSubscription<TItem> subscription,
            IStreamingDataClient client)
        {
            _subscription = subscription;
            _client = client;
        }

        public static async ValueTask<IDisposableAlpacaDataSubscription<TItem>> CreateAsync(
            IAlpacaDataSubscription<TItem> subscription,
            IStreamingDataClient client)
        {
            await client.SubscribeAsync(subscription).ConfigureAwait(false);
            return new DisposableAlpacaDataSubscription<TItem>(subscription, client);
        }

        public IEnumerable<String> Streams => _subscription.Streams;

        public Boolean Subscribed => _subscription.Subscribed;

        public event Action<TItem> Received
        {
            add => _subscription.Received += value;
            remove => _subscription.Received -= value;
        }

        public async void Dispose() => 
            await DisposeAsync().ConfigureAwait(false);

        public async ValueTask DisposeAsync()
        {
            if (_client is null)
            {
                return;
            }

            await _client.UnsubscribeAsync(_subscription)
                .ConfigureAwait(false);
            _client = null;
        }
    }
}
