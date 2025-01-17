﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Alpaca.Markets
{
    /// <summary>
    /// Configuration parameters object for <see cref="IAlpacaCryptoStreamingClient"/> instance.
    /// </summary>
    public sealed class AlpacaCryptoStreamingClientConfiguration : StreamingClientConfiguration
    {
        private readonly HashSet<CryptoExchange> _exchanges = new ();

        /// <summary>
        /// Creates new instance of <see cref="AlpacaCryptoStreamingClientConfiguration"/> class.
        /// </summary>
        public AlpacaCryptoStreamingClientConfiguration()
            : base(Environments.Live.AlpacaCryptoStreamingApi)
        {
        }
        private AlpacaCryptoStreamingClientConfiguration(
            AlpacaCryptoStreamingClientConfiguration configuration,
            IEnumerable<CryptoExchange> exchanges)
            : base(configuration.ApiEndpoint)
        {
            SecurityId = configuration.SecurityId;
            _exchanges.UnionWith(exchanges);
        }

        /// <summary>
        /// 
        /// </summary>
        [UsedImplicitly]
        public IReadOnlyCollection<CryptoExchange> Exchanges => _exchanges;
        
        /// <summary>
        /// Creates new instance of <see cref="AlpacaCryptoStreamingClientConfiguration"/> object
        /// with the updated <see cref="AlpacaCryptoStreamingClientConfiguration.Exchanges"/> list.
        /// </summary>
        /// <param name="exchanges">Crypto exchanges to add into the list.</param>
        /// <returns>The new instance of the <see cref="AlpacaCryptoStreamingClientConfiguration"/> object.</returns>
        [UsedImplicitly]
        public AlpacaCryptoStreamingClientConfiguration WithExchanges(
            IEnumerable<CryptoExchange> exchanges) =>
            new (this, exchanges);

        /// <summary>
        /// Creates new instance of <see cref="AlpacaCryptoStreamingClientConfiguration"/> object
        /// with the updated <see cref="AlpacaCryptoStreamingClientConfiguration.Exchanges"/> list.
        /// </summary>
        /// <param name="exchanges">Crypto exchanges to add into the list.</param>
        /// <returns>The new instance of the <see cref="AlpacaCryptoStreamingClientConfiguration"/> object.</returns>
        [UsedImplicitly]
        public AlpacaCryptoStreamingClientConfiguration WithExchanges(
            params CryptoExchange[] exchanges) =>
            new (this, exchanges);

        internal override Uri GetApiEndpoint() =>
            new UriBuilder(base.GetApiEndpoint())
            {
#pragma warning disable CA2012 // Use ValueTasks correctly
                Query = new QueryBuilder()
                    .AddParameter("exchanges", _exchanges)
                    .AsStringAsync().Result
#pragma warning restore CA2012 // Use ValueTasks correctly
            }.Uri;
    }
}
