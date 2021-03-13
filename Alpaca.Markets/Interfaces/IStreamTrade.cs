﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Alpaca.Markets
{
    /// <summary>
    /// Encapsulates trade information from Polygon streaming API.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
#pragma warning disable 618
    public interface IStreamTrade : IStreamBase
#pragma warning restore 618
    {
        /// <summary>
        /// Gets trade identifier.
        /// </summary>
        String TradeId { get; }

        /// <summary>
        /// Gets asset's exchange identifier.
        /// </summary>
        String Exchange { get; }

        /// <summary>
        /// Gets trade price level.
        /// </summary>
        Decimal Price { get; }

        /// <summary>
        /// Gets trade quantity.
        /// </summary>
        Int64 Size { get; }

        /// <summary>
        /// Gets trade timestamp in UTC time zone.
        /// </summary>
        DateTime TimeUtc { get; }
    }
}
