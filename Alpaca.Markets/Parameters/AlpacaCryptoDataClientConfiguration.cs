﻿using System;
using System.Net.Http;
using JetBrains.Annotations;

namespace Alpaca.Markets
{
    /// <summary>
    /// Configuration parameters object for <see cref="IAlpacaCryptoDataClient"/> instance.
    /// </summary>
    public sealed class AlpacaCryptoDataClientConfiguration
    {
        /// <summary>
        /// Creates new instance of <see cref="AlpacaCryptoDataClientConfiguration"/> class.
        /// </summary>
        public AlpacaCryptoDataClientConfiguration()
        {
            SecurityId = new SecretKey(String.Empty, String.Empty);
            ApiEndpoint = Environments.Live.AlpacaDataApi;
            ThrottleParameters = ThrottleParameters.Default;
        }

        /// <summary>
        /// Security identifier for API authentication.
        /// </summary>
        public SecurityKey SecurityId { get; set; }

        /// <summary>
        /// Gets or sets Alpaca Data API base URL.
        /// </summary>
        public Uri ApiEndpoint { get; set; }

        /// <summary>
        /// Gets or sets REST API throttling parameters.
        /// </summary>
        public ThrottleParameters ThrottleParameters { get; set; }

        /// <summary>
        /// Gets or sets <see cref="HttpClient"/> instance for connecting.
        /// </summary>
        public HttpClient? HttpClient { get; [UsedImplicitly] set; }

        internal void EnsureIsValid()
        {
            if (SecurityId is null)
            {
                throw new InvalidOperationException(
                    $"The value of '{nameof(SecurityId)}' property shouldn't be null.");
            }

            if (ApiEndpoint is null)
            {
                throw new InvalidOperationException(
                    $"The value of '{nameof(ApiEndpoint)}' property shouldn't be null.");
            }

            if (ThrottleParameters is null)
            {
                throw new InvalidOperationException(
                    $"The value of '{nameof(ThrottleParameters)}' property shouldn't be null.");
            }
        }
    }
}